using System.Runtime.InteropServices;

namespace ZuneUI;

public static class OSInfo
{
	private const uint SPI_GETKEYBOARDSPEED = 10u;

	private const uint SPI_GETKEYBOARDDELAY = 22u;

	private static int s_defaultKeyDelay = GetDefaultKeyDelay();

	private static int s_defaultKeyRepeat = GetDefaultKeyRepeat();

	public static int DefaultKeyDelay => s_defaultKeyDelay;

	public static int DefaultKeyRepeat => s_defaultKeyRepeat;

	private static int GetDefaultKeyDelay()
	{
		if (!SystemParametersInfo(22u, 0u, out var pParam, 0))
		{
			pParam = 1;
		}
		return (pParam + 1) * 250;
	}

	private static int GetDefaultKeyRepeat()
	{
		if (!SystemParametersInfo(10u, 0u, out var pParam, 0))
		{
			pParam = 1;
		}
		return 31000 / (62 + 28 * pParam);
	}

	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool SystemParametersInfo(uint uiAction, uint uiParam, out int pParam, int nWinIni);
}
