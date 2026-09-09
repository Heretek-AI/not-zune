using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;

namespace ZuneUI;

public class VideoLibraryPage : LibraryPage
{
	private class ViewCommand : Command
	{
		private VideoLibraryView _view;

		public VideoLibraryView View => _view;

		public ViewCommand(IModelItemOwner owner, string description, VideoLibraryView view)
			: base(owner, description, (EventHandler)null)
		{
			_view = view;
		}
	}

	private VideosPanel _videosPanel;

	private bool _hasSelectedVideo;

	private object _selectedVideo;

	private IList _selectedVideoIds;

	private DataProviderQueryStatus _queryStatus;

	public VideoLibraryView View
	{
		get
		{
			if (base.ShowDeviceContents)
			{
				return VideoLibraryView.All;
			}
			return ((ViewCommand)base.Views.ChosenValue).View;
		}
		private set
		{
			if (base.ShowDeviceContents)
			{
				return;
			}
			foreach (ViewCommand option in base.Views.Options)
			{
				if (option.View == value)
				{
					base.Views.ChosenValue = option;
					break;
				}
			}
		}
	}

	public bool HasSelectedVideo
	{
		get
		{
			return _hasSelectedVideo;
		}
		set
		{
			if (_hasSelectedVideo != value)
			{
				_hasSelectedVideo = value;
				((ModelItem)this).FirePropertyChanged("HasSelectedVideo");
			}
		}
	}

	public object SelectedVideo
	{
		get
		{
			return _selectedVideo;
		}
		set
		{
			if (_selectedVideo != value)
			{
				_selectedVideo = value;
				((ModelItem)this).FirePropertyChanged("SelectedVideo");
			}
		}
	}

	public IList SelectedVideoIds
	{
		get
		{
			return _selectedVideoIds;
		}
		set
		{
			if (_selectedVideoIds != value)
			{
				_selectedVideoIds = value;
				((ModelItem)this).FirePropertyChanged("SelectedVideoIds");
				((ModelItem)this).FirePropertyChanged("SelectedVideosCount");
			}
		}
	}

	public DataProviderQueryStatus QueryStatus
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _queryStatus;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (_queryStatus != value)
			{
				_queryStatus = value;
				((ModelItem)this).FirePropertyChanged("QueryStatus");
			}
		}
	}

	public int SelectedVideosCount
	{
		get
		{
			if (_selectedVideoIds == null)
			{
				return 0;
			}
			return _selectedVideoIds.Count;
		}
	}

	public VideosPanel VideosPanel => _videosPanel;

	private static string LibraryTemplate => "res://ZuneShellResources!VideoLibrary.uix#VideoLibrary";

	public VideoLibraryPage()
		: this(showDevice: false, VideoLibraryView.Invalid)
	{
	}

	public VideoLibraryPage(bool showDevice, VideoLibraryView desiredView)
		: base(showDevice, MediaType.Video)
	{
		base.UI = LibraryTemplate;
		base.UIPath = "Collection\\Videos\\Default";
		if (showDevice)
		{
			base.PivotPreference = Shell.MainFrame.Device.Videos;
			Deviceland.InitDevicePage(this);
		}
		else
		{
			base.PivotPreference = Shell.MainFrame.Collection.Videos;
		}
		base.IsRootPage = true;
		if (!base.ShowDeviceContents)
		{
			base.Views = (Choice)(object)new NotifyChoice((IModelItemOwner)(object)this);
			base.Views.Options = new ViewCommand[6]
			{
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_ALL_PIVOT), VideoLibraryView.All),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_TV_PIVOT), VideoLibraryView.TV),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_MUSIC_PIVOT), VideoLibraryView.Music),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_MOVIES_PIVOT), VideoLibraryView.Movies),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_OTHER_PIVOT), VideoLibraryView.Other),
				new ViewCommand((IModelItemOwner)(object)base.Views, Shell.LoadString(StringId.IDS_COLLECTION_VIDEO_PERSONAL_PIVOT), VideoLibraryView.Personal)
			};
			int num = -1;
			if (desiredView != VideoLibraryView.Invalid)
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
				num = ClientConfiguration.Shell.VideoCollectionView;
			}
			base.Views.ChosenChanged += ViewChanged;
			if (num >= 0 && num < base.Views.Options.Count)
			{
				base.Views.ChosenIndex = num;
			}
		}
		_videosPanel = new VideosPanel(this);
		base.ShowPlaylistIcon = false;
		base.TransportControlStyle = TransportControlStyle.Video;
		base.PlaybackContext = PlaybackContext.LibraryVideo;
	}

	private void ViewChanged(object sender, EventArgs e)
	{
		ClientConfiguration.Shell.VideoCollectionView = base.Views.ChosenIndex;
		((ModelItem)this).FirePropertyChanged("View");
		SelectedVideo = null;
		SelectedVideoIds = null;
	}

	protected override void OnNavigatedToWorker()
	{
		if (base.NavigationArguments != null)
		{
			if (base.NavigationArguments.Contains("ViewOverrideId"))
			{
				View = (VideoLibraryView)base.NavigationArguments["ViewOverrideId"];
			}
			_selectedVideoIds = null;
			if (base.NavigationArguments.Contains("VideoLibraryId"))
			{
				_selectedVideoIds = new int[1] { (int)base.NavigationArguments["VideoLibraryId"] };
			}
			base.NavigationArguments = null;
		}
		base.OnNavigatedToWorker();
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		base.OnNavigatedAwayWorker(destination);
		SelectedVideo = null;
	}

	public override IPageState SaveAndRelease()
	{
		_videosPanel.Release();
		return base.SaveAndRelease();
	}

	public static void FindInCollection(int videoId)
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add("VideoLibraryId", videoId);
		hashtable.Add("ViewOverrideId", VideoLibraryView.All);
		ZuneShell.DefaultInstance.Execute("Collection\\Videos\\Default", hashtable);
	}
}
