namespace ZuneUI;

public interface IInteractiveDeviceIconSet
{
	IBackgroundAwareDeviceIconSet Default { get; }

	IBackgroundAwareDeviceIconSet Hover { get; }

	IBackgroundAwareDeviceIconSet Drag { get; }

	IBackgroundAwareDeviceIconSet Click { get; }

	IBackgroundAwareDeviceIconSet Syncing { get; }
}
