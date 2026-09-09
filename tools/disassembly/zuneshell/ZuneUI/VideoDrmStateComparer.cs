using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class VideoDrmStateComparer : IComparer
{
	public int Compare(object x, object y)
	{
		DataProviderObject val = (DataProviderObject)((x is DataProviderObject) ? x : null);
		DataProviderObject val2 = (DataProviderObject)((y is DataProviderObject) ? y : null);
		if (val != null && val2 != null)
		{
			int num = (int)val.GetProperty("DrmState");
			int num2 = (int)val2.GetProperty("DrmState");
			if (num == 20 || num == 23)
			{
				num = 26;
			}
			if (num2 == 20 || num2 == 23)
			{
				num2 = 26;
			}
			return num.CompareTo(num2);
		}
		return 1;
	}
}
