using System;

namespace ZuneUI;

public static class GuidHelper
{
	public static Guid Empty => Guid.Empty;

	public static Guid CreateFromString(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return Guid.Empty;
		}
		Guid result = Guid.Empty;
		try
		{
			result = new Guid(value);
		}
		catch (Exception)
		{
		}
		return result;
	}

	public static bool IsEmpty(Guid guid)
	{
		return guid == Guid.Empty;
	}
}
