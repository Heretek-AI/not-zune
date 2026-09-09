using System;
using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public static class ProfileMerger
{
	public delegate object MergeObjects(object item1, object item2);

	public static IList Merge(IList profiles1, IList profiles2, bool matchesOnly)
	{
		DataProviderPropertyComparer dataProviderPropertyComparer = new DataProviderPropertyComparer();
		dataProviderPropertyComparer.PropertyName = "ZuneTag";
		return Merge(profiles1, profiles2, matchesOnly, dataProviderPropertyComparer, MergeProfile);
	}

	public static IList MergeWithFriends(IList profiles, IList friends, bool matchesOnly)
	{
		DataProviderPropertyComparer dataProviderPropertyComparer = new DataProviderPropertyComparer();
		dataProviderPropertyComparer.PropertyName = "ZuneTag";
		return Merge(profiles, friends, matchesOnly, dataProviderPropertyComparer, MergeWithFriend);
	}

	public static ProfileCardData MergeWithFriends(DataProviderObject profile, IList friends)
	{
		IList list = MergeWithFriends(new object[1] { profile }, friends, matchesOnly: false);
		return list[0] as ProfileCardData;
	}

	public static object MergeProfile(object item1, object item2)
	{
		return ProfileCardData.Create(item1, item2);
	}

	private static object MergeWithFriend(object profile, object friend)
	{
		ProfileCardData profileCardData = ProfileCardData.Create(profile, friend);
		if (friend != null)
		{
			profileCardData.IsFriend = true;
		}
		return profileCardData;
	}

	private static IList Merge(IList list1, IList list2, bool matchesOnly, IComparer comparer, MergeObjects mergeDelegate)
	{
		ArrayList arrayList;
		if (list1 != null && list2 != null)
		{
			arrayList = new ArrayList(Math.Min(list1.Count, list2.Count));
			Hashtable hashtable = new Hashtable();
			int num = 0;
			for (int i = 0; i < list1.Count; i++)
			{
				bool flag = false;
				for (int j = num; j < list2.Count; j++)
				{
					if (!hashtable.ContainsKey(j) && comparer.Compare(list1[i], list2[j]) == 0)
					{
						arrayList.Add(mergeDelegate(list1[i], list2[j]));
						hashtable.Add(j, true);
						if (num == j)
						{
							num++;
						}
						flag = true;
						break;
					}
				}
				if (!flag && !matchesOnly)
				{
					arrayList.Add(mergeDelegate(list1[i], null));
				}
			}
		}
		else if (!matchesOnly && list1 != null)
		{
			arrayList = new ArrayList(list1.Count);
			foreach (object item in list1)
			{
				arrayList.Add(mergeDelegate(item, null));
			}
		}
		else
		{
			arrayList = new ArrayList();
		}
		return arrayList;
	}
}
