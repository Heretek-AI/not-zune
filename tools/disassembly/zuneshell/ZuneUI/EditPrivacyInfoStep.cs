using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class EditPrivacyInfoStep : PrivacyInfoStep
{
	public override bool IsEnabled => _owner.CurrentPage != this || !base.ServiceRequestWorking;

	public override bool ShowPrivacyStatement => true;

	public EditPrivacyInfoStep(Wizard owner, AccountManagementWizardState state, bool parentAccount, PrivacyInfoSettings showSettings)
		: base(owner, state, parentAccount, showSettings)
	{
	}

	protected override bool OnCommitChanges()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		bool result = true;
		if (base.CommittedSettings != null)
		{
			HRESULT hr = HRESULT._S_OK;
			ServiceError serviceError = null;
			if (base.PrivacySettings != null && base.PrivacySettings.Count > 0)
			{
				hr = base.State.AccountManagement.SetPrivacySettings(base.CommittedSettings, base.State.PassportPasswordParentStep.PassportIdentity, ref serviceError);
			}
			if (((HRESULT)(ref hr)).IsSuccess && base.FamilySettings != null && base.FamilySettings.Settings != null && base.FamilySettings.Settings.Count > 0)
			{
				base.FamilySettings.CommitSettings();
			}
			if (SignIn.Instance.FamilySettings != null)
			{
				SignIn.Instance.FamilySettings.ReloadSettings();
			}
			if (((HRESULT)(ref hr)).IsSuccess && (base.AllowMicrosoftCommunications != null || base.AllowPartnerCommunications != null))
			{
				hr = base.State.AccountManagement.SetNewsLetterSettings(base.CommittedSettings, ref serviceError);
			}
			if (!((HRESULT)(ref hr)).IsSuccess)
			{
				result = false;
				SetError(hr, serviceError);
			}
		}
		return result;
	}

	protected override void OnActivate()
	{
		base.ServiceActivationRequestsDone = base.CommittedSettings != null;
		base.OnActivate();
	}

	protected override void OnStartActivationRequests(object state)
	{
		InitializeHelpersOnWorkerThread();
		AccountUser args = ObtainAccountUser();
		EndActivationRequests(args);
	}

	protected override void OnEndActivationRequests(object args)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Invalid comparison between Unknown and I4
		if (args == null)
		{
			NavigateToErrorHandler();
			return;
		}
		InitializeSettings();
		AccountUser val = (AccountUser)args;
		base.CommittedSettings = val.AccountSettings;
		if (base.ShowSettings == PrivacyInfoSettings.None)
		{
			if ((int)val.AccountUserType == 0)
			{
				base.ShowSettings = PrivacyInfoSettings.SocialSettings;
			}
			else if ((int)val.AccountUserType == 1)
			{
				base.ShowSettings = (FeatureEnablement.IsFeatureEnabled((Features)5) ? PrivacyInfoSettings.CreateChildAccountWithSocial : PrivacyInfoSettings.CreateChildAccount);
			}
			else
			{
				base.ShowSettings = PrivacyInfoSettings.CreateChildAccount;
			}
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
