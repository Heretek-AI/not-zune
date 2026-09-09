using Microsoft.Zune.Service;

namespace ZuneUI;

public class PurchaseBillingOfferWizard : AccountManagementWizard
{
	private AccountManagementFinishStep _finishStep;

	private AccountManagementErrorPage _errorStep;

	public PurchaseBillingOfferWizard(EBillingOfferType offerTypes)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		base.State.ContactInfoStep.LightWeightOnly = true;
		base.State.SelectBillingOfferStep.ShowOffers = offerTypes;
		base.State.ConfirmationStep.OfferType = offerTypes;
		_finishStep = new PurchaseBillingOfferFinishStep(this, base.State);
		_errorStep = new AccountManagementErrorPage(this, Shell.LoadString(StringId.IDS_ACCOUNT_PURCHASE_ERROR_TITLE), Shell.LoadString(StringId.IDS_ACCOUNT_PURCHASE_ERROR_DESC));
		AddPage(base.State.SelectBillingOfferStep);
		AddPage(base.State.ContactInfoStep);
		AddPage(base.State.SelectPaymentInstrumentStep);
		AddPage(base.State.PaymentInstrumentStep);
		AddPage(base.State.ConfirmationStep);
		AddPage(_finishStep);
		AddPage(_errorStep);
	}

	protected override void OnAsyncCommitCompleted(bool success)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		base.OnAsyncCommitCompleted(success);
		if (!success)
		{
			return;
		}
		BillingOffer selectedBillingOffer = base.State.SelectBillingOfferStep.SelectedBillingOffer;
		if (selectedBillingOffer != null)
		{
			if ((int)selectedBillingOffer.OfferType == 8)
			{
				_finishStep.ClosingMessage = string.Format(Shell.LoadString(StringId.IDS_BILLING_BUY_POINTS_SUCCESS), selectedBillingOffer.Points);
			}
			else
			{
				_finishStep.ClosingMessage = string.Format(Shell.LoadString(StringId.IDS_BILLING_BUY_PASS_SUCCESS), selectedBillingOffer.OfferName);
			}
		}
		else
		{
			_finishStep.ClosingMessage = Shell.LoadString(StringId.IDS_ACCOUNT_PURCHASE_SUCCESS_DESC);
		}
	}
}
