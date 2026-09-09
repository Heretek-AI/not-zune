namespace ZuneUI;

public class ContactInformationWizard : AccountManagementWizard
{
	private AccountManagementFinishStep _finishStep;

	private AccountManagementErrorPage _errorStep;

	public ContactInformationWizard()
	{
		_finishStep = new AccountManagementFinishStep(this, base.State, Shell.LoadString(StringId.IDS_ACCOUNT_FINISHED_DESCRIPTION), base.State.ContactInfoStep.DetailDescription);
		_errorStep = new AccountManagementErrorPage(this, Shell.LoadString(StringId.IDS_ACCOUNT_CONTACT_INFO_ERROR_TITLE), Shell.LoadString(StringId.IDS_ACCOUNT_CONTACT_INFO_ERROR_DESC));
		AddPage(base.State.ContactInfoStep);
		AddPage(_finishStep);
		AddPage(_errorStep);
	}

	protected override void OnAsyncCommitCompleted(bool success)
	{
		base.OnAsyncCommitCompleted(success);
		_finishStep.ClosingMessage = Shell.LoadString(StringId.IDS_ACCOUNT_CONTACT_INFO_SUCCESS_DESC);
	}
}
