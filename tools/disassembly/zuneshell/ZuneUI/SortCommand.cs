using Microsoft.Iris;

namespace ZuneUI;

public class SortCommand : Command
{
	private string _sort;

	private bool _supportsJumpInList;

	public string Sort
	{
		get
		{
			return _sort;
		}
		set
		{
			if (_sort != value)
			{
				_sort = value;
				((ModelItem)this).FirePropertyChanged("Sort");
			}
		}
	}

	public bool SupportsJumpInList
	{
		get
		{
			return _supportsJumpInList;
		}
		set
		{
			if (_supportsJumpInList != value)
			{
				_supportsJumpInList = value;
				((ModelItem)this).FirePropertyChanged("SupportsJumpInList");
			}
		}
	}

	public SortCommand()
		: this(null, null, supportsJumpInList: false)
	{
	}

	public SortCommand(string description, string sort, bool supportsJumpInList)
	{
		((ModelItem)this).Description = description;
		Sort = sort;
		SupportsJumpInList = supportsJumpInList;
	}
}
