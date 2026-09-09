namespace ZuneUI;

public interface IColorSet
{
	IDeviceColor Light { get; }

	IDeviceColor Dark { get; }

	IDeviceColor Text { get; }

	IDeviceColor HoverText { get; }
}
