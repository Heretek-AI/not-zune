using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class SelectCreditCardWizard : AccountManagementWizard
{
	private AccountManagementFinishStep _finishStep;

	private AccountManagementErrorPage _errorStep;

	private CreditCard _selectedCreditCard;

	public CreditCard SelectedCreditCard
	{
		get
		{
			return _selectedCreditCard;
		}
		private set
		{
			if (_selectedCreditCard != value)
			{
				_selectedCreditCard = value;
				((ModelItem)this).FirePropertyChanged("SelectedCreditCard");
			}
		}
	}

	public SelectCreditCardWizard()
	{
		base.State.ContactInfoStep.LightWeightOnly = true;
		_finishStep = new SelectCreditCardFinishStep(this, base.State, Shell.LoadString(StringId.IDS_ACCOUNT_FINISHED_DESCRIPTION));
		_errorStep = new AccountManagementErrorPage(this, Shell.LoadString(StringId.IDS_ACCOUNT_ADD_CC_TO_ACCOUNT_ERROR_TITLE), Shell.LoadString(StringId.IDS_ACCOUNT_ADD_CC_TO_ACCOUNT_ERROR_DESC));
		_finishStep.FinishTextOverride = Shell.LoadString(StringId.IDS_PURCHASE_BUTTON);
		PaymentInstrumentStep paymentInstrumentStep = base.State.PaymentInstrumentStep;
		paymentInstrumentStep.NextTextOverride = Shell.LoadString(StringId.IDS_OK_BUTTON);
		AddPage(base.State.ContactInfoStep);
		AddPage(base.State.SelectPaymentInstrumentStep);
		AddPage(paymentInstrumentStep);
		AddPage(_finishStep);
		AddPage(_errorStep);
	}

	protected override void OnAsyncCommitCompleted(bool success)
	{
		base.OnAsyncCommitCompleted(success);
		if (success)
		{
			if (base.State.PaymentInstrumentStep.CommittedCreditCard != null)
			{
				SelectedCreditCard = base.State.PaymentInstrumentStep.CommittedCreditCard;
				_finishStep.ClosingMessage = Shell.LoadString(StringId.IDS_PURCHASE_PAYMENT_METHOD_ADDED);
			}
			else if (base.State.SelectPaymentInstrumentStep.CommittedCreditCard != null)
			{
				SelectedCreditCard = base.State.SelectPaymentInstrumentStep.CommittedCreditCard;
				_finishStep.ClosingMessage = string.Format(Shell.LoadString(StringId.IDS_PURCHASE_SELECTED_PAYMENT_METHOD), ((object)base.State.SelectPaymentInstrumentStep.CommittedCreditCard).ToString());
			}
		}
	}
}
