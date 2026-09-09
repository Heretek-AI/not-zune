using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneUI;

public class ChannelTrackActionCommand : MarketplaceActionCommand
{
	private bool _lastSignedInUserHadActiveSubscription;

	protected override EContentType ContentType => (EContentType)0;

	public override bool CanFindInCollection => PlaylistManager.IsInVisibleCollection(base.CollectionId, MediaType.Track);

	public bool CanAddToCollection
	{
		get
		{
			if (_lastSignedInUserHadActiveSubscription)
			{
				return ZuneApplication.Service.InHiddenCollection(base.Id, (EContentType)0);
			}
			return false;
		}
	}

	public bool CanDownload
	{
		get
		{
			if (_lastSignedInUserHadActiveSubscription && !CanFindInCollection && !CanAddToCollection && !base.Downloading && base.Id != Guid.Empty)
			{
				return Download.Instance.GetErrorCode(base.Id) != ((HRESULT)(ref HRESULT._ZUNE_E_NO_SUBSCRIPTION_DOWNLOAD_RIGHTS)).Int;
			}
			return false;
		}
	}

	public bool CanPurchase => base.Id != Guid.Empty;

	protected override string ZuneMediaIdPropertyName => "ZuneMediaId";

	public ChannelTrackActionCommand()
	{
		ShowHiddenProgress = true;
		SignIn.Instance.SignInStatusUpdatedEvent += OnSignInEvent;
	}

	public override void UpdateState()
	{
		base.Progress = -1f;
		base.Downloading = false;
		_lastSignedInUserHadActiveSubscription = SignIn.Instance.LastSignedInUserHadActiveSubscription;
		bool flag = false;
		bool flag2 = false;
		base.CollectionId = (int)base.Model.GetProperty("MediaId");
		if (CanAddToCollection)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ADD_TO_COLLECTION);
			((Command)this).Available = true;
		}
		else if (CanFindInCollection)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_INCOLLECTION);
			((Command)this).Available = true;
		}
		else if (ZuneApplication.Service.IsDownloading(base.Id, (EContentType)0, ref flag, ref flag2))
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PENDING);
			base.Downloading = true;
			((Command)this).Available = true;
		}
		else if (CanDownload)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DOWNLOAD);
			((Command)this).Available = true;
		}
		else if (CanPurchase)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PURCHASE_BUTTON);
			((Command)this).Available = true;
		}
		else
		{
			((Command)this).Available = false;
		}
	}

	public override void FindInCollection()
	{
		MusicLibraryPage.FindInCollection(-1, -1, base.CollectionId);
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
