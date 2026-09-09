namespace ZuneUI;

public class RedeemCodeFinishStep : AccountManagementFinishStep
{
	public RedeemCodeFinishStep(Wizard owner, AccountManagementWizardState state)
		: base(owner, state, Shell.LoadString(StringId.IDS_ACCOUNT_FINISHED_DESCRIPTION))
	{
	}

	protected override bool OnCommitChanges()
	{
		return base.State.RedeemCode();
	}
}
