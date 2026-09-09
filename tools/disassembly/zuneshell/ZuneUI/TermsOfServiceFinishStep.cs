namespace ZuneUI;

public class TermsOfServiceFinishStep : AccountManagementFinishStep
{
	public TermsOfServiceFinishStep(Wizard owner, AccountManagementWizardState state)
		: base(owner, state, Shell.LoadString(StringId.IDS_ACCOUNT_FINISHED_DESCRIPTION))
	{
	}

	protected override bool OnCommitChanges()
	{
		return base.State.AcceptTermsOfService();
	}
}
