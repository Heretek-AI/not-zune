using System;

namespace ZuneXml;

public class AppReleaseDateComparer : PropertyComparer<DateTime>
{
	public AppReleaseDateComparer()
		: base((Converter<object, DateTime>)GetReleaseDate, true)
	{
	}

	private static DateTime GetReleaseDate(object o)
	{
		if (!(o is App app))
		{
			return DateTime.MinValue;
		}
		return app.ReleaseDate;
	}
}
