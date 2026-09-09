namespace ZuneUI;

public interface IBackgroundAwareDeviceIconSet
{
	ISimpleDeviceIconSet ForLightBackground { get; }

	ISimpleDeviceIconSet ForDarkBackground { get; }

	ISimpleDeviceIconSet GetSetForBackground(bool backgroundIsDark);
}
