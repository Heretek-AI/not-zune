using System;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class EmailSelectionStep : AccountManagementStep
{
	private bool _hasEmail;

	private bool _isEmailPassportId;

	private bool _pageLocked;

	private bool _skipOnce;

	private Dictionary<int, PropertyDescriptor> _errorMappings;

	public override string UI => "res://ZuneShellResources!CreatePassport.uix#EmailSelectionStep";

	public bool HasEmail
	{
		get
		{
			return _hasEmail;
		}
		set
		{
			if (_hasEmail != value)
			{
				_hasEmail = value;
				((ModelItem)this).FirePropertyChanged("HasEmail");
				((ModelItem)this).FirePropertyChanged("NeedsPassportId");
			}
		}
	}

	public string Email
	{
		get
		{
			return GetUncommittedValue(EmailSelectionPropertyEditor.Email) as string;
		}
		internal set
		{
			SetCommittedValue(EmailSelectionPropertyEditor.Email, value);
			((ModelItem)this).FirePropertyChanged("Email");
		}
	}

	private bool CheckedEmail => base.ServiceDeactivationRequestsDone;

	internal bool PageLocked
	{
		get
		{
			return _pageLocked;
		}
		set
		{
			if (_pageLocked != value)
			{
				_pageLocked = value;
				((ModelItem)this).FirePropertyChanged("PageLocked");
			}
		}
	}

	internal bool SkipOnce
	{
		get
		{
			return _skipOnce;
		}
		set
		{
			if (_skipOnce != value)
			{
				_skipOnce = value;
				((ModelItem)this).FirePropertyChanged("SkipOnce");
			}
		}
	}

	public bool IsEmailPassportId
	{
		get
		{
			return _isEmailPassportId;
		}
		private set
		{
			if (_isEmailPassportId != value)
			{
				_isEmailPassportId = value;
				((ModelItem)this).FirePropertyChanged("IsEmailPassportId");
				((ModelItem)this).FirePropertyChanged("NeedsPassportId");
			}
		}
	}

	public bool NeedsPassportId
	{
		get
		{
			if (HasEmail)
			{
				return !IsEmailPassportId;
			}
			return true;
		}
	}

	public override bool IsValid
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			MetadataEditProperty property = base.WizardPropertyEditor.GetProperty(EmailSelectionPropertyEditor.Email);
			HRESULT externalError = HRESULT._S_OK;
			if (HasEmail)
			{
				string email = Email;
				if (HasEmail && base.ParentAccount && (email.Equals(base.State.EmailSelectionStep.Email, StringComparison.InvariantCultureIgnoreCase) || email.Equals(base.State.CreatePassportStep.Email, StringComparison.InvariantCultureIgnoreCase)))
				{
					externalError = HRESULT._ZUNE_E_SIGNUP_INVALID_PARENT_EMAIL;
				}
			}
			if (((HRESULT)(ref externalError)).IsError || property.ExternalError == HRESULT._ZUNE_E_SIGNUP_INVALID_PARENT_EMAIL)
			{
				property.ExternalError = externalError;
			}
			if (!HasEmail)
			{
				return true;
			}
			return base.IsValid;
		}
	}

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled && !CreatePassportStep.CreatedPassport;
			if (flag)
			{
				flag = !base.ParentAccount || base.State.BasicAccountInfoStep.IsParentAccountNeeded;
			}
			if (flag)
			{
				flag = !CheckedEmail || !PageLocked;
			}
			return flag;
		}
	}

	internal override Dictionary<int, PropertyDescriptor> ErrorPropertyMappings
	{
		get
		{
			if (_errorMappings == null)
			{
				_errorMappings = new Dictionary<int, PropertyDescriptor>(1);
				_errorMappings.Add(((HRESULT)(ref HRESULT._ZUNE_E_WINLIVE_UNAUTHORIZED_DOMAIN)).Int, EmailSelectionPropertyEditor.Email);
			}
			return _errorMappings;
		}
	}

	private CreatePassportStep CreatePassportStep
	{
		get
		{
			CreatePassportStep createPassportStep = null;
			if (base.ParentAccount)
			{
				return base.State.CreatePassportParentStep;
			}
			return base.State.CreatePassportStep;
		}
	}

	public EmailSelectionStep(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner, state, parentAccount)
	{
		if (parentAccount)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_PARENTAL_INPUT_HEAD);
		}
		else
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_EMAIL_STEP);
		}
		EmailSelectionPropertyEditor wizardPropertyEditor = new EmailSelectionPropertyEditor();
		Initialize(wizardPropertyEditor);
	}

	internal override ErrorMapperResult GetMappedErrorDescriptionAndUrl(HRESULT hr)
	{
		return ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hr)).Int, (eErrorCondition)4);
	}

	protected override void OnActivate()
	{
		if (SkipOnce || PageLocked)
		{
			SkipOnce = false;
			_owner.MoveNext();
			return;
		}
		base.ServiceDeactivationRequestsDone = false;
		if (string.IsNullOrEmpty(Email) && !base.ParentAccount)
		{
			UIDevice uIDevice = ApplicationMarketplaceHelper.FindAppDevice();
			if (uIDevice != UIDeviceList.NullDevice)
			{
				Email = uIDevice.LiveId;
				HasEmail = !string.IsNullOrEmpty(Email);
			}
		}
		base.OnActivate();
	}

	internal override bool OnMovingNext()
	{
		if (!HasEmail)
		{
			base.ServiceDeactivationRequestsDone = true;
		}
		if (base.ServiceDeactivationRequestsDone)
		{
			return base.OnMovingNext();
		}
		string email = Email;
		StartDeactivationRequests(email);
		return false;
	}

	protected override void OnStartDeactivationRequests(object state)
	{
		bool flag = ValidatePassportId(state as string);
		EndDeactivationRequests(flag);
	}

	protected override void OnEndDeactivationRequests(object args)
	{
		IsEmailPassportId = (bool)args;
	}

	private bool ValidatePassportId(string email)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		bool result = false;
		ServiceError serviceError = null;
		WinLiveAvailableInformation val = default(WinLiveAvailableInformation);
		HRESULT hr = base.State.WinLiveSignup.CheckAvailableSigninName(email, false, (string)null, (string)null, ref val, ref serviceError);
		if (((HRESULT)(ref hr)).IsError)
		{
			SetError(hr, serviceError);
		}
		else
		{
			result = !val.Available;
		}
		return result;
	}
}
