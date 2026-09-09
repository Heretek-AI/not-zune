using System.Collections;

namespace ZuneUI;

public static class ListHelper
{
	public delegate object MergeObjects(object item1, object item2);

	public static IList Merge(IList list1, IList list2, bool matchesOnly, IComparer comparer, MergeObjects mergeDelegate)
	{
		ArrayList arrayList = new ArrayList();
		if (list1 != null && list2 != null)
		{
			for (int i = 0; i < list1.Count; i++)
			{
				bool flag = false;
				for (int j = 0; j < list2.Count; j++)
				{
					if (comparer.Compare(list1[i], list2[j]) == 0)
					{
						arrayList.Add(mergeDelegate(list1[i], list2[j]));
						list2.RemoveAt(j);
						flag = true;
						break;
					}
				}
				if (!flag && !matchesOnly)
				{
					arrayList.Add(mergeDelegate(list1[i], null));
				}
			}
			if (!matchesOnly)
			{
				foreach (object item in list2)
				{
					arrayList.Add(mergeDelegate(item, null));
				}
			}
		}
		else if (!matchesOnly && (list1 != null || list2 != null))
		{
			IList list3 = ((list1 != null) ? list1 : list2);
			foreach (object item2 in list3)
			{
				arrayList.Add(mergeDelegate(item2, null));
			}
		}
		return arrayList;
	}
}
