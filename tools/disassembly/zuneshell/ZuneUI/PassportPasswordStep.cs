using System;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class PassportPasswordStep : AccountManagementStep
{
	private struct ServiceData
	{
		public string Email;

		public string Password;

		public PassportIdentity PassportIdentity;

		public AccountUser ExistingAccountUser;
	}

	private bool _skipOnce;

	private bool _forceEnable;

	private bool _isUpgradeNeeded;

	private bool _isZuneAccount;

	private bool _isUnsupportedAccount;

	private bool _isUnsupportedRegion;

	private bool _lockEmail;

	private PassportIdentity _passportIdentity;

	private AccountUser _existingAccountUser;

	private Dictionary<int, PropertyDescriptor> _errorMappings;

	public override string UI => "res://ZuneShellResources!CreatePassport.uix#PassportPasswordStep";

	public override bool IsEnabled
	{
		get
		{
			bool flag = base.IsEnabled && !CreatePassportStep.CreatedPassport;
			if (flag)
			{
				flag = ForceEnable;
				if (!flag)
				{
					flag = SignIn.Instance.SignedIn && base.ParentAccount && SignIn.Instance.IsParentallyControlled;
				}
				if (!flag)
				{
					flag = (EmailSelectionStep.IsEnabled || EmailSelectionStep.PageLocked) && !EmailSelectionStep.NeedsPassportId;
					flag &= _owner.CurrentPage != this || base.ParentAccount || !SignIn.Instance.SigningIn;
				}
			}
			return flag;
		}
	}

	public bool ForceEnable
	{
		get
		{
			return _forceEnable;
		}
		set
		{
			if (_forceEnable != value)
			{
				_forceEnable = value;
				((ModelItem)this).FirePropertyChanged("ForceEnable");
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

	public bool LockEmail
	{
		get
		{
			return _lockEmail;
		}
		internal set
		{
			if (_lockEmail != value)
			{
				_lockEmail = value;
				((ModelItem)this).FirePropertyChanged("LockEmail");
			}
		}
	}

	public string CommittedEmail
	{
		get
		{
			return GetCommittedValue(PassportPasswordPropertyEditor.Email) as string;
		}
		internal set
		{
			SetCommittedValue(PassportPasswordPropertyEditor.Email, value);
			((ModelItem)this).FirePropertyChanged("CommittedEmail");
		}
	}

	public string CommittedPassword
	{
		get
		{
			return GetCommittedValue(PassportPasswordPropertyEditor.Password) as string;
		}
		internal set
		{
			SetCommittedValue(PassportPasswordPropertyEditor.Password, value);
			((ModelItem)this).FirePropertyChanged("CommittedPassword");
		}
	}

	internal string UncommittedEmail => GetUncommittedValue(PassportPasswordPropertyEditor.Email) as string;

	internal string UncommittedPassword => GetUncommittedValue(PassportPasswordPropertyEditor.Password) as string;

	public bool CanCreateAccount
	{
		get
		{
			bool flag = (base.ParentAccount && (base.State.CreatePassportParentStep.IsEnabled || base.State.CreatePassportParentStep.CreatedPassport)) || (!base.ParentAccount && (base.State.CreatePassportStep.IsEnabled || base.State.CreatePassportStep.CreatedPassport));
			if (!flag)
			{
				flag = !IsUnsupportedAccount && !IsUnsupportedRegion && !IsUpgradeNeeded && !IsZuneAccount;
			}
			return flag;
		}
	}

	public bool IsUpgradeNeeded
	{
		get
		{
			return _isUpgradeNeeded;
		}
		private set
		{
			if (_isUpgradeNeeded != value)
			{
				_isUpgradeNeeded = value;
				((ModelItem)this).FirePropertyChanged("IsUpgradeNeeded");
				((ModelItem)this).FirePropertyChanged("CanCreateAccount");
			}
		}
	}

	public bool IsZuneAccount
	{
		get
		{
			return _isZuneAccount;
		}
		private set
		{
			if (_isZuneAccount != value)
			{
				_isZuneAccount = value;
				((ModelItem)this).FirePropertyChanged("IsZuneAccount");
				((ModelItem)this).FirePropertyChanged("CanCreateAccount");
			}
		}
	}

	public bool IsUnsupportedAccount
	{
		get
		{
			return _isUnsupportedAccount;
		}
		private set
		{
			if (_isUnsupportedAccount != value)
			{
				_isUnsupportedAccount = value;
				((ModelItem)this).FirePropertyChanged("IsUnsupportedAccount");
				((ModelItem)this).FirePropertyChanged("CanCreateAccount");
			}
		}
	}

	public bool IsUnsupportedRegion
	{
		get
		{
			return _isUnsupportedRegion;
		}
		private set
		{
			if (_isUnsupportedRegion != value)
			{
				_isUnsupportedRegion = value;
				((ModelItem)this).FirePropertyChanged("IsUnsupportedRegion");
				((ModelItem)this).FirePropertyChanged("CanCreateAccount");
			}
		}
	}

	public bool IsParentAccountNeeded
	{
		get
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Invalid comparison between Unknown and I4
			bool result = false;
			if (_existingAccountUser != null)
			{
				result = (int)_existingAccountUser.AccountUserType != 0;
			}
			return result;
		}
	}

	public AccountUser ExistingAccountUser
	{
		get
		{
			return _existingAccountUser;
		}
		private set
		{
			if (_existingAccountUser != value)
			{
				_existingAccountUser = value;
				((ModelItem)this).FirePropertyChanged("ExistingAccountUser");
				((ModelItem)this).FirePropertyChanged("IsParentAccountNeeded");
			}
		}
	}

	public PassportIdentity PassportIdentity
	{
		get
		{
			return _passportIdentity;
		}
		internal set
		{
			if (_passportIdentity != value)
			{
				_passportIdentity = value;
				((ModelItem)this).FirePropertyChanged("PassportIdentity");
			}
		}
	}

	internal override Dictionary<int, PropertyDescriptor> ErrorPropertyMappings
	{
		get
		{
			if (_errorMappings == null)
			{
				_errorMappings = new Dictionary<int, PropertyDescriptor>(1);
				_errorMappings.Add(((HRESULT)(ref HRESULT._NS_E_PASSPORT_LOGIN_FAILED)).Int, PassportPasswordPropertyEditor.Password);
			}
			return _errorMappings;
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

	public PassportPasswordStep(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner, state, parentAccount)
	{
		if (parentAccount)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_START_PARENT_HEADER);
			base.DetailDescription = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_CREDENTIALS_PARENT);
		}
		else
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_PASSPORT_STEP);
			base.DetailDescription = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_CREDENTIALS_HEADER);
		}
		base.DetailDescription = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_CREDENTIALS_HEADER);
		PassportPasswordPropertyEditor wizardPropertyEditor = new PassportPasswordPropertyEditor();
		Initialize(wizardPropertyEditor);
	}

	protected override void OnActivate()
	{
		base.ServiceDeactivationRequestsDone = false;
		string text = EmailSelectionStep.GetCommittedValue(EmailSelectionPropertyEditor.Email) as string;
		string committedPassword = CommittedPassword;
		string committedEmail = CommittedEmail;
		if (text != committedEmail || string.IsNullOrEmpty(committedPassword))
		{
			PassportIdentity = null;
			CommittedEmail = text;
			CommittedPassword = string.Empty;
		}
		if (SkipOnce)
		{
			SkipOnce = false;
			_owner.MoveNext();
		}
	}

	internal override bool OnMovingNext()
	{
		if (!base.ServiceDeactivationRequestsDone)
		{
			IsZuneAccount = false;
			IsUnsupportedAccount = false;
			IsUnsupportedRegion = false;
			IsUpgradeNeeded = false;
			ServiceData serviceData = new ServiceData
			{
				Password = UncommittedPassword,
				Email = UncommittedEmail
			};
			if (UncommittedPassword != CommittedPassword || UncommittedEmail != CommittedEmail)
			{
				serviceData.PassportIdentity = null;
			}
			else
			{
				serviceData.PassportIdentity = PassportIdentity;
			}
			StartDeactivationRequests(serviceData);
			base.LoadStatus = Shell.LoadString(StringId.IDS_ACCOUNT_SIGNING_INTO_WINDOWS_LIVE);
			return false;
		}
		if (!SignIn.Instance.SigningIn || base.ParentAccount)
		{
			base.LoadStatus = null;
			return base.OnMovingNext();
		}
		base.LoadStatus = Shell.LoadString(StringId.IDS_PLEASE_WAIT);
		return false;
	}

	protected override void OnStartDeactivationRequests(object state)
	{
		ServiceData serviceData = (ServiceData)state;
		ValidatePassportAccount(ref serviceData);
		EndDeactivationRequests(serviceData);
	}

	protected override void OnEndDeactivationRequests(object args)
	{
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		ServiceData serviceData = (ServiceData)args;
		PassportIdentity = serviceData.PassportIdentity;
		if (PassportIdentity != null && !base.ParentAccount)
		{
			SignIn.Instance.SignOut();
			SignIn.Instance.SignInStatusUpdatedEvent += OnSignInStatusUpdatedEvent;
			SignIn.Instance.SignInUser(serviceData.Email, serviceData.Password);
			ExistingAccountUser = serviceData.ExistingAccountUser;
			if (ExistingAccountUser != null)
			{
				base.State.SetPrivacySettings(ExistingAccountUser.AccountUserType);
			}
		}
	}

	private void ValidatePassportAccount(ref ServiceData serviceData)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		HRESULT hr = HRESULT._S_OK;
		if (serviceData.PassportIdentity == null)
		{
			hr = AccountManagementHelper.GetPassportIdentity(serviceData.Email, serviceData.Password, out serviceData.PassportIdentity);
		}
		if (((HRESULT)(ref hr)).IsSuccess)
		{
			ServiceError val = null;
			base.State.AccountManagement.GetAccount(serviceData.PassportIdentity, ref serviceData.ExistingAccountUser, ref val);
		}
		if (((HRESULT)(ref hr)).IsError)
		{
			SetError(hr, null);
		}
	}

	private void OnSignInStatusUpdatedEvent(object sender, EventArgs e)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (!SignIn.Instance.SigningIn)
		{
			HRESULT signInError = SignIn.Instance.SignInError;
			if (((HRESULT)(ref signInError)).IsError || SignIn.Instance.SignedIn)
			{
				bool flag = false;
				HRESULT signInError2 = SignIn.Instance.SignInError;
				if (((HRESULT)(ref signInError2)).IsError)
				{
					if (SignIn.Instance.SignInError == HRESULT._NS_E_SIGNIN_TERMS_OF_SERVICE)
					{
						IsUpgradeNeeded = true;
					}
					else if (SignIn.Instance.SignInError == HRESULT._NS_E_SIGNIN_ACCOUNTS_NOT_XENON_USER)
					{
						flag = true;
						IsUnsupportedAccount = true;
					}
					else if (SignIn.Instance.SignInError == HRESULT._NS_E_SIGNIN_WCMUSIC_ACCOUNT_NOT_ELIGIBLE)
					{
						flag = true;
						IsUnsupportedRegion = true;
					}
					else if (SignIn.Instance.SignInError == HRESULT._NS_E_SIGNIN_INVALID_REGION)
					{
						IsZuneAccount = true;
					}
				}
				else if (SignIn.Instance.SignedIn)
				{
					IsZuneAccount = true;
				}
				SignIn.Instance.SignInStatusUpdatedEvent -= OnSignInStatusUpdatedEvent;
				if (flag)
				{
					SetError(SignIn.Instance.SignInError, null);
					NavigateToErrorHandler();
				}
				else if (_owner.CurrentPage == this)
				{
					_owner.MoveNext();
				}
			}
		}
		((ModelItem)this).FirePropertyChanged("Enabled");
	}
}
