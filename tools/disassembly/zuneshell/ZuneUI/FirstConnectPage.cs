using Microsoft.Iris;

namespace ZuneUI;

public abstract class FirstConnectPage : WizardPage
{
	private bool _isEnabled = true;

	private bool _isPageComplete;

	public virtual bool IsPageComplete
	{
		get
		{
			return _isPageComplete;
		}
		set
		{
			if (_isPageComplete != value)
			{
				_isPageComplete = value;
				((ModelItem)this).FirePropertyChanged("IsPageComplete");
			}
		}
	}

	public override bool IsEnabled => _isEnabled;

	internal FirstConnectPage(Wizard wizard)
		: base(wizard)
	{
	}

	internal override bool OnMovingNext()
	{
		IsPageComplete = true;
		return base.OnMovingNext();
	}

	internal override bool OnMovingBack()
	{
		IsPageComplete = false;
		return base.OnMovingBack();
	}
}
