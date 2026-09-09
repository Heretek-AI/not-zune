using Microsoft.Iris;

namespace ZuneUI;

public class AccountManagementFinishStep : AccountManagementStep
{
	private string _closingMessage;

	private bool _hideOnComplete;

	private string _oldDescription;

	public override string UI => "res://ZuneShellResources!AccountCreation.uix#AccountManagementFinishStep";

	public string ClosingMessage
	{
		get
		{
			return _closingMessage;
		}
		set
		{
			if (_closingMessage != value)
			{
				_closingMessage = value;
				((ModelItem)this).FirePropertyChanged("ClosingMessage");
			}
		}
	}

	public bool HideOnComplete
	{
		get
		{
			return _hideOnComplete;
		}
		set
		{
			if (_hideOnComplete != value)
			{
				_hideOnComplete = value;
				((ModelItem)this).FirePropertyChanged("HideOnComplete");
				UpdateDescription();
			}
		}
	}

	internal bool CommittSucceeded
	{
		get
		{
			bool result = false;
			if (_owner is AccountManagementWizard)
			{
				result = ((AccountManagementWizard)_owner).CommitSucceeded;
			}
			return result;
		}
	}

	public AccountManagementFinishStep(Wizard owner, AccountManagementWizardState state)
		: this(owner, state, null)
	{
	}

	public AccountManagementFinishStep(Wizard owner, AccountManagementWizardState state, string description)
		: this(owner, state, description, null)
	{
		((ModelItem)this).Description = description;
	}

	public AccountManagementFinishStep(Wizard owner, AccountManagementWizardState state, string description, string detailDescription)
		: base(owner, state, parentAccount: false)
	{
		((ModelItem)this).Description = description;
		base.DetailDescription = detailDescription;
	}

	private void UpdateDescription()
	{
		if (HideOnComplete)
		{
			_oldDescription = ((ModelItem)this).Description;
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PLEASE_WAIT_TITLE);
		}
		else
		{
			((ModelItem)this).Description = _oldDescription;
		}
	}
}
