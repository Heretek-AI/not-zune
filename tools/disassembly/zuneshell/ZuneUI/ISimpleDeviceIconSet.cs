using Microsoft.Iris;

namespace ZuneUI;

public interface ISimpleDeviceIconSet
{
	Image Connected { get; }

	Image Disconnected { get; }

	Image GetImageForConnectedness(bool isConnected);
}
