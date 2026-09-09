using Microsoft.Iris;

namespace ZuneUI;

public class Breadcrumb : ModelItem
{
	private WizardPage _page;

	private bool _active;

	private bool _complete;

	public WizardPage Page => _page;

	public bool Active
	{
		get
		{
			return _active;
		}
		set
		{
			if (_active != value)
			{
				_active = value;
				((ModelItem)this).FirePropertyChanged("Active");
			}
		}
	}

	public bool Complete
	{
		get
		{
			return _complete;
		}
		set
		{
			if (_complete != value)
			{
				_complete = value;
				((ModelItem)this).FirePropertyChanged("Complete");
			}
		}
	}

	public Breadcrumb(WizardPage page)
	{
		_page = page;
	}
}
