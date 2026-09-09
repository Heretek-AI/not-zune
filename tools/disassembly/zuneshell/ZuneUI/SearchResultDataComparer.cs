using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class SearchResultDataComparer : IComparer
{
	private string GetStringOrDataProviderTitle(object value)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if (value is DataProviderObject)
		{
			return (string)((DataProviderObject)value).GetProperty("Title");
		}
		return value as string;
	}

	public int Compare(object x, object y)
	{
		string stringOrDataProviderTitle = GetStringOrDataProviderTitle(x);
		string stringOrDataProviderTitle2 = GetStringOrDataProviderTitle(y);
		if (stringOrDataProviderTitle != null && stringOrDataProviderTitle2 != null)
		{
			return stringOrDataProviderTitle.CompareTo(stringOrDataProviderTitle2);
		}
		return 1;
	}
}
