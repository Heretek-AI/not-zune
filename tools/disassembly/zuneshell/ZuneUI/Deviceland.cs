using Microsoft.Iris;

namespace ZuneUI;

public class Deviceland : ZunePage, IDeviceContentsPage, IPage
{
	bool IDeviceContentsPage.ShowDeviceContents => true;

	public Deviceland()
	{
		base.PivotPreference = Shell.MainFrame.Device.Status;
		base.UI = "res://ZuneShellResources!DeviceSummary.uix#DeviceSummary";
		base.UIPath = "Device\\Summary";
		base.IsRootPage = true;
		InitDevicePage(this);
		base.ShowComputerIcon = ComputerIconState.Show;
	}

	public static void InitDevicePage(ZunePage page)
	{
		page.BackgroundUI = "res://ZuneShellResources!DevicelandElements.uix#DeviceBackground";
		page.TransportControlStyle = TransportControlStyle.None;
		page.ShowCDIcon = false;
		page.ShowDeviceIcon = false;
		page.ShowPlaylistIcon = false;
		page.ShowComputerIcon = ComputerIconState.ShowAsDropTarget;
		page.NotificationAreaVisible = false;
		page.TransportControlsVisible = false;
		page.BottomBarUI = "res://ZuneShellResources!DevicelandElements.uix#GasGauge";
		page.ShowAppBackground = false;
		page.ShowNowPlayingBackgroundOnIdle = false;
	}

	public override void InvokeSettings()
	{
		((Command)Shell.SettingsFrame.Settings.Device).Invoke();
	}

	protected override void OnNavigatedToWorker()
	{
		base.OnNavigatedToWorker();
		Management management = ZuneShell.DefaultInstance.Management;
		Category alertedDeviceCategory = management.AlertedDeviceCategory;
		if (alertedDeviceCategory != null)
		{
			management.AlertedDeviceCategory = null;
			ZuneShell.DefaultInstance.NavigateToPage(new FirstConnectLandPage());
		}
	}

	public override IPageState SaveAndRelease()
	{
		return new DevicePivotManagingPageState(this);
	}
}
