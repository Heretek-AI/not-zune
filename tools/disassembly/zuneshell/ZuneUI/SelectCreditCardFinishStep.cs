namespace ZuneUI;

public class SelectCreditCardFinishStep : AccountManagementFinishStep
{
	public override string UI => "res://ZuneShellResources!AccountCreation.uix#SelectCreditCardFinishStep";

	public SelectCreditCardFinishStep(Wizard owner, AccountManagementWizardState state, string description)
		: base(owner, state, description, null)
	{
	}
}
