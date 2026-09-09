using Microsoft.Iris;

namespace ZuneUI;

public class MarketplaceVideoPurchaseHistoryActionCommand : MarketplaceVideoHistoryActionCommand
{
	public override void UpdateState()
	{
		base.UpdateState();
		if (!base.Downloading)
		{
			if (CanFindInCollectionShortcut)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DOWNLOAD);
				((Command)this).Available = true;
			}
			else if (!CanFindInCollection)
			{
				if (!base.VideoHistoryModel.CanDownload && !base.VideoHistoryModel.CanPurchase)
				{
					((ModelItem)this).Description = Shell.LoadString(StringId.IDS_NOT_AVAILABLE);
					((Command)this).Available = false;
					Download.Instance.SetHistoryErrorCode(base.Id, ((HRESULT)(ref HRESULT._ZUNE_E_CONTENT_NOT_SUPPORTED_ON_TUNER)).Int);
				}
				else if (!base.VideoHistoryModel.CanDownload)
				{
					((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PURCHASE_BUTTON);
					((Command)this).Available = true;
					Download.Instance.SetHistoryErrorCode(base.Id, ((HRESULT)(ref HRESULT._NS_E_MEDIA_DOWNLOAD_MAXIMUM_EXCEEDED)).Int);
				}
				else
				{
					((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DOWNLOAD);
					((Command)this).Available = true;
				}
			}
			else
			{
				((Command)this).Available = false;
			}
		}
		else
		{
			((Command)this).Available = false;
		}
	}
}
