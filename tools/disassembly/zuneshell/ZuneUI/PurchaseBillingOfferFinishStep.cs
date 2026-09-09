using Microsoft.Zune.Service;

namespace ZuneUI;

public class PurchaseBillingOfferFinishStep : AccountManagementFinishStep
{
	public PurchaseBillingOfferFinishStep(Wizard owner, AccountManagementWizardState state)
		: base(owner, state, Shell.LoadString(StringId.IDS_ACCOUNT_FINISHED_DESCRIPTION))
	{
		base.RequireSignIn = true;
	}

	protected override bool OnCommitChanges()
	{
		PaymentInstrument paymentInstrument = (PaymentInstrument)(object)((base.State.SelectPaymentInstrumentStep.CommittedCreditCard != null) ? base.State.SelectPaymentInstrumentStep.CommittedCreditCard : base.State.PaymentInstrumentStep.CommittedCreditCard);
		return base.State.PurchaseBillingOffer(base.State.SelectBillingOfferStep.SelectedBillingOffer, paymentInstrument);
	}
}
