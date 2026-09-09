using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class PrivacyInfoStep : AccountManagementStep
{
	private BooleanChoice _allowMicrosoftCommunications;

	private BooleanChoice _allowPartnerCommunications;

	private BooleanChoice _usageCollection;

	private PrivacyInfoSettings _showSettings;

	private AccountSettings _committedSettings;

	private FamilySettings _familySettings;

	private IList _privacySettings;

	private IList _familySettingsChoices;

	public override string UI => "res://ZuneShellResources!AccountInfo.uix#PrivacyInfoStep";

	public override bool IsEnabled => ShowingSettings;

	public AccountSettings CommittedSettings
	{
		get
		{
			return _committedSettings;
		}
		set
		{
			if (_committedSettings != value)
			{
				_committedSettings = value;
				SetUncommittedSettings();
				((ModelItem)this).FirePropertyChanged("CommittedSettings");
			}
		}
	}

	public PrivacyInfoSettings ShowSettings
	{
		get
		{
			return _showSettings;
		}
		set
		{
			if (_showSettings != value)
			{
				_showSettings = value;
				ResetSettings();
				((ModelItem)this).FirePropertyChanged("ShowSettings");
			}
		}
	}

	public FamilySettings FamilySettings
	{
		get
		{
			return _familySettings;
		}
		private set
		{
			if (_familySettings != value)
			{
				_familySettings = value;
				((ModelItem)this).FirePropertyChanged("FamilySettings");
			}
		}
	}

	public IList PrivacySettings
	{
		get
		{
			return _privacySettings;
		}
		private set
		{
			if (_privacySettings != value)
			{
				_privacySettings = value;
				((ModelItem)this).FirePropertyChanged("PrivacySettings");
			}
		}
	}

	public IList FamilySettingsChoices
	{
		get
		{
			return _familySettingsChoices;
		}
		private set
		{
			if (_familySettingsChoices != value)
			{
				_familySettingsChoices = value;
				((ModelItem)this).FirePropertyChanged("FamilySettingsChoices");
			}
		}
	}

	public bool ShowingCommunicationSetting
	{
		get
		{
			if ((ShowSettings & PrivacyInfoSettings.Communications) == 0)
			{
				return (ShowSettings & PrivacyInfoSettings.AllSocial) != 0;
			}
			return true;
		}
	}

	public bool ShowingCreateNewSettings
	{
		get
		{
			if (ShowSettings != PrivacyInfoSettings.CreateNewAccount)
			{
				return ShowSettings == PrivacyInfoSettings.CreateNewAccountWithSocial;
			}
			return true;
		}
	}

	public bool ShowingFriendsListSharingSetting
	{
		get
		{
			if ((ShowSettings & PrivacyInfoSettings.FriendsSharing) == 0)
			{
				return (ShowSettings & PrivacyInfoSettings.AllSocial) != 0;
			}
			return true;
		}
	}

	public bool ShowingMusingSharingSetting
	{
		get
		{
			if ((ShowSettings & PrivacyInfoSettings.MusicSharing) == 0)
			{
				return (ShowSettings & PrivacyInfoSettings.AllSocial) != 0;
			}
			return true;
		}
	}

	public bool ShowingNewsletterSettings => ShowSettings == PrivacyInfoSettings.AllowMicrosoftCommunications;

	public bool ShowingProfileCustomizationSetting
	{
		get
		{
			if ((ShowSettings & PrivacyInfoSettings.ProfileCustomization) == 0)
			{
				return (ShowSettings & PrivacyInfoSettings.AllSocial) != 0;
			}
			return true;
		}
	}

	public BooleanChoice AllowMicrosoftCommunications
	{
		get
		{
			return _allowMicrosoftCommunications;
		}
		set
		{
			if (_allowMicrosoftCommunications != value)
			{
				_allowMicrosoftCommunications = value;
				((ModelItem)this).FirePropertyChanged("AllowMicrosoftCommunications");
			}
		}
	}

	public BooleanChoice AllowPartnerCommunications
	{
		get
		{
			return _allowPartnerCommunications;
		}
		set
		{
			if (_allowPartnerCommunications != value)
			{
				_allowPartnerCommunications = value;
				((ModelItem)this).FirePropertyChanged("AllowPartnerCommunications");
			}
		}
	}

	public BooleanChoice UsageCollection
	{
		get
		{
			return _usageCollection;
		}
		set
		{
			if (_usageCollection != value)
			{
				_usageCollection = value;
				((ModelItem)this).FirePropertyChanged("UsageCollection");
			}
		}
	}

	private bool CountrySupportsNewsletterOptions
	{
		get
		{
			bool result = true;
			if (base.State.BasicAccountInfoStep.IsEnabled)
			{
				AccountCountry country = AccountCountryList.Instance.GetCountry(base.State.BasicAccountInfoStep.SelectedCountry);
				if (country != null)
				{
					result = ((CountryBaseDetails)country).ShowNewsletterOptions;
				}
			}
			return result;
		}
	}

	private bool ShowingSettings
	{
		get
		{
			if (ShowSettings != PrivacyInfoSettings.None && (ShowSettings & PrivacyInfoSettings.NoNewsletterSettings) == 0)
			{
				return CountrySupportsNewsletterOptions;
			}
			return ShowSettings != PrivacyInfoSettings.None;
		}
	}

	private PrivacySettingValue UsageCollectionDefault
	{
		get
		{
			bool flag = false;
			string text = null;
			if (SignIn.Instance.SignedIn)
			{
				text = SignIn.Instance.CountryCode;
			}
			else if (base.State.BasicAccountInfoStep.IsEnabled)
			{
				text = base.State.BasicAccountInfoStep.SelectedCountry;
			}
			if (text != null)
			{
				AccountCountry country = AccountCountryList.Instance.GetCountry(text);
				if (country != null)
				{
					flag = ((CountryBaseDetails)country).UsageCollection;
				}
			}
			if (flag)
			{
				return (PrivacySettingValue)2;
			}
			return (PrivacySettingValue)0;
		}
	}

	public PrivacyInfoStep(Wizard owner, AccountManagementWizardState state, bool parentAccount, PrivacyInfoSettings showSettings)
		: base(owner, state, parentAccount)
	{
		_showSettings = showSettings;
		if (ShowingNewsletterSettings && !parentAccount)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ACCOUNT_INFO_STEP);
		}
		else if (parentAccount || (SignIn.Instance.SignedIn && SignIn.Instance.IsParentallyControlled))
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_FAMILY_HEADER);
		}
		else
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ACCOUNT_INFO_STEP);
		}
		Initialize(null);
		base.CanNavigateInto = false;
	}

	internal override bool OnMovingNext()
	{
		string text = null;
		if (PrivacySettings != null)
		{
			foreach (object privacySetting in PrivacySettings)
			{
				PrivacySettingChoice privacySettingChoice = (PrivacySettingChoice)privacySetting;
				if (((Choice)privacySettingChoice).ChosenValue == null)
				{
					text = ((ModelItem)privacySettingChoice).Description;
					break;
				}
			}
		}
		if (FamilySettingsChoices != null)
		{
			foreach (object familySettingsChoice in FamilySettingsChoices)
			{
				FamilySettingChoice familySettingChoice = (FamilySettingChoice)familySettingsChoice;
				if (((Choice)familySettingChoice).ChosenValue == null)
				{
					text = familySettingChoice.Title;
					break;
				}
			}
		}
		if (text == null)
		{
			base.StatusMessage = null;
			return base.OnMovingNext();
		}
		base.StatusMessage = string.Format(Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_SELECT_VALUE), text);
		return false;
	}

	protected override void OnActivate()
	{
		base.ServiceActivationRequestsDone = false;
		base.OnActivate();
	}

	internal override void Deactivate()
	{
		base.Deactivate();
		SetCommittedSettings();
	}

	protected override void OnStartActivationRequests(object state)
	{
		InitializeHelpersOnWorkerThread();
		base.OnStartActivationRequests(state);
	}

	protected void InitializeHelpersOnWorkerThread()
	{
		AccountCountryList.Instance.LoadDataOnWorkerThread();
		RatingSystemList.Instance.LoadDataOnWorkerThread();
	}

	protected override void OnEndActivationRequests(object args)
	{
		InitializeSettings();
		if (!CountrySupportsNewsletterOptions)
		{
			AllowMicrosoftCommunications = null;
			AllowPartnerCommunications = null;
			if ((ShowSettings & PrivacyInfoSettings.NoNewsletterSettings) == 0)
			{
				_owner.MoveNext();
			}
		}
	}

	private void SetCommittedSettings()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Invalid comparison between Unknown and I4
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		SetDefaultSettings();
		if (AllowMicrosoftCommunications != null)
		{
			CommittedSettings.AllowZuneEmails = AllowMicrosoftCommunications.Value;
		}
		if (AllowPartnerCommunications != null)
		{
			CommittedSettings.AllowPartnerEmails = AllowPartnerCommunications.Value;
		}
		if (UsageCollection != null)
		{
			PrivacySettingValue value = (PrivacySettingValue)(UsageCollection.Value ? 2 : 0);
			CommittedSettings.PrivacySettings[(PrivacySettingId)13] = value;
		}
		if (_privacySettings != null)
		{
			foreach (object privacySetting in _privacySettings)
			{
				PrivacySettingChoice privacySettingChoice = (PrivacySettingChoice)privacySetting;
				if ((int)privacySettingChoice.SettingId == -1)
				{
					SetCommittedSpecialProperties(privacySettingChoice);
				}
				else
				{
					CommittedSettings.PrivacySettings[privacySettingChoice.SettingId] = privacySettingChoice.SettingValue;
				}
			}
		}
		if (FamilySettingsChoices == null || FamilySettingsChoices.Count <= 0)
		{
			return;
		}
		foreach (FamilySettingChoice familySettingsChoice in FamilySettingsChoices)
		{
			if (familySettingsChoice.SettingValue != null)
			{
				FamilySettings.SetSetting(familySettingsChoice.SettingId, familySettingsChoice.SettingValue.Value, familySettingsChoice.BlockUnrated.Value);
			}
		}
	}

	private void SetCommittedSpecialProperties(PrivacySettingChoice multiSetting)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Invalid comparison between Unknown and I4
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		if (CommittedSettings != null && multiSetting != null && (int)multiSetting.SettingId == -1 && multiSetting.InfoSettings == PrivacyInfoSettings.AllSocial)
		{
			CommittedSettings.PrivacySettings[(PrivacySettingId)0] = multiSetting.SettingValue;
			CommittedSettings.PrivacySettings[(PrivacySettingId)12] = multiSetting.SettingValue;
			CommittedSettings.PrivacySettings[(PrivacySettingId)5] = multiSetting.SettingValue;
			CommittedSettings.PrivacySettings[(PrivacySettingId)9] = multiSetting.SettingValue;
		}
	}

	private void SetDefaultSettings()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Invalid comparison between Unknown and I4
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		if (CommittedSettings == null)
		{
			CommittedSettings = new AccountSettings();
			CommittedSettings.AllowZuneEmails = true;
			if ((int)base.State.BasicAccountInfoStep.NewAccounType == 0)
			{
				CommittedSettings.PrivacySettings[(PrivacySettingId)1] = (PrivacySettingValue)2;
				CommittedSettings.PrivacySettings[(PrivacySettingId)3] = (PrivacySettingValue)2;
				CommittedSettings.PrivacySettings[(PrivacySettingId)6] = (PrivacySettingValue)2;
				CommittedSettings.PrivacySettings[(PrivacySettingId)13] = UsageCollectionDefault;
				CommittedSettings.PrivacySettings[(PrivacySettingId)0] = (PrivacySettingValue)(ShowingCommunicationSetting ? (-1) : 0);
				CommittedSettings.PrivacySettings[(PrivacySettingId)12] = (PrivacySettingValue)(ShowingFriendsListSharingSetting ? (-1) : 0);
				CommittedSettings.PrivacySettings[(PrivacySettingId)9] = (PrivacySettingValue)(ShowingProfileCustomizationSetting ? (-1) : 0);
				CommittedSettings.PrivacySettings[(PrivacySettingId)5] = (PrivacySettingValue)(ShowingMusingSharingSetting ? (-1) : 0);
			}
			else if ((int)base.State.BasicAccountInfoStep.NewAccounType == 1)
			{
				CommittedSettings.PrivacySettings[(PrivacySettingId)1] = (PrivacySettingValue)(-1);
				CommittedSettings.PrivacySettings[(PrivacySettingId)3] = (PrivacySettingValue)(-1);
				CommittedSettings.PrivacySettings[(PrivacySettingId)6] = (PrivacySettingValue)(-1);
				CommittedSettings.PrivacySettings[(PrivacySettingId)0] = (PrivacySettingValue)(-1);
				CommittedSettings.PrivacySettings[(PrivacySettingId)12] = (PrivacySettingValue)(-1);
				CommittedSettings.PrivacySettings[(PrivacySettingId)9] = (PrivacySettingValue)(-1);
				CommittedSettings.PrivacySettings[(PrivacySettingId)5] = (PrivacySettingValue)(-1);
				CommittedSettings.PrivacySettings[(PrivacySettingId)13] = UsageCollectionDefault;
			}
			else
			{
				CommittedSettings.PrivacySettings[(PrivacySettingId)1] = (PrivacySettingValue)(-1);
				CommittedSettings.PrivacySettings[(PrivacySettingId)3] = (PrivacySettingValue)0;
				CommittedSettings.PrivacySettings[(PrivacySettingId)6] = (PrivacySettingValue)(-1);
				CommittedSettings.PrivacySettings[(PrivacySettingId)0] = (PrivacySettingValue)0;
				CommittedSettings.PrivacySettings[(PrivacySettingId)12] = (PrivacySettingValue)0;
				CommittedSettings.PrivacySettings[(PrivacySettingId)9] = (PrivacySettingValue)0;
				CommittedSettings.PrivacySettings[(PrivacySettingId)5] = (PrivacySettingValue)0;
				CommittedSettings.PrivacySettings[(PrivacySettingId)13] = UsageCollectionDefault;
			}
		}
		if (FamilySettings == null)
		{
			FamilySettings = new FamilySettings();
			if (SignIn.Instance.SignedIn)
			{
				FamilySettings.UserId = SignIn.Instance.LastSignedInUserId;
			}
		}
	}

	private void SetUncommittedSettings()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Invalid comparison between Unknown and I4
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Invalid comparison between Unknown and I4
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		if (((ModelItem)this).IsDisposed)
		{
			return;
		}
		SetDefaultSettings();
		if (AllowMicrosoftCommunications != null)
		{
			AllowMicrosoftCommunications.Value = CommittedSettings.AllowZuneEmails;
		}
		if (AllowPartnerCommunications != null)
		{
			AllowPartnerCommunications.Value = CommittedSettings.AllowPartnerEmails;
		}
		if (UsageCollection != null && CommittedSettings.PrivacySettings.ContainsKey((PrivacySettingId)13))
		{
			PrivacySettingValue val = CommittedSettings.PrivacySettings[(PrivacySettingId)13];
			UsageCollection.Value = (int)val == 2;
		}
		if (_privacySettings != null)
		{
			foreach (object privacySetting in _privacySettings)
			{
				PrivacySettingChoice privacySettingChoice = (PrivacySettingChoice)privacySetting;
				if ((int)privacySettingChoice.SettingId == -1)
				{
					SetUncommittedSpecialProperties(privacySettingChoice);
				}
				else if (CommittedSettings.PrivacySettings.ContainsKey(privacySettingChoice.SettingId))
				{
					privacySettingChoice.SettingValue = CommittedSettings.PrivacySettings[privacySettingChoice.SettingId];
				}
			}
		}
		if (FamilySettingsChoices == null)
		{
			return;
		}
		FamilySettings familySettings = SignIn.Instance.FamilySettings;
		if (familySettings == null && base.State.TermsOfServiceStep.IsEnabled && !string.IsNullOrEmpty(base.State.TermsOfServiceStep.Username))
		{
			int num = 0;
			num = SignIn.GetUserIdFromPassportId(base.State.TermsOfServiceStep.Username);
			if (num != 0)
			{
				familySettings = new FamilySettings(num);
			}
		}
		if (familySettings == null)
		{
			return;
		}
		FamilySettings.Settings = new Dictionary<string, FamilySetting>(familySettings.Settings);
		foreach (object familySettingsChoice in FamilySettingsChoices)
		{
			FamilySettingChoice familySettingChoice = (FamilySettingChoice)familySettingsChoice;
			RatingSystemList.Instance.GetRatingSystem(familySettingChoice.SettingId);
			if (familySettings.Settings.ContainsKey(familySettingChoice.SettingId))
			{
				familySettingChoice.SettingValue = familySettingChoice.GetSettingValueById(familySettings.Settings[familySettingChoice.SettingId].RatingLevel);
				familySettingChoice.BlockUnrated.Value = familySettings.Settings[familySettingChoice.SettingId].BlockUnrated;
			}
		}
	}

	private void SetUncommittedSpecialProperties(PrivacySettingChoice multiSetting)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Invalid comparison between Unknown and I4
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Invalid comparison between Unknown and I4
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Invalid comparison between Unknown and I4
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Invalid comparison between Unknown and I4
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Invalid comparison between Unknown and I4
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Invalid comparison between Unknown and I4
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Invalid comparison between Unknown and I4
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Invalid comparison between Unknown and I4
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Invalid comparison between Unknown and I4
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		if (CommittedSettings != null && multiSetting != null && (int)multiSetting.SettingId == -1 && multiSetting.InfoSettings == PrivacyInfoSettings.AllSocial)
		{
			multiSetting.SettingValue = (PrivacySettingValue)(-1);
			PrivacySettingValue val = (PrivacySettingValue)((!CommittedSettings.PrivacySettings.ContainsKey((PrivacySettingId)0)) ? (-1) : ((int)CommittedSettings.PrivacySettings[(PrivacySettingId)0]));
			PrivacySettingValue val2 = (PrivacySettingValue)((!CommittedSettings.PrivacySettings.ContainsKey((PrivacySettingId)12)) ? (-1) : ((int)CommittedSettings.PrivacySettings[(PrivacySettingId)12]));
			PrivacySettingValue val3 = (PrivacySettingValue)((!CommittedSettings.PrivacySettings.ContainsKey((PrivacySettingId)5)) ? (-1) : ((int)CommittedSettings.PrivacySettings[(PrivacySettingId)5]));
			PrivacySettingValue val4 = (PrivacySettingValue)((!CommittedSettings.PrivacySettings.ContainsKey((PrivacySettingId)9)) ? (-1) : ((int)CommittedSettings.PrivacySettings[(PrivacySettingId)9]));
			if ((int)val != -1 && ((int)multiSetting.SettingValue == -1 || multiSetting.SettingValue > val))
			{
				multiSetting.SettingValue = val;
			}
			if ((int)val2 != -1 && ((int)multiSetting.SettingValue == -1 || multiSetting.SettingValue > val2))
			{
				multiSetting.SettingValue = val2;
			}
			if ((int)val3 != -1 && ((int)multiSetting.SettingValue == -1 || multiSetting.SettingValue > val3))
			{
				multiSetting.SettingValue = val3;
			}
			if ((int)val4 != -1 && ((int)multiSetting.SettingValue == -1 || multiSetting.SettingValue > val4))
			{
				multiSetting.SettingValue = val4;
			}
		}
	}

	private void ResetSettings()
	{
		AllowMicrosoftCommunications = null;
		AllowPartnerCommunications = null;
		UsageCollection = null;
		PrivacySettings = null;
		FamilySettingsChoices = null;
		FamilySettings = null;
		if (_owner.CurrentPage == this)
		{
			InitializeSettings();
		}
	}

	protected void InitializeSettings()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Expected O, but got Unknown
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Expected O, but got Unknown
		if (((ModelItem)this).IsDisposed)
		{
			return;
		}
		if ((ShowSettings & PrivacyInfoSettings.AllowMicrosoftCommunications) == PrivacyInfoSettings.AllowMicrosoftCommunications && _allowMicrosoftCommunications == null && CountrySupportsNewsletterOptions)
		{
			_allowMicrosoftCommunications = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_COMM_DESC));
		}
		if ((ShowSettings & PrivacyInfoSettings.AllowPartnerCommunications) == PrivacyInfoSettings.AllowPartnerCommunications && _allowPartnerCommunications == null && CountrySupportsNewsletterOptions)
		{
			_allowPartnerCommunications = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_PARTNER_DESC));
		}
		bool flag = (ShowSettings & PrivacyInfoSettings.NoNewsletterSettings) != 0;
		if (PrivacySettings == null && flag)
		{
			ArrayList arrayList = new ArrayList();
			if ((ShowSettings & PrivacyInfoSettings.AllSocial) == PrivacyInfoSettings.AllSocial)
			{
				arrayList.Add(new PrivacySettingChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_ALL_SOCIAL_DESC), Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_ALL_SOCIAL_HEAD), PrivacySettingChoice.AllowFriendDenyChoices, (PrivacySettingId)(-1), PrivacyInfoSettings.AllSocial));
			}
			if ((ShowSettings & PrivacyInfoSettings.AllowExplicitContent) == PrivacyInfoSettings.AllowExplicitContent)
			{
				StringId stringId = StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_EXPLICIT_DESC;
				StringId stringId2 = StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_EXPLICIT_HEAD;
				string linkDescription = string.Empty;
				string linkUrl = string.Empty;
				if (FeatureEnablement.IsFeatureEnabled((Features)11) || FeatureEnablement.IsFeatureEnabled((Features)10))
				{
					linkDescription = Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_EXPLICIT_LINK);
					linkUrl = "http://go.microsoft.com/fwlink/?LinkId=218881";
					if (FeatureEnablement.IsFeatureEnabled((Features)28))
					{
						stringId = StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_EXPLICIT_DESC2;
						stringId2 = StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_EXPLICIT_HEAD2;
					}
					else
					{
						stringId = StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_EXPLICIT_DESC3;
						stringId2 = StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_EXPLICIT_HEAD3;
					}
				}
				arrayList.Add(new PrivacySettingChoice((IModelItemOwner)(object)this, Shell.LoadString(stringId), Shell.LoadString(stringId2), PrivacySettingChoice.AllowDenyChoices, (PrivacySettingId)1, PrivacyInfoSettings.AllowExplicitContent, linkDescription, linkUrl));
			}
			if ((ShowSettings & PrivacyInfoSettings.AllowFriends) == PrivacyInfoSettings.AllowFriends)
			{
				arrayList.Add(new PrivacySettingChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_NEWFRIEND_DESC), Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_NEWFRIEND_HEAD), PrivacySettingChoice.AllowDenyChoices, (PrivacySettingId)3, PrivacyInfoSettings.AllowFriends));
			}
			if ((ShowSettings & PrivacyInfoSettings.AllowPurchase) == PrivacyInfoSettings.AllowPurchase)
			{
				arrayList.Add(new PrivacySettingChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_PURCHASE_DESC), Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_PURCHASE_HEAD), PrivacySettingChoice.AllowDenyChoices, (PrivacySettingId)6, PrivacyInfoSettings.AllowPurchase));
			}
			if ((ShowSettings & PrivacyInfoSettings.Communications) == PrivacyInfoSettings.Communications)
			{
				arrayList.Add(new PrivacySettingChoice((IModelItemOwner)(object)this, Shell.LoadString(base.ParentAccount ? StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_COMM_DESC : StringId.IDS_ACCOUNT_CREATION_ZUNE_SOCIAL_COMM_DESC), Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_SOCIAL_COMM_HEADER), PrivacySettingChoice.AllowFriendDenyChoices, (PrivacySettingId)0, PrivacyInfoSettings.Communications));
			}
			if ((ShowSettings & PrivacyInfoSettings.FriendsSharing) == PrivacyInfoSettings.FriendsSharing)
			{
				arrayList.Add(new PrivacySettingChoice((IModelItemOwner)(object)this, Shell.LoadString(base.ParentAccount ? StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_FRIENDS_DESC : StringId.IDS_ACCOUNT_CREATION_ZUNE_FRIENDS_DESC), Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_FRIENDS_HEAD), PrivacySettingChoice.AllowFriendDenyChoices, (PrivacySettingId)12, PrivacyInfoSettings.FriendsSharing));
			}
			if ((ShowSettings & PrivacyInfoSettings.MusicSharing) == PrivacyInfoSettings.MusicSharing)
			{
				arrayList.Add(new PrivacySettingChoice((IModelItemOwner)(object)this, Shell.LoadString(base.ParentAccount ? StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_MUSIC_DESC : StringId.IDS_ACCOUNT_CREATION_SOCIAL_PROP_DESC), Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_SOCIAL_PROP_HEADER), PrivacySettingChoice.AllowFriendDenyChoices, (PrivacySettingId)5, PrivacyInfoSettings.MusicSharing));
			}
			if ((ShowSettings & PrivacyInfoSettings.ProfileCustomization) == PrivacyInfoSettings.ProfileCustomization)
			{
				arrayList.Add(new PrivacySettingChoice((IModelItemOwner)(object)this, Shell.LoadString(base.ParentAccount ? StringId.IDS_ACCOUNT_CREATION_ZUNE_CHILD_PROFILE_DESC : StringId.IDS_ACCOUNT_CREATION_ZUNE_PROFILE_DESC), Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_ZUNE_PROFILE_HEAD), PrivacySettingChoice.AllowFriendDenyChoices, (PrivacySettingId)9, PrivacyInfoSettings.ProfileCustomization));
			}
			if ((ShowSettings & PrivacyInfoSettings.UsageCollection) == PrivacyInfoSettings.UsageCollection && _usageCollection == null)
			{
				_usageCollection = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_PERSONALIZE_DESC));
			}
			PrivacySettings = arrayList;
		}
		if (FamilySettingsChoices == null && flag && RatingSystemList.Instance.RatingSystems != null && (ShowSettings & PrivacyInfoSettings.AllowExplicitContent) == PrivacyInfoSettings.AllowExplicitContent)
		{
			ArrayList arrayList2 = new ArrayList();
			foreach (string ratingSystem2 in RatingSystemList.Instance.RatingSystems)
			{
				RatingSystem ratingSystem = RatingSystemList.Instance.GetRatingSystem(ratingSystem2);
				ArrayList arrayList3 = new ArrayList();
				RatingValue[] ratings = ((RatingSystemBase)ratingSystem).Ratings;
				foreach (RatingValue val in ratings)
				{
					if (!val.TreatAsUnrated)
					{
						arrayList3.Add(new FamilySettingValue(ratingSystem.GetRatingName(val.Order), val.Order));
					}
				}
				arrayList2.Add(new FamilySettingChoice((IModelItemOwner)(object)this, ratingSystem.Title, ratingSystem.Description, ratingSystem.BlockText, ((RatingSystemBase)ratingSystem).Name, ((RatingSystemBase)ratingSystem).ShowBlockUnrated, arrayList3));
			}
			FamilySettingsChoices = arrayList2;
		}
		SetUncommittedSettings();
	}
}
