using Microsoft.Zune.Service;

namespace ZuneUI;

public class ParentPaymentIntrumentStep : PaymentInstrumentStepBase
{
	private struct ServiceData
	{
		public PassportIdentity PassportIdentity;

		public CreditCard CreditCard;
	}

	public override bool IsEnabled
	{
		get
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Invalid comparison between Unknown and I4
			bool flag = base.IsEnabled;
			if (flag)
			{
				flag = (int)base.State.BasicAccountInfoStep.NewAccounType == 2;
			}
			return flag;
		}
	}

	public ParentPaymentIntrumentStep(Wizard owner, AccountManagementWizardState state)
		: base(owner, state, parentAccount: true)
	{
	}

	protected override void OnActivate()
	{
		base.SelectedCountry = base.State.BasicAccountInfoStep.SelectedCountry;
		SetCommittedValue(PaymentInstrumentPropertyEditor.Country, base.SelectedCountry);
		SetCommittedValue(PaymentInstrumentPropertyEditor.Language, base.State.BasicAccountInfoStep.SelectedLanguage);
		SetCommittedValue(PaymentInstrumentPropertyEditor.Email, base.State.ContactInfoParentStep.GetCommittedValue(BaseContactInfoPropertyEditor.Email));
		base.OnActivate();
		base.ServiceDeactivationRequestsDone = false;
		MetadataEditProperty property = base.WizardPropertyEditor.GetProperty(PaymentInstrumentPropertyEditor.AccountHolderName);
		MetadataEditProperty property2 = base.WizardPropertyEditor.GetProperty(PaymentInstrumentPropertyEditor.PhoneNumber);
		MetadataEditProperty property3 = base.WizardPropertyEditor.GetProperty(PaymentInstrumentPropertyEditor.PhoneExtension);
		MetadataEditProperty property4 = base.WizardPropertyEditor.GetProperty(PaymentInstrumentPropertyEditor.PostalCode);
		if (string.IsNullOrEmpty(property.Value))
		{
			string arg = base.State.ContactInfoParentStep.GetCommittedValue(BaseContactInfoPropertyEditor.FirstName) as string;
			string arg2 = base.State.ContactInfoParentStep.GetCommittedValue(BaseContactInfoPropertyEditor.LastName) as string;
			string format = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_NAME_FORMAT);
			property.Value = string.Format(format, arg, arg2);
		}
		property4.State = base.SelectedCountry;
		if (string.IsNullOrEmpty(property4.Value))
		{
			property4.Value = base.State.BasicAccountInfoStep.GetCommittedValue(BasicAccountInfoPropertyEditor.PostalCode) as string;
		}
		if (string.IsNullOrEmpty(property2.Value))
		{
			property2.Value = base.State.ContactInfoParentStep.GetCommittedValue(BaseContactInfoPropertyEditor.PhoneNumber) as string;
		}
		if (string.IsNullOrEmpty(property3.Value))
		{
			property3.Value = base.State.ContactInfoParentStep.GetCommittedValue(BaseContactInfoPropertyEditor.PhoneExtension) as string;
		}
	}

	internal override bool OnMovingNext()
	{
		if (base.ServiceDeactivationRequestsDone)
		{
			return base.OnMovingNext();
		}
		ServiceData serviceData = default(ServiceData);
		serviceData.PassportIdentity = base.State.PassportPasswordParentStep.PassportIdentity;
		serviceData.CreditCard = CreateCreditCard();
		serviceData.CreditCard.ContactFirstName = base.State.ContactInfoParentStep.GetCommittedValue(BaseContactInfoPropertyEditor.FirstName) as string;
		serviceData.CreditCard.ContactLastName = base.State.ContactInfoParentStep.GetCommittedValue(BaseContactInfoPropertyEditor.LastName) as string;
		StartDeactivationRequests(serviceData);
		return false;
	}

	protected override void OnStartDeactivationRequests(object state)
	{
		ServiceData serviceData = (ServiceData)state;
		CreditCard args = null;
		if (IsValidCreditCard(serviceData))
		{
			args = serviceData.CreditCard;
		}
		EndDeactivationRequests(args);
	}

	protected override void OnEndDeactivationRequests(object args)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		CreditCard committedCreditCard = (CreditCard)args;
		base.CommittedCreditCard = committedCreditCard;
	}

	private bool IsValidCreditCard(ServiceData serviceData)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		bool flag = true;
		if (serviceData.PassportIdentity != null)
		{
			ServiceError serviceError = default(ServiceError);
			HRESULT hr = base.State.AccountManagement.ValidateCreditCard(serviceData.PassportIdentity, serviceData.CreditCard, ref serviceError);
			flag = ((HRESULT)(ref hr)).IsSuccess;
			if (!flag)
			{
				SetError(hr, serviceError);
			}
		}
		return flag;
	}
}
