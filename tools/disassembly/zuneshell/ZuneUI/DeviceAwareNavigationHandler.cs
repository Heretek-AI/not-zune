namespace ZuneUI;

public abstract class DeviceAwareNavigationHandler : NavigationCommandHandlerBase
{
	private bool _showDeviceContents;

	public bool ShowDeviceContents
	{
		get
		{
			return _showDeviceContents;
		}
		set
		{
			_showDeviceContents = value;
		}
	}
}
