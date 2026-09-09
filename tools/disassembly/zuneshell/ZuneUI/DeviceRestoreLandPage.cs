using Microsoft.Iris;

namespace ZuneUI;

public class DeviceRestoreLandPage : SetupLandPage
{
	public DeviceRestoreLandPage()
	{
		base.UI = "res://ZuneShellResources!SetupLand.uix#DeviceRestore";
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		base.OnDispose(disposing);
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			SyncControls.Instance.CurrentDeviceOverride.Enumerate();
			DeviceManagement.HandleSetupQueue();
		}, (DeferredInvokePriority)1);
	}
}
