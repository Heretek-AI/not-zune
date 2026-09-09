using Microsoft.Iris;
using Microsoft.Zune.Service;
using ZuneXml;

namespace ZuneUI;

public class MarketplaceTrackHistoryActionCommand : MarketplaceTrackActionCommand
{
	internal TrackHistory TrackHistoryModel => (TrackHistory)(object)base.Model;

	protected override EContentType ContentType => (EContentType)0;

	public override void UpdateState()
	{
		base.UpdateState();
		if (!CanFindInCollection && !base.Downloading)
		{
			if (!TrackHistoryModel.CanDownload)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PURCHASE_BUTTON);
				Download.Instance.SetHistoryErrorCode(base.Id, ((HRESULT)(ref HRESULT._NS_E_MEDIA_DOWNLOAD_MAXIMUM_EXCEEDED)).Int);
			}
			else
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DOWNLOAD);
			}
			((Command)this).Available = true;
		}
		else
		{
			((Command)this).Available = false;
		}
	}
}
