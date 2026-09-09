namespace ZuneUI;

public class FirstConnectForPhoneWizard : DeviceUpdateWizard
{
	private bool _wasPreviouslyNotPaired;

	public bool RequiredStepsComplete
	{
		get
		{
			if (base.CurrentPage is FirstConnectPhoneIntroPage || base.CurrentPage is FirstConnectDeviceNamePage)
			{
				FirstConnectPage firstConnectPage = (FirstConnectPage)base.CurrentPage;
				return firstConnectPage.IsPageComplete;
			}
			return true;
		}
	}

	public override bool CanCommitChanges
	{
		get
		{
			if (IsValid)
			{
				return RequiredStepsComplete;
			}
			return false;
		}
	}

	public FirstConnectForPhoneWizard()
	{
		_wasPreviouslyNotPaired = ZuneShell.DefaultInstance.Management.DeviceManagement.DevicePartnership == DeviceRelationship.None;
		ZuneShell.DefaultInstance.Management.DeviceManagement.DevicePartnership = DeviceRelationship.Permanent;
	}

	protected override void AddPages()
	{
		AddPage(new FirstConnectPhoneIntroPage(this), Shell.LoadString(StringId.IDS_BREADCRUMB_INTRODUCTION));
		AddPage(new FirstConnectDeviceNamePage(this), Shell.LoadString(StringId.IDS_BREADCRUMB_PHONE_NAME));
		AddPage(new DeviceUpdateCheckingPage(this), Shell.LoadString(StringId.IDS_BREADCRUMB_PHONE_UPDATE));
		AddPage(new DeviceUpdateEULAPage(this));
		AddPage(new DeviceUpdateBatteryPowerPage(this));
		AddPage(new DeviceUpdateGuestWarningPage(this));
		AddPage(new DeviceUpdateSyncingPage(this));
		AddPage(new DeviceUpdateDiskSpaceErrorPage(this));
		AddPage(new DeviceUpdateProgressPage(this));
		AddPage(new DeviceUpdateSummaryPage(this));
		AddPage(new DeviceUpdateErrorPage(this));
	}

	public override void Cancel()
	{
		if (base.CurrentPage is DeviceUpdateProgressPage)
		{
			if (base.UIFirmwareUpdater != null && base.UIFirmwareUpdater.UpdateInProgress)
			{
				base.UIFirmwareUpdater.CancelFirmwareUpdate();
			}
		}
		else if (!RequiredStepsComplete)
		{
			base.Cancel();
		}
	}

	public override bool MoveBack()
	{
		if (base.MoveBack())
		{
			if (base.CurrentPage is FirstConnectPage)
			{
				FirstConnectPage firstConnectPage = (FirstConnectPage)base.CurrentPage;
				firstConnectPage.IsPageComplete = false;
			}
			return true;
		}
		return false;
	}

	protected override bool OnCommitChanges()
	{
		bool navigateToLandingPage = _wasPreviouslyNotPaired && !(base.CurrentPage is FirstConnectDeviceNamePage);
		ZuneShell.DefaultInstance.Management.CommitListSave();
		ZuneShell.DefaultInstance.Management.DeviceManagement.SetupComplete(navigateToLandingPage);
		return base.OnCommitChanges();
	}
}
