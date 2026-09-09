using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.PerfTrace;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class MusicLibraryPage : LibraryPage
{
	private class ViewCommand : Command
	{
		private MusicLibraryView _view;

		public MusicLibraryView View => _view;

		public ViewCommand(IModelItemOwner owner, string description, EventHandler invokeHandler, MusicLibraryView view)
			: base(owner, description, invokeHandler)
		{
			_view = view;
		}

		protected override void OnInvoked()
		{
			PerfTrace.TraceUICollectionEvent(UICollectionEvent.MusicLibraryViewCommandInvoked, ((ModelItem)this).Description);
			((Command)this).OnInvoked();
		}
	}

	private ArtistsPanel _artistsPanel;

	private GenresPanel _genresPanel;

	private AlbumsPanel _albumsPanel;

	private TracksPanel _tracksPanel;

	private PlaylistsPanel _playlistsPanel;

	private PlaylistContentsPanel _playlistContentsPanel;

	private Guid _selectedArtistZuneMediaId;

	private string _selectedArtistTitle;

	private IList _selectedArtistIds;

	private IList _selectedGenreIds;

	private IList _selectedAlbumIds;

	private IList _selectedTrackIds;

	private object _selectedPlaylist;

	private string _trackListSort;

	private string _trackListSortAllTracksArtistView;

	private string _trackListSortAllTracksAlbumView;

	private string _trackListSortAllTracksGenreView;

	private string _trackListSortSelectedArtists;

	private string _trackListSortSelectedGenres;

	private string _trackListSortSelectedAlbums;

	private bool _allArtistsSelected;

	private bool _allGenresSelected;

	private bool _allAlbumsSelected;

	private int _artistsCount;

	private int _genresCount;

	private int _albumsCount;

	private Command _albumsChanged;

	private Command _albumEdited;

	private int _albumArtistCount;

	private MusicLibraryView _view;

	private Command _leftItemClicked;

	private Command _albumClicked;

	private Command _createPlaylistCommand;

	private Command _createAutoPlaylistCommand;

	private bool _updatePreferredView;

	private static Command _preferredContentType;

	private static bool _hasContentTypesPersonal;

	private static bool _hasContentTypesProtected;

	private static bool _hasContentTypesZunePass;

	private static Command _showContentTypesAll;

	private static Command _showContentTypesPersonal;

	private static Command _showContentTypesProtected;

	private static Command _showContentTypesZunePass;

	public MusicLibraryView View
	{
		get
		{
			return _view;
		}
		private set
		{
			if (_view != value)
			{
				_view = value;
				((ModelItem)this).FirePropertyChanged("View");
			}
		}
	}

	public IList SelectedArtistIds
	{
		get
		{
			return _selectedArtistIds;
		}
		set
		{
			if (_selectedArtistIds != value)
			{
				_selectedArtistIds = value;
				((ModelItem)this).FirePropertyChanged("SelectedArtistIds");
				((ModelItem)this).FirePropertyChanged("SelectedArtistsCount");
			}
		}
	}

	public int SelectedArtistsCount
	{
		get
		{
			if (AllArtistsSelected)
			{
				return ArtistsCount;
			}
			if (SelectedArtistIds == null)
			{
				return 0;
			}
			return SelectedArtistIds.Count;
		}
	}

	public IList SelectedGenreIds
	{
		get
		{
			return _selectedGenreIds;
		}
		set
		{
			if (_selectedGenreIds != value)
			{
				_selectedGenreIds = value;
				((ModelItem)this).FirePropertyChanged("SelectedGenreIds");
				((ModelItem)this).FirePropertyChanged("SelectedGenresCount");
			}
		}
	}

	public int SelectedGenresCount
	{
		get
		{
			if (AllGenresSelected)
			{
				return GenresCount;
			}
			if (SelectedGenreIds == null)
			{
				return 0;
			}
			return SelectedGenreIds.Count;
		}
	}

	public Guid SelectedArtistZuneMediaId
	{
		get
		{
			return _selectedArtistZuneMediaId;
		}
		set
		{
			if (_selectedArtistZuneMediaId != value)
			{
				_selectedArtistZuneMediaId = value;
				((ModelItem)this).FirePropertyChanged("SelectedArtistZuneMediaId");
			}
		}
	}

	public string SelectedArtistTitle
	{
		get
		{
			return _selectedArtistTitle;
		}
		set
		{
			if (_selectedArtistTitle != value)
			{
				_selectedArtistTitle = value;
				((ModelItem)this).FirePropertyChanged("SelectedArtistTitle");
			}
		}
	}

	public Command LeftItemClicked => _leftItemClicked;

	public Command AlbumsChanged => _albumsChanged;

	public Command AlbumEdited => _albumEdited;

	public bool AllArtistsSelected
	{
		get
		{
			return _allArtistsSelected;
		}
		set
		{
			if (_allArtistsSelected != value)
			{
				_allArtistsSelected = value;
				((ModelItem)this).FirePropertyChanged("AllArtistsSelected");
				((ModelItem)this).FirePropertyChanged("SelectedArtistsCount");
			}
		}
	}

	public bool AllGenresSelected
	{
		get
		{
			return _allGenresSelected;
		}
		set
		{
			if (_allGenresSelected != value)
			{
				_allGenresSelected = value;
				((ModelItem)this).FirePropertyChanged("AllGenresSelected");
				((ModelItem)this).FirePropertyChanged("SelectedGenresCount");
			}
		}
	}

	public bool AllAlbumsSelected
	{
		get
		{
			return _allAlbumsSelected;
		}
		set
		{
			if (_allAlbumsSelected != value)
			{
				_allAlbumsSelected = value;
				((ModelItem)this).FirePropertyChanged("AllAlbumsSelected");
				((ModelItem)this).FirePropertyChanged("SelectedAlbumsCount");
			}
		}
	}

	public int ArtistsCount
	{
		get
		{
			return _artistsCount;
		}
		set
		{
			if (_artistsCount != value)
			{
				_artistsCount = value;
				((ModelItem)this).FirePropertyChanged("ArtistsCount");
				if (AllArtistsSelected)
				{
					((ModelItem)this).FirePropertyChanged("SelectedArtistsCount");
				}
			}
		}
	}

	public int GenresCount
	{
		get
		{
			return _genresCount;
		}
		set
		{
			if (_genresCount != value)
			{
				_genresCount = value;
				((ModelItem)this).FirePropertyChanged("GenresCount");
				if (AllGenresSelected)
				{
					((ModelItem)this).FirePropertyChanged("SelectedGenresCount");
				}
			}
		}
	}

	public int AlbumsCount
	{
		get
		{
			return _albumsCount;
		}
		set
		{
			if (_albumsCount != value)
			{
				_albumsCount = value;
				((ModelItem)this).FirePropertyChanged("AlbumsCount");
				if (AllAlbumsSelected)
				{
					((ModelItem)this).FirePropertyChanged("SelectedAlbumsCount");
				}
			}
		}
	}

	public int AlbumArtistCount
	{
		get
		{
			return _albumArtistCount;
		}
		set
		{
			if (_albumArtistCount != value)
			{
				_albumArtistCount = value;
				((ModelItem)this).FirePropertyChanged("AlbumArtistCount");
			}
		}
	}

	public IList SelectedAlbumIds
	{
		get
		{
			return _selectedAlbumIds;
		}
		set
		{
			if (_selectedAlbumIds != value)
			{
				_selectedAlbumIds = value;
				((ModelItem)this).FirePropertyChanged("SelectedAlbumIds");
				((ModelItem)this).FirePropertyChanged("SelectedAlbumsCount");
			}
		}
	}

	public int SelectedAlbumsCount
	{
		get
		{
			if (AllAlbumsSelected)
			{
				return AlbumsCount;
			}
			if (SelectedAlbumIds == null)
			{
				return 0;
			}
			return SelectedAlbumIds.Count;
		}
	}

	public string TrackListSort
	{
		get
		{
			return _trackListSort;
		}
		set
		{
			if (_trackListSort != value)
			{
				_trackListSort = value;
				((ModelItem)this).FirePropertyChanged("TrackListSort");
			}
		}
	}

	public string TrackListSortAllTracksArtistView
	{
		get
		{
			return _trackListSortAllTracksArtistView;
		}
		set
		{
			if (_trackListSortAllTracksArtistView != value)
			{
				_trackListSortAllTracksArtistView = value;
				((ModelItem)this).FirePropertyChanged("TrackListSortAllTracksArtistView");
			}
		}
	}

	public string TrackListSortAllTracksAlbumView
	{
		get
		{
			return _trackListSortAllTracksAlbumView;
		}
		set
		{
			if (_trackListSortAllTracksAlbumView != value)
			{
				_trackListSortAllTracksAlbumView = value;
				((ModelItem)this).FirePropertyChanged("TrackListSortAllTracksAlbumView");
			}
		}
	}

	public string TrackListSortAllTracksGenreView
	{
		get
		{
			return _trackListSortAllTracksGenreView;
		}
		set
		{
			if (_trackListSortAllTracksGenreView != value)
			{
				_trackListSortAllTracksGenreView = value;
				((ModelItem)this).FirePropertyChanged("TrackListSortAllTracksGenreView");
			}
		}
	}

	public string TrackListSortSelectedArtists
	{
		get
		{
			return _trackListSortSelectedArtists;
		}
		set
		{
			if (_trackListSortSelectedArtists != value)
			{
				_trackListSortSelectedArtists = value;
				((ModelItem)this).FirePropertyChanged("TrackListSortSelectedArtists");
			}
		}
	}

	public string TrackListSortSelectedGenres
	{
		get
		{
			return _trackListSortSelectedGenres;
		}
		set
		{
			if (_trackListSortSelectedGenres != value)
			{
				_trackListSortSelectedGenres = value;
				((ModelItem)this).FirePropertyChanged("TrackListSortSelectedGenres");
			}
		}
	}

	public string TrackListSortSelectedAlbums
	{
		get
		{
			return _trackListSortSelectedAlbums;
		}
		set
		{
			if (_trackListSortSelectedAlbums != value)
			{
				_trackListSortSelectedAlbums = value;
				((ModelItem)this).FirePropertyChanged("TrackListSortSelectedAlbums");
			}
		}
	}

	public Command AlbumClicked => _albumClicked;

	public IList SelectedTrackIds
	{
		get
		{
			return _selectedTrackIds;
		}
		set
		{
			if (_selectedTrackIds != value)
			{
				_selectedTrackIds = value;
				((ModelItem)this).FirePropertyChanged("SelectedTrackIds");
			}
		}
	}

	public Command CreatePlaylistCommand => _createPlaylistCommand;

	public Command CreateAutoPlaylistCommand => _createAutoPlaylistCommand;

	public object SelectedPlaylist
	{
		get
		{
			return _selectedPlaylist;
		}
		set
		{
			if (_selectedPlaylist != value)
			{
				_selectedPlaylist = value;
				((ModelItem)this).FirePropertyChanged("SelectedPlaylist");
			}
		}
	}

	public ArtistsPanel ArtistsPanel
	{
		get
		{
			if (_artistsPanel == null)
			{
				_artistsPanel = new ArtistsPanel(this);
			}
			return _artistsPanel;
		}
	}

	public GenresPanel GenresPanel
	{
		get
		{
			if (_genresPanel == null)
			{
				_genresPanel = new GenresPanel(this);
			}
			return _genresPanel;
		}
	}

	public AlbumsPanel AlbumsPanel
	{
		get
		{
			if (_albumsPanel == null)
			{
				_albumsPanel = new AlbumsPanel(this);
			}
			return _albumsPanel;
		}
	}

	public TracksPanel TracksPanel
	{
		get
		{
			if (_tracksPanel == null)
			{
				_tracksPanel = new TracksPanel(this);
			}
			return _tracksPanel;
		}
	}

	public PlaylistsPanel PlaylistsPanel
	{
		get
		{
			if (_playlistsPanel == null)
			{
				_playlistsPanel = new PlaylistsPanel(this);
				if (!base.ShowDeviceContents)
				{
					_playlistsPanel.SelectedLibraryIds = new int[1] { PlaylistManager.Instance.DefaultPlaylistId };
				}
			}
			return _playlistsPanel;
		}
	}

	public PlaylistContentsPanel PlaylistContentsPanel
	{
		get
		{
			if (_playlistContentsPanel == null)
			{
				_playlistContentsPanel = new PlaylistContentsPanel(this);
			}
			return _playlistContentsPanel;
		}
	}

	public bool HasContentTypesPersonal
	{
		get
		{
			return _hasContentTypesPersonal;
		}
		set
		{
			if (_hasContentTypesPersonal != value)
			{
				_hasContentTypesPersonal = value;
				((ModelItem)this).FirePropertyChanged("HasContentTypesPersonal");
				UpdateContentTypesPivots();
			}
		}
	}

	public bool HasContentTypesProtected
	{
		get
		{
			return _hasContentTypesProtected;
		}
		set
		{
			if (_hasContentTypesProtected != value)
			{
				_hasContentTypesProtected = value;
				((ModelItem)this).FirePropertyChanged("HasContentTypesProtected");
				UpdateContentTypesPivots();
			}
		}
	}

	public bool HasContentTypesZunePass
	{
		get
		{
			return _hasContentTypesZunePass;
		}
		set
		{
			if (_hasContentTypesZunePass != value)
			{
				_hasContentTypesZunePass = value;
				((ModelItem)this).FirePropertyChanged("HasContentTypesZunePass");
				UpdateContentTypesPivots();
			}
		}
	}

	private static string LibraryTemplate => "res://ZuneShellResources!MusicLibrary.uix#MusicLibrary";

	public MusicLibraryPage(bool showDevice)
		: this(showDevice, MusicLibraryView.Invalid)
	{
	}

	public MusicLibraryPage(bool showDevice, MusicLibraryView desiredView)
		: base(showDevice, MediaType.Track)
	{
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Expected O, but got Unknown
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected O, but got Unknown
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Expected O, but got Unknown
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Expected O, but got Unknown
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Expected O, but got Unknown
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Expected O, but got Unknown
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Expected O, but got Unknown
		base.UI = LibraryTemplate;
		base.UIPath = "Collection\\Music";
		_updatePreferredView = true;
		if (showDevice)
		{
			base.PivotPreference = Shell.MainFrame.Device.Music;
			Deviceland.InitDevicePage(this);
		}
		else
		{
			base.PivotPreference = Shell.MainFrame.Collection.Music;
		}
		base.IsRootPage = true;
		base.Views = (Choice)(object)new NotifyChoice((IModelItemOwner)(object)this);
		if (!base.ShowDeviceContents)
		{
			base.Views.Options = new ViewCommand[5]
			{
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_BROWSE), ShowArtistPivot, MusicLibraryView.Artist),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_GENRE), ShowGenrePivot, MusicLibraryView.Genre),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_ALBUM), ShowAlbumPivot, MusicLibraryView.Album),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_LIST), ShowSongPivot, MusicLibraryView.Song),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_PLAYLISTS_PIVOT), ShowPlaylistPivot, MusicLibraryView.Playlist)
			};
		}
		else
		{
			base.Views.Options = new ViewCommand[2]
			{
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_BROWSE), ShowArtistPivot, MusicLibraryView.Artist),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_PLAYLISTS_PIVOT), ShowPlaylistPivot, MusicLibraryView.Playlist)
			};
		}
		int num = -1;
		if (desiredView != MusicLibraryView.Invalid)
		{
			for (int i = 0; i < base.Views.Options.Count; i++)
			{
				if (desiredView == ((ViewCommand)base.Views.Options[i]).View)
				{
					num = i;
					break;
				}
			}
		}
		if (num == -1)
		{
			num = ((!base.ShowDeviceContents) ? ClientConfiguration.Shell.MusicCollectionView : ClientConfiguration.Shell.MusicDeviceView);
		}
		base.Views.ChosenChanged += ViewChanged;
		if (num >= 0 && num < base.Views.Options.Count)
		{
			base.Views.ChosenIndex = num;
		}
		base.ContentTypes = (Choice)(object)new NotifyChoice((IModelItemOwner)(object)this);
		base.ContentTypes.Options = (IList)new ArrayListDataSet((IModelItemOwner)(object)base.ContentTypes);
		base.ShowContentTypes.Value = ClientConfiguration.Shell.ShowContentTypes;
		((Choice)base.ShowContentTypes).ChosenChanged += ShowContentTypesChanged;
		base.TransportControlStyle = TransportControlStyle.Music;
		base.PlaybackContext = PlaybackContext.Music;
		_createPlaylistCommand = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PLAYLIST_DIALOG_CREATEPLAYLIST), (EventHandler)null);
		_createAutoPlaylistCommand = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_CREATEAUTOPLAYLIST_BUTTON), (EventHandler)null);
		_albumsChanged = new Command((IModelItemOwner)(object)this);
		_albumEdited = new Command((IModelItemOwner)(object)this);
		_leftItemClicked = new Command((IModelItemOwner)(object)this);
		_albumClicked = new Command((IModelItemOwner)(object)this);
	}

	public string GetSort(bool singleAlbum, MediaType mediaType)
	{
		if (View == MusicLibraryView.Artist || View == MusicLibraryView.Genre || View == MusicLibraryView.Album)
		{
			bool flag = (SelectedAlbumIds == null || SelectedAlbumIds.Count == 0 || AllAlbumsSelected) && (SelectedGenreIds == null || SelectedGenreIds.Count == 0 || AllGenresSelected) && (SelectedArtistIds == null || SelectedArtistIds.Count == 0 || AllArtistsSelected);
			if (mediaType == MediaType.Album && !flag)
			{
				return TrackListSortSelectedAlbums;
			}
			if (mediaType == MediaType.Artist && !flag)
			{
				return TrackListSortSelectedArtists;
			}
			if (mediaType == MediaType.Genre && !flag)
			{
				return TrackListSortSelectedGenres;
			}
			if (View == MusicLibraryView.Artist)
			{
				return TrackListSortAllTracksArtistView;
			}
			if (View == MusicLibraryView.Album)
			{
				return TrackListSortAllTracksAlbumView;
			}
			return TrackListSortAllTracksGenreView;
		}
		return TrackListSort;
	}

	private void ViewChanged(object sender, EventArgs e)
	{
		if (_updatePreferredView)
		{
			if (base.ShowDeviceContents)
			{
				ClientConfiguration.Shell.MusicDeviceView = base.Views.ChosenIndex;
			}
			else
			{
				ClientConfiguration.Shell.MusicCollectionView = base.Views.ChosenIndex;
			}
		}
		_selectedArtistIds = null;
		_selectedGenreIds = null;
		_selectedAlbumIds = null;
		_selectedPlaylist = null;
		base.ShowPlaylistIcon = !base.ShowDeviceContents && ((ViewCommand)base.Views.ChosenValue).View != MusicLibraryView.Playlist;
	}

	private void ShowContentTypesChanged(object sender, EventArgs e)
	{
		if (!base.ShowContentTypes.Value)
		{
			base.DrmStateMask = ZuneUI.DrmStateMask.All();
		}
		ClientConfiguration.Shell.ShowContentTypes = base.ShowContentTypes.Value;
	}

	private void ContentTypesChanged(object sender, EventArgs e)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		_preferredContentType = (Command)base.ContentTypes.ChosenValue;
	}

	private void UpdateContentTypesPivots()
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		if (base.ShowDeviceContents)
		{
			return;
		}
		int num = 0;
		if (_hasContentTypesPersonal)
		{
			num++;
		}
		if (_hasContentTypesProtected)
		{
			num++;
		}
		if (_hasContentTypesZunePass)
		{
			num++;
		}
		base.ContentTypes.Options.Clear();
		if (num > 1)
		{
			if (_showContentTypesAll == null)
			{
				_showContentTypesAll = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_COLLECTION_CONTENT_TYPES_ALL), (EventHandler)ShowContentTypesAll);
			}
			base.ContentTypes.Options.Add(_showContentTypesAll);
			if (_hasContentTypesPersonal)
			{
				if (_showContentTypesPersonal == null)
				{
					_showContentTypesPersonal = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_COLLECTION_CONTENT_TYPES_PERSONAL), (EventHandler)ShowContentTypesPersonal);
				}
				base.ContentTypes.Options.Add(_showContentTypesPersonal);
			}
			if (_hasContentTypesProtected)
			{
				if (_showContentTypesProtected == null)
				{
					_showContentTypesProtected = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_COLLECTION_CONTENT_TYPES_PROTECTED), (EventHandler)ShowContentTypesProtected);
				}
				base.ContentTypes.Options.Add(_showContentTypesProtected);
			}
			if (_hasContentTypesZunePass)
			{
				if (_showContentTypesZunePass == null)
				{
					_showContentTypesZunePass = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_COLLECTION_CONTENT_TYPES_ZUNEPASS), (EventHandler)ShowContentTypesZunePass);
				}
				base.ContentTypes.Options.Add(_showContentTypesZunePass);
			}
			if (!base.ContentTypes.Options.Contains(_preferredContentType))
			{
				_preferredContentType = _showContentTypesAll;
			}
			base.ContentTypes.ChosenValue = _preferredContentType;
		}
		base.ShowContentTypes.Value = num > 1;
	}

	private static void ShowContentTypesAll(object sender, EventArgs args)
	{
		if (ZuneShell.DefaultInstance.CurrentPage is LibraryPage libraryPage)
		{
			libraryPage.DrmStateMask = ZuneUI.DrmStateMask.All();
			SQMLog.Log((SQMDataId)169, 1);
		}
	}

	private static void ShowContentTypesPersonal(object sender, EventArgs args)
	{
		if (ZuneShell.DefaultInstance.CurrentPage is LibraryPage libraryPage)
		{
			libraryPage.DrmStateMask = ZuneUI.DrmStateMask.Personal();
			SQMLog.Log((SQMDataId)170, 1);
		}
	}

	private static void ShowContentTypesProtected(object sender, EventArgs args)
	{
		if (ZuneShell.DefaultInstance.CurrentPage is LibraryPage libraryPage)
		{
			libraryPage.DrmStateMask = ZuneUI.DrmStateMask.Protected();
			SQMLog.Log((SQMDataId)171, 1);
		}
	}

	private static void ShowContentTypesZunePass(object sender, EventArgs args)
	{
		if (ZuneShell.DefaultInstance.CurrentPage is LibraryPage libraryPage)
		{
			libraryPage.DrmStateMask = ZuneUI.DrmStateMask.ZunePass();
			SQMLog.Log((SQMDataId)172, 1);
		}
	}

	private void ShowArtistPivot(object sender, EventArgs args)
	{
		View = MusicLibraryView.Artist;
		SQMLog.Log((SQMDataId)153, 1);
		ViewTimeLogger.Instance.ViewChanged((SQMDataId)180);
	}

	private void ShowGenrePivot(object sender, EventArgs args)
	{
		View = MusicLibraryView.Genre;
		SQMLog.Log((SQMDataId)154, 1);
		ViewTimeLogger.Instance.ViewChanged((SQMDataId)183);
	}

	private void ShowAlbumPivot(object sender, EventArgs args)
	{
		View = MusicLibraryView.Album;
		SQMLog.Log((SQMDataId)155, 1);
		ViewTimeLogger.Instance.ViewChanged((SQMDataId)182);
	}

	private void ShowSongPivot(object sender, EventArgs args)
	{
		View = MusicLibraryView.Song;
		SQMLog.Log((SQMDataId)156, 1);
		ViewTimeLogger.Instance.ViewChanged((SQMDataId)181);
	}

	private void ShowPlaylistPivot(object sender, EventArgs args)
	{
		View = MusicLibraryView.Playlist;
		SQMLog.Log((SQMDataId)157, 1);
		ViewTimeLogger.Instance.ViewChanged((SQMDataId)184);
	}

	protected override void OnNavigatedToWorker()
	{
		if (!base.ShowDeviceContents)
		{
			base.ContentTypes.ChosenChanged += ContentTypesChanged;
		}
		if (base.NavigationArguments != null)
		{
			_preferredContentType = _showContentTypesAll;
			base.DrmStateMask = ZuneUI.DrmStateMask.All();
			if (base.NavigationArguments.Contains("ViewOverrideId"))
			{
				MusicLibraryView musicLibraryView = (MusicLibraryView)base.NavigationArguments["ViewOverrideId"];
				for (int i = 0; i < base.Views.Options.Count; i++)
				{
					ViewCommand viewCommand = (ViewCommand)base.Views.Options[i];
					if (viewCommand.View == musicLibraryView)
					{
						bool updatePreferredView = _updatePreferredView;
						_updatePreferredView = false;
						base.Views.ChosenIndex = i;
						_updatePreferredView = updatePreferredView;
						break;
					}
				}
			}
			_selectedArtistIds = null;
			if (base.NavigationArguments.Contains("ArtistLibraryId"))
			{
				_selectedArtistIds = new int[1] { (int)base.NavigationArguments["ArtistLibraryId"] };
			}
			_selectedGenreIds = null;
			if (base.NavigationArguments.Contains("GenreLibraryId"))
			{
				_selectedGenreIds = new int[1] { (int)base.NavigationArguments["GenreLibraryId"] };
			}
			_selectedAlbumIds = null;
			if (base.NavigationArguments.Contains("AlbumLibraryId"))
			{
				_selectedAlbumIds = new int[1] { (int)base.NavigationArguments["AlbumLibraryId"] };
			}
			_selectedTrackIds = null;
			if (base.NavigationArguments.Contains("TrackLibraryId"))
			{
				_selectedTrackIds = new int[1] { (int)base.NavigationArguments["TrackLibraryId"] };
			}
			if (base.NavigationArguments.Contains("PlaylistLibraryId"))
			{
				PlaylistsPanel.SelectedLibraryIds = new int[1] { (int)base.NavigationArguments["PlaylistLibraryId"] };
				PlaylistContentsPanel.SelectedLibraryIds = _selectedTrackIds;
			}
			base.NavigationArguments = null;
		}
		UpdateContentTypesPivots();
		base.OnNavigatedToWorker();
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		ViewTimeLogger.Instance.ViewChanged((SQMDataId)0);
		base.OnNavigatedAwayWorker(destination);
		SelectedPlaylist = null;
		if (!base.ShowDeviceContents)
		{
			base.ContentTypes.ChosenChanged -= ContentTypesChanged;
		}
		PlaylistManager.Instance.ValidateDefaultPlaylist();
	}

	public override IPageState SaveAndRelease()
	{
		if (_artistsPanel != null)
		{
			_artistsPanel.Release();
		}
		if (_genresPanel != null)
		{
			_genresPanel.Release();
		}
		if (_albumsPanel != null)
		{
			_albumsPanel.Release();
		}
		if (_tracksPanel != null)
		{
			_tracksPanel.Release();
		}
		if (_playlistsPanel != null)
		{
			_playlistsPanel.Release();
		}
		if (_playlistContentsPanel != null)
		{
			_playlistContentsPanel.Release();
		}
		return base.SaveAndRelease();
	}

	public static void FindInCollection(int artistId, int albumId, int trackId)
	{
		FindInCollection(artistId, albumId, trackId, selectTrack: true);
	}

	public static void FindInCollection(int artistId, int albumId, int trackId, bool selectTrack)
	{
		if (trackId >= 0 && albumId < 0)
		{
			albumId = PlaylistManager.GetFieldValue(trackId, (EListType)2, 11, -1);
		}
		if (albumId >= 0 && artistId < 0)
		{
			artistId = PlaylistManager.GetFieldValue(albumId, (EListType)1, 78, -1);
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add("AlbumLibraryId", albumId);
		hashtable.Add("ArtistLibraryId", artistId);
		if (selectTrack)
		{
			hashtable.Add("TrackLibraryId", trackId);
		}
		hashtable.Add("ViewOverrideId", MusicLibraryView.Artist);
		ZuneShell.DefaultInstance.Execute("Collection\\Music\\Default", hashtable);
	}

	public static void FindPlaylistInCollection(int playlistId)
	{
		FindPlaylistInCollection(playlistId, -1, selectTrack: false);
	}

	public static void FindPlaylistInCollection(int playlistId, int trackId, bool selectTrack)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("PlaylistLibraryId", playlistId);
		hashtable.Add("ViewOverrideId", MusicLibraryView.Playlist);
		if (selectTrack)
		{
			hashtable.Add("TrackLibraryId", trackId);
		}
		ZuneShell.DefaultInstance.Execute("Collection\\Music\\Default", hashtable);
	}

	public static Guid GetArtistZuneMediaId(int dbMediaId)
	{
		return PlaylistManager.GetFieldValue(dbMediaId, (EListType)0, 451, Guid.Empty);
	}

	public static void NavigateToPlaylistLand()
	{
		if (!(ZuneShell.DefaultInstance.CurrentPage is MusicLibraryPage { Views: var views }))
		{
			ZuneShell.DefaultInstance.NavigateToPage(new MusicLibraryPage(showDevice: false, MusicLibraryView.Playlist));
			return;
		}
		for (int i = 0; i < views.Options.Count; i++)
		{
			if (((ViewCommand)views.Options[i]).View == MusicLibraryView.Playlist)
			{
				views.ChosenIndex = i;
				break;
			}
		}
	}
}
