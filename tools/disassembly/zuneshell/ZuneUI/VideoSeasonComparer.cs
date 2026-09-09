using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class VideoSeasonComparer : IComparer
{
	public int Compare(object x, object y)
	{
		DataProviderObject val = (DataProviderObject)((x is DataProviderObject) ? x : null);
		DataProviderObject val2 = (DataProviderObject)((y is DataProviderObject) ? y : null);
		if (val != null && val2 != null)
		{
			int num = (int)val.GetProperty("CategoryId");
			int value = (int)val2.GetProperty("CategoryId");
			int num2 = num.CompareTo(value);
			if (num2 != 0)
			{
				return num2;
			}
			if (num != 5)
			{
				return 0;
			}
			string strA = (string)val.GetProperty("SeriesTitle");
			string strB = (string)val2.GetProperty("SeriesTitle");
			int num3 = string.Compare(strA, strB);
			if (num3 != 0)
			{
				return num3;
			}
			int num4 = (int)val.GetProperty("SeasonNumber");
			int value2 = (int)val2.GetProperty("SeasonNumber");
			return num4.CompareTo(value2);
		}
		return 1;
	}
}
