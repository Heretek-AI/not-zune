using System;
using Microsoft.Iris;
using UIXControls;

namespace ZuneUI;

public class WizardHelper : DialogHelper, IWizardNavigation
{
	private Wizard _wizard;

	private Command _backCommand;

	private Command _nextCommand;

	private Command _finishCommand;

	public Wizard Wizard
	{
		get
		{
			return _wizard;
		}
		set
		{
			if (_wizard != value)
			{
				if (_wizard != null && _wizard.CancelCommandHandler != null)
				{
					((DialogHelper)this).Cancel.Invoked -= _wizard.CancelCommandHandler;
				}
				_wizard = value;
				((ModelItem)this).FirePropertyChanged("Wizard");
				if (_wizard.CancelCommandHandler != null)
				{
					((DialogHelper)this).Cancel.Invoked += _wizard.CancelCommandHandler;
				}
			}
		}
	}

	public Command Back => _backCommand;

	public Command Next => _nextCommand;

	public Command Finish => _finishCommand;

	public WizardHelper()
		: this(null)
	{
	}

	public WizardHelper(EventHandler cancelCommandHandler)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		_backCommand = new Command((IModelItemOwner)(object)this);
		((ModelItem)_backCommand).Description = Shell.LoadString(StringId.IDS_BACK_BUTTON);
		_nextCommand = new Command((IModelItemOwner)(object)this);
		((ModelItem)_nextCommand).Description = Shell.LoadString(StringId.IDS_NEXT_BUTTON);
		_finishCommand = new Command((IModelItemOwner)(object)this);
		((ModelItem)_finishCommand).Description = Shell.LoadString(StringId.IDS_FINISH_BUTTON);
		if (cancelCommandHandler != null)
		{
			((DialogHelper)this).Cancel.Invoked += cancelCommandHandler;
		}
	}

	public static void Show(EventHandler cancelCommandHandler)
	{
		WizardHelper wizardHelper = new WizardHelper(cancelCommandHandler);
		((DialogHelper)wizardHelper).Show();
	}
}
