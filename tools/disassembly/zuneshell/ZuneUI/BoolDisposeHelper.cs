using Microsoft.Iris;

namespace ZuneUI;

public class BoolDisposeHelper : ModelItem
{
	private BooleanChoice _choice;

	private bool _resetOnDispose;

	public BooleanChoice Choice
	{
		get
		{
			return _choice;
		}
		set
		{
			if (_choice != value)
			{
				_choice = value;
				((ModelItem)this).FirePropertyChanged("Choice");
			}
		}
	}

	public bool ResetOnDispose
	{
		get
		{
			return _resetOnDispose;
		}
		set
		{
			if (_resetOnDispose != value)
			{
				_resetOnDispose = value;
				((ModelItem)this).FirePropertyChanged("ResetOnDispose");
			}
		}
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing && _choice != null && _resetOnDispose)
		{
			_choice.Value = false;
		}
		((ModelItem)this).OnDispose(disposing);
	}
}
