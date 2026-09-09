using System;

namespace ZuneXml;

public class AppTitleComparer : PropertyComparer<string>
{
	public AppTitleComparer()
		: base((Converter<object, string>)GetSortTitle, false)
	{
	}

	private static string GetSortTitle(object o)
	{
		if (!(o is App app))
		{
			return null;
		}
		return app.SortTitle;
	}
}
