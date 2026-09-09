using Microsoft.Iris;

namespace ZuneUI;

public class MarketplaceVideoRentalHistoryActionCommand : MarketplaceVideoHistoryActionCommand
{
	public override void UpdateState()
	{
		base.UpdateState();
		if (!CanFindInCollection && !base.Downloading)
		{
			if (!base.VideoHistoryModel.CanDownload && !base.VideoHistoryModel.CanRent)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_NOT_AVAILABLE);
				((Command)this).Available = false;
				Download.Instance.SetHistoryErrorCode(base.Id, ((HRESULT)(ref HRESULT._ZUNE_E_CONTENT_NOT_SUPPORTED_ON_TUNER)).Int);
			}
			else if (!base.VideoHistoryModel.CanDownload)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_RENT_BUTTON);
				((Command)this).Available = true;
				Download.Instance.SetHistoryErrorCode(base.Id, ((HRESULT)(ref HRESULT._NS_E_MEDIA_DOWNLOAD_MAXIMUM_EXCEEDED)).Int);
			}
			else
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DOWNLOAD);
				((Command)this).Available = true;
			}
		}
		else if (CanFindInCollection && DrmHelper.IsRentalExpired(26, base.Id))
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_RENT_BUTTON);
			((Command)this).Available = true;
		}
		else
		{
			((Command)this).Available = false;
		}
	}
}
