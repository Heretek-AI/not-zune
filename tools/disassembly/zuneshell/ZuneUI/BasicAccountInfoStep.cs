using System;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class BasicAccountInfoStep : RegionInfoStep
{
	public class WorkerThreadData
	{
		public DateTime Birthday;

		public string SelectedCountry;
	}

	private AccountUserType _newAccountType;

	private string _termsOfService;

	private string _termsOfServiceUrl;

	private string _privacyUrl;

	private Dictionary<int, PropertyDescriptor> _errorMappings;

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled;
			if (flag)
			{
				flag = _owner.CurrentPage != this || TermsOfService != null;
				flag &= base.State.CreatePassportStep.IsEnabled || base.State.PassportPasswordStep.CanCreateAccount || base.State.PassportPasswordStep.IsUpgradeNeeded;
			}
			return flag;
		}
	}

	protected override PropertyDescriptor CountryDescriptor => BasicAccountInfoPropertyEditor.Country;

	protected override PropertyDescriptor LanguageDescriptor => BasicAccountInfoPropertyEditor.Language;

	public bool IsParentAccountNeeded => (int)_newAccountType != 0;

	public AccountUserType NewAccounType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _newAccountType;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (_newAccountType != value)
			{
				_newAccountType = value;
				((ModelItem)this).FirePropertyChanged("NewAccounType");
				((ModelItem)this).FirePropertyChanged("IsParentAccountNeeded");
			}
		}
	}

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
				((ModelItem)this).FirePropertyChanged("IsEnabled");
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

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#BasicAccountInfoStep";

	internal override Dictionary<int, PropertyDescriptor> ErrorPropertyMappings
	{
		get
		{
			if (_errorMappings == null)
			{
				_errorMappings = new Dictionary<int, PropertyDescriptor>(4);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_BIRTH_YEAR_INVALID)).Int, BasicAccountInfoPropertyEditor.Birthday);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_BIRTH_DAY_INVALID)).Int, BasicAccountInfoPropertyEditor.Birthday);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_BIRTH_MONTH_INVALID)).Int, BasicAccountInfoPropertyEditor.Birthday);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_BIRTH_DATE_FUTURE)).Int, BasicAccountInfoPropertyEditor.Birthday);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZEST_E_INVALID_ARG_LANGUAGE)).Int, BasicAccountInfoPropertyEditor.Language);
			}
			return _errorMappings;
		}
	}

	public BasicAccountInfoStep(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner, state, parentAccount)
	{
		base.EnableVerticalScrolling = false;
		base.LoadLanguages = true;
		base.NextTextOverride = Shell.LoadString(StringId.IDS_I_ACCEPT_BUTTON);
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ACCOUNT_INFO_STEP);
		BasicAccountInfoPropertyEditor wizardPropertyEditor = new BasicAccountInfoPropertyEditor();
		Initialize(wizardPropertyEditor);
	}

	protected override void OnCountryChanged()
	{
		SetPropertyState(BasicAccountInfoPropertyEditor.PostalCode, base.SelectedCountry);
		SetPropertyState(BasicAccountInfoPropertyEditor.Birthday, base.SelectedLocale);
		TermsOfService = null;
		TermsOfServiceUrl = TermsOfServiceStep.GetTermsOfServiceUrl(base.SelectedLocale);
		PrivacyUrl = TermsOfServiceStep.GetPrivacyUrl(base.SelectedLocale);
		SetUncommittedValue(BasicAccountInfoPropertyEditor.Birthday, GetCommittedValue(BasicAccountInfoPropertyEditor.Birthday));
		if (base.ServiceActivationRequestsDone)
		{
			Activate();
		}
	}

	protected override void OnLanguageChanged()
	{
		SetPropertyState(BasicAccountInfoPropertyEditor.Birthday, base.SelectedLocale);
		TermsOfService = null;
		TermsOfServiceUrl = TermsOfServiceStep.GetTermsOfServiceUrl(base.SelectedLocale);
		PrivacyUrl = TermsOfServiceStep.GetPrivacyUrl(base.SelectedLocale);
		SetUncommittedValue(BasicAccountInfoPropertyEditor.Birthday, GetCommittedValue(BasicAccountInfoPropertyEditor.Birthday));
		if (base.ServiceActivationRequestsDone)
		{
			Activate();
		}
	}

	protected override void OnActivate()
	{
		base.ServiceDeactivationRequestsDone = false;
		SetUncommittedValue(BasicAccountInfoPropertyEditor.Birthday, GetCommittedValue(BasicAccountInfoPropertyEditor.Birthday));
		base.ServiceActivationRequestsDone = TermsOfService != null;
		base.OnActivate();
	}

	internal override bool OnMovingNext()
	{
		if (base.ServiceDeactivationRequestsDone)
		{
			return base.OnMovingNext();
		}
		DateTime? dateTime = (DateTime?)GetUncommittedValue(BasicAccountInfoPropertyEditor.Birthday);
		WorkerThreadData workerThreadData = new WorkerThreadData();
		workerThreadData.Birthday = dateTime.Value;
		workerThreadData.SelectedCountry = base.SelectedCountry;
		StartDeactivationRequests(workerThreadData);
		return false;
	}

	internal override ErrorMapperResult GetMappedErrorDescriptionAndUrl(HRESULT hr)
	{
		return ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hr)).Int, (eErrorCondition)4);
	}

	protected override void OnStartActivationRequests(object state)
	{
		base.OnStartActivationRequests(state);
		if (state is RegionServiceData regionServiceData && !string.IsNullOrEmpty(regionServiceData.SelectedCountry))
		{
			string args = ObtainTermsOfService(regionServiceData.SelectedLanguage, regionServiceData.SelectedCountry);
			EndActivationRequests(args);
		}
	}

	protected override void OnEndActivationRequests(object args)
	{
		if (args == null)
		{
			NavigateToErrorHandler();
		}
		else if (args is string)
		{
			TermsOfService = (string)args;
		}
		else
		{
			base.OnEndActivationRequests(args);
		}
	}

	protected override void OnStartDeactivationRequests(object state)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		WorkerThreadData data = (WorkerThreadData)state;
		AccountUserType val = ObtainNewAccountType(data);
		EndDeactivationRequests(val);
	}

	protected override void OnEndDeactivationRequests(object args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		NewAccounType = (AccountUserType)args;
		base.State.SetPrivacySettings(NewAccounType);
	}

	private AccountUserType ObtainNewAccountType(WorkerThreadData data)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		AccountUserType result = (AccountUserType)(-1);
		AccountCountry country = AccountCountryList.Instance.GetCountry(data.SelectedCountry);
		if (country != null)
		{
			int num = ObtainAge(data.Birthday);
			result = ((num >= ((CountryBaseDetails)country).AdultAge) ? ((AccountUserType)0) : ((num < ((CountryBaseDetails)country).TeenagerAge) ? ((AccountUserType)2) : ((AccountUserType)1)));
		}
		return result;
	}

	private int ObtainAge(DateTime birthday)
	{
		DateTime today = DateTime.Today;
		int num = today.Year - birthday.Year;
		if (today.Month < birthday.Month || (today.Month == birthday.Month && today.Day < birthday.Day))
		{
			num--;
		}
		return num;
	}

	private string ObtainTermsOfService(string language, string country)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		string result = null;
		HRESULT termsOfService = base.State.AccountManagement.GetTermsOfService(language, country, ref result);
		if (((HRESULT)(ref termsOfService)).IsError)
		{
			SetError(termsOfService, null);
		}
		return result;
	}
}
