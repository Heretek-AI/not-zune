using System;

namespace ZuneUI;

public class TermsOfServiceWizard : AccountManagementWizard
{
	private TermsOfServiceFinishStep _finishStep;

	private AccountManagementErrorPage _errorStep;

	public bool ChildAccount
	{
		get
		{
			return base.State.PassportPasswordParentStep.ForceEnable;
		}
		private set
		{
			base.State.PassportPasswordParentStep.ForceEnable = value;
		}
	}

	public string Username
	{
		get
		{
			return base.State.TermsOfServiceStep.Username;
		}
		private set
		{
			base.State.TermsOfServiceStep.Username = value;
		}
	}

	public string Password
	{
		get
		{
			return base.State.TermsOfServiceStep.Password;
		}
		private set
		{
			base.State.TermsOfServiceStep.Password = value;
		}
	}

	public TermsOfServiceWizard()
	{
		base.RequiresSignIn = false;
		base.State.PassportPasswordParentStep.DetailDescription = Shell.LoadString(StringId.IDS_ACCOUNT_TOS_STEP_PARENT_NEEDED);
		_finishStep = new TermsOfServiceFinishStep(this, base.State);
		_errorStep = new AccountManagementErrorPage(this, Shell.LoadString(StringId.IDS_ACCOUNT_TOS_ERROR_TITLE), Shell.LoadString(StringId.IDS_ACCOUNT_TOS_ERROR_DESC));
		SignIn.Instance.SignInStatusUpdatedEvent += OnSignInStatusUpdatedEvent;
	}

	protected override void OnDispose(bool disposing)
	{
		base.OnDispose(disposing);
		if (disposing)
		{
			SignIn.Instance.SignInStatusUpdatedEvent -= OnSignInStatusUpdatedEvent;
		}
	}

	public void Initialize(string username, string password, bool childAccount)
	{
		Username = username;
		Password = password;
		ChildAccount = childAccount;
		AddPage(base.State.PassportPasswordParentStep);
		AddPage(base.State.TermsOfServiceStep);
		AddPage(base.State.PrivacyInfoParentStep);
		AddPage(base.State.PrivacyInfoStep);
		AddPage(_finishStep);
		AddPage(_errorStep);
	}

	protected override void OnAsyncCommitCompleted(bool success)
	{
		if (success && !SignIn.Instance.SignedIn)
		{
			SignIn.Instance.SignInUser(Username, Password);
			_finishStep.LoadStatus = Shell.LoadString(StringId.IDS_LOGON_STATUS_BUTTON);
		}
		else
		{
			base.OnAsyncCommitCompleted(success);
		}
	}

	private void OnSignInStatusUpdatedEvent(object sender, EventArgs e)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		HRESULT signInError = SignIn.Instance.SignInError;
		if (((HRESULT)(ref signInError)).IsError || SignIn.Instance.SignedIn)
		{
			_finishStep.ClosingMessage = Shell.LoadString(StringId.IDS_ACCOUNT_TOS_ERROR_SUCCESS_DESC);
			HRESULT signInError2 = SignIn.Instance.SignInError;
			if (!((HRESULT)(ref signInError2)).IsError)
			{
				base.State.SaveFamilySettings();
			}
			base.OnAsyncCommitCompleted(true);
		}
	}
}
