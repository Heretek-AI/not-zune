using System;
using System.Runtime.InteropServices;

namespace ZuneUI;

internal class Win32InternetConnection
{
	[Flags]
	private enum InternetGetConnectedStateFlags
	{
		INTERNET_CONNECTION_MODEM = 1,
		INTERNET_CONNECTION_LAN = 2,
		INTERNET_CONNECTION_PROXY = 4,
		INTERNET_CONNECTION_RAS_INSTALLED = 0x10,
		INTERNET_CONNECTION_OFFLINE = 0x20,
		INTERNET_CONNECTION_CONFIGURED = 0x40
	}

	public static bool IsConnected
	{
		get
		{
			InternetGetConnectedStateFlags Description;
			return InternetGetConnectedState(out Description, 0);
		}
	}

	[DllImport("wininet.dll", SetLastError = true)]
	private static extern bool InternetGetConnectedState(out InternetGetConnectedStateFlags Description, int ReservedValue);
}
