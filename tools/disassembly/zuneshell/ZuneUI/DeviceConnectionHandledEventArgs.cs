namespace ZuneUI;

internal class DeviceConnectionHandledEventArgs
{
	public readonly UIDevice Device;

	public readonly bool IsFirstConnect;

	public DeviceConnectionHandledEventArgs(UIDevice device, bool isFirstConnect)
	{
		Device = device;
		IsFirstConnect = isFirstConnect;
	}
}
