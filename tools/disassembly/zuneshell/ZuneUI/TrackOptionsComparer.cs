using System;
using System.Collections;

namespace ZuneUI;

public class TrackOptionsComparer : IComparer
{
	public int Compare(object x, object y)
	{
		TrackOptionGroupItem trackOptionGroupItem = x as TrackOptionGroupItem;
		TrackOptionGroupItem trackOptionGroupItem2 = y as TrackOptionGroupItem;
		if (trackOptionGroupItem != null && trackOptionGroupItem2 != null)
		{
			return ((IComparable)trackOptionGroupItem.Original).CompareTo((object?)trackOptionGroupItem2.Original);
		}
		return 1;
	}
}
