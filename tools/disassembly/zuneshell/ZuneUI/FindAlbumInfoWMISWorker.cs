using Microsoft.Iris;
using Microsoft.Zune.Shell;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class FindAlbumInfoWMISWorker : ModelItem
{
	private AlbumMetadata _metadata;

	private DataProviderQueryStatus _status;

	public AlbumMetadata Metadata
	{
		get
		{
			return _metadata;
		}
		private set
		{
			_metadata = value;
			((ModelItem)this).FirePropertyChanged("Metadata");
		}
	}

	public DataProviderQueryStatus Status
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _status;
		}
		internal set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_status = value;
			((ModelItem)this).FirePropertyChanged("Status");
		}
	}

	public void BeginGetAlbumFromWMIS(long wmisAlbumId, int wmisVolume, AlbumMetadata albumMetadata)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		Status = (DataProviderQueryStatus)1;
		ZuneApplication.ZuneLibrary.GetAlbumMetadataForAlbumId(wmisAlbumId, wmisVolume, albumMetadata, new GetAlbumForAlbumIdCompleteHandler(OnCompleteGetAlbumFromWMIS));
	}

	public void OnCompleteGetAlbumFromWMIS(long wmisAlbumId, int wmisVolume, int hr, AlbumMetadata albumMetadata)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (hr == 0)
			{
				Metadata = albumMetadata;
				Status = (DataProviderQueryStatus)3;
			}
			else
			{
				Status = (DataProviderQueryStatus)4;
			}
		}, (object)null);
	}
}
