using Microsoft.Iris;

namespace ZuneUI;

public interface IDeviceIconSet
{
	ISimpleDeviceIconSet Large { get; }

	ISimpleDeviceIconSet Medium { get; }

	IInteractiveDeviceIconSet Small { get; }

	Image Background { get; }

	IColorSet Colors { get; }
}
