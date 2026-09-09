using System;
using System.Collections;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.User;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class SignIn : ModelItem
{
	private const int m_iInvalidUserId = 0;

	private static SignIn s_instance;

	private static object s_cs = new object();

	private static TimeSpan s_subscriptionEndingWarning = TimeSpan.FromDays(7.0);

	private static TimeSpan s_subscriptionFreeTrackFirstExpireWarning = TimeSpan.FromDays(7.0);

	private static TimeSpan s_subscriptionFreeTrackSecondExpireWarning = TimeSpan.FromDays(2.0);

	private bool m_initialized;

	private bool m_fSigningIn;

	private bool m_fSignedIn;

	private bool m_fSignedInWithSubscription;

	private string m_strSignedInUsername;

	private uint m_dwSignedInGeoId;

	private int m_iLastSignedInUserId;

	private string m_lastSignedInUsername;

	private Guid m_lastSignedInUserGuid;

	private bool m_lastSignedInUserHadActiveSubscription;

	private string m_zunetag;

	private string m_countryCode;

	private Image m_zuneTile;

	private string m_zuneTilePath;

	private Guid m_userguid;

	private string m_tempPasswordStorage;

	private int m_pointsBalance;

	private int m_subscriptionFreeTrackBalance;

	private DateTime m_subscriptionFreeTrackExpiration;

	private bool m_subscriptionFreeTrackExpiring;

	private bool m_subscriptionFreeTrackExpiringWithinDays;

	private IList m_persistedUsernames;

	private string m_strErrorMessage;

	private string m_strErrorWebHelpUrl;

	private HRESULT m_hrError;

	private bool m_fErrorTermsOfService;

	private bool m_fErrorTermsOfServiceForChild;

	private bool m_fRegionMismatchError;

	private bool m_fErrorHttpGone;

	private bool m_fErrorCredentials;

	private bool m_fSubscriptionMachineCountExceeded;

	private bool m_fShowLabelTakedownWarning;

	private bool m_fSubscriptionAvailable;

	private bool m_fSubscriptionBillingViolation;

	private bool m_fSubscriptionExpiring;

	private bool m_fSubscriptionExpired;

	private bool m_fSubscriptionTrialsAvailable;

	private bool m_fIsParentallyControlled;

	private bool m_fIsLightWeight;

	private bool m_noZuneAccountError;

	private DateTime m_subscriptionEndDate;

	private ulong m_subscriptionId;

	private ulong m_subscriptionRenewalId;

	private FamilySettings m_familySettings;

	private Version m_maxConnectedPhoneVersion;

	private Version m_maxRegisteredPhoneVersion;

	private Version m_maxWindowsPhoneVersion;

	private ITunerInfoHandler m_tunerHandler;

	public static SignIn Instance
	{
		get
		{
			if (s_instance == null)
			{
				lock (s_cs)
				{
					if (s_instance == null)
					{
						s_instance = new SignIn();
					}
				}
			}
			return s_instance;
		}
	}

	public bool Initialized
	{
		get
		{
			return m_initialized;
		}
		private set
		{
			if (m_initialized != value)
			{
				m_initialized = value;
				((ModelItem)this).FirePropertyChanged("Initialized");
			}
		}
	}

	public bool SigningIn
	{
		get
		{
			return m_fSigningIn;
		}
		set
		{
			if (m_fSigningIn != value)
			{
				m_fSigningIn = value;
				((ModelItem)this).FirePropertyChanged("SigningIn");
			}
		}
	}

	public bool SignedIn
	{
		get
		{
			return m_fSignedIn;
		}
		set
		{
			if (m_fSignedIn != value)
			{
				m_fSignedIn = value;
				((ModelItem)this).FirePropertyChanged("SignedIn");
			}
		}
	}

	public bool SignedInWithSubscription
	{
		get
		{
			return m_fSignedInWithSubscription;
		}
		set
		{
			if (m_fSignedInWithSubscription != value)
			{
				m_fSignedInWithSubscription = value;
				((ModelItem)this).FirePropertyChanged("SignedInWithSubscription");
			}
		}
	}

	public string SignedInUsername
	{
		get
		{
			return m_strSignedInUsername;
		}
		set
		{
			if (m_strSignedInUsername != value)
			{
				m_strSignedInUsername = value;
				((ModelItem)this).FirePropertyChanged("SignedInUsername");
			}
		}
	}

	public uint SignedInGeoId
	{
		get
		{
			return m_dwSignedInGeoId;
		}
		set
		{
			if (m_dwSignedInGeoId != value)
			{
				m_dwSignedInGeoId = value;
				((ModelItem)this).FirePropertyChanged("SignedInGeoId");
			}
		}
	}

	public int LastSignedInUserId
	{
		get
		{
			GetLastSignedIdUser();
			return m_iLastSignedInUserId;
		}
		set
		{
			if (m_iLastSignedInUserId != value)
			{
				m_iLastSignedInUserId = value;
				((ModelItem)this).FirePropertyChanged("LastSignedInUserId");
			}
		}
	}

	public string LastSignedInUsername
	{
		get
		{
			if (m_lastSignedInUsername == null && LastSignedInUserId > 0)
			{
				m_lastSignedInUsername = GetPassportIdFromUserId(LastSignedInUserId);
			}
			return m_lastSignedInUsername;
		}
		private set
		{
			if (m_lastSignedInUsername != value)
			{
				m_lastSignedInUsername = value;
				((ModelItem)this).FirePropertyChanged("LastSignedInUsername");
			}
		}
	}

	public Guid LastSignedInUserGuid
	{
		get
		{
			GetLastSignedIdUser();
			return m_lastSignedInUserGuid;
		}
		set
		{
			if (m_lastSignedInUserGuid != value)
			{
				m_lastSignedInUserGuid = value;
				((ModelItem)this).FirePropertyChanged("LastSignedInUserGuid");
			}
		}
	}

	public bool LastSignedInUserHadActiveSubscription
	{
		get
		{
			return m_lastSignedInUserHadActiveSubscription;
		}
		private set
		{
			if (m_lastSignedInUserHadActiveSubscription != value)
			{
				m_lastSignedInUserHadActiveSubscription = value;
				((ModelItem)this).FirePropertyChanged("LastSignedInUserHadActiveSubscription");
			}
		}
	}

	public Image ZuneTile
	{
		get
		{
			return m_zuneTile;
		}
		set
		{
			if (m_zuneTile != value)
			{
				m_zuneTile = value;
				((ModelItem)this).FirePropertyChanged("ZuneTile");
			}
		}
	}

	public string ZuneTag
	{
		get
		{
			return m_zunetag;
		}
		set
		{
			if (m_zunetag != value)
			{
				m_zunetag = value;
				((ModelItem)this).FirePropertyChanged("ZuneTag");
			}
		}
	}

	public string CountryCode
	{
		get
		{
			return m_countryCode;
		}
		set
		{
			if (m_countryCode != value)
			{
				m_countryCode = value;
				((ModelItem)this).FirePropertyChanged("CountryCode");
			}
		}
	}

	public Guid UserGuid
	{
		get
		{
			return m_userguid;
		}
		set
		{
			if (m_userguid != value)
			{
				m_userguid = value;
				((ModelItem)this).FirePropertyChanged("UserGuid");
			}
		}
	}

	public string TempPasswordStorage
	{
		get
		{
			return m_tempPasswordStorage;
		}
		set
		{
			if (m_tempPasswordStorage != value)
			{
				m_tempPasswordStorage = value;
				((ModelItem)this).FirePropertyChanged("TempPasswordStorage");
			}
		}
	}

	public int PointsBalance
	{
		get
		{
			UpdatePointsBalance();
			return m_pointsBalance;
		}
	}

	public DateTime SubscriptionFreeTrackExpiration
	{
		get
		{
			UpdateSubscriptionFreeTrackExpiration();
			return m_subscriptionFreeTrackExpiration;
		}
		private set
		{
			if (m_subscriptionFreeTrackExpiration != value)
			{
				m_subscriptionFreeTrackExpiration = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionFreeTrackExpiration");
			}
		}
	}

	public bool SubscriptionFreeTrackExpiring
	{
		get
		{
			return m_subscriptionFreeTrackExpiring;
		}
		private set
		{
			if (m_subscriptionFreeTrackExpiring != value)
			{
				m_subscriptionFreeTrackExpiring = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionFreeTrackExpiring");
			}
		}
	}

	public bool SubscriptionFreeTrackExpiringWithinDays
	{
		get
		{
			return m_subscriptionFreeTrackExpiringWithinDays;
		}
		private set
		{
			if (m_subscriptionFreeTrackExpiringWithinDays != value)
			{
				m_subscriptionFreeTrackExpiringWithinDays = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionFreeTrackExpiringWithinDays");
			}
		}
	}

	public int SubscriptionFreeTrackBalance
	{
		get
		{
			UpdateSubscriptionFreeTrackBalance();
			return m_subscriptionFreeTrackBalance;
		}
		private set
		{
			if (m_subscriptionFreeTrackBalance != value)
			{
				m_subscriptionFreeTrackBalance = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionFreeTrackBalance");
			}
		}
	}

	public IList PersistedUsernames
	{
		get
		{
			if (m_persistedUsernames == null)
			{
				m_persistedUsernames = ZuneApplication.Service.GetPersistedUsernames();
			}
			return m_persistedUsernames;
		}
		set
		{
			if (m_persistedUsernames != value)
			{
				m_persistedUsernames = value;
				((ModelItem)this).FirePropertyChanged("PersistedUsernames");
			}
		}
	}

	public bool SubscriptionMachineCountExceeded
	{
		get
		{
			return m_fSubscriptionMachineCountExceeded;
		}
		private set
		{
			if (m_fSubscriptionMachineCountExceeded != value)
			{
				m_fSubscriptionMachineCountExceeded = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionMachineCountExceeded");
			}
		}
	}

	public bool ShowLabelTakedownWarning
	{
		get
		{
			return m_fShowLabelTakedownWarning;
		}
		private set
		{
			if (m_fShowLabelTakedownWarning != value)
			{
				m_fShowLabelTakedownWarning = value;
				((ModelItem)this).FirePropertyChanged("ShowLabelTakedownWarning");
			}
		}
	}

	public bool SubscriptionBillingViolation
	{
		get
		{
			return m_fSubscriptionBillingViolation;
		}
		private set
		{
			if (m_fSubscriptionBillingViolation != value)
			{
				m_fSubscriptionBillingViolation = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionBillingViolation");
			}
		}
	}

	public bool SubscriptionAvailable
	{
		get
		{
			return m_fSubscriptionAvailable;
		}
		private set
		{
			if (m_fSubscriptionAvailable != value)
			{
				m_fSubscriptionAvailable = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionAvailable");
			}
		}
	}

	public bool SubscriptionTrialsAvailable
	{
		get
		{
			return m_fSubscriptionTrialsAvailable;
		}
		private set
		{
			if (m_fSubscriptionTrialsAvailable != value)
			{
				m_fSubscriptionTrialsAvailable = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionTrialsAvailable");
			}
		}
	}

	public HRESULT SignInError
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_hrError;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (m_hrError != value)
			{
				m_hrError = value;
				((ModelItem)this).FirePropertyChanged("SignInError");
			}
		}
	}

	public string SignInErrorMessage
	{
		get
		{
			return m_strErrorMessage;
		}
		set
		{
			if (m_strErrorMessage != value)
			{
				m_strErrorMessage = value;
				((ModelItem)this).FirePropertyChanged("SignInErrorMessage");
			}
		}
	}

	public string SignInErrorWebHelpUrl
	{
		get
		{
			return m_strErrorWebHelpUrl;
		}
		set
		{
			if (m_strErrorWebHelpUrl != value)
			{
				m_strErrorWebHelpUrl = value;
				((ModelItem)this).FirePropertyChanged("SignInErrorWebHelpUrl");
			}
		}
	}

	public bool SignInTermsOfServiceError
	{
		get
		{
			return m_fErrorTermsOfService;
		}
		set
		{
			if (m_fErrorTermsOfService != value)
			{
				m_fErrorTermsOfService = value;
				((ModelItem)this).FirePropertyChanged("SignInTermsOfServiceError");
			}
		}
	}

	public bool SignInRegionMismatchError
	{
		get
		{
			return m_fRegionMismatchError;
		}
		set
		{
			if (m_fRegionMismatchError != value)
			{
				m_fRegionMismatchError = value;
				((ModelItem)this).FirePropertyChanged("SignInRegionMismatchError");
			}
		}
	}

	public bool SignInTermsOfServiceErrorForChild
	{
		get
		{
			return m_fErrorTermsOfServiceForChild;
		}
		set
		{
			if (m_fErrorTermsOfServiceForChild != value)
			{
				m_fErrorTermsOfServiceForChild = value;
				((ModelItem)this).FirePropertyChanged("SignInTermsOfServiceErrorForChild");
			}
		}
	}

	public bool SignInHttpGoneError
	{
		get
		{
			return m_fErrorHttpGone;
		}
		set
		{
			if (m_fErrorHttpGone != value)
			{
				m_fErrorHttpGone = value;
				((ModelItem)this).FirePropertyChanged("SignInHttpGoneError");
			}
		}
	}

	public bool SignInCredentialsError
	{
		get
		{
			return m_fErrorCredentials;
		}
		set
		{
			if (m_fErrorCredentials != value)
			{
				m_fErrorCredentials = value;
				((ModelItem)this).FirePropertyChanged("SignInCredentialsError");
			}
		}
	}

	public bool SignInNoZuneAccountError
	{
		get
		{
			return m_noZuneAccountError;
		}
		private set
		{
			if (m_noZuneAccountError != value)
			{
				m_noZuneAccountError = value;
				((ModelItem)this).FirePropertyChanged("SignInNoZuneAccountError");
			}
		}
	}

	public bool IsParentallyControlled
	{
		get
		{
			return m_fIsParentallyControlled;
		}
		private set
		{
			if (m_fIsParentallyControlled != value)
			{
				m_fIsParentallyControlled = value;
				((ModelItem)this).FirePropertyChanged("IsParentallyControlled");
			}
		}
	}

	public bool IsLightWeight
	{
		get
		{
			return m_fIsLightWeight;
		}
		private set
		{
			if (m_fIsLightWeight != value)
			{
				m_fIsLightWeight = value;
				((ModelItem)this).FirePropertyChanged("IsLightWeight");
			}
		}
	}

	public bool SubscriptionExpiring
	{
		get
		{
			return m_fSubscriptionExpiring;
		}
		private set
		{
			if (m_fSubscriptionExpiring != value)
			{
				m_fSubscriptionExpiring = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionExpiring");
			}
		}
	}

	public bool SubscriptionExpired
	{
		get
		{
			return m_fSubscriptionExpired;
		}
		private set
		{
			if (m_fSubscriptionExpired != value)
			{
				m_fSubscriptionExpired = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionExpired");
			}
		}
	}

	public DateTime SubscriptionEndDate
	{
		get
		{
			return m_subscriptionEndDate;
		}
		private set
		{
			if (m_subscriptionEndDate != value)
			{
				m_subscriptionEndDate = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionExpired");
				((ModelItem)this).FirePropertyChanged("SubscriptionHasEndDate");
			}
		}
	}

	public ulong SubscriptionId
	{
		get
		{
			return m_subscriptionId;
		}
		private set
		{
			if (m_subscriptionId != value)
			{
				m_subscriptionId = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionId");
				((ModelItem)this).FirePropertyChanged("HasSubscriptionId");
			}
		}
	}

	public bool HasSubscriptionId => m_subscriptionId != 0;

	public ulong SubscriptionRenewalId
	{
		get
		{
			return m_subscriptionRenewalId;
		}
		private set
		{
			if (m_subscriptionRenewalId != value)
			{
				m_subscriptionRenewalId = value;
				((ModelItem)this).FirePropertyChanged("SubscriptionRenewalId");
			}
		}
	}

	public bool SubscriptionHasEndDate
	{
		get
		{
			if (m_subscriptionEndDate != DateTime.MaxValue)
			{
				return m_subscriptionEndDate.Year < 9999;
			}
			return false;
		}
	}

	private Version MaxWindowsPhoneVersion
	{
		get
		{
			return m_maxWindowsPhoneVersion;
		}
		set
		{
			if (m_maxWindowsPhoneVersion != value)
			{
				m_maxWindowsPhoneVersion = value;
				((ModelItem)this).FirePropertyChanged("WindowsPhoneClientType");
			}
		}
	}

	private Version MaxConnectedPhoneVersion
	{
		get
		{
			return m_maxConnectedPhoneVersion;
		}
		set
		{
			if (m_maxConnectedPhoneVersion != value)
			{
				m_maxConnectedPhoneVersion = value;
				UpdateMaxWindowsPhoneVersion();
			}
		}
	}

	private Version MaxRegisteredPhoneVersion
	{
		get
		{
			return m_maxRegisteredPhoneVersion;
		}
		set
		{
			if (m_maxRegisteredPhoneVersion != value)
			{
				m_maxRegisteredPhoneVersion = value;
				UpdateMaxWindowsPhoneVersion();
			}
		}
	}

	public string WindowsPhoneClientType => GetWindowsPhoneClientType(MaxWindowsPhoneVersion);

	private ITunerInfoHandler TunerHandler
	{
		get
		{
			if (m_tunerHandler == null)
			{
				m_tunerHandler = TunerInfoHandlerFactory.CreateTunerInfoHandler();
				m_tunerHandler.OnChanged += OnTunerInfoChanged;
			}
			return m_tunerHandler;
		}
	}

	public string PseudoPassword => "********";

	public FamilySettings FamilySettings
	{
		get
		{
			if (SignedIn)
			{
				if (m_familySettings == null)
				{
					m_familySettings = new FamilySettings(LastSignedInUserId);
				}
			}
			else
			{
				m_familySettings = null;
			}
			return m_familySettings;
		}
		private set
		{
			m_familySettings = value;
			((ModelItem)this).FirePropertyChanged("FamilySettings");
		}
	}

	internal event EventHandler SignInStatusUpdatedEvent;

	private void UpdatePointsBalance()
	{
		int pointsBalance = ZuneApplication.Service.GetPointsBalance();
		if (pointsBalance != m_pointsBalance)
		{
			m_pointsBalance = pointsBalance;
			((ModelItem)this).FirePropertyChanged("PointsBalance");
		}
	}

	public void UpdateSubscriptionFreeTrackBalance()
	{
		SubscriptionFreeTrackBalance = ZuneApplication.Service.GetSubscriptionFreeTrackBalance();
	}

	private void UpdateMaxWindowsPhoneVersion()
	{
		if (MaxConnectedPhoneVersion == null)
		{
			MaxWindowsPhoneVersion = MaxRegisteredPhoneVersion;
		}
		else if (MaxRegisteredPhoneVersion == null)
		{
			MaxWindowsPhoneVersion = MaxConnectedPhoneVersion;
		}
		else
		{
			MaxWindowsPhoneVersion = ((MaxRegisteredPhoneVersion > MaxConnectedPhoneVersion) ? MaxRegisteredPhoneVersion : MaxConnectedPhoneVersion);
		}
	}

	private string GetWindowsPhoneClientType(Version phoneVersion)
	{
		string text = string.Empty;
		if (phoneVersion != null)
		{
			text = $"{phoneVersion.Major}.{phoneVersion.Minor}";
		}
		return Service.Instance.GetPhoneClientType(text);
	}

	public void CheckConnectedPhoneForNewerFirmware()
	{
		MaxConnectedPhoneVersion = GetConnectedPhoneFirmwareVersion();
		UpdateMaxWindowsPhoneVersion();
	}

	private Version GetConnectedPhoneFirmwareVersion()
	{
		Version result = null;
		UIDevice uIDevice = ApplicationMarketplaceHelper.FindConnectedPaidAppDevice();
		if (uIDevice != UIDeviceList.NullDevice && uIDevice.Class == DeviceClass.WindowsPhone && !string.IsNullOrEmpty(uIDevice.FirmwareVersion))
		{
			try
			{
				string[] array = uIDevice.FirmwareVersion.Split(new char[1] { '-' });
				result = new Version(array[0]);
			}
			catch
			{
			}
		}
		return result;
	}

	private void OnTunerInfoChanged(object oSenderUNUSED, EventArgs eargs)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(UpdatedAssociatedPhoneVersion), (DeferredInvokePriority)0);
	}

	private void UpdatedAssociatedPhoneVersion(object argsUNUSED)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Invalid comparison between Unknown and I4
		Version version = null;
		foreach (TunerInfo appStoreDevices in TunerHandler.GetAppStoreDevicesList())
		{
			if ((int)appStoreDevices.TunerType == 2)
			{
				Version version2 = new Version(appStoreDevices.TunerVersion);
				if (version == null || version2 > version)
				{
					version = version2;
				}
			}
		}
		MaxRegisteredPhoneVersion = version;
		MaxConnectedPhoneVersion = GetConnectedPhoneFirmwareVersion();
		UpdateMaxWindowsPhoneVersion();
	}

	public bool PasswordRequired(string strUsername)
	{
		return ZuneApplication.Service.SignInPasswordRequired(strUsername);
	}

	public bool SignInAtStartup(string strUsername)
	{
		return ZuneApplication.Service.SignInAtStartup(strUsername);
	}

	public bool RememberUsername(string strUsername)
	{
		bool flag = PersistedUsernames != null && PersistedUsernames.Contains(strUsername);
		if (!flag)
		{
			flag = !PasswordRequired(strUsername);
		}
		return flag;
	}

	public void RefreshAccount()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		if (ZuneApplication.Service.IsSignedIn())
		{
			ZuneApplication.Service.RefreshAccount(new AsyncCompleteHandler(OnManualSignIn));
		}
	}

	public void SignInUser(string strUsername, string strPassword)
	{
		bool fRememberUsername = RememberUsername(strUsername);
		bool fRememberPassword = !PasswordRequired(strUsername);
		bool fSignInAtStartup = SignInAtStartup(strUsername);
		SignInUser(strUsername, strPassword, fRememberUsername, fRememberPassword, fSignInAtStartup);
	}

	public void SignInUser(string strUsername, string strPassword, bool fRememberUsername, bool fRememberPassword, bool fSignInAtStartup)
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		if (SigningIn)
		{
			ZuneApplication.Service.CancelSignIn();
		}
		else if (SignedIn)
		{
			ZuneApplication.Service.SignOut();
		}
		if (strPassword == null || strPassword == PseudoPassword)
		{
			strPassword = string.Empty;
		}
		ZuneApplication.Service.SignIn(strUsername, strPassword, fRememberUsername, fRememberPassword, fSignInAtStartup, new AsyncCompleteHandler(OnManualSignIn));
		UpdateState();
	}

	public void SwitchToUser(string strUsername)
	{
		Guid guidFromPassportId = GetGuidFromPassportId(strUsername);
		int lastSignedInUserId = 0;
		if (ZuneApplication.Service.SetLastSignedInUserGuid(ref guidFromPassportId, ref lastSignedInUserId))
		{
			if (SigningIn)
			{
				ZuneApplication.Service.CancelSignIn();
			}
			else if (SignedIn)
			{
				ZuneApplication.Service.SignOut();
			}
			LastSignedInUserId = lastSignedInUserId;
			LastSignedInUserGuid = guidFromPassportId;
			UpdateState();
		}
	}

	public void CancelSignIn()
	{
		ZuneApplication.Service.CancelSignIn();
		UpdateState();
	}

	public void SignOut()
	{
		SignOut(forget: false);
	}

	private void SignOut(bool forget)
	{
		ZuneApplication.Service.SignOut();
		if (forget)
		{
			ClearLastSignedIdUser();
		}
		UpdateState();
	}

	public void RemovePersistedUsername(string persistedUsername)
	{
		ZuneApplication.Service.RemovePersistedUsername(persistedUsername);
		int userIdFromPassportId = GetUserIdFromPassportId(persistedUsername);
		if (userIdFromPassportId > 0)
		{
			UserManager.Instance.CleanupUserData(userIdFromPassportId);
		}
		PersistedUsernames = null;
		string zuneTagFromPassportId = GetZuneTagFromPassportId(persistedUsername);
		if (TagsMatch(ZuneTag, zuneTagFromPassportId))
		{
			SignOut(forget: true);
		}
	}

	public static bool TagsMatch(string zuneTag1, string zuneTag2)
	{
		return StringHelper.CaseInsensitiveCompare(zuneTag1, zuneTag2);
	}

	public static bool LiveIdsMatch(string liveId1, string liveId2)
	{
		return StringHelper.CaseInsensitiveCompare(liveId1, liveId2);
	}

	public bool IsSignedInUser(string zuneTag)
	{
		if (SignedIn)
		{
			return TagsMatch(zuneTag, m_zunetag);
		}
		return false;
	}

	public bool IsSignedInLiveId(string liveId)
	{
		if (SignedIn)
		{
			return LiveIdsMatch(liveId, SignedInUsername);
		}
		return false;
	}

	public bool IsLastSignedInLiveId(string liveId)
	{
		return LiveIdsMatch(liveId, LastSignedInUsername);
	}

	private static object GetUserFieldValues(int userId, SchemaMap columnIndex, object defaultValue)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected I4, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		object result = null;
		if (userId > 0)
		{
			int[] array = new int[1] { (int)columnIndex };
			object[] array2 = new object[1] { defaultValue };
			HRESULT fieldValues = ZuneLibrary.GetFieldValues(userId, (EListType)17, array.Length, array, array2, PlaylistManager.Instance.QueryContext);
			if (((HRESULT)(ref fieldValues)).IsSuccess)
			{
				result = array2[0];
			}
		}
		return result;
	}

	public static string GetPassportIdFromUserId(int userId)
	{
		object userFieldValues = GetUserFieldValues(userId, (SchemaMap)249, string.Empty);
		return userFieldValues as string;
	}

	public static Guid GetGuidFromPassportId(string passportId)
	{
		int userIdFromPassportId = GetUserIdFromPassportId(passportId);
		return GetGuidFromUserId(userIdFromPassportId);
	}

	public static Guid GetGuidFromUserId(int userId)
	{
		object userFieldValues = GetUserFieldValues(userId, (SchemaMap)451, Guid.Empty);
		if (userFieldValues is Guid)
		{
			return (Guid)userFieldValues;
		}
		return Guid.Empty;
	}

	public static int GetUserIdFromPassportId(string passportId)
	{
		int result = 0;
		if (!string.IsNullOrEmpty(passportId))
		{
			HRESULT val = default(HRESULT);
			((HRESULT)(ref val))._002Ector(UserManager.Instance.FindUserByPassportId(passportId, ref result));
			if (((HRESULT)(ref val)).IsError)
			{
				result = 0;
			}
		}
		return result;
	}

	public static string GetZuneTagFromPassportId(string passportId)
	{
		int userIdFromPassportId = GetUserIdFromPassportId(passportId);
		return GetZuneTagFromUserId(userIdFromPassportId);
	}

	public static string GetZuneTagFromUserId(int userId)
	{
		object userFieldValues = GetUserFieldValues(userId, (SchemaMap)453, string.Empty);
		return userFieldValues as string;
	}

	public static string GetImagePathFromPassportId(string passportId)
	{
		int userIdFromPassportId = GetUserIdFromPassportId(passportId);
		return GetImagePathFromUserId(userIdFromPassportId);
	}

	public static string GetImagePathFromUserId(int userId)
	{
		string text = GetUserFieldValues(userId, (SchemaMap)17, string.Empty) as string;
		if (!string.IsNullOrEmpty(text) && !text.StartsWith("file://"))
		{
			text = "file://" + text;
		}
		return text;
	}

	public static Image GetImageFromPassportId(string passportId)
	{
		int userIdFromPassportId = GetUserIdFromPassportId(passportId);
		return GetImageFromUserId(userIdFromPassportId);
	}

	public static Image GetImageFromUserId(int userId)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Invalid comparison between Unknown and I4
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		Image result = null;
		string imagePathFromUserId = GetImagePathFromUserId(userId);
		if (!string.IsNullOrEmpty(imagePathFromUserId))
		{
			bool flag = (int)Application.RenderingType != 0;
			result = new Image(imagePathFromUserId, ProfileImage.DefaultTileSize.Width, ProfileImage.DefaultTileSize.Height, false, flag);
		}
		return result;
	}

	public static void ErrorMessageRegionInvalid()
	{
		ErrorDialogInfo.Show(((HRESULT)(ref HRESULT._NS_E_SIGNIN_INVALID_REGION)).Int, Shell.LoadString(StringId.IDS_SIGNIN_REGION_INVALID_TITLE), Shell.LoadString(StringId.IDS_SIGNIN_REGION_INVALID_MESSAGE));
	}

	internal void SetError(HRESULT hrError)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hrError)).Int, (eErrorCondition)1);
		SignInTermsOfServiceErrorForChild = HRESULT._ZUNE_E_SIGNIN_TERMS_OF_SERVICE_CHILD == HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr);
		SignInTermsOfServiceError = HRESULT._NS_E_SIGNIN_TERMS_OF_SERVICE == HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr);
		SignInRegionMismatchError = HRESULT._NS_E_SIGNIN_INVALID_REGION == HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr);
		SignInHttpGoneError = HRESULT._NS_E_SIGNIN_HTTP_GONE == HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr);
		SignInCredentialsError = HRESULT._NS_E_SERVER_ACCESSDENIED == HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr) || HRESULT._NS_E_PASSPORT_LOGIN_FAILED == HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr) || HRESULT._NS_E_SUBSCRIPTIONSERVICE_LOGIN_FAILED == HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr) || HRESULT._NS_E_INVALID_USERNAME_AND_PASSWORD == HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr);
		SignInNoZuneAccountError = HRESULT._ZEST_E_UNAUTHENTICATED == HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr);
		SignInError = HRESULT.op_Implicit(mappedErrorDescriptionAndUrl.Hr);
		SignInErrorMessage = ((SignInTermsOfServiceError || SignInTermsOfServiceErrorForChild) ? null : mappedErrorDescriptionAndUrl.Description);
		SignInErrorWebHelpUrl = ((SignInTermsOfServiceError || SignInTermsOfServiceErrorForChild) ? null : mappedErrorDescriptionAndUrl.WebHelpUrl);
		SigningIn = false;
		if (this.SignInStatusUpdatedEvent != null)
		{
			this.SignInStatusUpdatedEvent(this, EventArgs.Empty);
		}
	}

	internal void UpdateState()
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		SubscriptionMachineCountExceeded = ZuneApplication.Service.IsSignedInWithSubscription() && !ZuneApplication.Service.CanDownloadSubscriptionContent();
		SubscriptionBillingViolation = ZuneApplication.Service.IsSignedInWithSubscription() && ZuneApplication.Service.HasSignInBillingViolation();
		ShowLabelTakedownWarning = ZuneApplication.Service.HasSignInLabelTakedown();
		SignedIn = ZuneApplication.Service.IsSignedIn();
		SigningIn = !SignedIn && ZuneApplication.Service.IsSigningIn();
		SignedInUsername = ZuneApplication.Service.GetSignedInUsername();
		SignedInGeoId = ZuneApplication.Service.GetSignedInGeoId();
		SignInErrorMessage = null;
		SignInErrorWebHelpUrl = null;
		SignInError = HRESULT._S_OK;
		SignInTermsOfServiceError = false;
		SignInTermsOfServiceErrorForChild = false;
		SignInRegionMismatchError = false;
		SignInHttpGoneError = false;
		SignInCredentialsError = false;
		SignInNoZuneAccountError = false;
		UserGuid = (Guid)(object)ZuneApplication.Service.GetUserGuid();
		UpdatePointsBalance();
		UpdateSubscriptionFreeTrackBalance();
		UpdateSubscriptionFreeTrackExpiration();
		FamilySettings = null;
		LastSignedInUsername = null;
		CountryCode = null;
		if (SignedIn)
		{
			int lastSignedInUserId = default(int);
			Guid lastSignedInUserGuid = default(Guid);
			ZuneApplication.Service.GetLastSignedInUserGuid(ref lastSignedInUserId, ref lastSignedInUserGuid);
			LastSignedInUserId = lastSignedInUserId;
			LastSignedInUserGuid = lastSignedInUserGuid;
			ZuneTag = ZuneApplication.Service.GetZuneTag();
			IsParentallyControlled = ZuneApplication.Service.IsParentallyControlled();
			IsLightWeight = ZuneApplication.Service.IsLightWeight();
			string locale = ZuneApplication.Service.GetLocale();
			if (!string.IsNullOrEmpty(locale))
			{
				string[] array = locale.Split(new char[1] { '-' });
				if (array.Length >= 2)
				{
					CountryCode = array[1];
				}
			}
			PersistedUsernames = null;
			if (TunerHandler.CanQueryTunerList())
			{
				TunerHandler.RefreshTunerList();
			}
		}
		else
		{
			ZuneTag = GetZuneTagFromUserId(LastSignedInUserId);
			IsParentallyControlled = false;
			IsLightWeight = false;
			MaxRegisteredPhoneVersion = null;
			MaxConnectedPhoneVersion = GetConnectedPhoneFirmwareVersion();
			UpdateMaxWindowsPhoneVersion();
		}
		UpdateSubscriptionState();
		UpdateStateAsyncStart();
		if (this.SignInStatusUpdatedEvent != null)
		{
			this.SignInStatusUpdatedEvent(this, EventArgs.Empty);
		}
		Initialized = true;
	}

	private void UpdateStateAsyncStart()
	{
		ThreadPool.QueueUserWorkItem(UpdateStateAsync, new object[3] { m_zuneTile, m_zuneTilePath, LastSignedInUserId });
	}

	private void UpdateStateAsync(object args)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Expected O, but got Unknown
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Invalid comparison between Unknown and I4
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Expected O, but got Unknown
		object[] array = (object[])args;
		object obj = array[0];
		Image val = (Image)((obj is Image) ? obj : null);
		string text = array[1] as string;
		int userId = (int)array[2];
		string imagePathFromUserId = GetImagePathFromUserId(userId);
		Image val2 = val;
		if (imagePathFromUserId != text)
		{
			if (!string.IsNullOrEmpty(imagePathFromUserId))
			{
				bool flag = (int)Application.RenderingType != 0;
				val2 = new Image(imagePathFromUserId, ProfileImage.DefaultTileSize.Width, ProfileImage.DefaultTileSize.Height, false, flag);
			}
			else
			{
				val2 = null;
			}
		}
		array[0] = val2;
		array[1] = imagePathFromUserId;
		Application.DeferredInvoke(new DeferredInvokeHandler(UpdateStateAsyncEnd), (object)array, (DeferredInvokePriority)1);
	}

	private void UpdateStateAsyncEnd(object args)
	{
		object[] array = (object[])args;
		object obj = array[0];
		ZuneTile = (Image)((obj is Image) ? obj : null);
		m_zuneTilePath = array[1] as string;
	}

	private void UpdateSubscriptionFreeTrackExpiration()
	{
		bool subscriptionFreeTrackExpiring = false;
		bool subscriptionFreeTrackExpiringWithinDays = false;
		DateTime dateTime = DateTime.MaxValue;
		DateTime maxValue = DateTime.MaxValue;
		DateTime maxValue2 = DateTime.MaxValue;
		if (SignedIn)
		{
			dateTime = ZuneApplication.Service.GetSubscriptionFreeTrackExpiration();
			DateTime dateTime2 = dateTime;
			DateTime minValue = DateTime.MinValue;
			maxValue = ((!(dateTime2 >= minValue.Add(s_subscriptionFreeTrackFirstExpireWarning))) ? DateTime.MinValue : dateTime.Subtract(s_subscriptionFreeTrackFirstExpireWarning));
			DateTime dateTime3 = dateTime;
			DateTime minValue2 = DateTime.MinValue;
			maxValue2 = ((!(dateTime3 >= minValue2.Add(s_subscriptionFreeTrackSecondExpireWarning))) ? DateTime.MinValue : dateTime.Subtract(s_subscriptionFreeTrackSecondExpireWarning));
			subscriptionFreeTrackExpiring = maxValue <= DateTime.UtcNow;
			subscriptionFreeTrackExpiringWithinDays = maxValue2 <= DateTime.UtcNow;
		}
		SubscriptionFreeTrackExpiration = dateTime;
		SubscriptionFreeTrackExpiring = subscriptionFreeTrackExpiring;
		SubscriptionFreeTrackExpiringWithinDays = subscriptionFreeTrackExpiringWithinDays;
	}

	private void UpdateSubscriptionState()
	{
		bool subscriptionExpired = false;
		bool subscriptionExpiring = false;
		DateTime dateTime = DateTime.MaxValue;
		SignedInWithSubscription = ZuneApplication.Service.IsSignedInWithSubscription();
		if (SignedIn)
		{
			LastSignedInUserHadActiveSubscription = SignedInWithSubscription;
			SubscriptionEndDate = ZuneApplication.Service.GetSubscriptionEndDate();
			SubscriptionId = ZuneApplication.Service.GetSubscriptionOfferId();
			SubscriptionRenewalId = ZuneApplication.Service.GetSubscriptionRenewalOfferId();
			if (ZuneApplication.Service.SubscriptionPendingCancel() || SubscriptionRenewalId == 0)
			{
				DateTime subscriptionEndDate = SubscriptionEndDate;
				DateTime minValue = DateTime.MinValue;
				dateTime = ((!(subscriptionEndDate >= minValue.Add(s_subscriptionEndingWarning))) ? DateTime.MinValue : SubscriptionEndDate.Subtract(s_subscriptionEndingWarning));
				subscriptionExpired = !SignedInWithSubscription && SubscriptionHasEndDate && SubscriptionEndDate <= DateTime.Today;
				subscriptionExpiring = SignedInWithSubscription && SubscriptionHasEndDate && SubscriptionEndDate >= DateTime.Today;
			}
		}
		else
		{
			bool lastSignedInUserHadActiveSubscription = default(bool);
			ulong subscriptionId = default(ulong);
			ZuneApplication.Service.GetLastSignedInUserSubscriptionState(ref lastSignedInUserHadActiveSubscription, ref subscriptionId);
			LastSignedInUserHadActiveSubscription = lastSignedInUserHadActiveSubscription;
			SubscriptionId = subscriptionId;
			SubscriptionEndDate = DateTime.MaxValue;
			SubscriptionRenewalId = 0uL;
		}
		SubscriptionExpired = subscriptionExpired;
		SubscriptionExpiring = subscriptionExpiring;
		SubscriptionTrialsAvailable = !SignedInWithSubscription && SubscriptionId == 0 && FeatureEnablement.IsFeatureEnabled((Features)17);
		SubscriptionAvailable = !SignedInWithSubscription && FeatureEnablement.IsFeatureEnabled((Features)14);
		if (SubscriptionExpired || (SubscriptionExpiring && dateTime <= DateTime.Today))
		{
			SubscriptionEndingDialog.Show(SubscriptionEndDate);
		}
	}

	public void UpdateUserTile()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Invalid comparison between Unknown and I4
		if (LastSignedInUserId > 0)
		{
			string imagePathFromUserId = GetImagePathFromUserId(LastSignedInUserId);
			if (!string.IsNullOrEmpty(imagePathFromUserId))
			{
				bool flag = (int)Application.RenderingType != 0;
				Image.RemoveCache(imagePathFromUserId, ProfileImage.DefaultTileSize.Height, ProfileImage.DefaultTileSize.Width, false, flag);
			}
			UserManager.Instance.RefreshUserTile(LastSignedInUserId);
		}
	}

	public void OnManualSignIn(HRESULT hr)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		if (((HRESULT)(ref hr)).IsSuccess || hr == HRESULT._E_ABORT)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredUpdateStatus), (object)null);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetError), (object)hr);
		}
	}

	public void OnAutomaticSignIn(HRESULT hr)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredUpdateStatus), (object)null);
	}

	private void DeferredUpdateStatus(object args)
	{
		UpdateState();
	}

	private void DeferredSetError(object args)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetError((HRESULT)args);
	}

	internal void Phase3Init()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected O, but got Unknown
		DeferredInvokeHandler val = null;
		if (AutomaticSignIn())
		{
			return;
		}
		if (val == null)
		{
			val = (DeferredInvokeHandler)delegate
			{
				UpdateState();
			};
		}
		Application.DeferredInvoke(val, (DeferredInvokePriority)1);
	}

	private bool AutomaticSignIn()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		bool result = false;
		string signInAtStartupUsername = ZuneApplication.Service.GetSignInAtStartupUsername();
		if (signInAtStartupUsername != null)
		{
			result = true;
			ZuneApplication.Service.SignIn(signInAtStartupUsername, "", true, true, true, new AsyncCompleteHandler(OnAutomaticSignIn));
		}
		return result;
	}

	private SignIn()
	{
		m_iLastSignedInUserId = 0;
		m_lastSignedInUserGuid = Guid.Empty;
	}

	private void GetLastSignedIdUser()
	{
		if (m_lastSignedInUserGuid == Guid.Empty || m_iLastSignedInUserId == 0)
		{
			ZuneApplication.Service.GetLastSignedInUserGuid(ref m_iLastSignedInUserId, ref m_lastSignedInUserGuid);
		}
	}

	private void ClearLastSignedIdUser()
	{
		if (ZuneApplication.Service.ClearLastSignedInUser())
		{
			m_lastSignedInUserGuid = Guid.Empty;
			m_iLastSignedInUserId = 0;
		}
	}
}
