using Microsoft.Iris;

namespace MicrosoftZuneLibrary;

public class StaticLibraryDataProviderQuery : DataProviderQuery
{
	public static DataProviderQuery CreateInstance(object queryTypeCookie)
	{
		return (DataProviderQuery)(object)new StaticLibraryDataProviderQuery(queryTypeCookie);
	}

	protected override void BeginExecute()
	{
		int userId = 1;
		int deviceId = 1;
		int libraryId = -1;
		object property = ((DataProviderQuery)this).GetProperty("UserId");
		if (property != null)
		{
			userId = (int)property;
		}
		object property2 = ((DataProviderQuery)this).GetProperty("LibraryId");
		if (property2 != null)
		{
			libraryId = (int)property2;
		}
		object property3 = ((DataProviderQuery)this).GetProperty("DeviceId");
		if (property3 != null)
		{
			deviceId = (int)property3;
		}
		string thumbnailFallbackImageUrl = (string)((DataProviderQuery)this).GetProperty("ThumbnailFallbackImageUrl");
		EListType eListType;
		StaticLibraryDataProviderObject result;
		switch ((string)((DataProviderQuery)this).GetProperty("QueryType"))
		{
		case "Artist":
			eListType = EListType.eArtistList;
			goto IL_0118;
		case "Album":
			eListType = EListType.eAlbumList;
			goto IL_0118;
		case "Folder":
			eListType = EListType.eFolderList;
			goto IL_0118;
		case "Video":
			eListType = EListType.eVideoList;
			goto IL_0118;
		case "PodcastSeries":
			eListType = EListType.ePodcastList;
			goto IL_0118;
		case "Playlist":
			eListType = EListType.ePlaylistList;
			goto IL_0118;
		case "Genre":
			eListType = EListType.eGenreList;
			goto IL_0118;
		case "Pin":
			eListType = EListType.ePinList;
			goto IL_0118;
		case "Photo":
			eListType = EListType.ePhotoList;
			goto IL_0118;
		default:
			{
				((DataProviderQuery)this).Status = (DataProviderQueryStatus)4;
				break;
			}
			IL_0118:
			result = new StaticLibraryDataProviderObject((DataProviderQuery)(object)this, ((DataProviderQuery)this).ResultTypeCookie, eListType, libraryId, userId, deviceId, thumbnailFallbackImageUrl);
			((DataProviderQuery)this).Result = result;
			((DataProviderQuery)this).Status = (DataProviderQueryStatus)1;
			((DataProviderQuery)this).Status = (DataProviderQueryStatus)3;
			break;
		}
	}

	private StaticLibraryDataProviderQuery(object queryTypeCookie)
		: base(queryTypeCookie)
	{
	}
}
