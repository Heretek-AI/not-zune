using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using UIXControls;

namespace ZuneUI;

public class DeviceManagement : ModelItem
{
	private static UIDevice _setupDevice;

	private static bool _setupQueueHandlingLocked = false;

	private static MessageBox _deviceOOBEIncompleteDialog;

	private int[] _defaultBitRates = new int[5] { 96, 128, 192, 256, 320 };

	private string errorMessage;

	private bool validatingCreds;

	private string _friendlyNameOnDevice;

	private int _deviceNameMaxLength = 32;

	private Choice _enableMarketplaceChoice;

	private BooleanChoice _enableMarketplacePurchase;

	private MarketplaceCredentialsForDevice _marketplaceCredentials;

	private float _reservedSpaceOnDevice = float.NaN;

	private Choice _musicSyncChoice;

	private Choice _videoSyncChoice;

	private Choice _photoSyncChoice;

	private Choice _podcastSyncChoice;

	private Choice _friendSyncChoice;

	private Choice _channelSyncChoice;

	private Choice _applicationSyncChoice;

	private BooleanChoice _dontSyncDislikedContent;

	private Command _formatNotifier;

	private int _transcodeSizeLimit = -1;

	private bool _transcodeInBG;

	private bool _transcodeInBGIsSet;

	private string _transcodedFilesCachePath;

	private IList<Command> _bitRateList;

	private Choice _audioConversionChoice;

	private IList<Command> _audioConversionOptions;

	private string _audioThresholdBitRate;

	private string _audioTargetBitRate;

	private Choice _videoConversionChoice;

	private IList<Command> _videoConversionOptions;

	private string _cameraRollDestinationPath;

	private string _savedFolderDestinationPath;

	private Choice _deleteAfterReverseSyncChoice;

	private IList<Command> _deleteAfterReverseSyncOptions;

	private Choice _imageQualitySyncChoice;

	private IList<Command> _imageQualitySyncOptions;

	private SyncGroupList _syncGroups;

	private BooleanChoice _privacyChoice;

	public static bool NavigatingToWizard = false;

	internal static DeviceSetupHashtable SetupQueue = new DeviceSetupHashtable();

	private UIDevice ActiveDevice => SyncControls.Instance.CurrentDeviceOverride;

	public string ErrorMessage
	{
		get
		{
			return errorMessage;
		}
		set
		{
			if (errorMessage != value)
			{
				errorMessage = value;
				((ModelItem)this).FirePropertyChanged("ErrorMessage");
			}
		}
	}

	public bool ValidatingCredentials
	{
		get
		{
			return validatingCreds;
		}
		set
		{
			if (validatingCreds != value)
			{
				validatingCreds = value;
				((ModelItem)this).FirePropertyChanged("ValidatingCredentials");
			}
		}
	}

	public static UIDevice SetupDevice
	{
		get
		{
			return _setupDevice;
		}
		set
		{
			if (_setupDevice != value)
			{
				if (value == null)
				{
					_setupQueueHandlingLocked = false;
					SetupQueue.Remove(_setupDevice.ID);
				}
				_setupDevice = value;
				if (_setupDevice != null && !Shell.SettingsFrame.IsCurrent)
				{
					PhoneBrandingStringMap.Instance.BrandingEnabled = _setupDevice.SupportsBrandingType(DeviceBranding.WindowsPhone);
					KinBrandingStringMap.Instance.BrandingEnabled = _setupDevice.SupportsBrandingType(DeviceBranding.Kin);
				}
			}
		}
	}

	public Choice MusicSyncChoice
	{
		get
		{
			if (_musicSyncChoice == null)
			{
				_musicSyncChoice = GenerateSyncModeChoice(SyncCategory.Music);
			}
			return _musicSyncChoice;
		}
		private set
		{
			if (_musicSyncChoice != value)
			{
				_musicSyncChoice = value;
				((ModelItem)this).FirePropertyChanged("MusicSyncChoice");
			}
		}
	}

	public Choice VideoSyncChoice
	{
		get
		{
			if (_videoSyncChoice == null)
			{
				_videoSyncChoice = GenerateSyncModeChoice(SyncCategory.Video);
			}
			return _videoSyncChoice;
		}
		private set
		{
			if (_videoSyncChoice != value)
			{
				_videoSyncChoice = value;
				((ModelItem)this).FirePropertyChanged("VideoSyncChoice");
			}
		}
	}

	public Choice PhotoSyncChoice
	{
		get
		{
			if (_photoSyncChoice == null)
			{
				_photoSyncChoice = GenerateSyncModeChoice(SyncCategory.Photo);
			}
			return _photoSyncChoice;
		}
		private set
		{
			if (_photoSyncChoice != value)
			{
				_photoSyncChoice = value;
				((ModelItem)this).FirePropertyChanged("PhotoSyncChoice");
			}
		}
	}

	public Choice PodcastSyncChoice
	{
		get
		{
			if (_podcastSyncChoice == null)
			{
				_podcastSyncChoice = GenerateSyncModeChoice(SyncCategory.Podcast);
			}
			return _podcastSyncChoice;
		}
		private set
		{
			if (_podcastSyncChoice != value)
			{
				_podcastSyncChoice = value;
				((ModelItem)this).FirePropertyChanged("PodcastSyncChoice");
			}
		}
	}

	public Choice FriendSyncChoice
	{
		get
		{
			if (_friendSyncChoice == null)
			{
				_friendSyncChoice = GenerateSyncModeChoice(SyncCategory.Friend);
			}
			return _friendSyncChoice;
		}
		private set
		{
			if (_friendSyncChoice != value)
			{
				_friendSyncChoice = value;
				((ModelItem)this).FirePropertyChanged("FriendSyncChoice");
			}
		}
	}

	public Choice ChannelSyncChoice
	{
		get
		{
			if (_channelSyncChoice == null)
			{
				_channelSyncChoice = GenerateSyncModeChoice(SyncCategory.Channel);
			}
			return _channelSyncChoice;
		}
		private set
		{
			if (_channelSyncChoice != value)
			{
				_channelSyncChoice = value;
				((ModelItem)this).FirePropertyChanged("ChannelSyncChoice");
			}
		}
	}

