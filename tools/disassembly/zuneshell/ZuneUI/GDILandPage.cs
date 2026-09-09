namespace ZuneUI;

public class GDILandPage : SetupLandPage
{
	public GDILandPage()
	{
		base.UI = "res://ZuneShellResources!SetupLand.uix#GDIWarning";
		base.BackgroundUI = "res://ZuneShellResources!SetupLand.uix#SetupLandBackground";
	}

	protected override void SetConnectionRule()
	{
		SingletonModelItem<UIDeviceList>.Instance.AllowDeviceConnections = true;
	}
}
