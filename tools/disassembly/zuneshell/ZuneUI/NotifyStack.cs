using Microsoft.Iris;

namespace ZuneUI;

public class NotifyStack : ArrayListDataSet
{
	public void Push(object value)
	{
		((ListDataSet)this).Insert(0, value);
	}

	public object Pop()
	{
		object result = Peek();
		((ListDataSet)this).RemoveAt(0);
		return result;
	}

	public object Peek()
	{
		return ((ListDataSet)this)[0];
	}
}
