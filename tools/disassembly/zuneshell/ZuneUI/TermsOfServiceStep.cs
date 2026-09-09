using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class TermsOfServiceStep : AccountManagementStep
{
	private class TermsOfServiceData
	{
		public string Username;

		public string Password;

		public string TermsOfService;

		public PassportIdentity PassportIdentity;

		public AccountUser AccountUser;
	}

	private string _termsOfService;

	private string _termsOfServiceUrl;

	private string _privacyUrl;

	private string _username;

	private string _password;

	private PassportIdentity _passportIdentity;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#TermsOfServiceStep";

	public string TermsOfService
	{
		get
		{
			return _termsOfService;
		}
		private set
		{
			if (_termsOfService != value)
			{
				_termsOfService = value;
				((ModelItem)this).FirePropertyChanged("TermsOfService");
			}
		}
	}

	public string TermsOfServiceUrl
	{
		get
		{
			return _termsOfServiceUrl;
		}
		private set
		{
			if (_termsOfServiceUrl != value)
			{
				_termsOfServiceUrl = value;
				((ModelItem)this).FirePropertyChanged("TermsOfServiceUrl");
			}
		}
	}

	public string PrivacyUrl
	{
		get
		{
			return _privacyUrl;
		}
		private set
		{
			if (_privacyUrl != value)
			{
				_privacyUrl = value;
				((ModelItem)this).FirePropertyChanged("PrivacyUrl");
			}
		}
	}

	public string Username
	{
		get
		{
			return _username;
		}
		set
		{
			if (_username != value)
			{
				_username = value;
				((ModelItem)this).FirePropertyChanged("Username");
			}
		}
	}

	public string Password
	{
		get
		{
			return _password;
		}
		set
		{
			if (value == null || value == SignIn.Instance.PseudoPassword)
			{
				value = string.Empty;
			}
			if (_password != value)
			{
				_password = value;
				((ModelItem)this).FirePropertyChanged("Password");
			}
		}
	}

	public PassportIdentity PassportIdentity
	{
		get
		{
			return _passportIdentity;
		}
		private set
		{
			if (_passportIdentity != value)
			{
				_passportIdentity = value;
				((ModelItem)this).FirePropertyChanged("PassportIdentity");
			}
		}
	}

	public TermsOfServiceStep(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner, state, parentAccount)
	{
		base.NextTextOverride = Shell.LoadString(StringId.IDS_I_ACCEPT_BUTTON);
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_TOS_STEP_TITLE);
		Initialize(null);
	}

	internal static string GetTermsOfServiceUrl(string locale)
	{
		string uri = Shell.LoadString(StringId.IDS_TERMS_OF_SERVICE_URL);
		return CultureHelper.AppendFwlinkCulture(uri, locale);
	}

	internal static string GetPrivacyUrl(string locale)
	{
		string uri = Shell.LoadString(StringId.IDS_PRIVACY_STATEMENT_URL);
		return CultureHelper.AppendFwlinkCulture(uri, locale);
	}

	protected override void OnActivate()
	{
		base.ServiceActivationRequestsDone = TermsOfService != null && PassportIdentity != null;
		if (!base.ServiceActivationRequestsDone)
		{
			TermsOfServiceData termsOfServiceData = new TermsOfServiceData();
			termsOfServiceData.Username = Username;
			termsOfServiceData.Password = Password;
			StartActivationRequests(termsOfServiceData);
		}
	}

	protected override void OnStartActivationRequests(object state)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hr = HRESULT._S_OK;
		TermsOfServiceData termsOfServiceData = (TermsOfServiceData)state;
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			hr = AccountManagementHelper.GetPassportIdentity(termsOfServiceData.Username, termsOfServiceData.Password, out termsOfServiceData.PassportIdentity);
		}
		ServiceError serviceError = null;
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			hr = base.State.AccountManagement.GetAccount(termsOfServiceData.PassportIdentity, ref termsOfServiceData.AccountUser, ref serviceError);
		}
		string language = null;
		string country = null;
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			RegionInfoStep.GetLanguageAndCountry(termsOfServiceData.AccountUser.Locale, out language, out country);
			if (string.IsNullOrEmpty(country))
			{
				hr = HRESULT._E_FAIL;
			}
		}
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			hr = base.State.AccountManagement.GetTermsOfService(language, country, ref termsOfServiceData.TermsOfService);
		}
		if (((HRESULT)(ref hr)).IsError)
		{
			SetError(hr, serviceError);
		}
		EndActivationRequests(termsOfServiceData);
	}

	protected override void OnEndActivationRequests(object args)
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Invalid comparison between Unknown and I4
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Invalid comparison between Unknown and I4
		TermsOfServiceData termsOfServiceData = (TermsOfServiceData)args;
		TermsOfService = termsOfServiceData.TermsOfService;
		PassportIdentity = termsOfServiceData.PassportIdentity;
		if (termsOfServiceData.AccountUser == null)
		{
			return;
		}
		TermsOfServiceUrl = GetTermsOfServiceUrl(termsOfServiceData.AccountUser.Locale);
		PrivacyUrl = GetPrivacyUrl(termsOfServiceData.AccountUser.Locale);
		base.State.PrivacyInfoParentStep.CommittedSettings = termsOfServiceData.AccountUser.AccountSettings;
		base.State.PrivacyInfoStep.CommittedSettings = termsOfServiceData.AccountUser.AccountSettings;
		base.State.SetPrivacySettings(termsOfServiceData.AccountUser.AccountUserType);
		if (((int)termsOfServiceData.AccountUser.AccountUserType != 2 && (int)termsOfServiceData.AccountUser.AccountUserType != 1) || base.State.PassportPasswordParentStep.IsEnabled || _owner.Pages == null)
		{
			return;
		}
		base.State.PassportPasswordParentStep.ForceEnable = true;
		int num = -1;
		foreach (object page in _owner.Pages)
		{
			num++;
			if (page == base.State.PassportPasswordParentStep)
			{
				break;
			}
		}
		if (num < _owner.CurrentPageIndex && num < _owner.Pages.Count)
		{
			_owner.CurrentPageIndex = num;
		}
	}
}
