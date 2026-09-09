using Microsoft.Iris;

namespace ZuneUI;

public class AddCreditCardToAccountWizard : AccountManagementWizard
{
	private AccountManagementFinishStep _finishStep;

	private AccountManagementErrorPage _errorStep;

	public bool HideOnComplete
	{
		get
		{
			return _finishStep.HideOnComplete;
		}
		set
		{
			if (_finishStep.HideOnComplete != value)
			{
				_finishStep.HideOnComplete = value;
				((ModelItem)this).FirePropertyChanged("HideOnComplete");
			}
		}
	}

	public AddCreditCardToAccountWizard()
	{
		base.State.ContactInfoStep.LightWeightOnly = true;
		_finishStep = new AccountManagementFinishStep(this, base.State, Shell.LoadString(StringId.IDS_ACCOUNT_FINISHED_DESCRIPTION));
		_errorStep = new AccountManagementErrorPage(this, Shell.LoadString(StringId.IDS_ACCOUNT_ADD_CC_TO_ACCOUNT_ERROR_TITLE), Shell.LoadString(StringId.IDS_ACCOUNT_ADD_CC_TO_ACCOUNT_ERROR_DESC));
		PaymentInstrumentStep paymentInstrumentStep = base.State.PaymentInstrumentStep;
		paymentInstrumentStep.NextTextOverride = Shell.LoadString(StringId.IDS_OK_BUTTON);
		paymentInstrumentStep.DetailDescription = Shell.LoadString(StringId.IDS_BILLING_ADD_CC_TO_ACCOUNT_DESC);
		AddPage(base.State.ContactInfoStep);
		AddPage(base.State.ListAndAddPaymentInstrumentStep);
		AddPage(paymentInstrumentStep);
		AddPage(_finishStep);
		AddPage(_errorStep);
	}

	protected override void OnAsyncCommitCompleted(bool success)
	{
		base.OnAsyncCommitCompleted(success);
		if (success)
		{
			_finishStep.ClosingMessage = Shell.LoadString(StringId.IDS_ACCOUNT_ADD_CC_TO_ACCOUNT_SUCCESS_DESC);
		}
	}
}
