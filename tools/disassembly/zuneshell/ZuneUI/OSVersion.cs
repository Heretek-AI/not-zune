using System;

namespace ZuneUI;

public class OSVersion
{
	public static bool IsXP()
	{
		return Environment.OSVersion.Version.Major == 5;
	}

	public static bool IsVista()
	{
		if (Environment.OSVersion.Version.Major == 6)
		{
			return Environment.OSVersion.Version.Minor == 0;
		}
		return false;
	}

	public static bool IsWin7()
	{
		if (Environment.OSVersion.Version.Major <= 6)
		{
			if (Environment.OSVersion.Version.Major == 6)
			{
				return Environment.OSVersion.Version.Minor >= 1;
			}
			return false;
		}
		return true;
	}
}
