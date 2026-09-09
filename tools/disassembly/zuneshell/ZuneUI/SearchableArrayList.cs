using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneUI;

public class SearchableArrayList : ArrayListDataSet, ISearchableList
{
	public int SearchForString(string str)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		int num = -1;
		if (((ListDataSet)this).Source is ISearchableList)
		{
			num = ((ISearchableList)((ListDataSet)this).Source).SearchForString(str);
		}
		else if (((ListDataSet)this).Source is ArrayList)
		{
			ArrayList arrayList = (ArrayList)((ListDataSet)this).Source;
			num = arrayList.BinarySearch(str, ToStringCaseInsensitiveComparer.Instance);
			if (num < 0)
			{
				num = ~num;
			}
		}
		else if (((ListDataSet)this).Source is Array)
		{
			Array array = (Array)((ListDataSet)this).Source;
			num = Array.BinarySearch(array, str, ToStringCaseInsensitiveComparer.Instance);
			if (num < 0)
			{
				num = ~num;
			}
		}
		else if (((ListDataSet)this).Source is List<string>)
		{
			List<string> list = (List<string>)((ListDataSet)this).Source;
			num = list.BinarySearch(str, StringCaseInsensitiveComparer.Instance);
			if (num < 0)
			{
				num = ~num;
			}
		}
		return num;
	}
}
