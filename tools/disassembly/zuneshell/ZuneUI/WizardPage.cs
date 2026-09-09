using Microsoft.Iris;

namespace ZuneUI;

public abstract class WizardPage : ModelItem
{
	protected Wizard _owner;

	private string _breadcrumbTitle;

	private string _detailDescription;

	private string _loadStatus;

	protected bool _showValidationState;

	private string _statusMessage;

	private bool _showStatusMessageIcon;

	private bool _enableVerticalScrolling;

	private bool _canNavigateInto;

	public abstract string UI { get; }

	public virtual bool IsEnabled => !_owner.ErrorPageIsEnabled;

	public virtual bool IsValid => true;

	public virtual bool ShowNavigation => true;

	public virtual bool ShowClose => false;

	public virtual bool CanCancel => true;

	public string BreadcrumbTitle
	{
		get
		{
			return _breadcrumbTitle;
		}
		set
		{
			if (_breadcrumbTitle != value)
			{
				_breadcrumbTitle = value;
				((ModelItem)this).FirePropertyChanged("BreadcrumbTitle");
			}
		}
	}

	public string DetailDescription
	{
		get
		{
			return _detailDescription;
		}
		set
		{
			if (_detailDescription != value)
			{
				_detailDescription = value;
				((ModelItem)this).FirePropertyChanged("DetailDescription");
			}
		}
	}

	public string LoadStatus
	{
		get
		{
			return _loadStatus;
		}
		set
		{
			if (_loadStatus != value)
			{
				_loadStatus = value;
				((ModelItem)this).FirePropertyChanged("LoadStatus");
			}
		}
	}

	public bool ShowValidationState
	{
		get
		{
			return _showValidationState;
		}
		private set
		{
			if (_showValidationState != value)
			{
				_showValidationState = value;
				((ModelItem)this).FirePropertyChanged("ShowValidationState");
			}
		}
	}

	public string StatusMessage
	{
		get
		{
			return _statusMessage;
		}
		set
		{
			if (_statusMessage != value)
			{
				_statusMessage = value;
				((ModelItem)this).FirePropertyChanged("StatusMessage");
			}
		}
	}

	public virtual bool ShowPrivacyStatement => false;

	public bool CanShowStatusMessageIcon
	{
		get
		{
			return _showStatusMessageIcon;
		}
		set
		{
			if (_showStatusMessageIcon != value)
			{
				_showStatusMessageIcon = value;
				((ModelItem)this).FirePropertyChanged("CanShowStatusMessageIcon");
			}
		}
	}

	public bool CanNavigateInto
	{
		get
		{
			return _canNavigateInto;
		}
		set
		{
			if (_canNavigateInto != value)
			{
				_canNavigateInto = value;
				((ModelItem)this).FirePropertyChanged("CanNavigateInto");
			}
		}
	}

	public bool EnableVerticalScrolling
	{
		get
		{
			return _enableVerticalScrolling;
		}
		set
		{
			if (_enableVerticalScrolling != value)
			{
				_enableVerticalScrolling = value;
				((ModelItem)this).FirePropertyChanged("EnableVerticalScrolling");
			}
		}
	}

	protected WizardPage(Wizard owner)
	{
		_canNavigateInto = true;
		_showStatusMessageIcon = true;
		_owner = owner;
	}

	internal virtual void Activate()
	{
	}

	internal virtual void Deactivate()
	{
		ShowValidationState = false;
	}

	public void ShowValidation()
	{
		ShowValidationState = true;
		((ModelItem)this).FirePropertyChanged("ShowValidationState");
	}

	public virtual void RefreshValidationState()
	{
		ShowValidation();
	}

	public void ShowGenericErrorStatus()
	{
		if (string.IsNullOrEmpty(StatusMessage) && !IsValid)
		{
			StatusMessage = Shell.LoadString(StringId.IDS_WIZARD_GENERIC_ERROR);
		}
	}

	public bool CommitChanges()
	{
		return OnCommitChanges();
	}

	protected virtual bool OnCommitChanges()
	{
		return true;
	}

	internal virtual bool OnMovingNext()
	{
		return true;
	}

	internal virtual bool OnMovingBack()
	{
		return true;
	}
}
