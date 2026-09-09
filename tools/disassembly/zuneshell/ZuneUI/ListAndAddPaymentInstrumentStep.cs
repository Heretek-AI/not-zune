namespace ZuneUI;

public class ListAndAddPaymentInstrumentStep : SelectPaymentInstrumentStep
{
	public override string UI => "res://ZuneShellResources!AccountInfo.uix#ListAndAddPaymentInstrumentStep";

	public ListAndAddPaymentInstrumentStep(Wizard owner, AccountManagementWizardState state, bool parent)
		: base(owner, state, parent)
	{
		base.NextTextOverride = Shell.LoadString(StringId.IDS_BILLING_EDIT_CC_ADD_BTN);
	}

	protected override void ResetNextTextOverride()
	{
	}
}
