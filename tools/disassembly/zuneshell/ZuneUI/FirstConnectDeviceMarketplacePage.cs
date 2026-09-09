using Microsoft.Iris;

namespace ZuneUI;

public class FirstConnectDeviceMarketplacePage : FirstConnectPage
{
	public override string UI => "res://ZuneShellResources!FirstConnect.uix#FirstConnectDeviceMarketplacePage";

	internal FirstConnectDeviceMarketplacePage(Wizard wizard)
		: base(wizard)
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ENABLE_ZUNE_MARKETPLACE_HEADER);
	}

	internal override bool OnMovingNext()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		DeviceManagement deviceManagement = ZuneShell.DefaultInstance.Management.DeviceManagement;
		if (deviceManagement.EnableMarketplaceChoice.ChosenIndex == 0 || HRESULT.op_Implicit(deviceManagement.MarketplaceCredentials.hr) == HRESULT._S_OK)
		{
			return base.OnMovingNext();
		}
		deviceManagement.CredentialValidationRequested = true;
		return false;
	}
}
