using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class AccountManagementWizard : Wizard
{
	private bool _commitSucceeded;

	private bool _commitFailed;

	private bool _requiresSignIn = true;

	private AccountManagementWizardState _state;

	private AccountManagementErrorState _lastErrorState;

	public AccountManagementWizardState State
	{
		get
		{
			if (_state == null)
			{
				_state = new AccountManagementWizardState(this);
			}
			return _state;
		}
	}

	public override bool CanStart
	{
		get
		{
			if (RequiresSignIn)
			{
				return SignIn.Instance.SignedIn;
			}
			return true;
		}
	}

	public bool RequiresSignIn
	{
		get
		{
			return _requiresSignIn;
		}
		set
		{
			if (_requiresSignIn != value)
			{
				_requiresSignIn = value;
				((ModelItem)this).FirePropertyChanged("RequiresSignIn");
			}
		}
	}

	public bool CommitSucceeded
	{
		get
		{
			return _commitSucceeded;
		}
		private set
		{
			if (_commitSucceeded != value)
			{
				_commitSucceeded = value;
				((ModelItem)this).FirePropertyChanged("CommitSucceeded");
			}
		}
	}

	public bool CommitFailed
	{
		get
		{
			return _commitFailed;
		}
		private set
		{
			if (_commitFailed != value)
			{
				_commitFailed = value;
				((ModelItem)this).FirePropertyChanged("CommitFailed");
			}
		}
	}

	public AccountManagementErrorState LastErrorState
	{
		get
		{
			return _lastErrorState;
		}
		private set
		{
			if (_lastErrorState != value)
			{
				_lastErrorState = value;
				((ModelItem)this).FirePropertyChanged("LastServiceError");
			}
		}
	}

	protected AccountManagementWizard()
	{
	}

	public void NavigateToErrorHandler()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		NavigateToErrorHandler(base.Error, LastErrorState);
	}

	public void NavigateToErrorHandler(HRESULT hr, AccountManagementErrorState errorState)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		int num = -1;
		IList pages = base.Pages;
		int currentPageIndex = base.CurrentPageIndex;
		for (int i = 0; i < pages.Count; i++)
		{
			int num2 = (currentPageIndex + i) % pages.Count;
			if (num == -1 && pages[num2] is AccountManagementErrorPage)
			{
				num = num2;
			}
			else if (pages[num2] is AccountManagementStep && ((AccountManagementStep)pages[num2]).HandleError(hr, errorState))
			{
				num = num2;
				break;
			}
		}
		if (num >= 0)
		{
			base.CurrentPageIndex = num;
		}
	}

	protected override void OnAsyncCommitCompleted(bool success)
	{
		CommitSucceeded = success;
		CommitFailed = !success;
	}

	protected override void OnSetError(HRESULT hr, object state)
	{
		LastErrorState = state as AccountManagementErrorState;
	}
}
