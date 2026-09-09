using System;
using System.Threading;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class LibraryAlbumInfo : ModelItem
{
	private int _trackId;

	private string _trackTitle;

	private string _albumTitle;

	private string _artistName;

	private string _albumArtUrl;

	private Image _thumbnailImage;

	private Guid _zuneMediaId;

	private ICommand _onAsyncUpdateAlbumArtUrlCompleted;

	public ICommand OnAsyncUpdateAlbumArtUrlCompleted => _onAsyncUpdateAlbumArtUrlCompleted;

	public string TrackTitle => _trackTitle;

	public string AlbumTitle => _albumTitle;

	public string ArtistName => _artistName;

	public string AlbumArtUrl => _albumArtUrl;

	public Guid ZuneMediaId => _zuneMediaId;

	public Image ThumbnailImage => _thumbnailImage;

	public LibraryAlbumInfo(LibraryPlaybackTrack track, int thumbnailMaxWidth, int thumbnailMaxHeight)
		: this(track, thumbnailMaxWidth, thumbnailMaxHeight, null)
	{
	}

	public LibraryAlbumInfo(LibraryPlaybackTrack track, int thumbnailMaxWidth, int thumbnailMaxHeight, ICommand onAsyncUpdateAlbumArtUrlCompleted)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		if (onAsyncUpdateAlbumArtUrlCompleted != null)
		{
			_onAsyncUpdateAlbumArtUrlCompleted = onAsyncUpdateAlbumArtUrlCompleted;
		}
		else
		{
			_onAsyncUpdateAlbumArtUrlCompleted = (ICommand)new Command((IModelItemOwner)(object)this);
		}
		if (track.MediaType == MediaType.Track)
		{
			_trackId = track.MediaId;
			_trackTitle = track.Title;
			int[] array = new int[2] { 11, 78 };
			object[] array2 = new object[2] { -1, -1 };
			ZuneLibrary.GetFieldValues(_trackId, (EListType)2, array.Length, array, array2, PlaylistManager.Instance.QueryContext);
			int albumId = (int)array2[0];
			int num = (int)array2[1];
			if (albumId >= 0)
			{
				array = new int[2] { 382, 451 };
				object[] array3 = new object[2];
				array2 = array3;
				ZuneLibrary.GetFieldValues(albumId, (EListType)1, array.Length, array, array2, PlaylistManager.Instance.QueryContext);
				_albumTitle = (string)array2[0];
				_zuneMediaId = GuidHelper.CreateFromString((string)array2[1]);
				ThreadPool.QueueUserWorkItem(delegate
				{
					//IL_0032: Unknown result type (might be due to invalid IL or missing references)
					//IL_003d: Expected O, but got Unknown
					string text = LibraryDataProviderItemBase.GetArtUrl(albumId, "Album", false);
					if (!string.IsNullOrEmpty(text))
					{
						text = "file://" + text;
					}
					Application.DeferredInvoke(new DeferredInvokeHandler(AsyncUpdateAlbumArtUrl), (object)text);
				}, null);
			}
			if (num >= 0)
			{
				array = new int[1] { 138 };
				object[] array4 = new object[1];
				array2 = array4;
				ZuneLibrary.GetFieldValues(num, (EListType)0, array.Length, array, array2, PlaylistManager.Instance.QueryContext);
				_artistName = (string)array2[0];
			}
		}
		else if (track.MediaType == MediaType.PodcastEpisode)
		{
			_trackId = track.MediaId;
			_trackTitle = track.Title;
			int[] array5 = new int[2] { 311, 24 };
			object[] array6 = new object[2] { -1, null };
			ZuneLibrary.GetFieldValues(_trackId, (EListType)7, array5.Length, array5, array6, PlaylistManager.Instance.QueryContext);
			int num2 = (int)array6[0];
			_artistName = (string)array6[1];
			if (num2 >= 0)
			{
				array5 = new int[2] { 344, 17 };
				object[] array7 = new object[2];
				array6 = array7;
				ZuneLibrary.GetFieldValues(num2, (EListType)6, array5.Length, array5, array6, PlaylistManager.Instance.QueryContext);
				_albumTitle = (string)array6[0];
				_albumArtUrl = (string)array6[1];
			}
		}
		else
		{
			if (track.MediaType != MediaType.Video)
			{
				return;
			}
			_trackId = track.MediaId;
			_trackTitle = track.Title;
			int[] array8 = new int[3] { 380, 382, 312 };
			object[] array9 = new object[3];
			object[] array10 = array9;
			ZuneLibrary.GetFieldValues(_trackId, (EListType)4, array8.Length, array8, array10, PlaylistManager.Instance.QueryContext);
			_artistName = (string)array10[0];
			_albumTitle = (string)array10[1];
			if (string.IsNullOrEmpty(_albumTitle))
			{
				_albumTitle = (string)array10[2];
			}
			if ((int)Application.RenderingType != 0)
			{
				return;
			}
			ThreadPool.QueueUserWorkItem(delegate
			{
				//IL_002d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0038: Expected O, but got Unknown
				string artUrl = LibraryDataProviderItemBase.GetArtUrl(_trackId, "Video", false);
				if (!string.IsNullOrEmpty(artUrl))
				{
					artUrl = "file://" + artUrl;
					Application.DeferredInvoke(new DeferredInvokeHandler(AsyncUpdateThumbnailUrl), (object)artUrl);
				}
			}, null);
		}
	}

	private void AsyncUpdateAlbumArtUrl(object args)
	{
		_albumArtUrl = (string)args;
		((ModelItem)this).FirePropertyChanged("AlbumArtUrl");
		OnAsyncUpdateAlbumArtUrlCompleted.Invoke();
	}

	private void AsyncUpdateThumbnailUrl(object args)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		string text = (string)args;
		Image.RemoveCache(text);
		_thumbnailImage = new Image(text);
		((ModelItem)this).FirePropertyChanged("ThumbnailImage");
	}
}
