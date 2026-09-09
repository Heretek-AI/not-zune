using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;
using ZuneXml;

namespace ZuneUI;

public class MarketplaceTrackActionCommand : MarketplaceActionCommand
{
	internal Track TrackModel => base.Model as Track;

	protected override EContentType ContentType => (EContentType)0;

	public MarketplaceTrackActionCommand()
	{
		SignIn.Instance.SignInStatusUpdatedEvent += OnSignInEvent;
	}

	public override void UpdateState()
	{
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		base.UpdateState();
		if (TrackModel == null || CanFindInCollection || CanFindInHiddenCollection)
		{
			return;
		}
		if (!base.Downloading)
		{
			if (TrackModel.CanPurchaseFree)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FREE);
				((Command)this).Available = true;
			}
			else if (TrackModel.CanDownload)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DOWNLOAD);
				((Command)this).Available = !base.DownloadingHidden;
			}
			else if (TrackModel.CanPurchaseSubscriptionFree)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PURCHASE_BUTTON);
				((Command)this).Available = true;
			}
			else if (TrackModel.CanPurchaseAlbumOnly)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ALBUM_ONLY);
				((Command)this).Available = false;
			}
			else if (TrackModel.CanPurchase)
			{
				((ModelItem)this).Description = string.Format(Shell.LoadString(StringId.IDS_BUY), TrackModel.PointsPrice);
				((Command)this).Available = true;
				base.HasPoints = TrackModel.HasPoints;
			}
			else if (TrackModel.CanSubscriptionPlay)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PLAY_SONG);
				((Command)this).Available = true;
			}
			else if (TrackModel.CanPreview)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PREVIEW_SONG);
				((Command)this).Available = true;
			}
			else if (base.CanFindInZuneDotNet && !FeatureEnablement.IsFeatureEnabled((Features)28))
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_MORE_INFO);
				((Command)this).Available = true;
			}
		}
		if (!TrackModel.CanPurchase && HRESULT.op_Implicit(Download.Instance.GetErrorCode(base.Id)) == HRESULT._NS_E_MEDIA_NOT_PURCHASED)
		{
			Download.Instance.SetErrorCode(base.Id, ((HRESULT)(ref HRESULT._S_OK)).Int);
		}
	}

	public override void FindInCollection()
	{
		if (CanFindInCollection)
		{
			MusicLibraryPage.FindInCollection(-1, -1, base.CollectionId);
		}
	}

	protected override void OnDispose(bool fDisposing)
	{
		base.OnDispose(fDisposing);
		if (fDisposing)
		{
			SignIn.Instance.SignInStatusUpdatedEvent -= OnSignInEvent;
		}
	}

	private void OnSignInEvent(object sender, EventArgs args)
	{
		UpdateState();
	}
}
