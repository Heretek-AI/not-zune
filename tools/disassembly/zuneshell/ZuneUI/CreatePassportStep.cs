using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class CreatePassportStep : AccountManagementStep
{
	private IList _suggestedPassportIds;

	private WinLiveInformation _winLiveInformation;

	private string _lastLocale;

	private bool _createdPassport;

	private Dictionary<int, PropertyDescriptor> _errorMappings;

	private static IList s_secretQuestions;

	private string _secretAnswerString;

	public override string UI => "res://ZuneShellResources!CreatePassport.uix#CreatePassportStep";

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled && !CreatedPassport;
			if (flag)
			{
				flag = EmailSelectionStep.IsEnabled && (!EmailSelectionStep.HasEmail || !EmailSelectionStep.IsEmailPassportId) && (_owner.CurrentPage != this || WinLiveInformation != null);
			}
			return flag;
		}
	}

	public override bool IsValid
	{
		get
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_008b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_007e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0069: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			MetadataEditProperty property = base.WizardPropertyEditor.GetProperty(CreatePassportPropertyEditor.PassportId);
			MetadataEditProperty property2 = base.WizardPropertyEditor.GetProperty(CreatePassportPropertyEditor.PassportDomain);
			string email = Email;
			HRESULT externalError = HRESULT._S_OK;
			if (base.ParentAccount && (email.Equals(base.State.EmailSelectionStep.Email, StringComparison.InvariantCultureIgnoreCase) || email.Equals(base.State.CreatePassportStep.Email, StringComparison.InvariantCultureIgnoreCase)))
			{
				externalError = HRESULT._ZUNE_E_SIGNUP_INVALID_PARENT_EMAIL;
			}
			if (((HRESULT)(ref externalError)).IsError || property.ExternalError == HRESULT._ZUNE_E_SIGNUP_INVALID_PARENT_EMAIL)
			{
				property.ExternalError = externalError;
			}
			if (((HRESULT)(ref externalError)).IsError || property2.ExternalError == HRESULT._ZUNE_E_SIGNUP_INVALID_PARENT_EMAIL)
			{
				property2.ExternalError = externalError;
			}
			return base.IsValid;
		}
	}

	public WinLiveInformation WinLiveInformation
	{
		get
		{
			return _winLiveInformation;
		}
		private set
		{
			if (_winLiveInformation != value)
			{
				_winLiveInformation = value;
				((ModelItem)this).FirePropertyChanged("WinLiveInformation");
				((ModelItem)this).FirePropertyChanged("IsEnabled");
				if (CreateEmail)
				{
					SetDefaultWindowsLiveDomain();
				}
			}
		}
	}

	public WinLiveInformation WinLiveHip
	{
		get
		{
			if (base.ParentAccount)
			{
				return base.State.HipPassportParentStep.WinLiveHip;
			}
			return base.State.HipPassportStep.WinLiveHip;
		}
	}

	public bool CreateEmail => EmailSelectionStep.IsEnabled && !EmailSelectionStep.HasEmail;

	public IList SuggestedPassportIds
	{
		get
		{
			return _suggestedPassportIds;
		}
		private set
		{
			if (_suggestedPassportIds != value)
			{
				_suggestedPassportIds = value;
				((ModelItem)this).FirePropertyChanged("SuggestedPassportIds");
			}
		}
	}

	public string CommittedEmail
	{
		get
		{
			string text = GetCommittedValue(CreatePassportPropertyEditor.PassportId) as string;
			string text2 = GetCommittedValue(CreatePassportPropertyEditor.PassportDomain) as string;
			string result = null;
			if (text != null && text2 != null)
			{
				result = $"{text}@{text2}";
			}
			return result;
		}
	}

	public string Email
	{
		get
		{
			string text = GetUncommittedValue(CreatePassportPropertyEditor.PassportId) as string;
			string text2 = GetUncommittedValue(CreatePassportPropertyEditor.PassportDomain) as string;
			string result = null;
			if (text != null && text2 != null)
			{
				result = $"{text}@{text2}";
			}
			return result;
		}
		set
		{
			if (Email != value)
			{
				string value2 = string.Empty;
				string value3 = string.Empty;
				int num = 0;
				if (value != null)
				{
					num = value.IndexOf('@');
				}
				if (num > 0 && num + 2 <= value.Length)
				{
					value2 = value.Substring(0, num);
					value3 = value.Substring(num + 1);
				}
				SetCommittedValue(CreatePassportPropertyEditor.PassportId, value2);
				SetCommittedValue(CreatePassportPropertyEditor.PassportDomain, value3);
				((ModelItem)this).FirePropertyChanged("Email");
			}
		}
	}

	internal bool CreatedPassport
	{
		get
		{
			return _createdPassport;
		}
		private set
		{
			_createdPassport = value;
		}
	}

	internal override Dictionary<int, PropertyDescriptor> ErrorPropertyMappings
	{
		get
		{
			if (_errorMappings == null)
			{
				_errorMappings = new Dictionary<int, PropertyDescriptor>(18);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SIGNIN_NAME_TOO_SHORT)).Int, CreatePassportPropertyEditor.PassportId);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SIGNIN_NAME_TOO_LONG)).Int, CreatePassportPropertyEditor.PassportId);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SIGNIN_NAME_INVALID)).Int, CreatePassportPropertyEditor.PassportId);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_EMAIL_INVALID)).Int, CreatePassportPropertyEditor.PassportId);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_NAME_INVALID)).Int, CreatePassportPropertyEditor.PassportId);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_MEMBER_EXISTS)).Int, CreatePassportPropertyEditor.PassportId);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_DOMAIN_IS_MANAGED)).Int, CreatePassportPropertyEditor.PassportDomain);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_PASSWORD_TOO_LONG)).Int, CreatePassportPropertyEditor.Password1);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_PASSWORD_TOO_SHORT)).Int, CreatePassportPropertyEditor.Password1);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_PASSWORD_INVALID)).Int, CreatePassportPropertyEditor.Password1);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SECRET_QUESTION_TOO_SHORT)).Int, CreatePassportPropertyEditor.SecretQuestion);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SECRET_QUESTION_TOO_LONG)).Int, CreatePassportPropertyEditor.SecretQuestion);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SECRET_QUESTION_CONTAINS_ANSWER)).Int, CreatePassportPropertyEditor.SecretQuestion);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SECRET_QUESTION_CONTAINS_PASSWORD)).Int, CreatePassportPropertyEditor.SecretQuestion);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SECRET_ANSWER_TOO_SHORT)).Int, CreatePassportPropertyEditor.SecretAnswer);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SECRET_ANSWER_TOO_LONG)).Int, CreatePassportPropertyEditor.SecretAnswer);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SECRET_ANSWER_CONTAINS_MEMBER_NAME)).Int, CreatePassportPropertyEditor.SecretAnswer);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_WINLIVE_SECRET_ANSWER_CONTAINS_PASSWORD)).Int, CreatePassportPropertyEditor.SecretAnswer);
			}
			return _errorMappings;
		}
	}

	public static IList SecretQuestions
	{
		get
		{
			if (s_secretQuestions == null)
			{
				string text = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_SECRET_QUESTIONS);
				if (text != null)
				{
					s_secretQuestions = text.Split(new char[1] { ';' });
				}
			}
			return s_secretQuestions;
		}
	}

	private EmailSelectionStep EmailSelectionStep
	{
		get
		{
			EmailSelectionStep emailSelectionStep = null;
			if (base.ParentAccount)
			{
				return base.State.EmailSelectionParentStep;
			}
			return base.State.EmailSelectionStep;
		}
	}

	public string SecretAnswerString
	{
		get
		{
			return _secretAnswerString;
		}
		private set
		{
			if (_secretAnswerString != value)
			{
				_secretAnswerString = value;
				((ModelItem)this).FirePropertyChanged("SecretAnswerString");
			}
		}
	}

	public CreatePassportStep(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner, state, parentAccount)
	{
		if (parentAccount)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_START_PARENT_HEADER);
		}
		else
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_PASSPORT_STEP);
		}
		_secretAnswerString = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_SECRET_ANSWER_HELP);
		base.NextTextOverride = Shell.LoadString(StringId.IDS_I_ACCEPT_BUTTON);
		CreatePassportPropertyEditor wizardPropertyEditor = new CreatePassportPropertyEditor();
		Initialize(wizardPropertyEditor);
	}

	protected override void OnActivate()
	{
		base.ServiceDeactivationRequestsDone = false;
		if (EmailSelectionStep.HasEmail && !CreateEmail)
		{
			Email = EmailSelectionStep.GetCommittedValue(EmailSelectionPropertyEditor.Email) as string;
		}
		string selectedLocale = base.State.BasicAccountInfoStep.SelectedLocale;
		if (_lastLocale != selectedLocale)
		{
			base.ServiceActivationRequestsDone = false;
		}
		if (!base.ServiceActivationRequestsDone)
		{
			_lastLocale = selectedLocale;
			StartActivationRequests(selectedLocale);
		}
		else if (CreateEmail)
		{
			SetDefaultWindowsLiveDomain();
		}
		SecretAnswerString = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_SECRET_ANSWER_HELP);
		CreatePassportPropertyEditor.SecretAnswer.MinLength = 5;
	}

	internal override void Deactivate()
	{
		base.Deactivate();
		SuggestedPassportIds = null;
	}

	internal override bool OnMovingNext()
	{
		string email = Email;
		string committedEmail = CommittedEmail;
		if (email == committedEmail)
		{
			base.ServiceDeactivationRequestsDone = true;
		}
		if (base.ServiceDeactivationRequestsDone)
		{
			if (SuggestedPassportIds == null || SuggestedPassportIds.Count == 0 || SuggestedPassportIds.Contains(email))
			{
				SuggestedPassportIds = null;
				return base.OnMovingNext();
			}
			base.ServiceDeactivationRequestsDone = false;
			return false;
		}
		StartDeactivationRequests(email);
		return false;
	}

	internal override ErrorMapperResult GetMappedErrorDescriptionAndUrl(HRESULT hr)
	{
		return ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hr)).Int, (eErrorCondition)4);
	}

	protected override bool OnCommitChanges()
	{
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		bool flag = true;
		if (IsEnabled && !CreatedPassport)
		{
			string email = Email;
			string text = GetCommittedValue(CreatePassportPropertyEditor.Password1) as string;
			string text2 = GetCommittedValue(CreatePassportPropertyEditor.SecretQuestion) as string;
			string text3 = GetCommittedValue(CreatePassportPropertyEditor.SecretAnswer) as string;
			string selectedCountry = base.State.BasicAccountInfoStep.SelectedCountry;
			int num = 0;
			string selectedLocale = base.State.BasicAccountInfoStep.SelectedLocale;
			if (!string.IsNullOrEmpty(selectedLocale))
			{
				num = CultureHelper.GetLCIDFromCultureString(selectedLocale, useDefault: true);
			}
			int num2 = 0;
			if (WinLiveInformation != null)
			{
				num2 = WinLiveInformation.TermsOfServiceVersion;
			}
			string text4 = string.Empty;
			if (WinLiveHip != null)
			{
				text4 = WinLiveHip.HipChallenge;
			}
			string text5;
			DateTime? dateTime;
			if (base.ParentAccount)
			{
				text5 = base.State.HipPassportParentStep.GetCommittedValue(HipPropertyEditor.HipCharacters) as string;
				dateTime = (DateTime?)base.State.ContactInfoParentStep.GetCommittedValue(ContactInfoParentPropertyEditor.Birthday);
			}
			else
			{
				text5 = base.State.HipPassportStep.GetCommittedValue(HipPropertyEditor.HipCharacters) as string;
				dateTime = (DateTime?)base.State.BasicAccountInfoStep.GetCommittedValue(BasicAccountInfoPropertyEditor.Birthday);
			}
			ServiceError serviceError = null;
			HRESULT hr = base.State.WinLiveSignup.CreateAccount(email, text, text2, text3, selectedCountry, text4, text5, dateTime.Value, num2, num, ref serviceError);
			flag = ((HRESULT)(ref hr)).IsSuccess;
			if (flag)
			{
				CreatedPassport = true;
			}
			else
			{
				SetError(hr, serviceError);
			}
		}
		return flag;
	}

	protected override void OnStartActivationRequests(object state)
	{
		WinLiveInformation args = ObtainWinLiveInformation(state as string);
		EndActivationRequests(args);
	}

	protected override void OnEndActivationRequests(object args)
	{
		if (args == null)
		{
			NavigateToErrorHandler();
		}
		else
		{
			WinLiveInformation = (WinLiveInformation)((args is WinLiveInformation) ? args : null);
		}
	}

	protected override void OnStartDeactivationRequests(object state)
	{
		string email = state as string;
		IList args = ObtainUniquePassportIds(email);
		EndDeactivationRequests(args);
	}

	protected override void OnEndDeactivationRequests(object args)
	{
		IList suggestedPassportIds = args as IList;
		SuggestedPassportIds = suggestedPassportIds;
	}

	private void SetDefaultWindowsLiveDomain()
	{
		if (!(GetUncommittedValue(CreatePassportPropertyEditor.PassportDomain) is string value) || (_winLiveInformation.Domains != null && _winLiveInformation.Domains.Count > 0 && !_winLiveInformation.Domains.Contains(value)))
		{
			SetUncommittedValue(CreatePassportPropertyEditor.PassportDomain, _winLiveInformation.Domains[0] as string);
		}
	}

	private WinLiveInformation ObtainWinLiveInformation(string locale)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		WinLiveInformation result = null;
		ServiceError serviceError = null;
		HRESULT information = base.State.WinLiveSignup.GetInformation(locale, (EHipType)0, ref result, ref serviceError);
		if (((HRESULT)(ref information)).IsError)
		{
			SetError(information, serviceError);
		}
		return result;
	}

	private IList ObtainUniquePassportIds(string email)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		IList result = null;
		ServiceError serviceError = null;
		WinLiveAvailableInformation val = default(WinLiveAvailableInformation);
		HRESULT hr = base.State.WinLiveSignup.CheckAvailableSigninName(email, true, (string)null, (string)null, ref val, ref serviceError);
		if (((HRESULT)(ref hr)).IsError)
		{
			SetError(hr, serviceError);
		}
		else if (!val.Available)
		{
			result = val.SuggestedNames;
		}
		return result;
	}
}
