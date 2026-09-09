using Microsoft.Iris;
using Microsoft.Zune.Configuration;

namespace ZuneUI;

public class FirstLaunchForPhoneWizard : DeviceUpdateWizard
{
	private bool _isSoftwareSettingsEnabled = true;

	private bool _wasPreviouslyNotPaired;

	public bool IsSoftwareSettingsEnabled
	{
		get
		{
			return _isSoftwareSettingsEnabled;
		}
		set
		{
			if (_isSoftwareSettingsEnabled != value)
			{
				_isSoftwareSettingsEnabled = value;
				((ModelItem)this).FirePropertyChanged("IsSoftwareSettingsEnabled");
			}
		}
	}

	public override bool CanCommitChanges
	{
		get
		{
			if (IsValid)
			{
				if (base.CurrentPage is DeviceUpdateCheckingPage && base.ActiveDevice.UIFirmwareUpdater != null && !base.ActiveDevice.UIFirmwareUpdater.IsUpdateAvailable)
				{
					return true;
				}
				if (!base.CanAdvancePageIndex || base.CurrentPage is FirstLaunchWelcomePage || base.CurrentPage is FirstConnectDeviceNamePage)
				{
					return true;
				}
				return false;
			}
			return false;
		}
	}

	public bool RequiredStepsComplete
	{
		get
		{
			if (base.CurrentPage is DeviceUpdatePage || base.CurrentPage is DeviceUpdateErrorPage)
			{
				return true;
			}
			return false;
		}
	}

	public FirstLaunchForPhoneWizard()
	{
		_wasPreviouslyNotPaired = ZuneShell.DefaultInstance.Management.DeviceManagement.DevicePartnership == DeviceRelationship.None;
		ZuneShell.DefaultInstance.Management.DeviceManagement.DevicePartnership = DeviceRelationship.Permanent;
	}

	protected override void AddPages()
	{
		AddPage(new FirstConnectPhoneIntroPage(this), Shell.LoadString(StringId.IDS_BREADCRUMB_INTRODUCTION));
		AddPage(new FirstLaunchForPhoneWelcomePage(this), Shell.LoadString(StringId.IDS_BREADCRUMB_SOFTWARE_SETTINGS));
		AddPage(new FirstLaunchMonitoredFoldersPage(this));
		AddPage(new FirstLaunchDownloadFoldersPage(this));
		AddPage(new FirstLaunchFileTypesPage(this));
		AddPage(new FirstLaunchPrivacyPage(this));
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

	protected override bool OnCommitChanges()
	{
		bool flag = _wasPreviouslyNotPaired && !(base.CurrentPage is FirstConnectDeviceNamePage);
		if (!IsSoftwareSettingsEnabled)
		{
			ClientConfiguration.FUE.AcceptedPrivacyStatement = true;
		}
		ZuneShell.DefaultInstance.Management.CommitListSave();
		ZuneShell.DefaultInstance.Management.DeviceManagement.SetupComplete(flag);
		if (flag || base.CurrentPage is DeviceUpdatePage || base.CurrentPage is DeviceUpdateErrorPage)
		{
			Fue.Instance.MigrateLegacyConfiguration();
			Fue.Instance.CompleteFUE();
			if (!base.ActiveDevice.AllowChainedUpdates)
			{
				((Command)Shell.MainFrame.Device).Invoke();
			}
			else
			{
				base.ActiveDevice.NavigateToDeviceSummaryAfterUpdate = true;
				base.ActiveDevice.UIFirmwareUpdater.StartCheckForUpdates(forceServerRequest: false, launchWizardIfUpdatesFound: true);
			}
		}
		return base.OnCommitChanges();
	}
}