	public Choice ApplicationSyncChoice
	{
		get
		{
			if (_applicationSyncChoice == null)
			{
				_applicationSyncChoice = GenerateSyncModeChoice(SyncCategory.Application);
			}
			return _applicationSyncChoice;
		}
		private set
		{
			if (_applicationSyncChoice != value)
			{
				_applicationSyncChoice = value;
				((ModelItem)this).FirePropertyChanged("ApplicationSyncChoice");
			}
		}
	}

	public Command FormatBegun
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_formatNotifier == null)
			{
				_formatNotifier = new Command((IModelItemOwner)(object)this);
			}
			return _formatNotifier;
		}
	}

	public BooleanChoice DontSyncDislikedContentChoice
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_dontSyncDislikedContent == null)
			{
				_dontSyncDislikedContent = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_DONT_SYNC_DISLIKED_CONTENT));
				_dontSyncDislikedContent.Value = ActiveDevice.ExcludeDislikedContent;
				((Choice)_dontSyncDislikedContent).ChosenChanged += delegate
				{
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnDontSyncDislikedContentChoiceCommit)] = CommitDeviceID;
				};
			}
			return _dontSyncDislikedContent;
		}
	}

	public Choice DeleteAfterReverseSyncChoice
	{
		get
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Expected O, but got Unknown
			if (_deleteAfterReverseSyncChoice == null)
			{
				_deleteAfterReverseSyncOptions = new List<Command>();
				_deleteAfterReverseSyncOptions.Add(new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PHOTOS_SETTINGS_LEAVE_AFTER_REVERSE_SYNC), (EventHandler)null));
				_deleteAfterReverseSyncOptions.Add(new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PHOTOS_SETTINGS_DELETE_AFTER_REVERSE_SYNC), (EventHandler)null));
				_deleteAfterReverseSyncChoice = new Choice((IModelItemOwner)(object)this);
				_deleteAfterReverseSyncChoice.Options = (IList)_deleteAfterReverseSyncOptions;
				_deleteAfterReverseSyncChoice.DefaultIndex = (ActiveDevice.DeletePicsFromPhoneAfterReverseSync ? 1 : 0);
				_deleteAfterReverseSyncChoice.DefaultValue();
				_deleteAfterReverseSyncChoice.ChosenChanged += delegate
				{
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnDeleteAfterReverseSyncCommit)] = CommitDeviceID;
				};
			}
			return _deleteAfterReverseSyncChoice;
		}
	}

	public string FriendlyNameOnDevice
	{
		get
		{
			if (_friendlyNameOnDevice == null)
			{
				_friendlyNameOnDevice = ActiveDevice.Name;
			}
			return _friendlyNameOnDevice;
		}
		set
		{
			if (IsDeviceNameValid(value, out var text))
			{
				if (_friendlyNameOnDevice != value)
				{
					_friendlyNameOnDevice = value;
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnFriendlyNameOnDeviceCommit)] = CommitDeviceID;
					((ModelItem)this).FirePropertyChanged("FriendlyNameOnDevice");
				}
				ErrorMessage = null;
			}
			else
			{
				ErrorMessage = text;
			}
		}
	}

	public MarketplaceCredentialsForDevice MarketplaceCredentials => _marketplaceCredentials;

	public Choice EnableMarketplaceChoice
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Expected O, but got Unknown
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Expected O, but got Unknown
			if (_enableMarketplaceChoice == null)
			{
				_enableMarketplaceChoice = new Choice((IModelItemOwner)(object)this);
				_enableMarketplaceChoice.Options = new Command[2]
				{
					new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_SKIP_FOR_NOW_OPTION), (EventHandler)null),
					new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ENABLE_ZUNE_MARKETPLACE_OPTION), (EventHandler)null)
				};
				_marketplaceCredentials = new MarketplaceCredentialsForDevice(null, null, ActiveDevice.PurchaseEnabled, !Shell.SettingsFrame.Wizard.IsCurrent && !string.IsNullOrEmpty(ActiveDevice.ZuneTag), ActiveDevice.ZuneTag, _enableMarketplaceChoice);
				if (Shell.SettingsFrame.Wizard.IsCurrent)
				{
					MarketplaceCredentials.Email = SignIn.GetPassportIdFromUserId(SignIn.Instance.LastSignedInUserId);
					_enableMarketplaceChoice.DefaultIndex = ((!string.IsNullOrEmpty(MarketplaceCredentials.Email)) ? 1 : 0);
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnMarketplaceCredentialsCommit)] = CommitDeviceID;
				}
				else
				{
					_enableMarketplaceChoice.DefaultIndex = ((!string.IsNullOrEmpty(ActiveDevice.ZuneTag)) ? 1 : 0);
				}
				_enableMarketplaceChoice.DefaultValue();
				_enableMarketplaceChoice.ChosenChanged += delegate
				{
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnMarketplaceCredentialsCommit)] = CommitDeviceID;
					((ModelItem)this).FirePropertyChanged("EnableMarketplaceChoice");
				};
			}
			return _enableMarketplaceChoice;
		}
	}

	public bool IsDevicePurchaseEnabled
	{
		get
		{
			if (MarketplaceCredentials != null)
			{
				if (MarketplaceCredentials.IsAssociated)
				{
					return MarketplaceCredentials.PurchaseEnabled;
				}
				return false;
			}
			return ActiveDevice.PurchaseEnabled;
		}
	}

	public bool IsAssociated
	{
		get
		{
			if (MarketplaceCredentials != null)
			{
				return MarketplaceCredentials.IsAssociated;
			}
			return !string.IsNullOrEmpty(ActiveDevice.ZuneTag);
		}
	}

	public bool DeviceHasTag
	{
		get
		{
			bool flag = false;
			if (IsPartnered)
			{
				flag = SyncControls.Instance.CurrentDeviceOverride.UserId > 0;
				if (!flag)
				{
					flag = IsAssociated;
				}
			}
			return flag;
		}
	}

	public BooleanChoice EnableMarketplacePurchase
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_enableMarketplacePurchase == null)
			{
				_enableMarketplacePurchase = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_ENABLE_DEVICE_PURCHASE));
				_enableMarketplacePurchase.Value = MarketplaceCredentials.PurchaseEnabled;
				((Choice)_enableMarketplacePurchase).ChosenChanged += delegate
				{
					MarketplaceCredentials.PurchaseEnabled = _enableMarketplacePurchase.Value;
				};
			}
			return _enableMarketplacePurchase;
		}
	}

	public bool CredentialValidationRequested
	{
		get
		{
			return true;
		}
		set
		{
			((ModelItem)this).FirePropertyChanged("CredentialValidationRequested");
		}
	}

	public DeviceRelationship DevicePartnership
	{
		get
		{
			Management management = ZuneShell.DefaultInstance.Management;
			if (management.CommitList.ContainsValue("OnSyncPartnershipCommit"))
			{
				return DeviceRelationship.Permanent;
			}
			DeviceRelationship deviceRelationship = ActiveDevice.Relationship;
			if (ActiveDevice.IsValid && deviceRelationship == DeviceRelationship.None)
			{
				management.CommitList[new ProxySettingDelegate(OnSyncPartnershipCommit)] = "OnSyncPartnershipCommit";
				deviceRelationship = DeviceRelationship.Permanent;
			}
			return deviceRelationship;
		}
		set
		{
			Management management = ZuneShell.DefaultInstance.Management;
			if (value == DeviceRelationship.Permanent)
			{
				management.CommitList[new ProxySettingDelegate(OnSyncPartnershipCommit)] = "OnSyncPartnershipCommit";
			}
			else
			{
				management.CommitList[new ProxySettingDelegate(OnSyncPartnershipCommit)] = null;
				ActiveDevice.Relationship = value;
			}
			((ModelItem)this).FirePropertyChanged("DevicePartnership");
			((ModelItem)this).FirePropertyChanged("IsPartnered");
		}
	}

	public bool IsPartnered => DevicePartnership == DeviceRelationship.Permanent;

	public bool IsCurrentDeviceNull => ActiveDevice == UIDeviceList.NullDevice;

	public string TranscodeSizeLimit
	{
		get
		{
			_ = TranscodedFilesCachePath;
			if (_transcodeSizeLimit == -1)
			{
				_transcodeSizeLimit = SingletonModelItem<UIDeviceList>.Instance.TranscodedFilesCacheSize;
			}
			return _transcodeSizeLimit.ToString();
		}
		set
		{
			int result = 1;
			if (string.IsNullOrEmpty(value) || int.TryParse(value, out result))
			{
				if (result < 1)
				{
					result = 1;
				}
				else if (result > 999999)
				{
					result = 999999;
				}
				if (_transcodeSizeLimit != result)
				{
					_transcodeSizeLimit = result;
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnTranscodeSizeLimitCommit)] = CommitDeviceID;
				}
			}
			((ModelItem)this).FirePropertyChanged("TranscodeSizeLimit");
		}
	}

	public string TranscodedFilesCachePath
	{
		get
		{
			if (_transcodedFilesCachePath == null)
			{
				_transcodedFilesCachePath = SingletonModelItem<UIDeviceList>.Instance.TranscodedFilesCachePath;
			}
			return _transcodedFilesCachePath;
		}
		private set
		{
			if (_transcodedFilesCachePath != value)
			{
				_transcodedFilesCachePath = value;
				((ModelItem)this).FirePropertyChanged("TranscodedFilesCachePath");
			}
		}
	}

	public string CameraRollDestinationPath
	{
		get
		{
			if (_cameraRollDestinationPath == null)
			{
				_cameraRollDestinationPath = ActiveDevice.CameraRollDestinationPath;
			}
			return _cameraRollDestinationPath;
		}
		private set
		{
			if (_cameraRollDestinationPath != value)
			{
				_cameraRollDestinationPath = value;
				ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnCameraRollDestinationPathCommit)] = CommitDeviceID;
				((ModelItem)this).FirePropertyChanged("CameraRollDestinationPath");
			}
		}
	}

	public string SavedFolderDestinationPath
	{
		get
		{
			if (_savedFolderDestinationPath == null)
			{
				_savedFolderDestinationPath = ActiveDevice.SavedFolderDestinationPath;
			}
			return _savedFolderDestinationPath;
		}
		private set
		{
			if (_savedFolderDestinationPath != value)
			{
				_savedFolderDestinationPath = value;
				ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnSavedFolderDestinationPathCommit)] = CommitDeviceID;
				((ModelItem)this).FirePropertyChanged("SavedFolderDestinationPath");
			}
		}
	}

	public bool TranscodeInBG
	{
		get
		{
			if (!_transcodeInBGIsSet)
			{
				_transcodeInBG = ClientConfiguration.Transcode.BackgroundTranscode;
				_transcodeInBGIsSet = true;
			}
			return _transcodeInBG;
		}
		set
		{
			if (_transcodeInBG != value)
			{
				_transcodeInBG = value;
				ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnTranscodeInBGCommit)] = null;
				((ModelItem)this).FirePropertyChanged("TranscodeInBG");
				_transcodeInBGIsSet = true;
			}
		}
	}

	public Choice ImageQualitySyncChoice
	{
		get
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Expected O, but got Unknown
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Expected O, but got Unknown
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Invalid comparison between Unknown and I4
			//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
			if (_imageQualitySyncChoice == null)
			{
				_imageQualitySyncOptions = new List<Command>();
				_imageQualitySyncOptions.Add(new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PHOTOS_SETTINGS_QUALITY_DEFAULT), (EventHandler)null));
				_imageQualitySyncOptions.Add(new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PHOTOS_SETTINGS_QUALITY_ORIGINAL), (EventHandler)null));
				_imageQualitySyncOptions.Add(new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PHOTOS_SETTINGS_QUALITY_VGA), (EventHandler)null));
				_imageQualitySyncChoice = new Choice((IModelItemOwner)(object)this);
				_imageQualitySyncChoice.Options = (IList)_imageQualitySyncOptions;
				_imageQualitySyncChoice.DefaultIndex = (((int)ActiveDevice.ImageTranscodeQuality > 0) ? ((int)ActiveDevice.ImageTranscodeQuality) : 0);
				_imageQualitySyncChoice.DefaultValue();
				_imageQualitySyncChoice.ChosenChanged += delegate
				{
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnImageQualitySyncChoiceCommit)] = CommitDeviceID;
				};
			}
			return _imageQualitySyncChoice;
		}
		private set
		{
			if (_imageQualitySyncChoice != value)
			{
				_imageQualitySyncChoice = value;
				((ModelItem)this).FirePropertyChanged("ImageQualitySyncChoice");
			}
		}
	}

	public Choice AudioConversionChoice
	{
		get
		{
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Expected O, but got Unknown
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Expected O, but got Unknown
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_005c: Expected O, but got Unknown
			if (_audioConversionChoice == null)
			{
				_audioConversionOptions = new List<Command>();
				_audioConversionOptions.Add(new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_TRANSCODE_AUDIO_NOSUPPORT_OPTION), (EventHandler)null));
				_audioConversionOptions.Add(new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_TRANSCODE_AUDIO_EXCEEDS_OPTION), (EventHandler)null));
				_audioConversionChoice = new Choice((IModelItemOwner)(object)this);
				_audioConversionChoice.Options = (IList)_audioConversionOptions;
				_audioConversionChoice.DefaultIndex = ((ActiveDevice.AudioTranscodeLimit > 0) ? 1 : 0);
				_audioConversionChoice.DefaultValue();
				_audioConversionChoice.ChosenChanged += delegate
				{
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnAudioConversionChoiceCommit)] = CommitDeviceID;
				};
			}
			return _audioConversionChoice;
		}
		private set
		{
			if (_audioConversionChoice != value)
			{
				_audioConversionChoice = value;
				((ModelItem)this).FirePropertyChanged("AudioConversionChoice");
			}
		}
	}

	public string AudioThresholdBitRate
	{
		get
		{
			if (_audioThresholdBitRate == null)
			{
				_audioThresholdBitRate = ActiveDevice.AudioTranscodeLimit.ToString();
			}
			return _audioThresholdBitRate;
		}
		set
		{
			if (_audioThresholdBitRate != value)
			{
				_audioThresholdBitRate = value;
				ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnAudioConversionChoiceCommit)] = CommitDeviceID;
				((ModelItem)this).FirePropertyChanged("AudioThresholdBitRate");
			}
		}
	}

	public string AudioTargetBitRate
	{
		get
		{
			if (_audioTargetBitRate == null)
			{
				_audioTargetBitRate = ActiveDevice.AudioTranscodeTarget.ToString();
			}
			return _audioTargetBitRate;
		}
		set
		{
			if (_audioTargetBitRate != value)
			{
				_audioTargetBitRate = value;
				ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnAudioConversionChoiceCommit)] = CommitDeviceID;
				((ModelItem)this).FirePropertyChanged("AudioTargetBitRate");
			}
		}
	}

	public Choice VideoConversionChoice
	{
		get
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Expected O, but got Unknown
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0060: Expected O, but got Unknown
			if (_videoConversionChoice == null)
			{
				_videoConversionOptions = new List<Command>();
				Command item = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_TRANSCODE_VIDEO_PLAYBACK_OPTION), (EventHandler)null);
				_videoConversionOptions.Add(item);
				item = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_TRANSCODE_VIDEO_TV_OUT_OPTION), (EventHandler)null);
				_videoConversionOptions.Add(item);
				_videoConversionChoice = new Choice((IModelItemOwner)(object)this);
				_videoConversionChoice.Options = (IList)_videoConversionOptions;
				_videoConversionChoice.DefaultIndex = (ActiveDevice.OptimizeVideoForTV ? 1 : 0);
				_videoConversionChoice.DefaultValue();
				_videoConversionChoice.ChosenChanged += delegate
				{
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnVideoConversionChoiceCommit)] = CommitDeviceID;
				};
			}
			return _videoConversionChoice;
		}
		private set
		{
			if (_videoConversionChoice != value)
			{
				_videoConversionChoice = value;
				((ModelItem)this).FirePropertyChanged("VideoConversionChoice");
			}
		}
	}

	public IList<Command> BitRateList
	{
		get
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Expected O, but got Unknown
			if (_bitRateList == null)
			{
				_bitRateList = new List<Command>(_defaultBitRates.Length);
				for (int i = 0; i < _defaultBitRates.Length; i++)
				{
					_bitRateList.Add(new Command((IModelItemOwner)null, _defaultBitRates[i].ToString(), (EventHandler)null));
				}
			}
			return _bitRateList;
		}
	}

	public float ReservedSpaceOnDevice
	{
		get
		{
			if (float.IsNaN(_reservedSpaceOnDevice))
			{
				_reservedSpaceOnDevice = ActiveDevice.PercentReserved;
			}
			return _reservedSpaceOnDevice;
		}
		set
		{
			if (_reservedSpaceOnDevice != value)
			{
				ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnReservedSpaceOnDeviceCommit)] = CommitDeviceID;
				_reservedSpaceOnDevice = value;
			}
		}
	}

	public SyncGroupList SyncGroupList
	{
		get
		{
			if (_syncGroups == null)
			{
				_syncGroups = ActiveDevice.GenerateSyncGroupList((IModelItemOwner)(object)this, expandSyncAllEntries: false);
			}
			return _syncGroups;
		}
	}

	public BooleanChoice PrivacyChoice
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_privacyChoice == null)
			{
				_privacyChoice = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_DEVICE_USAGE_DATA_CHECK));
				_privacyChoice.Value = ActiveDevice.EnableWatson;
				((Choice)_privacyChoice).ChosenChanged += delegate
				{
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnPrivacyChoiceCommit)] = CommitDeviceID;
				};
			}
			return _privacyChoice;
		}
	}

	private int CommitDeviceID
	{
		get
		{
			if (!ActiveDevice.IsGuest)
			{
				return ActiveDevice.ID;
			}
			return -1;
		}
	}

	internal static event DeviceConnectionHandledEventHandler DeviceConnectionHandled;

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)this).OnDispose(disposing);
		SignIn.Instance.TempPasswordStorage = null;
		WirelessSync.Instance = null;
		SignIn.Instance.SignInStatusUpdatedEvent -= OnSignInStatusUpdatedEvent;
	}

	public static void NavigateToWizardMode(CategoryPageNode node, Category category)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Expected O, but got Unknown
		Management management = ZuneShell.DefaultInstance.Management;
		if (management.HasPendingCommits)
		{
			Command val = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_DIALOG_YES), (EventHandler)null);
			val.Invoked += delegate
			{
				management.CommitListSave();
				NavigateToWizardMode(node, category);
			};
			Command val2 = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_DIALOG_NO), (EventHandler)null);
			val2.Invoked += delegate
			{
				management.CommitList = null;
				NavigateToWizardMode(node, category);
			};
			MessageBox.Show(Shell.LoadString(StringId.IDS_SAVE_CHANGES_DIALOG_TITLE), Shell.LoadString(StringId.IDS_SAVE_CHANGES_ON_BACK_DIALOG_TEXT), val, val2, (BooleanChoice)null);
			return;
		}
		NavigatingToWizard = true;
		if (Shell.SettingsFrame.IsCurrent && !Shell.SettingsFrame.Wizard.FUE.IsCurrent)
		{
			management.CurrentCategoryPage.CancelAndExit();
		}
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (category != null)
			{
				node.Invoke(category);
			}
			else
			{
				((Command)node).Invoke();
			}
		}, (object)null);
	}

	internal static void HandleSetupQueue()
	{
		if (_setupQueueHandlingLocked)
		{
			return;
		}
		_setupQueueHandlingLocked = true;
		SetupDevice = null;
		List<int> list = new List<int>();
		foreach (DictionaryEntry item in SetupQueue)
		{
			UIDevice uIDevice = (UIDevice)item.Value;
			if (!uIDevice.IsConnectedToClient)
			{
				list.Add(uIDevice.ID);
				continue;
			}
			SetupDevice = uIDevice;
			break;
		}
		foreach (int item2 in list)
		{
			SetupQueue.Remove(item2);
		}
		if (SetupDevice == null)
		{
			_setupQueueHandlingLocked = false;
		}
		else
		{
			HandleCurrentSetupDevice();
		}
	}

	public static void HandleCurrentSetupDevice()
	{
		if (SetupDevice == null)
		{
			return;
		}
		if (SetupDevice.SupportsOOBECompleted)
		{
			if (!SetupDevice.OOBECompleted)
			{
				SingletonModelItem<UIDeviceList>.Instance.HideDevice(SetupDevice);
				ShowDeviceOOBEIncompleteDialog();
				SetupDevice = null;
				return;
			}
			HideDeviceOOBEIncompleteDialog();
		}
		if (SetupDevice.RequiresClientUpdate)
		{
			SingletonModelItem<UIDeviceList>.Instance.HideDevice(SetupDevice);
			MessageBox.Show(Shell.LoadString(StringId.IDS_CLIENT_UPDATE_HEADER), Shell.LoadString(StringId.IDS_CLIENT_UPDATE_DESC), (EventHandler)delegate
			{
				ClientUpdate.Instance.InvokeClientUpdate();
			});
			SetupDevice = null;
			return;
		}
		if (!UIDeviceList.IsSuitableForConnection(SetupDevice))
		{
			if (SetupDevice.RequiresAutoRestore)
			{
				ZuneShell.DefaultInstance.NavigateToPage(new AutoRestoreLandPage());
			}
			else
			{
				ZuneShell.DefaultInstance.NavigateToPage(new FirstConnectLandPage());
			}
			return;
		}
		UIFirmwareUpdater uIFirmwareUpdater = SetupDevice.UIFirmwareUpdater;
		if (uIFirmwareUpdater != null)
		{
			bool launchWizardIfUpdatesFound = SetupDevice.SupportsBrandingType(DeviceBranding.WindowsPhone);
			uIFirmwareUpdater.StartCheckForUpdates(SetupDevice.RequiresFirmwareUpdate, launchWizardIfUpdatesFound);
		}
		EndDeviceHandling(comingOutOfFirstConnect: false, navigateToLandingPage: true);
	}

	public static void HideSetupDevice()
	{
		SingletonModelItem<UIDeviceList>.Instance.HideDevice(SetupDevice);
		SetupDevice = null;
	}

	public void SetupComplete(bool navigateToLandingPage)
	{
		if (SetupDevice != null)
		{
			if (ActiveDevice.IsValid)
			{
				ActiveDevice.PromptForAccountLinkage = true;
				SyncControls.Instance.ChangeIntoSetupDevice = false;
				EndDeviceHandling(comingOutOfFirstConnect: true, navigateToLandingPage);
				ZuneShell.DefaultInstance.Management.DisposeDeviceManagement(deviceManagementLocked: false);
			}
			else
			{
				ErrorMessage = Shell.LoadString(StringId.IDS_SYNC_SETUP_COMPLETION_ERROR);
			}
		}
	}

	private static void EndDeviceHandling(bool comingOutOfFirstConnect, bool navigateToLandingPage)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (SetupDevice != null)
		{
			SetupDevice.Enumerate();
			SyncControls instance = SyncControls.Instance;
			if (!Shell.SettingsFrame.IsCurrent || comingOutOfFirstConnect)
			{
				instance.SetCurrentDeviceIfNecessary(SetupDevice, comingOutOfFirstConnect);
			}
			if (!SetupDevice.IsGuest && SetupDevice.HasFailedLogin)
			{
				instance.AddDeviceToFailedSignInQueue(SetupDevice);
			}
			if (SetupDevice.SupportsBrandingType(DeviceBranding.WindowsPhone) || SetupDevice.SupportsBrandingType(DeviceBranding.Kin))
			{
				instance.ShowPhoneWelcomeMessage = true;
				ClientConfiguration.Devices.HasPhoneBeenConnected = true;
			}
			if (navigateToLandingPage && comingOutOfFirstConnect && !(ZuneShell.DefaultInstance.CurrentPage is Deviceland))
			{
				((Command)Shell.MainFrame.Device).Invoke();
			}
			instance.ShowSyncInstructionsToast = true;
			if (DeviceManagement.DeviceConnectionHandled != null)
			{
				DeviceManagement.DeviceConnectionHandled(null, new DeviceConnectionHandledEventArgs(SetupDevice, comingOutOfFirstConnect));
			}
			SetupDevice = null;
		}
	}

	internal static void ShowDeviceOOBEIncompleteDialog()
	{
		if (_deviceOOBEIncompleteDialog == null)
		{
			_deviceOOBEIncompleteDialog = MessageBox.Show(Shell.LoadString(StringId.IDS_PHONE_OOBE_INCOMPLETE_TITLE), Shell.LoadString(StringId.IDS_PHONE_OOBE_INCOMPLETE_DESCRIPTION), (EventHandler)null, (EventHandler)null, (EventHandler)null, (EventHandler)delegate
			{
				HideDeviceOOBEIncompleteDialog();
			}, (BooleanChoice)null);
		}
	}

	internal static void HideDeviceOOBEIncompleteDialog()
	{
		if (_deviceOOBEIncompleteDialog != null)
		{
			((DialogHelper)_deviceOOBEIncompleteDialog).Hide();
			_deviceOOBEIncompleteDialog = null;
		}
	}

	private Choice GenerateSyncModeChoice(SyncCategory syncType)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		Choice val = new Choice((IModelItemOwner)(object)this);
		List<SyncModeOptionPair> list = new List<SyncModeOptionPair>();
		string name = "";
		string name2 = "";
		string text = null;
		EventHandler eventHandler = null;
		switch (syncType)
		{
		case SyncCategory.Music:
			name = Shell.LoadString(StringId.IDS_SYNC_ALL_OPTION_MUSIC);
			name2 = Shell.LoadString(StringId.IDS_LET_ME_CHOOSE_OPTION_MUSIC);
			text = Shell.LoadString(StringId.IDS_MANUAL_OPTION_MUSIC);
			eventHandler = HandleMusicSyncOptionChanged;
			break;
		case SyncCategory.Video:
			name = Shell.LoadString(StringId.IDS_SYNC_ALL_OPTION_VIDEOS);
			name2 = Shell.LoadString(StringId.IDS_LET_ME_CHOOSE_OPTION_VIDEOS);
			text = Shell.LoadString(StringId.IDS_MANUAL_OPTION_VIDEOS);
			eventHandler = HandleVideoSyncOptionChanged;
			break;
		case SyncCategory.Photo:
			name = Shell.LoadString(StringId.IDS_SYNC_ALL_OPTION_PICTURES);
			name2 = Shell.LoadString(StringId.IDS_LET_ME_CHOOSE_OPTION_PICTURES);
			text = Shell.LoadString(StringId.IDS_MANUAL_OPTION_PICTURES);
			eventHandler = HandlePhotoSyncOptionChanged;
			break;
		case SyncCategory.Podcast:
			name = Shell.LoadString(StringId.IDS_SYNC_ALL_OPTION_PODCASTS);
			name2 = Shell.LoadString(StringId.IDS_LET_ME_CHOOSE_OPTION_PODCASTS);
			eventHandler = HandlePodcastSyncOptionChanged;
			break;
		case SyncCategory.Channel:
			name = Shell.LoadString(StringId.IDS_SYNC_ALL_OPTION_CHANNELS);
			name2 = Shell.LoadString(StringId.IDS_LET_ME_CHOOSE_OPTION_CHANNELS);
			eventHandler = HandleChannelSyncOptionChanged;
			break;
		case SyncCategory.Friend:
			name = Shell.LoadString(StringId.IDS_SYNC_ALL_OPTION_FRIENDS);
			name2 = Shell.LoadString(StringId.IDS_LET_ME_CHOOSE_OPTION_FRIENDS);
			eventHandler = HandleFriendSyncOptionChanged;
			break;
		case SyncCategory.Application:
			name = Shell.LoadString(StringId.IDS_SYNC_ALL_OPTION_APPLICATIONS);
			name2 = Shell.LoadString(StringId.IDS_LET_ME_CHOOSE_OPTION_APPLICATIONS);
			text = Shell.LoadString(StringId.IDS_MANUAL_OPTION_APPLICATIONS);
			eventHandler = HandleApplicationSyncOptionChanged;
			break;
		}
		SyncModeOptionPair item = new SyncModeOptionPair(name, SyncMode.SyncAll);
		list.Add(item);
		item = new SyncModeOptionPair(name2, SyncMode.LetMeChoose);
		list.Add(item);
		if (!string.IsNullOrEmpty(text))
		{
			item = new SyncModeOptionPair(text, SyncMode.Manual);
			list.Add(item);
		}
		bool flag = ActiveDevice.IsSyncAllFor(syncType, fEstablishingPartnership: true);
		bool flag2 = !string.IsNullOrEmpty(text) && ActiveDevice.IsManualFor(syncType, fEstablishingPartnership: true);
		val.Options = list;
		val.ChosenIndex = (flag2 ? 2 : ((!flag) ? 1 : 0));
		if (eventHandler != null)
		{
			val.ChosenChanged += eventHandler;
		}
		return val;
	}

	private void HandleMusicSyncOptionChanged(object sender, EventArgs args)
	{
		ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnMusicSyncChoiceCommit)] = CommitDeviceID;
	}

	private void OnMusicSyncChoiceCommit(object data)
	{
		OnCategorySyncChoiceCommit(SyncCategory.Music, ((SyncModeOptionPair)_musicSyncChoice.ChosenValue).Mode);
	}

	private void HandleVideoSyncOptionChanged(object sender, EventArgs args)
	{
		ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnVideoSyncChoiceCommit)] = CommitDeviceID;
	}

	private void OnVideoSyncChoiceCommit(object data)
	{
		OnCategorySyncChoiceCommit(SyncCategory.Video, ((SyncModeOptionPair)_videoSyncChoice.ChosenValue).Mode);
	}

	private void HandlePhotoSyncOptionChanged(object sender, EventArgs args)
	{
		ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnPhotoSyncChoiceCommit)] = CommitDeviceID;
	}

	private void OnPhotoSyncChoiceCommit(object data)
	{
		OnCategorySyncChoiceCommit(SyncCategory.Photo, ((SyncModeOptionPair)_photoSyncChoice.ChosenValue).Mode);
	}

	private void HandlePodcastSyncOptionChanged(object sender, EventArgs args)
	{
		ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnPodcastSyncChoiceCommit)] = CommitDeviceID;
	}

	private void OnPodcastSyncChoiceCommit(object data)
	{
		OnCategorySyncChoiceCommit(SyncCategory.Podcast, ((SyncModeOptionPair)_podcastSyncChoice.ChosenValue).Mode);
	}

	private void HandleFriendSyncOptionChanged(object sender, EventArgs args)
	{
		if (((SyncModeOptionPair)_friendSyncChoice.ChosenValue).Mode == SyncMode.SyncAll)
		{
			UIDevice.WarnUserAboutFriendSyncSize();
		}
		ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnFriendSyncChoiceCommit)] = CommitDeviceID;
	}

	private void OnFriendSyncChoiceCommit(object data)
	{
		if (DeviceHasTag)
		{
			OnCategorySyncChoiceCommit(SyncCategory.Friend, ((SyncModeOptionPair)_friendSyncChoice.ChosenValue).Mode);
		}
	}

	private void HandleChannelSyncOptionChanged(object sender, EventArgs args)
	{
		ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnChannelSyncChoiceCommit)] = CommitDeviceID;
	}

	private void OnChannelSyncChoiceCommit(object data)
	{
		if (DeviceHasTag)
		{
			OnCategorySyncChoiceCommit(SyncCategory.Channel, ((SyncModeOptionPair)_channelSyncChoice.ChosenValue).Mode);
		}
	}

	private void HandleApplicationSyncOptionChanged(object sender, EventArgs args)
	{
		ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnApplicationSyncChoiceCommit)] = CommitDeviceID;
	}

	private void OnApplicationSyncChoiceCommit(object data)
	{
		OnCategorySyncChoiceCommit(SyncCategory.Application, ((SyncModeOptionPair)_applicationSyncChoice.ChosenValue).Mode);
	}

	private void OnCategorySyncChoiceCommit(SyncCategory cat, SyncMode mode)
	{
		if (ActiveDevice.IsValid)
		{
			SyncMode syncMode = ActiveDevice.GetSyncMode(cat);
			ActiveDevice.SetSyncMode(cat, mode);
			HandleSyncOptionChanges(syncMode, mode);
		}
	}

	private void HandleSyncOptionChanges(SyncMode oldMode, SyncMode mode)
	{
		if (mode != oldMode)
		{
			CategoryPage currentCategoryPage = ZuneShell.DefaultInstance.Management.CurrentCategoryPage;
		}
	}

	public void FormatCurrentDevice()
	{
		MessageBox.Show(Shell.LoadString(StringId.IDS_FORMAT_DIALOG_TITLE), Shell.LoadString(StringId.IDS_FORMAT_DIALOG_TEXT), (EventHandler)ConfirmedFormatDevice, (EventHandler)null);
	}

	private void ConfirmedFormatDevice(object sender, EventArgs e)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		FormatBegun.Invoke();
		SyncControls.Instance.CurrentDevice.Format();
	}

	private void OnDontSyncDislikedContentChoiceCommit(object data)
	{
		ActiveDevice.ExcludeDislikedContent = DontSyncDislikedContentChoice.Value;
	}

	private void OnDeleteAfterReverseSyncCommit(object data)
	{
		ActiveDevice.DeletePicsFromPhoneAfterReverseSync = DeleteAfterReverseSyncChoice.ChosenIndex == 1;
	}

	private void OnFriendlyNameOnDeviceCommit(object data)
	{
		ActiveDevice.Name = _friendlyNameOnDevice.Trim();
	}

	private bool IsDeviceNameValid(string name, out string errorMessage)
	{
		if (string.IsNullOrEmpty(name))
		{
			errorMessage = Shell.LoadString(StringId.IDS_DEVICE_NAME_EMPTY);
			return false;
		}
		if (name.Length > _deviceNameMaxLength)
		{
			errorMessage = Shell.LoadString(StringId.IDS_DEVICE_NAME_TOO_LONG);
			return false;
		}
		if (DeviceNameHasInvalidCharacters(name))
		{
			errorMessage = Shell.LoadString(StringId.IDS_DEVICE_NAME_INVALID_CHARS);
			return false;
		}
		errorMessage = null;
		return true;
	}

	private bool DeviceNameHasInvalidCharacters(string name)
	{
		if (ActiveDevice.SupportsBrandingType(DeviceBranding.WindowsPhone) && name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
		{
			return true;
		}
		try
		{
			UnicodeEncoding unicodeEncoding = new UnicodeEncoding(bigEndian: false, byteOrderMark: false, throwOnInvalidBytes: true);
			unicodeEncoding.GetBytes(name);
			return false;
		}
		catch (EncoderFallbackException)
		{
			return true;
		}
		catch (ArgumentException)
		{
			return true;
		}
	}

	public void CheckForAutomatedRequirements()
	{
	}

	private void OnMarketplaceCredentialsCommit(object data)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		if (EnableMarketplaceChoice.ChosenIndex == 0)
		{
			ActiveDevice.ClearAccountAssociation();
			return;
		}
		ActiveDevice.SetGeoId();
		ActiveDevice.PurchaseEnabled = _marketplaceCredentials.PurchaseEnabled;
		HRESULT val = ActiveDevice.AssociateWithAccount(_marketplaceCredentials.UserGuid, _marketplaceCredentials.ZuneTag);
		if (((HRESULT)(ref val)).IsSuccess)
		{
			ActiveDevice.SendMarketplaceCredentials(_marketplaceCredentials.Email, _marketplaceCredentials.Password);
		}
		else
		{
			ActiveDevice.ClearAccountAssociation();
		}
	}

	public void ValidatePassportAccount(string email, string password)
	{
		if (email == string.Empty || password == string.Empty)
		{
			ErrorMessage = Shell.LoadString(StringId.IDS_VERIFY_CREDS_ERROR);
			MarketplaceCredentials.PurchaseEnabled = false;
			MarketplaceCredentials.ZuneTag = string.Empty;
			((ModelItem)this).FirePropertyChanged("MarketplaceCredentials");
		}
		else
		{
			ErrorMessage = string.Empty;
			ValidatingCredentials = true;
			MarketplaceCredentials.Email = email;
			MarketplaceCredentials.Password = password;
			SignIn.Instance.SignOut();
			SignIn.Instance.SignInStatusUpdatedEvent += OnSignInStatusUpdatedEvent;
			SignIn.Instance.SignInUser(email, password);
		}
	}

	private void OnSignInStatusUpdatedEvent(object sender, EventArgs e)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		if (SignIn.Instance.SigningIn)
		{
			return;
		}
		HRESULT signInError = SignIn.Instance.SignInError;
		if (((HRESULT)(ref signInError)).IsError || SignIn.Instance.SignedIn)
		{
			SignIn.Instance.SignInStatusUpdatedEvent -= OnSignInStatusUpdatedEvent;
			MarketplaceCredentials.hr = SignIn.Instance.SignInError.hr;
			HRESULT signInError2 = SignIn.Instance.SignInError;
			if (((HRESULT)(ref signInError2)).IsError)
			{
				ErrorMessage = Shell.LoadString(StringId.IDS_VERIFY_CREDS_ERROR);
				MarketplaceCredentials.PurchaseEnabled = false;
				MarketplaceCredentials.ZuneTag = string.Empty;
			}
			else
			{
				ErrorMessage = string.Empty;
				MarketplaceCredentials.ZuneTag = SignIn.Instance.ZuneTag;
				MarketplaceCredentials.UserGuid = SignIn.Instance.UserGuid;
			}
			if (!Shell.SettingsFrame.Wizard.IsCurrent)
			{
				OnMarketplaceCredentialsCommit(null);
			}
			ValidatingCredentials = false;
			((ModelItem)this).FirePropertyChanged("MarketplaceCredentials");
		}
	}

	private void OnSyncPartnershipCommit(object data)
	{
		if (!ActiveDevice.IsValid || data == null)
		{
			return;
		}
		DeviceRelationship relationship = ActiveDevice.Relationship;
		if (relationship != DeviceRelationship.Permanent)
		{
			ActiveDevice.Relationship = DeviceRelationship.Permanent;
			if (ActiveDevice.SupportsWirelessSetupMethod1 || ActiveDevice.SupportsWirelessSetupMethod2)
			{
				WirelessSync.Instance.ClearWirelessOnDevice();
			}
		}
	}

	private void OnTranscodeSizeLimitCommit(object data)
	{
		SingletonModelItem<UIDeviceList>.Instance.TranscodedFilesCacheSize = _transcodeSizeLimit;
	}

	public void OnCameraRollDestinationPathCommit(object data)
	{
		ActiveDevice.CameraRollDestinationPath = _cameraRollDestinationPath;
	}

	public void OnSavedFolderDestinationPathCommit(object data)
	{
		ActiveDevice.SavedFolderDestinationPath = _savedFolderDestinationPath;
	}

	private void OnTranscodeInBGCommit(object data)
	{
		ClientConfiguration.Transcode.BackgroundTranscode = _transcodeInBG;
	}

	public void ChangeTranscodeFolder()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		FolderBrowseDialog.Show("", (DeferredInvokeHandler)delegate(object args)
		{
			if (args != null)
			{
				ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnChangeTranscodeFolderCommit)] = null;
				TranscodedFilesCachePath = (string)args;
			}
		}, validate: true);
	}

	private void OnChangeTranscodeFolderCommit(object data)
	{
		SingletonModelItem<UIDeviceList>.Instance.TranscodedFilesCachePath = _transcodedFilesCachePath;
	}

	public void ClearTranscodeFolder()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (ActiveDevice.IsValid)
		{
			HRESULT val = SingletonModelItem<UIDeviceList>.Instance.ClearTranscodeCache();
			if (((HRESULT)(ref val)).IsError)
			{
				Shell.ShowErrorDialog(((HRESULT)(ref val)).Int, StringId.IDS_CACHE_CLEAR_FAILED);
			}
		}
	}

	public void ChangeCameraRollDestinationPath()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Expected O, but got Unknown
		DeferredInvokeHandler val = null;
		if (!ActiveDevice.IsValid)
		{
			return;
		}
		if (val == null)
		{
			val = (DeferredInvokeHandler)delegate(object args)
			{
				if (args != null)
				{
					ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnChangeCameraRollDestinationPathCommit)] = CommitDeviceID;
					CameraRollDestinationPath = (string)args;
				}
			};
		}
		FolderBrowseDialog.Show("", val, validate: true);
	}

	private void OnChangeCameraRollDestinationPathCommit(object data)
	{
		ActiveDevice.CameraRollDestinationPath = _transcodedFilesCachePath;
	}

	private void OnImageQualitySyncChoiceCommit(object data)
	{
		ActiveDevice.ImageTranscodeQuality = (ETranscodePhotoSetting)_imageQualitySyncChoice.ChosenIndex;
	}

	private void OnAudioConversionChoiceCommit(object data)
	{
		int result = 0;
		if (AudioConversionChoice.ChosenIndex == 1)
		{
			int.TryParse(_audioThresholdBitRate, out result);
			ActiveDevice.AudioTranscodeLimit = result;
		}
		else
		{
			ActiveDevice.AudioTranscodeLimit = -1;
		}
		int.TryParse(_audioTargetBitRate, out result);
		ActiveDevice.AudioTranscodeTarget = result;
	}

	private void OnVideoConversionChoiceCommit(object data)
	{
		ActiveDevice.OptimizeVideoForTV = _videoConversionChoice.ChosenIndex == 1;
	}

	private void OnReservedSpaceOnDeviceCommit(object data)
	{
		ActiveDevice.PercentReserved = (int)_reservedSpaceOnDevice;
	}

	private void OnPrivacyChoiceCommit(object data)
	{
		ActiveDevice.EnableWatson = PrivacyChoice.Value;
	}

	public void AutomatePrivacy(bool enable)
	{
		ActiveDevice.EnableWatson = enable;
		ZuneShell.DefaultInstance.Management.CommitList[new ProxySettingDelegate(OnPrivacyChoiceCommit)] = CommitDeviceID;
	}
}
