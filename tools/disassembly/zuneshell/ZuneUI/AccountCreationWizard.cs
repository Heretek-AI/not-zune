using System;
using Microsoft.Iris;

namespace ZuneUI;

public class AccountCreationWizard : AccountManagementWizard
{
	private AccountCreationFinishStep _finishStep;

	private AccountManagementErrorPage _errorStep;

	private static bool _inProgress;

	public bool ShowNextSteps
	{
		get
		{
			return _finishStep.NextSteps != AccountCreationNextSteps.None;
		}
		set
		{
			AccountCreationNextSteps accountCreationNextSteps = AccountCreationNextSteps.EditTile;
			if (!value)
			{
				accountCreationNextSteps = AccountCreationNextSteps.None;
			}
			if (_finishStep.NextSteps != accountCreationNextSteps)
			{
				_finishStep.NextSteps = accountCreationNextSteps;
				((ModelItem)this).FirePropertyChanged("ShowNextSteps");
			}
		}
	}

	public bool HideOnComplete
	{
		get
		{
			return _finishStep.HideOnComplete;
		}
		set
		{
			if (_finishStep.HideOnComplete != value)
			{
				_finishStep.HideOnComplete = value;
				((ModelItem)this).FirePropertyChanged("HideOnComplete");
			}
		}
	}

	public static bool AccountCreationInProgress
	{
		get
		{
			return _inProgress;
		}
		private set
		{
			_inProgress = value;
		}
	}

	public static event EventHandler CreationCompleted;

	public AccountCreationWizard()
	{
		base.RequiresSignIn = false;
		AddPage(base.State.EmailSelectionStep);
		AddPage(base.State.PassportPasswordStep);
		AddPage(base.State.BasicAccountInfoStep);
		AddPage(base.State.CreatePassportStep);
		AddPage(base.State.HipPassportStep);
		AddPage(base.State.EmailSelectionParentStep);
		AddPage(base.State.PassportPasswordParentStep);
		AddPage(base.State.CreatePassportParentStep);
		AddPage(base.State.HipPassportParentStep);
		AddPage(base.State.ContactInfoParentStep);
		AddPage(base.State.PaymentInstrumentParentStep);
		AddPage(base.State.PrivacyInfoParentStep);
		AddPage(base.State.PrivacyInfoStep);
		AddPage(base.State.ZuneTagStep);
		_finishStep = new AccountCreationFinishStep(this, base.State);
		AddPage(_finishStep);
		_errorStep = new AccountManagementErrorPage(this, Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ERROR_TITLE), Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ERROR_DESC));
		AddPage(_errorStep);
	}

	public void InitializeDefaults(string defaultUsername, string defaultPassword, bool lockUsername)
	{
		if (!string.IsNullOrEmpty(defaultUsername) && !string.IsNullOrEmpty(defaultPassword))
		{
			base.State.EmailSelectionStep.PageLocked = lockUsername;
			base.State.EmailSelectionStep.SkipOnce = !lockUsername;
			base.State.EmailSelectionStep.HasEmail = true;
			base.State.EmailSelectionStep.Email = defaultUsername;
			base.State.PassportPasswordStep.SkipOnce = true;
			base.State.PassportPasswordStep.CommittedEmail = defaultUsername;
			base.State.PassportPasswordStep.CommittedPassword = defaultPassword;
			base.State.PassportPasswordStep.LockEmail = lockUsername;
		}
	}

	protected override bool OnStart()
	{
		base.CurrentPageIndex = -1;
		AccountCreationInProgress = true;
		return base.OnStart();
	}

	protected override void OnAsyncCommitCompleted(bool success)
	{
		base.OnAsyncCommitCompleted(success);
		if (base.CommitSucceeded)
		{
			if (base.State.PassportPasswordStep.IsZuneAccount)
			{
				_finishStep.ClosingMessage = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_SUCCESS_EXISTING);
			}
			else
			{
				_finishStep.ClosingMessage = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_SUCCESS_NEW);
			}
		}
	}

	public override void Cancel()
	{
		WizardClosed(isCancelled: true);
		base.Cancel();
	}

	public static void WizardClosed(bool isCancelled)
	{
		if (AccountCreationInProgress)
		{
			AccountCreationInProgress = false;
			if (!isCancelled && AccountCreationWizard.CreationCompleted != null)
			{
				AccountCreationWizard.CreationCompleted(null, null);
			}
		}
	}
}
