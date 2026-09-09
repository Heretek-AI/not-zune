using System;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class PaymentInstrumentStepBase : RegionInfoStep
{
	private CreditCard _committedCreditCard;

	private Dictionary<int, PropertyDescriptor> _errorMappings;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#PaymentInstrumentStep";

	protected override PropertyDescriptor CountryDescriptor => PaymentInstrumentPropertyEditor.Country;

	protected override PropertyDescriptor LanguageDescriptor => PaymentInstrumentPropertyEditor.Language;

	protected override PropertyDescriptor StateDescriptor => PaymentInstrumentPropertyEditor.State;

	public CreditCard CommittedCreditCard
	{
		get
		{
			return _committedCreditCard;
		}
		protected set
		{
			if (_committedCreditCard != value)
			{
				_committedCreditCard = value;
				((ModelItem)this).FirePropertyChanged("CommittedCreditCard");
			}
		}
	}

	internal override Dictionary<int, PropertyDescriptor> ErrorPropertyMappings
	{
		get
		{
			if (_errorMappings == null)
			{
				_errorMappings = new Dictionary<int, PropertyDescriptor>(11);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_CREDITCARD_ADDRESS_CITY_INVALID)).Int, PaymentInstrumentPropertyEditor.City);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_CREDITCARD_ADDRESS_POSTALCODE_INVALID)).Int, PaymentInstrumentPropertyEditor.PostalCode);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_CREDITCARD_ADDRESS_STATE_INVALID)).Int, PaymentInstrumentPropertyEditor.State);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_CREDITCARD_ADDRESS_STREET1_INVALID)).Int, PaymentInstrumentPropertyEditor.Street1);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_CREDITCARD_PARENTPHONE_INVALID)).Int, PaymentInstrumentPropertyEditor.PhoneNumber);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_INVALID_ARG_PARENT_PHONE_INVALID)).Int, PaymentInstrumentPropertyEditor.PhoneNumber);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_CREDITCARD_ADD_FAILED)).Int, null);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_CREDITCARD_VALIDATE_FAILED)).Int, null);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_CREDITCARD_INVALID)).Int, null);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_CREDITCARD_ADDRESS_INVALID)).Int, null);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_LIVEACCOUNT_PAYMENT_INSTRUMENT_INVALID)).Int, null);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_LIVEACCOUNT_ADDRESS_INVALID)).Int, null);
			}
			return _errorMappings;
		}
	}

	public PaymentInstrumentStepBase(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner, state, parentAccount)
	{
		base.LoadLanguages = false;
		base.LoadStates = true;
		if (parentAccount)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_PARENT_AGE_HEADER);
		}
		else
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_BILLING_EDIT_CC_ADD_HEADER);
		}
		WizardPropertyEditor wizardPropertyEditor = new PaymentInstrumentPropertyEditor();
		Initialize(wizardPropertyEditor);
	}

	protected override void OnCountryChanged()
	{
		if (base.WizardPropertyEditor != null)
		{
			base.WizardPropertyEditor.SetPropertyState(PaymentInstrumentPropertyEditor.AccountHolderName, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(PaymentInstrumentPropertyEditor.Street1, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(PaymentInstrumentPropertyEditor.Street2, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(PaymentInstrumentPropertyEditor.City, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(PaymentInstrumentPropertyEditor.State, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(PaymentInstrumentPropertyEditor.PostalCode, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(PaymentInstrumentPropertyEditor.PhoneNumber, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(PaymentInstrumentPropertyEditor.PhoneExtension, base.SelectedCountry);
		}
	}

	protected CreditCard CreateCreditCard()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Expected O, but got Unknown
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		CreditCard val = new CreditCard();
		val.ParentCreditCard = base.ParentAccount;
		val.AccountNumber = GetUncommittedValue(PaymentInstrumentPropertyEditor.AccountNumber) as string;
		val.Address.City = GetUncommittedValue(PaymentInstrumentPropertyEditor.City) as string;
		val.Address.PostalCode = GetUncommittedValue(PaymentInstrumentPropertyEditor.PostalCode) as string;
		val.Address.State = GetUncommittedValue(PaymentInstrumentPropertyEditor.State) as string;
		val.Address.Street1 = GetUncommittedValue(PaymentInstrumentPropertyEditor.Street1) as string;
		val.Address.Street2 = GetUncommittedValue(PaymentInstrumentPropertyEditor.Street2) as string;
		val.CCVNumber = GetUncommittedValue(PaymentInstrumentPropertyEditor.CcvNumber) as string;
		CreditCardType? val2 = (CreditCardType?)GetUncommittedValue(PaymentInstrumentPropertyEditor.CardType);
		if (val2.HasValue && val2.HasValue)
		{
			val.CreditCardType = val2.Value;
		}
		val.Email = GetUncommittedValue(PaymentInstrumentPropertyEditor.Email) as string;
		DateTime? dateTime = (DateTime?)GetUncommittedValue(PaymentInstrumentPropertyEditor.ExpirationDate);
		if (dateTime.HasValue && dateTime.HasValue)
		{
			val.ExpirationDate = dateTime.Value;
		}
		val.AccountHolderName = GetUncommittedValue(PaymentInstrumentPropertyEditor.AccountHolderName) as string;
		val.Locale = base.SelectedLocale;
		val.PhoneNumber = GetUncommittedValue(PaymentInstrumentPropertyEditor.PhoneNumber) as string;
		val.PhoneExtension = GetUncommittedValue(PaymentInstrumentPropertyEditor.PhoneExtension) as string;
		return val;
	}
}
