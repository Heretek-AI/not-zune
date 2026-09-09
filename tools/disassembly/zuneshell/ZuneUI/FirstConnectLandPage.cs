using Microsoft.Iris;

namespace ZuneUI;

public class FirstConnectLandPage : SetupLandPage
{
	public FirstConnectLandPage()
	{
		base.UI = "res://ZuneShellResources!SetupLand.uix#FirstConnect";
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		base.OnDispose(disposing);
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			if (UIDeviceList.IsSuitableForConnection(SyncControls.Instance.CurrentDeviceOverride))
			{
				SyncControls.Instance.CurrentDeviceOverride.Enumerate();
			}
			DeviceManagement.HandleSetupQueue();
		}, (DeferredInvokePriority)1);
	}
}
