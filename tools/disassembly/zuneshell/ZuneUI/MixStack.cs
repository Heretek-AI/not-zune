using System.Collections;
using System.Collections.Generic;

namespace ZuneUI;

public class MixStack : Stack<MixStackEntry>
{
	public void Push(MixResult seedResult, object layout, IList dataList)
	{
		MixStackEntry item = new MixStackEntry(seedResult, layout, dataList);
		Push(item);
	}

	public static bool IsNullOrEmpty(MixStack mixStack)
	{
		if (mixStack != null)
		{
			return mixStack.Count == 0;
		}
		return true;
	}
}
