namespace ZuneUI;

public class NewletterSettingsWizard : AccountManagementWizard
{
	private AccountManagementFinishStep _finishStep;

	private AccountManagementErrorPage _errorStep;

	private EditPrivacyInfoStep _editPrivacyInfoStep;

	public NewletterSettingsWizard()
	{
		_editPrivacyInfoStep = new EditPrivacyInfoStep(this, base.State, parentAccount: false, PrivacyInfoSettings.AllowMicrosoftCommunications);
		_finishStep = new AccountManagementFinishStep(this, base.State, Shell.LoadString(StringId.IDS_ACCOUNT_FINISHED_DESCRIPTION), base.State.PrivacyInfoStep.DetailDescription);
		_errorStep = new AccountManagementErrorPage(this, Shell.LoadString(StringId.IDS_ACCOUNT_NEWSLTR_UPDATE_ERROR_TITLE), Shell.LoadString(StringId.IDS_ACCOUNT_NEWSLTR_UPDATE_ERROR_DESC));
		AddPage(_editPrivacyInfoStep);
		AddPage(_finishStep);
		AddPage(_errorStep);
	}

	protected override void OnAsyncCommitCompleted(bool success)
	{
		base.OnAsyncCommitCompleted(success);
		_finishStep.ClosingMessage = Shell.LoadString(StringId.IDS_ACCOUNT_NEWSLTR_UPDATE_SUCCESS_DESC);
	}
}
