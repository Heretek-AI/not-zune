using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class EditContactInfoStep : RegionInfoStep
{
	private AccountUser _accountUser;

	private bool _lightWeightOnly;

	private Dictionary<int, PropertyDescriptor> _errorMappings;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#ContactInfoStep";

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled;
			if (flag)
			{
				flag = (LightWeightOnly && SignIn.Instance.IsLightWeight) || !LightWeightOnly;
			}
			return flag;
		}
	}

	public bool LightWeightOnly
	{
		get
		{
			return _lightWeightOnly;
		}
		set
		{
			if (_lightWeightOnly != value)
			{
				_lightWeightOnly = value;
				((ModelItem)this).FirePropertyChanged("LightWeightOnly");
				((ModelItem)this).FirePropertyChanged("Enabled");
			}
		}
	}

	internal override Dictionary<int, PropertyDescriptor> ErrorPropertyMappings
	{
		get
		{
			if (_errorMappings == null)
			{
				_errorMappings = new Dictionary<int, PropertyDescriptor>(2);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_INVALID_POSTALCODE)).Int, ContactInfoPropertyEditor.PostalCode);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_LIVEACCOUNT_INVALIDPHONE)).Int, BaseContactInfoPropertyEditor.PhoneNumber);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZUNE_E_UPDATE_ACCOUNT_INFO_FAILED)).Int, null);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_LIVEACCOUNT_ADDRESS_INVALID)).Int, null);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_INVALID_ARG_CONTACT_INFO)).Int, null);
			}
			return _errorMappings;
		}
	}

	protected override PropertyDescriptor CountryDescriptor => ContactInfoPropertyEditor.Country;

	protected override PropertyDescriptor LanguageDescriptor => ContactInfoPropertyEditor.Language;

	protected override PropertyDescriptor StateDescriptor => ContactInfoPropertyEditor.State;

	public AccountUser AccountUser
	{
		get
		{
			return _accountUser;
		}
		private set
		{
			if (_accountUser != value)
			{
				_accountUser = value;
				SetCommittedValue(ContactInfoPropertyEditor.City, AccountUser.Address.City);
				SetCommittedValue(ContactInfoPropertyEditor.District, AccountUser.Address.District);
				SetCommittedValue(ContactInfoPropertyEditor.PostalCode, AccountUser.Address.PostalCode);
				SetCommittedValue(ContactInfoPropertyEditor.Street1, AccountUser.Address.Street1);
				SetCommittedValue(ContactInfoPropertyEditor.Street2, AccountUser.Address.Street2);
				SetCommittedValue(BaseContactInfoPropertyEditor.FirstName, AccountUser.FirstName);
				SetCommittedValue(BaseContactInfoPropertyEditor.LastName, AccountUser.LastName);
				SetCommittedValue(BaseContactInfoPropertyEditor.Email, AccountUser.Email);
				SetCommittedValue(BaseContactInfoPropertyEditor.PhoneNumber, AccountUser.PhoneNumber);
				base.SelectedLocale = AccountUser.Locale;
				base.SelectedState = AccountUser.Address.State;
				((ModelItem)this).FirePropertyChanged("AccountUser");
			}
		}
	}

	public EditContactInfoStep(Wizard owner, AccountManagementWizardState state)
		: base(owner, state, parentAccount: false)
	{
		base.LoadStates = true;
		base.LoadLanguages = true;
		_lightWeightOnly = false;
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_CONTACT_HEAD);
		base.DetailDescription = Shell.LoadString(StringId.IDS_ACCOUNT_EDIT_CONTACT_INFO_DETAIL);
		WizardPropertyEditor wizardPropertyEditor = new ContactInfoPropertyEditor();
		base.RequireSignIn = true;
		Initialize(wizardPropertyEditor);
	}

	protected override void OnCountryChanged()
	{
		if (base.WizardPropertyEditor != null)
		{
			base.WizardPropertyEditor.SetPropertyState(BaseContactInfoPropertyEditor.FirstName, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(BaseContactInfoPropertyEditor.LastName, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(ContactInfoPropertyEditor.Street1, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(ContactInfoPropertyEditor.Street2, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(ContactInfoPropertyEditor.City, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(ContactInfoPropertyEditor.District, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(ContactInfoPropertyEditor.State, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(ContactInfoPropertyEditor.PostalCode, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(BaseContactInfoPropertyEditor.PhoneNumber, base.SelectedCountry);
			base.WizardPropertyEditor.SetPropertyState(BaseContactInfoPropertyEditor.PhoneExtension, base.SelectedCountry);
		}
	}

	protected override bool OnCommitChanges()
	{
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		bool result = true;
		if (AccountUser != null)
		{
			ServiceError serviceError = null;
			AccountUser.Address.City = GetCommittedValue(ContactInfoPropertyEditor.City) as string;
			AccountUser.Address.District = GetCommittedValue(ContactInfoPropertyEditor.District) as string;
			AccountUser.Address.PostalCode = GetCommittedValue(ContactInfoPropertyEditor.PostalCode) as string;
			AccountUser.Address.Street1 = GetCommittedValue(ContactInfoPropertyEditor.Street1) as string;
			AccountUser.Address.Street2 = GetCommittedValue(ContactInfoPropertyEditor.Street2) as string;
			AccountUser.FirstName = GetCommittedValue(BaseContactInfoPropertyEditor.FirstName) as string;
			AccountUser.LastName = GetCommittedValue(BaseContactInfoPropertyEditor.LastName) as string;
			AccountUser.Email = GetCommittedValue(BaseContactInfoPropertyEditor.Email) as string;
			AccountUser.PhoneNumber = GetCommittedValue(BaseContactInfoPropertyEditor.PhoneNumber) as string;
			AccountUser.Address.State = base.SelectedState;
			AccountUser.AccountSettings = null;
			AccountUser.ParentPassportIdentity = base.State.PassportPasswordParentStep.PassportIdentity;
			HRESULT hr = base.State.AccountManagement.SetAccount((PassportIdentity)null, AccountUser, ref serviceError);
			if (((HRESULT)(ref hr)).IsSuccess)
			{
				SignIn.Instance.RefreshAccount();
			}
			else
			{
				result = false;
				SetError(hr, serviceError);
			}
		}
		return result;
	}

	protected override void OnActivate()
	{
		base.ServiceActivationRequestsDone = AccountUser != null;
		base.OnActivate();
	}

	protected override void OnStartActivationRequests(object state)
	{
		AccountUser val = ObtainAccountUser();
		RegionServiceData regionServiceData = (RegionServiceData)state;
		if (val != null && val.Locale != null && regionServiceData.SelectedCountry == null)
		{
			string[] array = null;
			array = val.Locale.Split(new char[1] { '-' });
			if (array.Length >= 2)
			{
				regionServiceData.SelectedCountry = array[1];
			}
		}
		base.OnStartActivationRequests(state);
		EndActivationRequests(val);
	}

	protected override void OnEndActivationRequests(object args)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		if (args == null)
		{
			NavigateToErrorHandler();
		}
		else if (args is AccountUser)
		{
			AccountUser = (AccountUser)args;
		}
		else
		{
			base.OnEndActivationRequests(args);
		}
	}

	private AccountUser ObtainAccountUser()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		AccountUser result = null;
		ServiceError serviceError = null;
		HRESULT account = base.State.AccountManagement.GetAccount((PassportIdentity)null, ref result, ref serviceError);
		if (((HRESULT)(ref account)).IsError)
		{
			result = null;
			SetError(account, serviceError);
		}
		return result;
	}
}
