namespace ZuneUI;

public class SetupLandPage : NoStackPage
{
	public SetupLandPage()
	{
		SetConnectionRule();
		base.ShowComputerIcon = ComputerIconState.Hide;
		base.ShowSearch = false;
		base.ShowSettings = false;
		base.ShowLogo = false;
		base.ShowBackArrow = false;
		base.ShowCDIcon = false;
		base.ShowDeviceIcon = false;
		base.ShowPlaylistIcon = false;
		base.ShowPivots = false;
		base.CanEnterCompactMode = false;
		base.TransportControlStyle = TransportControlStyle.None;
		base.NotificationAreaVisible = false;
		base.TransportControlsVisible = false;
		base.PivotPreference = Shell.SettingsFrame.Wizard.FUE;
	}

	protected override void OnNavigatedToWorker()
	{
		SyncControls.Instance.CurrentDevice.IsLockedAgainstSyncing = true;
		base.OnNavigatedToWorker();
	}

	protected override void OnNavigatedAwayWorker(IPage destination)
	{
		if (!(destination is SetupLandPage))
		{
			SyncControls.Instance.CurrentDevice.IsLockedAgainstSyncing = false;
		}
		base.OnNavigatedAwayWorker(destination);
	}

	protected virtual void SetConnectionRule()
	{
		SingletonModelItem<UIDeviceList>.Instance.AllowDeviceConnections = false;
	}

	protected override void OnDispose(bool disposing)
	{
		DeviceManagement.SetupDevice = null;
		SingletonModelItem<UIDeviceList>.Instance.AllowDeviceConnections = true;
		base.OnDispose(disposing);
	}
}
