using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class OrderedGroupedList : SearchableGroupedList
{
	public OrderedGroupedList(IList source, IComparer comparer, int count)
		: base(null, comparer, count)
	{
		Reorder(source, count);
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (disposing && ((GroupedList)this).Source is ArrayListDataSet)
		{
			((ModelItem)(ArrayListDataSet)((GroupedList)this).Source).Dispose();
		}
		((GroupedList)this).OnDispose(disposing);
	}

	public void Reorder(IList source, int count)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		ArrayListDataSet val = null;
		if (source != null)
		{
			val = new ArrayListDataSet();
			((ListDataSet)val).CopyFrom((IEnumerable)source);
			int num;
			for (num = 0; num < ((ListDataSet)val).Count; num++)
			{
				int num2 = num;
				for (int i = num + 1; i < ((ListDataSet)val).Count; i++)
				{
					if (((GroupedList)this).Comparer.Compare(((ListDataSet)val)[num], ((ListDataSet)val)[i]) == 0)
					{
						num2++;
						if (num2 != i)
						{
							((ListDataSet)val).Move(i, num2);
						}
					}
				}
				num = num2;
			}
		}
		if (((GroupedList)this).Source is ArrayListDataSet)
		{
			((ModelItem)(ArrayListDataSet)((GroupedList)this).Source).Dispose();
		}
		((GroupedList)this).SetSource((IList)val, count);
	}
}
