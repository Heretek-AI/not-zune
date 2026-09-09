using System;

namespace ZuneUI;

public static class TimeSpanHelper
{
	public static TimeSpan Empty => TimeSpan.Zero;

	public static bool IsEmpty(TimeSpan span)
	{
		return span == TimeSpan.Zero;
	}
}
