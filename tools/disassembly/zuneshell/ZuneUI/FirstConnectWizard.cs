using Microsoft.Zune.Util;

namespace ZuneUI;

public class FirstConnectWizard : DeviceWizard
{
	public FirstConnectWizard()
	{
		AddPage(new FirstConnectDeviceNamePage(this));
		if (FeatureEnablement.IsFeatureEnabled((Features)5) || FeatureEnablement.IsFeatureEnabled((Features)2))
		{
			AddPage(new FirstConnectDeviceMarketplacePage(this));
		}
		AddPage(new FirstConnectDeviceSyncOptionsPage(this));
		if (base.ActiveDevice.SupportsUsageData)
		{
			AddPage(new FirstConnectDeviceCustomPrivacyPage(this));
		}
		else
		{
			ZuneShell.DefaultInstance.Management.DeviceManagement.PrivacyChoice.Value = false;
		}
		ZuneShell.DefaultInstance.Management.DeviceManagement.DevicePartnership = DeviceRelationship.Permanent;
	}

	protected override bool OnCommitChanges()
	{
		ZuneShell.DefaultInstance.Management.CommitListSave();
		bool navigateToLandingPage = base.ActiveDevice.SupportsBrandingType(DeviceBranding.Kin);
		ZuneShell.DefaultInstance.Management.DeviceManagement.SetupComplete(navigateToLandingPage);
		return base.OnCommitChanges();
	}
}
