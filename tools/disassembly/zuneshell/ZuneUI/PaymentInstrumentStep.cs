using Microsoft.Zune.Service;

namespace ZuneUI;

public class PaymentInstrumentStep : PaymentInstrumentStepBase
{
	private bool _firstView;

	public override bool IsEnabled
	{
		get
		{
			if (base.IsEnabled)
			{
				return base.State.SelectPaymentInstrumentStep.CommittedCreditCard == null;
			}
			return false;
		}
	}

	public PaymentInstrumentStep(Wizard owner, AccountManagementWizardState state, bool parent)
		: base(owner, state, parent)
	{
		_firstView = true;
		base.RequireSignIn = true;
	}

	protected override bool OnCommitChanges()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		bool flag = !IsEnabled;
		if (!flag && base.CommittedCreditCard != null)
		{
			string id = null;
			ServiceError serviceError = default(ServiceError);
			HRESULT hr = HRESULT.op_Implicit(Service.Instance.AddPaymentInstrument((PaymentInstrument)(object)base.CommittedCreditCard, ref id, ref serviceError));
			if (((HRESULT)(ref hr)).IsError)
			{
				SetError(hr, serviceError);
			}
			else
			{
				((PaymentInstrument)base.CommittedCreditCard).Id = id;
				flag = true;
			}
		}
		return flag;
	}

	protected override void OnActivate()
	{
		UpdateButtonText();
		if (base.State.ContactInfoStep.IsEnabled && _firstView)
		{
			_firstView = false;
			string arg = base.State.ContactInfoStep.GetCommittedValue(BaseContactInfoPropertyEditor.FirstName) as string;
			string arg2 = base.State.ContactInfoStep.GetCommittedValue(BaseContactInfoPropertyEditor.LastName) as string;
			string format = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_NAME_FORMAT);
			string value = string.Format(format, arg, arg2);
			SetCommittedValue(PaymentInstrumentPropertyEditor.AccountHolderName, value);
			SetCommittedValue(PaymentInstrumentPropertyEditor.City, base.State.ContactInfoStep.GetCommittedValue(ContactInfoPropertyEditor.City));
			SetCommittedValue(PaymentInstrumentPropertyEditor.Country, base.State.ContactInfoStep.GetCommittedValue(ContactInfoPropertyEditor.Country));
			SetCommittedValue(PaymentInstrumentPropertyEditor.District, base.State.ContactInfoStep.GetCommittedValue(ContactInfoPropertyEditor.District));
			SetCommittedValue(PaymentInstrumentPropertyEditor.PhoneExtension, base.State.ContactInfoStep.GetCommittedValue(BaseContactInfoPropertyEditor.PhoneExtension));
			SetCommittedValue(PaymentInstrumentPropertyEditor.PhoneNumber, base.State.ContactInfoStep.GetCommittedValue(BaseContactInfoPropertyEditor.PhoneNumber));
			SetPropertyState(PaymentInstrumentPropertyEditor.PostalCode, base.State.ContactInfoStep.GetCommittedValue(ContactInfoPropertyEditor.Country));
			SetCommittedValue(PaymentInstrumentPropertyEditor.PostalCode, base.State.ContactInfoStep.GetCommittedValue(ContactInfoPropertyEditor.PostalCode));
			SetPropertyState(PaymentInstrumentPropertyEditor.State, base.State.ContactInfoStep.GetCommittedValue(ContactInfoPropertyEditor.Country));
			SetCommittedValue(PaymentInstrumentPropertyEditor.State, base.State.ContactInfoStep.GetCommittedValue(ContactInfoPropertyEditor.State));
			SetCommittedValue(PaymentInstrumentPropertyEditor.Street1, base.State.ContactInfoStep.GetCommittedValue(ContactInfoPropertyEditor.Street1));
			SetCommittedValue(PaymentInstrumentPropertyEditor.Street2, base.State.ContactInfoStep.GetCommittedValue(ContactInfoPropertyEditor.Street2));
		}
		base.OnActivate();
	}

	internal override bool OnMovingNext()
	{
		base.CommittedCreditCard = CreateCreditCard();
		if (base.State.ContactInfoStep.IsEnabled)
		{
			base.CommittedCreditCard.ContactFirstName = base.State.ContactInfoStep.GetCommittedValue(BaseContactInfoPropertyEditor.FirstName) as string;
			base.CommittedCreditCard.ContactLastName = base.State.ContactInfoStep.GetCommittedValue(BaseContactInfoPropertyEditor.LastName) as string;
			base.CommittedCreditCard.Email = base.State.ContactInfoStep.GetCommittedValue(BaseContactInfoPropertyEditor.Email) as string;
		}
		return base.OnMovingNext();
	}

	private void UpdateButtonText()
	{
		if (base.State.SelectBillingOfferStep.SubscriptionsOnly && !base.State.IsPurchaseConfirmationNeeded)
		{
			base.NextTextOverride = Shell.LoadString(StringId.IDS_BILLING_SIGN_UP);
		}
		else if (base.State.SelectBillingOfferStep.PointsOffersOnly)
		{
			base.NextTextOverride = Shell.LoadString(StringId.IDS_BILLING_BUY_BTN);
		}
	}
}
