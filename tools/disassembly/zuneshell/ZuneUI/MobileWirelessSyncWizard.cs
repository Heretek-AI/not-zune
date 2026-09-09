using Microsoft.Iris;

namespace ZuneUI;

public class MobileWirelessSyncWizard : Wizard
{
	private MobileWirelessSync _mobileWirelessSync;

	public MobileWirelessSync MobileWirelessSync
	{
		get
		{
			return _mobileWirelessSync;
		}
		set
		{
			if (_mobileWirelessSync != value)
			{
				_mobileWirelessSync = value;
				((ModelItem)this).FirePropertyChanged("MobileWirelessSync");
			}
		}
	}

	public MobileWirelessSyncWizard()
	{
		AddPage(new MobileWirelessSyncConfirmPage(this));
		AddPage(new MobileWirelessSyncSummaryPage(this));
		AddPage(new MobileWirelessSyncErrorPage(this));
	}

	protected override void OnSetError(HRESULT hr, object state)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		if (hr == HRESULT._S_OK)
		{
			base.ErrorPageIsEnabled = false;
		}
		base.OnSetError(hr, state);
	}

	public override void Cancel()
	{
		if (base.CurrentPage is MobileWirelessSyncWizardPage mobileWirelessSyncWizardPage)
		{
			mobileWirelessSyncWizardPage.OnCancel();
		}
		if (base.CurrentPage is MobileWirelessSyncErrorPage mobileWirelessSyncErrorPage)
		{
			mobileWirelessSyncErrorPage.OnCancel();
		}
		base.Cancel();
	}
}
