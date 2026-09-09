namespace ZuneUI;

public static class DrmStateMask
{
	private static readonly long _drmStateMaskAll = 1100653528065L;

	private static readonly long _drmStateMaskProtected = 1073742848L;

	private static readonly long _drmStateMaskZunePass = 68157440L;

	private static readonly long _drmStateMaskPersonal = 1099511627776L;

	private static readonly long _drmStateMaskUnknown = 1L;

	public static bool Match(long drmStateMask1, long drmStateMask2)
	{
		if (drmStateMask1 == 0)
		{
			drmStateMask1 = _drmStateMaskUnknown;
		}
		if (drmStateMask2 == 0)
		{
			drmStateMask2 = _drmStateMaskUnknown;
		}
		return (drmStateMask1 & drmStateMask2) != 0;
	}

	public static bool IsMixed(long drmStateMask)
	{
		int num = 0;
		if ((drmStateMask & _drmStateMaskPersonal) != 0)
		{
			num++;
		}
		if ((drmStateMask & _drmStateMaskProtected) != 0)
		{
			num++;
		}
		if ((drmStateMask & _drmStateMaskZunePass) != 0)
		{
			num++;
		}
		return num > 1;
	}

	public static long Combine(long drmStateMask1, long drmStateMask2)
	{
		return drmStateMask1 | drmStateMask2;
	}

	public static long Diff(long drmStateMask1, long drmStateMask2)
	{
		return drmStateMask1 ^ (drmStateMask1 & drmStateMask2);
	}

	public static long All()
	{
		return _drmStateMaskAll;
	}

	public static long Unknown()
	{
		return _drmStateMaskUnknown;
	}

	public static long Personal()
	{
		return _drmStateMaskPersonal;
	}

	public static long Protected()
	{
		return _drmStateMaskProtected;
	}

	public static long ZunePass()
	{
		return _drmStateMaskZunePass;
	}
}
