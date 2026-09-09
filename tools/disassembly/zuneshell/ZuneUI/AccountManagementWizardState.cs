using System;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class AccountManagementWizardState
{
	private AccountManagementWizard _wizard;

	private BasicAccountInfoStep _basicAccountInfoStep;

	private EditContactInfoStep _contactInfoStep;

	private ContactInfoParentStep _contactInfoParentStep;

	private CreatePassportStep _createPassportStep;

	private CreatePassportStep _createPassportParentStep;

	private EmailSelectionStep _emailSelectionStep;

	private EmailSelectionStep _emailSelectionParentStep;

	private PassportPasswordStep _passportPasswordStep;

	private PassportPasswordStep _passportPasswordParentStep;

	private PaymentInstrumentStep _paymentInstrumentStep;

	private ParentPaymentIntrumentStep _paymentInstrumentParentStep;

	private HipPassportStep _hipPassportStep;

	private HipPassportStep _hipPassportParentStep;

	private PrivacyInfoStep _privacyInfoStep;

	private PrivacyInfoStep _privacyInfoParentStep;

	private TermsOfServiceStep _termsOfServiceStep;

	private ZuneTagStep _zuneTagStep;

	private SelectBillingOfferStep _selectBillingOfferStep;

	private SelectPaymentInstrumentStep _selectPaymentInstrumentStep;

	private ConfirmationStep _confirmationStep;

	private ListAndAddPaymentInstrumentStep _listAndAddPaymentInstrumentStep;

	private RedeemCodeStep _redeemCodeStep;

	private WinLiveSignup _winLiveSignup;

	private AccountManagement _accountManagment;

	public BasicAccountInfoStep BasicAccountInfoStep
	{
		get
		{
			if (_basicAccountInfoStep == null)
			{
				_basicAccountInfoStep = new BasicAccountInfoStep(_wizard, this, parentAccount: false);
			}
			return _basicAccountInfoStep;
		}
	}

	public EditContactInfoStep ContactInfoStep
	{
		get
		{
			if (_contactInfoStep == null)
			{
				_contactInfoStep = new EditContactInfoStep(_wizard, this);
			}
			return _contactInfoStep;
		}
	}

	public ContactInfoParentStep ContactInfoParentStep
	{
		get
		{
			if (_contactInfoParentStep == null)
			{
				_contactInfoParentStep = new ContactInfoParentStep(_wizard, this);
			}
			return _contactInfoParentStep;
		}
	}

	public CreatePassportStep CreatePassportStep
	{
		get
		{
			if (_createPassportStep == null)
			{
				_createPassportStep = new CreatePassportStep(_wizard, this, parentAccount: false);
			}
			return _createPassportStep;
		}
	}

	public CreatePassportStep CreatePassportParentStep
	{
		get
		{
			if (_createPassportParentStep == null)
			{
				_createPassportParentStep = new CreatePassportStep(_wizard, this, parentAccount: true);
			}
			return _createPassportParentStep;
		}
	}

	public EmailSelectionStep EmailSelectionStep
	{
		get
		{
			if (_emailSelectionStep == null)
			{
				_emailSelectionStep = new EmailSelectionStep(_wizard, this, parentAccount: false);
			}
			return _emailSelectionStep;
		}
	}

	public EmailSelectionStep EmailSelectionParentStep
	{
		get
		{
			if (_emailSelectionParentStep == null)
			{
				_emailSelectionParentStep = new EmailSelectionStep(_wizard, this, parentAccount: true);
			}
			return _emailSelectionParentStep;
		}
	}

	public HipPassportStep HipPassportStep
	{
		get
		{
			if (_hipPassportStep == null)
			{
				_hipPassportStep = new HipPassportStep(_wizard, this, parentAccount: false);
			}
			return _hipPassportStep;
		}
	}

	public HipPassportStep HipPassportParentStep
	{
		get
		{
			if (_hipPassportParentStep == null)
			{
				_hipPassportParentStep = new HipPassportStep(_wizard, this, parentAccount: true);
			}
			return _hipPassportParentStep;
		}
	}

	public PassportPasswordStep PassportPasswordStep
	{
		get
		{
			if (_passportPasswordStep == null)
			{
				_passportPasswordStep = new PassportPasswordStep(_wizard, this, parentAccount: false);
			}
			return _passportPasswordStep;
		}
	}

	public PassportPasswordStep PassportPasswordParentStep
	{
		get
		{
			if (_passportPasswordParentStep == null)
			{
				_passportPasswordParentStep = new PassportPasswordStep(_wizard, this, parentAccount: true);
			}
			return _passportPasswordParentStep;
		}
	}

	public PaymentInstrumentStep PaymentInstrumentStep
	{
		get
		{
			if (_paymentInstrumentStep == null)
			{
				_paymentInstrumentStep = new PaymentInstrumentStep(_wizard, this, parent: false);
			}
			return _paymentInstrumentStep;
		}
	}

	public ParentPaymentIntrumentStep PaymentInstrumentParentStep
	{
		get
		{
			if (_paymentInstrumentParentStep == null)
			{
				_paymentInstrumentParentStep = new ParentPaymentIntrumentStep(_wizard, this);
			}
			return _paymentInstrumentParentStep;
		}
	}

	public PrivacyInfoStep PrivacyInfoStep
	{
		get
		{
			if (_privacyInfoStep == null)
			{
				_privacyInfoStep = new PrivacyInfoStep(_wizard, this, parentAccount: false, PrivacyInfoSettings.None);
			}
			return _privacyInfoStep;
		}
	}

	public PrivacyInfoStep PrivacyInfoParentStep
	{
		get
		{
			if (_privacyInfoParentStep == null)
			{
				_privacyInfoParentStep = new PrivacyInfoStep(_wizard, this, parentAccount: true, PrivacyInfoSettings.None);
			}
			return _privacyInfoParentStep;
		}
	}

	public TermsOfServiceStep TermsOfServiceStep
	{
		get
		{
			if (_termsOfServiceStep == null)
			{
				_termsOfServiceStep = new TermsOfServiceStep(_wizard, this, parentAccount: false);
			}
			return _termsOfServiceStep;
		}
	}

	public ZuneTagStep ZuneTagStep
	{
		get
		{
			if (_zuneTagStep == null)
			{
				_zuneTagStep = new ZuneTagStep(_wizard, this, parentAccount: false);
			}
			return _zuneTagStep;
		}
	}

	public SelectBillingOfferStep SelectBillingOfferStep
	{
		get
		{
			if (_selectBillingOfferStep == null)
			{
				_selectBillingOfferStep = new SelectBillingOfferStep(_wizard, this);
			}
			return _selectBillingOfferStep;
		}
	}

	public SelectPaymentInstrumentStep SelectPaymentInstrumentStep
	{
		get
		{
			if (_selectPaymentInstrumentStep == null)
			{
				_selectPaymentInstrumentStep = new SelectPaymentInstrumentStep(_wizard, this, parent: false);
			}
			return _selectPaymentInstrumentStep;
		}
	}

	public ConfirmationStep ConfirmationStep
	{
		get
		{
			if (_confirmationStep == null)
			{
				_confirmationStep = new ConfirmationStep(_wizard, this, parent: false);
			}
			return _confirmationStep;
		}
	}

	public ListAndAddPaymentInstrumentStep ListAndAddPaymentInstrumentStep
	{
		get
		{
			if (_listAndAddPaymentInstrumentStep == null)
			{
				_listAndAddPaymentInstrumentStep = new ListAndAddPaymentInstrumentStep(_wizard, this, parent: false);
			}
			return _listAndAddPaymentInstrumentStep;
		}
	}

	public RedeemCodeStep RedeemCodeStep
	{
		get
		{
			if (_redeemCodeStep == null)
			{
				_redeemCodeStep = new RedeemCodeStep(_wizard, this);
			}
			return _redeemCodeStep;
		}
	}

	public WinLiveSignup WinLiveSignup
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			if (_winLiveSignup == null)
			{
				_winLiveSignup = new WinLiveSignup();
			}
			return _winLiveSignup;
		}
	}

	public AccountManagement AccountManagement
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			if (_accountManagment == null)
			{
				_accountManagment = new AccountManagement();
			}
			return _accountManagment;
		}
	}

	public bool IsPurchaseConfirmationNeeded => FeatureEnablement.IsFeatureEnabled((Features)15);

	public AccountManagementWizardState(AccountManagementWizard wizard)
	{
		_wizard = wizard;
	}

	internal void SetPrivacySettings(AccountUserType type)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Invalid comparison between Unknown and I4
		if ((int)type == 0)
		{
			PrivacyInfoStep.ShowSettings = (FeatureEnablement.IsFeatureEnabled((Features)5) ? PrivacyInfoSettings.CreateNewAccountWithSocial : PrivacyInfoSettings.CreateNewAccount);
			PrivacyInfoParentStep.ShowSettings = PrivacyInfoSettings.None;
			return;
		}
		PrivacyInfoStep.ShowSettings = PrivacyInfoSettings.AllowMicrosoftCommunications;
		if ((int)type == 1)
		{
			PrivacyInfoParentStep.ShowSettings = (FeatureEnablement.IsFeatureEnabled((Features)5) ? PrivacyInfoSettings.CreateChildAccountWithSocial : PrivacyInfoSettings.CreateChildAccount);
		}
		else
		{
			PrivacyInfoParentStep.ShowSettings = PrivacyInfoSettings.CreateChildAccount;
		}
	}

	internal bool AcceptTermsOfService()
	{
		return UpgradeZuneAccount(includeAccountSettings: true, TermsOfServiceStep.PassportIdentity);
	}

	internal bool UpgradeZuneAccount(bool includeAccountSettings)
	{
		PassportIdentity passportIdentity = GetPassportIdentity();
		return UpgradeZuneAccount(includeAccountSettings, passportIdentity);
	}

	public void SaveFamilySettings()
	{
		if (SignIn.Instance.SignedIn && PrivacyInfoParentStep.IsEnabled && PrivacyInfoParentStep.FamilySettings != null)
		{
			PrivacyInfoParentStep.FamilySettings.UserId = SignIn.Instance.LastSignedInUserId;
			PrivacyInfoParentStep.FamilySettings.CommitSettings();
			SignIn.Instance.FamilySettings.ReloadSettings();
		}
	}

	internal bool UpgradeZuneAccount(bool includeAccountSettings, PassportIdentity passportIdentity)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		bool flag = true;
		PassportIdentity parentPassportIdentity = GetParentPassportIdentity();
		AccountSettings val = null;
		if (includeAccountSettings)
		{
			if (PrivacyInfoParentStep.IsEnabled)
			{
				val = PrivacyInfoParentStep.CommittedSettings;
				val.AllowPartnerEmails = PrivacyInfoStep.CommittedSettings.AllowPartnerEmails;
				val.AllowZuneEmails = PrivacyInfoStep.CommittedSettings.AllowZuneEmails;
			}
			else
			{
				val = PrivacyInfoStep.CommittedSettings;
			}
		}
		ServiceError serviceError = null;
		HRESULT hr = AccountManagement.UpgradeAccount(passportIdentity, val, parentPassportIdentity, ref serviceError);
		flag = ((HRESULT)(ref hr)).IsSuccess;
		if (!flag)
		{
			_wizard.SetError(hr, new AccountManagementErrorState(parentAccount: false, serviceError));
		}
		return flag;
	}

	internal string GetEmailAddress()
	{
		string empty = string.Empty;
		if (CreatePassportStep.IsEnabled || CreatePassportStep.CreatedPassport)
		{
			return CreatePassportStep.Email;
		}
		return PassportPasswordStep.CommittedEmail;
	}

	internal bool CreateZuneAccount()
	{
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Expected O, but got Unknown
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		bool flag = true;
		PassportIdentity passportIdentity = GetPassportIdentity();
		string text = ZuneTagStep.GetCommittedValue(ZuneTagPropertyEditor.ZuneTag) as string;
		string emailAddress = GetEmailAddress();
		string selectedLocale = BasicAccountInfoStep.SelectedLocale;
		DateTime? dateTime = (DateTime?)BasicAccountInfoStep.GetCommittedValue(BasicAccountInfoPropertyEditor.Birthday);
		PassportIdentity parentPassportIdentity = GetParentPassportIdentity();
		AccountSettings val = null;
		CreditCard val2 = null;
		if (BasicAccountInfoStep.IsParentAccountNeeded || PassportPasswordStep.IsParentAccountNeeded)
		{
			val = PrivacyInfoParentStep.CommittedSettings;
			val.AllowPartnerEmails = PrivacyInfoStep.CommittedSettings != null && PrivacyInfoStep.CommittedSettings.AllowPartnerEmails;
			val.AllowZuneEmails = PrivacyInfoStep.CommittedSettings != null && PrivacyInfoStep.CommittedSettings.AllowZuneEmails;
			val2 = PaymentInstrumentParentStep.CommittedCreditCard;
		}
		else
		{
			val = PrivacyInfoStep.CommittedSettings;
		}
		Address val3 = new Address();
		val3.PostalCode = BasicAccountInfoStep.GetCommittedValue(BasicAccountInfoPropertyEditor.PostalCode) as string;
		ServiceError serviceError = null;
		HRESULT hr = AccountManagement.CreateAccount(passportIdentity, text, selectedLocale, dateTime.Value, string.Empty, string.Empty, emailAddress, val3, val, parentPassportIdentity, val2, ref serviceError);
		flag = ((HRESULT)(ref hr)).IsSuccess;
		if (!flag)
		{
			_wizard.SetError(hr, new AccountManagementErrorState(parentAccount: false, serviceError));
		}
		return flag;
	}

	internal bool PurchaseBillingOffer(BillingOffer billingOffer, PaymentInstrument paymentInstrument)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Invalid comparison between Unknown and I4
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Invalid comparison between Unknown and I4
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Invalid comparison between Unknown and I4
		HRESULT hr = HRESULT.op_Implicit(Service.Instance.PurchaseBillingOffer(billingOffer, paymentInstrument));
		if (((HRESULT)(ref hr)).IsError)
		{
			_wizard.SetError(hr, null);
		}
		else if ((int)billingOffer.OfferType == 1 || (int)billingOffer.OfferType == 4096 || (int)billingOffer.OfferType == 4)
		{
			SignIn.Instance.RefreshAccount();
		}
		return ((HRESULT)(ref hr)).IsSuccess;
	}

	internal bool RedeemCode()
	{
		return PurchaseBillingOffer(RedeemCodeStep.MatchingBillingOffer, (PaymentInstrument)(object)RedeemCodeStep.TokenDetails);
	}

	private PassportIdentity GetPassportIdentity()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		PassportIdentity passportIdentity = null;
		if (PassportPasswordStep.IsEnabled)
		{
			passportIdentity = PassportPasswordStep.PassportIdentity;
		}
		else
		{
			string email = CreatePassportStep.Email;
			string password = CreatePassportStep.GetCommittedValue(CreatePassportPropertyEditor.Password1) as string;
			AccountManagementHelper.GetPassportIdentity(email, password, out passportIdentity);
		}
		return passportIdentity;
	}

	private PassportIdentity GetParentPassportIdentity()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		PassportIdentity passportIdentity = null;
		if (PassportPasswordParentStep.IsEnabled)
		{
			passportIdentity = PassportPasswordParentStep.PassportIdentity;
		}
		else if (CreatePassportParentStep.CreatedPassport)
		{
			string email = CreatePassportParentStep.Email;
			string password = CreatePassportParentStep.GetCommittedValue(CreatePassportPropertyEditor.Password1) as string;
			AccountManagementHelper.GetPassportIdentity(email, password, out passportIdentity);
		}
		return passportIdentity;
	}

	public void SignInNewUser()
	{
		string emailAddress = GetEmailAddress();
		string text = PassportPasswordStep.GetCommittedValue(PassportPasswordPropertyEditor.Password) as string;
		if (string.IsNullOrEmpty(text))
		{
			text = CreatePassportStep.GetCommittedValue(CreatePassportPropertyEditor.Password1) as string;
		}
		SignIn.Instance.SignOut();
		SignIn.Instance.SignInUser(emailAddress, text, fRememberUsername: true, fRememberPassword: false, fSignInAtStartup: false);
	}
}
