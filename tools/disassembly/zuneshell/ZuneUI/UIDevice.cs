using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using UIXControls;
using ZuneXml;

namespace ZuneUI;

public class UIDevice : ModelItem
{
	protected interface INewRuleMessageType
	{
		MediaType Type { get; }

		string SingularMessage { get; }

		string PluralMessage { get; }

		string GetMessageForCount(int count);
	}

	protected class NewRuleMessageType : INewRuleMessageType
	{
		private MediaType _type;

		private string _singular;

		private string _plural;

		public MediaType Type => _type;

		public string SingularMessage => _singular;

		public string PluralMessage => _plural;

		public NewRuleMessageType(MediaType type, StringId singular, StringId plural)
		{
			_type = type;
			_singular = Shell.LoadString(singular);
			_plural = Shell.LoadString(plural);
		}

		public string GetMessageForCount(int count)
		{
			if (count == 1)
			{
				return SingularMessage;
			}
			return string.Format(PluralMessage, count);
		}
	}

	private IDeviceIconSet _iconSet;

	private static readonly INewRuleMessageType[] _newRuleMessageLookupTable = new INewRuleMessageType[11]
	{
		new NewRuleMessageType(MediaType.Track, StringId.IDS_ADDED_1_TRACK, StringId.IDS_ADDED_N_TRACKS),
		new NewRuleMessageType(MediaType.Album, StringId.IDS_ADDED_1_ALBUM, StringId.IDS_ADDED_N_ALBUMS),
		new NewRuleMessageType(MediaType.Artist, StringId.IDS_ADDED_1_ARTIST, StringId.IDS_ADDED_N_ARTISTS),
		new NewRuleMessageType(MediaType.Genre, StringId.IDS_ADDED_1_GENRE, StringId.IDS_ADDED_N_GENRES),
		new NewRuleMessageType(MediaType.Playlist, StringId.IDS_ADDED_1_PLAYLIST, StringId.IDS_ADDED_N_PLAYLISTS),
		new NewRuleMessageType(MediaType.Video, StringId.IDS_ADDED_1_VIDEO, StringId.IDS_ADDED_N_VIDEOS),
		new NewRuleMessageType(MediaType.Photo, StringId.IDS_ADDED_1_PHOTO, StringId.IDS_ADDED_N_PHOTOS),
		new NewRuleMessageType(MediaType.MediaFolder, StringId.IDS_ADDED_1_FOLDER, StringId.IDS_ADDED_N_FOLDERS),
		new NewRuleMessageType(MediaType.PodcastEpisode, StringId.IDS_ADDED_1_PODCAST_EPISODE, StringId.IDS_ADDED_N_PODCAST_EPISODES),
		new NewRuleMessageType(MediaType.Podcast, StringId.IDS_ADDED_1_PODCAST, StringId.IDS_ADDED_N_PODCASTS),
		new NewRuleMessageType(MediaType.UserCard, StringId.IDS_ADDED_1_FRIEND, StringId.IDS_ADDED_N_FRIENDS)
	};

	private static readonly INewRuleMessageType _channelNewRuleMessage = new NewRuleMessageType(MediaType.Playlist, StringId.IDS_ADDED_1_CHANNEL, StringId.IDS_ADDED_N_CHANNELS);

	private static readonly INewRuleMessageType _genericNewRuleMessage = new NewRuleMessageType(MediaType.Undefined, StringId.IDS_ADDED_1_ITEM, StringId.IDS_ADDED_N_ITEMS);

	private static bool _isOosDialogVisible;

	private static string _syncStatusTitleDivider = Shell.LoadString(StringId.IDS_SYNCITEM_TITLE_SEPARATOR);

	private static string _syncStatusDatumDivider = Shell.LoadString(StringId.IDS_SYNCITEM_METADATUM_SEPARATOR);

	private UIFirmwareUpdater _firmwareUpdater;

	private UIFirmwareRestorer _firmwareRestorer;

	private Device _device;

	private UIGasGauge _actualGasGauge;

	private UIGasGauge _predictedGasGauge;

	private SyncNotification _syncProgress;

	private Command _syncBegun;

	private Command _syncProgressed;

	private Command _syncCompleted;

	private bool _currentlySyncing;

	private bool _currentlyFormatting;

	private bool _isWirelessSyncEnabled;

	private EEndpointStatus _lastDeviceState;

	private bool _isReadyForSync;

	private bool _isLocked;

	private bool _userStoppedLastSync;

	private Timer _formatSanityTimer;

	private string _lastConnectTimestring;

	private string _lastSyncStartTimestring;

	private HRESULT _lastLoginFailure;

	private HRESULT _lastSyncFailure;

	private MessageBox _pinUnlockMessageBox;

	public IDeviceIconSet IconSet
	{
		get
		{
			if (_iconSet == null)
			{
				if (IsValid && _device.DeviceAssetSet != null)
				{
					_iconSet = DeviceIconSetFactory.BuildDeviceIconSet(_device.DeviceAssetSet, SetDeviceIconSetCallback);
				}
				else
				{
					_iconSet = DeviceIconSetFactory.DefaultIconSet;
				}
			}
			return _iconSet;
		}
		private set
		{
			if (_iconSet != value)
			{
				_iconSet = value;
				((ModelItem)this).FirePropertyChanged("IconSet");
			}
		}
	}

	public bool IsValid
	{
		get
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Invalid comparison between Unknown and I4
			if (_device != null && !((ModelItem)this).IsDisposed)
			{
				return (int)_lastDeviceState > 0;
			}
			return false;
		}
	}

	public int ID
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			return _device.DeviceID;
		}
	}

	public string MyPhoneDeviceID
	{
		get
		{
			string result = string.Empty;
			if (IsValid && SupportsMyPhoneLinks && _device.MyPhoneDeviceID != null)
			{
				result = _device.MyPhoneDeviceID;
			}
			return result;
		}
	}

	public string EndpointId
	{
		get
		{
			if (!IsValid)
			{
				return string.Empty;
			}
			return _device.EndpointId;
		}
	}

	public string CanonicalName
	{
		get
		{
			if (!IsConnectedToClient)
			{
				return string.Empty;
			}
			return _device.CanonicalName;
		}
	}

	public DeviceClass Class
	{
		get
		{
			if (!IsValid)
			{
				return DeviceClass.Invalid;
			}
			return (DeviceClass)_device.ClassID;
		}
	}

	public int AdvertisedCapacity
	{
		get
		{
			if (!IsValid)
			{
				return 0;
			}
			return (int)_device.StatedCapacity;
		}
	}

	public bool AllowChainedUpdates { get; set; }

	public bool NavigateToDeviceSummaryAfterUpdate { get; set; }

	public int SequentialUpdatesInstalled { get; set; }

	public bool SkipFutureBackupRequests { get; set; }

	public bool SupportsTVOutput
	{
		get
		{
			bool result = false;
			if (IsValid)
			{
				_device.GetIsTvOutSupported(ref result);
			}
			return result;
		}
	}

	public bool SupportsClassicGames
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)4, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsLiveId
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)22, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsOOBECompleted
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)23, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsRestorePoint
	{
		get
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			if (IsValid)
			{
				HRESULT val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)14, ref result));
				if (((HRESULT)(ref val)).IsSuccess)
				{
					return result;
				}
				return false;
			}
			return result;
		}
	}

	public bool RequiresAutoRestore
	{
		get
		{
			if (SupportsRestorePoint)
			{
				return !_device.InStandardMode;
			}
			return false;
		}
	}

	public bool InStandardMode
	{
		get
		{
			if (IsValid)
			{
				return _device.InStandardMode;
			}
			return false;
		}
	}

	public bool SupportsSyncApplications
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)5, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsPaidApplications
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)6, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsStoreApplications
	{
		get
		{
			if (!SupportsSyncApplications)
			{
				return SupportsPaidApplications;
			}
			return true;
		}
	}

	public bool SupportsHD
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)7, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsWiFi
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)1, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsWirelessSetupMethod1
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			bool result = true;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)18, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsWirelessSetupMethod2
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			bool result = true;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)19, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsFirmwareUpdate
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)10, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsMyPhoneLinks
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)13, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsRental
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)3, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsUserCards
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)8, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsChannels
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)8, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsFormat
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			bool result = true;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)12, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsZuneTagLinking
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)20, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool SupportsUsageData
	{
		get
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			HRESULT val = HRESULT._S_OK;
			if (IsValid)
			{
				val = HRESULT.op_Implicit(_device.GetCapability((EEndpointCapability)24, ref result));
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
			return false;
		}
	}

	public bool HasOffloadedContent
	{
		get
		{
			if (IsValid && !string.IsNullOrEmpty(OffloadedContentMessage))
			{
				return !string.IsNullOrEmpty(OffloadedContentUrl);
			}
			return false;
		}
	}

	public string OffloadedContentMessage
	{
		get
		{
			string text = null;
			if (IsValid)
			{
				text = _device.PicturesVideosViewText;
			}
			return text ?? string.Empty;
		}
	}

	public string OffloadedContentUrl
	{
		get
		{
			string text = null;
			if (IsValid)
			{
				text = _device.PicturesVideosViewUrl;
			}
			return text ?? string.Empty;
		}
	}

	public bool HasRestorePoint
	{
		get
		{
			if (SupportsRestorePoint && UIFirmwareRestorer != null)
			{
				return UIFirmwareRestorer.RestorePoint != null;
			}
			return false;
		}
	}

	public string RestorePointDate
	{
		get
		{
			if (!HasRestorePoint)
			{
				return string.Empty;
			}
			return StringFormatHelper.Format(UIFirmwareRestorer.RestorePoint.CreationDate.ToLocalTime(), StringFormatHelper.FriendlyMonthYearPattern);
		}
	}

	public string Manufacturer
	{
		get
		{
			string text = null;
			if (IsValid)
			{
				_device.GetManufacturer(ref text);
			}
			return text ?? string.Empty;
		}
	}

	public string ModelName
	{
		get
		{
			string text = null;
			if (IsValid)
			{
				_device.GetModelName(ref text);
			}
			return text ?? string.Empty;
		}
	}

	public bool IsConnectedToPC
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Invalid comparison between Unknown and I4
			if (!IsValid)
			{
				return false;
			}
			return (int)_lastDeviceState >= 3;
		}
	}

	public bool IsConnectedToClient
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Invalid comparison between Unknown and I4
			if (!IsValid)
			{
				return false;
			}
			return (int)_lastDeviceState >= 11;
		}
	}

	public bool IsConnectedToClientWirelessly
	{
		get
		{
			if (!IsValid)
			{
				return false;
			}
			if (IsConnectedToClient)
			{
				return _device.IsConnectedWirelessly;
			}
			return false;
		}
	}

	public bool IsConnectedToClientPhysically
	{
		get
		{
			if (IsConnectedToClient)
			{
				return !IsConnectedToClientWirelessly;
			}
			return false;
		}
	}

	public bool IsConnectedToSideloader
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Invalid comparison between Unknown and I4
			if (!IsValid)
			{
				return false;
			}
			return (int)_lastDeviceState == 4;
		}
	}

	public string OwnerApplicationName
	{
		get
		{
			if (!IsConnectedToSideloader)
			{
				return string.Empty;
			}
			return _device.OwnerApplicationName;
		}
	}

	public bool IsReadyForSync
	{
		get
		{
			if (!IsConnectedToClient)
			{
				return false;
			}
			return _isReadyForSync;
		}
		private set
		{
			if (IsValid && _isReadyForSync != value)
			{
				_isReadyForSync = value;
				((ModelItem)this).FirePropertyChanged("IsReadyForSync");
			}
		}
	}

	public bool IsFormatting
	{
		get
		{
			return _currentlyFormatting;
		}
		private set
		{
			if (IsValid && _currentlyFormatting != value)
			{
				_currentlyFormatting = value;
				((ModelItem)this).FirePropertyChanged("IsFormatting");
			}
		}
	}

	public bool IsLockedAgainstSyncing
	{
		get
		{
			return _isLocked;
		}
		set
		{
			if (IsValid && _isLocked != value)
			{
				_isLocked = value;
				if (_isLocked)
				{
					EndSync();
				}
			}
		}
	}

	public bool UserStoppedLastSync
	{
		get
		{
			return _userStoppedLastSync;
		}
		private set
		{
			if (_userStoppedLastSync != value)
			{
				_userStoppedLastSync = value;
				((ModelItem)this).FirePropertyChanged("UserStoppedLastSync");
			}
		}
	}

	public HRESULT LastFailedLoginError
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _lastLoginFailure;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (_lastLoginFailure != value)
			{
				_lastLoginFailure = value;
				((ModelItem)this).FirePropertyChanged("LastFailedLoginError");
				((ModelItem)this).FirePropertyChanged("HasFailedLogin");
			}
		}
	}

	public bool HasFailedLogin
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			HRESULT lastFailedLoginError = LastFailedLoginError;
			return ((HRESULT)(ref lastFailedLoginError)).IsError;
		}
	}

	public HRESULT LastSyncError
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _lastSyncFailure;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (_lastSyncFailure != value)
			{
				_lastSyncFailure = value;
				((ModelItem)this).FirePropertyChanged("LastSyncError");
				((ModelItem)this).FirePropertyChanged("HasFailedSync");
			}
		}
	}

	public bool HasFailedSync
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			HRESULT lastSyncError = LastSyncError;
			return ((HRESULT)(ref lastSyncError)).IsError;
		}
	}

	public string PivotDescription => Shell.LoadString(StringId.IDS_DEVICE_PIVOT);

	public bool IsWirelessSyncEnabled
	{
		get
		{
			if (IsValid)
			{
				return _isWirelessSyncEnabled;
			}
			return false;
		}
		private set
		{
			if (_isWirelessSyncEnabled != value)
			{
				_isWirelessSyncEnabled = value;
				((ModelItem)this).FirePropertyChanged("IsWirelessSyncEnabled");
			}
		}
	}

	public DeviceRelationship Relationship
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected I4, but got Unknown
			DeviceRelationship result = DeviceRelationship.None;
			if (IsValid)
			{
				ESyncRelationship val = (ESyncRelationship)0;
				HRESULT val2 = HRESULT.op_Implicit(_device.GetSyncRelationship(ref val));
				if (((HRESULT)(ref val2)).IsSuccess)
				{
					result = (DeviceRelationship)val;
				}
			}
			return result;
		}
		set
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			if (IsValid && Relationship != value)
			{
				ESyncRelationship syncRelationship = (ESyncRelationship)value;
				_device.SetSyncRelationship(syncRelationship);
				if (value == DeviceRelationship.Permanent)
				{
					SetGeoId();
				}
				int timeZoneBias = Convert.ToInt32(TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).TotalMinutes);
				_device.SetTimeZoneBias(timeZoneBias);
				_ = HasFailedLogin;
				((ModelItem)this).FirePropertyChanged("Relationship");
			}
		}
	}

	public bool IsGuest
	{
		get
		{
			if (!IsValid)
			{
				return false;
			}
			if (Relationship != DeviceRelationship.Guest)
			{
				return Relationship == DeviceRelationship.None;
			}
			return true;
		}
	}

	public string Name
	{
		get
		{
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (!IsValid)
			{
				return Shell.LoadString(StringId.IDS_NO_DEVICE);
			}
			string result = null;
			HRESULT val = HRESULT.op_Implicit(_device.GetFriendlyName(ref result));
			if (!((HRESULT)(ref val)).IsSuccess)
			{
				return Shell.LoadString(StringId.IDS_SYNC_DEFAULT_DEVICE_WORD);
			}
			return result;
		}
		set
		{
			if (IsConnectedToClient)
			{
				_device.SetFriendlyName(value);
			}
		}
	}

	public string NameXMLEscaped => SecurityElement.Escape(Name);

	public bool IsPermanentGuest
	{
		get
		{
			bool flag = true;
			if (IsValid)
			{
				_device.GetPromptGuest(ref flag);
			}
			return !flag;
		}
		set
		{
			if (IsValid && IsPermanentGuest != value)
			{
				_device.SetPromptGuest(!value);
				((ModelItem)this).FirePropertyChanged("IsPermanentGuest");
			}
		}
	}

	public Guid UserGuid
	{
		get
		{
			Guid empty = Guid.Empty;
			if (IsValid)
			{
				_device.GetUserGuid(ref empty);
			}
			return empty;
		}
	}

	public int UserId
	{
		get
		{
			int result = 0;
			if (IsValid)
			{
				_device.GetUserId(ref result);
			}
			return result;
		}
	}

	public string ZuneTag
	{
		get
		{
			string empty = string.Empty;
			if (IsConnectedToClient)
			{
				_device.GetZuneTag(ref empty);
			}
			return empty;
		}
	}

	public string LiveId
	{
		get
		{
			string empty = string.Empty;
			if (SupportsLiveId)
			{
				_device.GetLiveId(ref empty);
			}
			return empty;
		}
	}

	public bool OOBECompleted
	{
		get
		{
			bool result = false;
			if (SupportsOOBECompleted && IsConnectedToClient)
			{
				if (!_device.InStandardMode)
				{
					return true;
				}
				_device.GetOOBECompleted(ref result);
			}
			return result;
		}
	}

	public bool ExcludeDislikedContent
	{
		get
		{
			bool result = false;
			if (IsValid)
			{
				_device.Rules.GetDontSyncHatedContent(ref result);
			}
			return result;
		}
		set
		{
			if (IsValid && ExcludeDislikedContent != value)
			{
				_device.Rules.SetDontSyncHatedContent(value);
				((ModelItem)this).FirePropertyChanged("ExcludeDislikedContent");
			}
		}
	}

	private bool ReverseSyncPicsFromPhone
	{
		get
		{
			bool result = false;
			if (IsValid)
			{
				_device.GetPhotoVideoReverseSync(ref result);
			}
			return result;
		}
		set
		{
			if (IsValid && ReverseSyncPicsFromPhone != value)
			{
				_device.SetPhotoVideoReverseSync(value);
				((ModelItem)this).FirePropertyChanged("ReverseSyncPicsFromPhone");
			}
		}
	}

	public bool DeletePicsFromPhoneAfterReverseSync
	{
		get
		{
			bool result = false;
			if (IsValid)
			{
				_device.GetDeletePhotoVideoAfterReverseSync(ref result);
			}
			return result;
		}
		set
		{
			if (IsValid && DeletePicsFromPhoneAfterReverseSync != value)
			{
				_device.SetDeletePhotoVideoAfterReverseSync(value);
				((ModelItem)this).FirePropertyChanged("DeletePicsFromPhoneAfterReverseSync");
			}
		}
	}

	public ETranscodePhotoSetting ImageTranscodeQuality
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			ETranscodePhotoSetting result = (ETranscodePhotoSetting)(-1);
			if (IsValid)
			{
				_device.GetPhotoTranscodeSetting(ref result);
			}
			return result;
		}
		set
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			if (IsValid && ImageTranscodeQuality != value)
			{
				_device.SetPhotoTranscodeSetting(value);
				((ModelItem)this).FirePropertyChanged("ImageTranscodeQuality");
			}
		}
	}

	public string CameraRollDestinationPath
	{
		get
		{
			string empty = string.Empty;
			if (IsValid)
			{
				_device.GetCameraRollDestinationFolder(ref empty);
			}
			return empty;
		}
		set
		{
			if (IsValid)
			{
				_device.SetCameraRollDestinationFolder(value);
				((ModelItem)this).FirePropertyChanged("CameraRollDestinationPath");
			}
		}
	}

	public string SavedFolderDestinationPath
	{
		get
		{
			string empty = string.Empty;
			if (IsValid)
			{
				_device.GetSavedDestinationFolder(ref empty);
			}
			return empty;
		}
		set
		{
			if (IsValid)
			{
				_device.SetSavedDestinationFolder(value);
				((ModelItem)this).FirePropertyChanged("SavedFolderDestinationPath");
			}
		}
	}

	public int AudioTranscodeLimit
	{
		get
		{
			int result = 0;
			int num = 0;
			if (IsValid)
			{
				_device.GetAudioTranscodeParams(ref result, ref num);
			}
			return result;
		}
		set
		{
			if (IsValid)
			{
				_device.SetAudioTranscodeParams(value, AudioTranscodeTarget);
				((ModelItem)this).FirePropertyChanged("AudioTranscodeLimit");
			}
		}
	}

	public int AudioTranscodeTarget
	{
		get
		{
			int num = 0;
			int result = 0;
			if (IsValid)
			{
				_device.GetAudioTranscodeParams(ref num, ref result);
			}
			return result;
		}
		set
		{
			if (IsValid)
			{
				_device.SetAudioTranscodeParams(AudioTranscodeLimit, value);
				((ModelItem)this).FirePropertyChanged("AudioTranscodeTarget");
			}
		}
	}

	public bool OptimizeVideoForTV
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Invalid comparison between Unknown and I4
			ETranscodeOptimization val = (ETranscodeOptimization)0;
			if (IsValid)
			{
				_device.GetVideoTranscodeOptimization(ref val);
			}
			return (int)val == 1;
		}
		set
		{
			if (IsValid && OptimizeVideoForTV != value)
			{
				if (value)
				{
					_device.SetVideoTranscodeOptimization((ETranscodeOptimization)1);
				}
				else
				{
					_device.SetVideoTranscodeOptimization((ETranscodeOptimization)0);
				}
				((ModelItem)this).FirePropertyChanged("OptimizeVideoForTV");
			}
		}
	}

	public bool PurchaseEnabled
	{
		get
		{
			bool result = false;
			if (IsConnectedToClient)
			{
				_device.GetPurchaseEnabled(ref result);
			}
			return result;
		}
		set
		{
			if (IsConnectedToClient && PurchaseEnabled != value)
			{
				_device.SetPurchaseEnabled(value);
				((ModelItem)this).FirePropertyChanged("PurchaseEnabled");
			}
		}
	}

	public int PercentReserved
	{
		get
		{
			uint result = 0u;
			if (IsValid)
			{
				_device.GetPercentSpaceReserved(ref result);
			}
			return (int)result;
		}
		set
		{
			if (IsValid)
			{
				_device.SetPercentSpaceReserved((uint)value);
				((ModelItem)this).FirePropertyChanged("PercentReserved");
			}
		}
	}

	public bool PromptForAccountLinkage
	{
		get
		{
			bool result = false;
			if (IsValid)
			{
				_device.GetPromptLink(ref result);
			}
			return result;
		}
		set
		{
			if (IsValid)
			{
				_device.SetPromptLink(value);
				((ModelItem)this).FirePropertyChanged("PromptForAccountLinkage");
			}
		}
	}

	public bool RequiresClientUpdate
	{
		get
		{
			if (!IsConnectedToClient)
			{
				return false;
			}
			return _device.ClientUpdateRequired;
		}
	}

	public bool RequiresFirmwareUpdate
	{
		get
		{
			if (!IsConnectedToClient)
			{
				return false;
			}
			return _device.FirmwareUpdateRequired;
		}
	}

	public UIFirmwareUpdater UIFirmwareUpdater
	{
		get
		{
			if (_firmwareUpdater == null && IsValid && _device.FirmwareUpdater != null && IsConnectedToClientPhysically)
			{
				UIFirmwareUpdater = new UIFirmwareUpdater(this, _device.FirmwareUpdater);
			}
			return _firmwareUpdater;
		}
		private set
		{
			if (_firmwareUpdater != value)
			{
				_firmwareUpdater = value;
				((ModelItem)this).FirePropertyChanged("UIFirmwareUpdater");
			}
		}
	}

	public UIFirmwareRestorer UIFirmwareRestorer
	{
		get
		{
			if (_firmwareRestorer == null && IsValid && _device.FirmwareUpdater != null && IsConnectedToClientPhysically)
			{
				UIFirmwareRestorer = new UIFirmwareRestorer(_device.FirmwareUpdater.Restorer, RequiresAutoRestore);
			}
			return _firmwareRestorer;
		}
		private set
		{
			_firmwareRestorer = value;
			((ModelItem)this).FirePropertyChanged("UIFirmwareRestorer");
		}
	}

	public string FirmwareVersion
	{
		get
		{
			string empty = string.Empty;
			if (IsConnectedToClient)
			{
				_device.GetFirmwareVersion(ref empty);
			}
			return empty;
		}
	}

	public bool IsSyncing
	{
		get
		{
			return _currentlySyncing;
		}
		private set
		{
			if (IsValid && _currentlySyncing != value)
			{
				_currentlySyncing = value;
				((ModelItem)this).FirePropertyChanged("IsSyncing");
			}
		}
	}

	public SyncNotification SyncProgress
	{
		get
		{
			return _syncProgress;
		}
		private set
		{
			if (IsValid && _syncProgress != value)
			{
				_syncProgress = value;
				((ModelItem)this).FirePropertyChanged("SyncProgress");
			}
		}
	}

	public Command SyncBegun => _syncBegun;

	public Command SyncProgressed => _syncProgressed;

	public Command SyncCompleted => _syncCompleted;

	private Timer FormatSanityTimer
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_formatSanityTimer == null)
			{
				_formatSanityTimer = new Timer((IModelItemOwner)(object)this);
				_formatSanityTimer.Interval = 60000;
				_formatSanityTimer.AutoRepeat = false;
				_formatSanityTimer.Tick += FormatSanityFailed;
			}
			return _formatSanityTimer;
		}
	}

	public DateTime LastConnectTime
	{
		get
		{
			DateTime result = DateTime.MinValue;
			if (IsValid)
			{
				result = _device.LastConnectTime;
			}
			return result;
		}
	}

	public string LastSyncTime
	{
		get
		{
			string result = string.Empty;
			if (IsValid)
			{
				DateTime lastSyncTime = _device.LastSyncTime;
				if (lastSyncTime.Year >= 2006)
				{
					result = ((!(lastSyncTime.Date == DateTime.Now.Date)) ? lastSyncTime.ToString("d") : lastSyncTime.ToString("t"));
				}
			}
			return result;
		}
	}

	public string LastConnectTimestring
	{
		get
		{
			return _lastConnectTimestring ?? string.Empty;
		}
		private set
		{
			if (_lastConnectTimestring != value)
			{
				_lastConnectTimestring = value;
				((ModelItem)this).FirePropertyChanged("LastConnectTimestring");
			}
		}
	}

	public string LastSyncStartTimestring
	{
		get
		{
			return _lastSyncStartTimestring ?? LastConnectTimestring;
		}
		private set
		{
			if (_lastSyncStartTimestring != value)
			{
				_lastSyncStartTimestring = value;
				((ModelItem)this).FirePropertyChanged("LastSyncStartTimestring");
			}
		}
	}

	public UIGasGauge ActualGasGauge
	{
		get
		{
			return _actualGasGauge;
		}
		private set
		{
			if (_actualGasGauge != value)
			{
				_actualGasGauge = value;
				((ModelItem)this).FirePropertyChanged("ActualGasGauge");
			}
		}
	}

	public UIGasGauge PredictedGasGauge
	{
		get
		{
			return _predictedGasGauge;
		}
		private set
		{
			if (_predictedGasGauge != value)
			{
				_predictedGasGauge = value;
				((ModelItem)this).FirePropertyChanged("PredictedGasGauge");
			}
		}
	}

	public bool EnableWatson
	{
		get
		{
			uint num = 0u;
			if (IsConnectedToClient)
			{
				_device.GetWatsonSetting(ref num);
			}
			return num != 0;
		}
		set
		{
			if (IsConnectedToClient && EnableWatson != value)
			{
				_device.SetWatsonSetting(value ? 1u : 0u);
				((ModelItem)this).FirePropertyChanged("EnableWatson");
			}
		}
	}

	public event FallibleEventHandler FormatCompletedEvent;

	public event FallibleEventHandler EnumeratedEvent;

	public event FallibleEventHandler WiFiRemovalCompletedEvent;

	public event FallibleEventHandler WiFiProfilesSentEvent;

	public event FallibleEventHandler WiFiProfilesReceivedEvent;

	public event FallibleEventHandler WiFiTestCompletedEvent;

	public event FallibleEventHandler WiFiAssociationCompletedEvent;

	public event FallibleEventHandler ComputerWiFiProfilesLoadedEvent;

	public event FallibleEventHandler WiFiScanCompletedEvent;

	private void ReloadIconSet()
	{
		IconSet = null;
	}

	private void SetDeviceIconSetCallback(IDeviceIconSet iconSet)
	{
		IconSet = iconSet;
	}

	internal UIDevice(IModelItemOwner owner, Device device)
		: base(owner)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Expected O, but got Unknown
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Expected O, but got Unknown
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Expected O, but got Unknown
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Expected O, but got Unknown
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Expected O, but got Unknown
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Expected O, but got Unknown
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Expected O, but got Unknown
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Expected O, but got Unknown
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Expected O, but got Unknown
		_device = device;
		_lastLoginFailure = HRESULT._S_OK;
		_lastSyncFailure = HRESULT._S_OK;
		if (_device != null)
		{
			DeferredInvokeHandler val = null;
			_device.FriendlyNameChangedEvent += new FriendlyNameChangedHandler(OnNameChange);
			_device.DeviceStatusChangedEvent += new DeviceStatusChangedHandler(OnDeviceStatusChanged);
			_device.FormatCompleteEvent += new FormatCompleteHandler(OnFormatPhase1Completed);
			_device.SyncBegan += new SyncBeganHandler(OnSyncBegun);
			_device.SyncProgressed += new SyncProgressedHandler(OnSyncProgressed);
			_device.SyncCompleted += new SyncCompletedHandler(OnSyncCompleted);
			_device.UnassociateWlanDeviceCompleteEvent += new UnassociateWlanDeviceCompleteHandler(OnUnassociateWlanCompleted);
			_device.SetDeviceWlanProfilesCompleteEvent += new SetDeviceWlanProfilesCompleteHandler(OnSetDeviceWlanProfilesCompleted);
			_device.GetDeviceWlanProfilesCompleteEvent += new GetDeviceWlanProfilesCompleteHandler(OnGetDeviceWlanProfilesCompleted);
			_device.TestDeviceWlanCompleteEvent += new TestDeviceWlanCompleteHandler(OnTestWlanCompleted);
			_device.AssociateWlanDeviceCompleteEvent += new AssociateWlanDeviceCompleteHandler(OnAssociateWlanCompleted);
			_device.GetWlanProfilesCompleteEvent += new GetWlanProfilesCompleteHandler(OnGetWlanProfilesCompleted);
			_device.GetDeviceWlanNetworksCompleteEvent += new GetDeviceWlanNetworksCompleteHandler(OnGetDeviceWlanVisibleNetworksCompleted);
			_actualGasGauge = new UIGasGauge((IModelItemOwner)(object)this, _device.ActualGasGauge);
			_predictedGasGauge = new UIGasGauge((IModelItemOwner)(object)this, _device.PredictedGasGauge);
			if (_device.PredictedGasGauge != null)
			{
				_device.PredictedGasGauge.DeviceOverflowEvent += new DeviceOverflowHandler(OnDeviceOverfill);
			}
			EEndpointStatus currentStatus = _device.DeviceStatus;
			if ((int)currentStatus != 0)
			{
				if (val == null)
				{
					val = (DeferredInvokeHandler)delegate
					{
						//IL_0007: Unknown result type (might be due to invalid IL or missing references)
						//IL_000c: Unknown result type (might be due to invalid IL or missing references)
						HandleDeviceState(currentStatus, HRESULT._S_OK);
					};
				}
				Application.DeferredInvoke(val, (object)null);
			}
		}
		else
		{
			_actualGasGauge = new UIGasGauge((IModelItemOwner)(object)this, null);
			_predictedGasGauge = new UIGasGauge((IModelItemOwner)(object)this, null);
		}
		_syncBegun = new Command((IModelItemOwner)(object)this);
		_syncProgressed = new Command((IModelItemOwner)(object)this);
		_syncCompleted = new Command((IModelItemOwner)(object)this);
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Expected O, but got Unknown
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Expected O, but got Unknown
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Expected O, but got Unknown
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Expected O, but got Unknown
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Expected O, but got Unknown
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Expected O, but got Unknown
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Expected O, but got Unknown
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		if (_device != null)
		{
			_device.FriendlyNameChangedEvent -= new FriendlyNameChangedHandler(OnNameChange);
			_device.DeviceStatusChangedEvent -= new DeviceStatusChangedHandler(OnDeviceStatusChanged);
			_device.FormatCompleteEvent -= new FormatCompleteHandler(OnFormatPhase1Completed);
			if (_device.PredictedGasGauge != null)
			{
				_device.PredictedGasGauge.DeviceOverflowEvent -= new DeviceOverflowHandler(OnDeviceOverfill);
			}
			_device.SyncBegan -= new SyncBeganHandler(OnSyncBegun);
			_device.SyncProgressed -= new SyncProgressedHandler(OnSyncProgressed);
			_device.SyncCompleted -= new SyncCompletedHandler(OnSyncCompleted);
			_device.UnassociateWlanDeviceCompleteEvent -= new UnassociateWlanDeviceCompleteHandler(OnUnassociateWlanCompleted);
			_device.SetDeviceWlanProfilesCompleteEvent -= new SetDeviceWlanProfilesCompleteHandler(OnSetDeviceWlanProfilesCompleted);
			_device.GetDeviceWlanProfilesCompleteEvent -= new GetDeviceWlanProfilesCompleteHandler(OnGetDeviceWlanProfilesCompleted);
			_device.TestDeviceWlanCompleteEvent -= new TestDeviceWlanCompleteHandler(OnTestWlanCompleted);
			_device.AssociateWlanDeviceCompleteEvent -= new AssociateWlanDeviceCompleteHandler(OnAssociateWlanCompleted);
			_device.GetWlanProfilesCompleteEvent -= new GetWlanProfilesCompleteHandler(OnGetWlanProfilesCompleted);
			_device.GetDeviceWlanNetworksCompleteEvent -= new GetDeviceWlanNetworksCompleteHandler(OnGetDeviceWlanVisibleNetworksCompleted);
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public string GetLocalizedDevicePath(string devicePath)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		if (!IsValid)
		{
			return devicePath;
		}
		string result = devicePath;
		HRESULT val = HRESULT.op_Implicit(_device.GetLocalizedDevicePath(ref result));
		if (!((HRESULT)(ref val)).IsSuccess)
		{
			return devicePath;
		}
		return result;
	}

	public SyncMode GetSyncMode(SyncCategory schema)
	{
		return GetSyncMode(schema, fEstablishingPartnership: false);
	}

	internal SyncMode GetSyncMode(SyncCategory schema, bool fEstablishingPartnership)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected I4, but got Unknown
		ESyncMode val = (ESyncMode)(-1);
		if (IsValid && SupportsSyncCategory(schema))
		{
			_device.Rules.GetCategorySyncMode((ESyncCategory)schema, ref val, fEstablishingPartnership);
		}
		return (SyncMode)val;
	}

	public void SetSyncMode(SyncCategory schema, SyncMode mode)
	{
		if (IsValid)
		{
			_device.Rules.SetCategorySyncMode((ESyncCategory)schema, (ESyncMode)mode);
		}
	}

	public bool IsManualFor(MediaType type)
	{
		return IsManualFor(UIDeviceList.MapMediaTypeToSyncCategory(type));
	}

	public bool IsManualFor(SyncCategory type)
	{
		return IsManualFor(type, fEstablishingPartnership: false);
	}

	internal bool IsManualFor(SyncCategory type, bool fEstablishingPartnership)
	{
		return GetSyncMode(type, fEstablishingPartnership) == SyncMode.Manual;
	}

	public bool IsSyncAllFor(MediaType schema)
	{
		return IsSyncAllFor(UIDeviceList.MapMediaTypeToSyncCategory(schema));
	}

	public bool IsSyncAllFor(SyncCategory schema)
	{
		return IsSyncAllFor(schema, fEstablishingPartnership: false);
	}

	internal bool IsSyncAllFor(SyncCategory schema, bool fEstablishingPartnership)
	{
		return GetSyncMode(schema, fEstablishingPartnership) == SyncMode.SyncAll;
	}

	public void AddSyncRule(IList items)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		if (items == null || items.Count <= 0 || !IsValid)
		{
			return;
		}
		TypeDiscoveringSyncEventArgs args = new TypeDiscoveringSyncEventArgs();
		((SyncEventArgs)args).Device = _device;
		long startingSize = ActualGasGauge.UsedSpace;
		MaterializeMarketplaceMedia(items);
		MediaIdAndType[] threadSafeItems = GenerateThreadSafeDatabaseItems(items);
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			bool actionSucceeded = LibraryDataProvider.ActOnItems((IList)threadSafeItems, (BulkItemAction)3, (EventArgs)(object)args);
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				//IL_004b: Unknown result type (might be due to invalid IL or missing references)
				if (actionSucceeded)
				{
					string text = null;
					MediaType mediaType = ((args.ContainedTypes == null || args.ContainedTypes.Count != 1) ? MediaType.Undefined : ((MediaType)args.ContainedTypes[0]));
					if (mediaType == MediaType.Playlist)
					{
						object? obj = items[0];
						IDatabaseMedia val = (IDatabaseMedia)((obj is IDatabaseMedia) ? obj : null);
						if (val != null)
						{
							int playlistId = default(int);
							EMediaTypes val2 = default(EMediaTypes);
							val.GetMediaIdAndType(ref playlistId, ref val2);
							if (PlaylistManager.IsChannel(playlistId))
							{
								text = _channelNewRuleMessage.GetMessageForCount(items.Count);
							}
						}
					}
					if (text == null)
					{
						INewRuleMessageType[] newRuleMessageLookupTable = _newRuleMessageLookupTable;
						foreach (INewRuleMessageType newRuleMessageType in newRuleMessageLookupTable)
						{
							if (newRuleMessageType.Type == mediaType)
							{
								text = newRuleMessageType.GetMessageForCount(items.Count);
								break;
							}
						}
					}
					if (text == null)
					{
						text = _genericNewRuleMessage.GetMessageForCount(items.Count);
					}
					NotificationArea.Instance.Override(new SyncNewRuleAddedNotification(text, startingSize, PredictedGasGauge));
				}
				BeginSync(userInitiated: true, syncOnNextNotify: false);
			}, (object)null);
		}, null);
	}

	public void AddPlaylistSyncRule(int dbPlaylistId)
	{
		AddSyncRule(new MediaIdAndType[1]
		{
			new MediaIdAndType(dbPlaylistId, MediaType.Playlist)
		});
	}

	public void RemoveSyncRule(IList items)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if (items == null || items.Count <= 0 || !IsValid)
		{
			return;
		}
		SyncEventArgs args = new SyncEventArgs();
		args.Device = _device;
		MediaIdAndType[] threadSafeItems = GenerateThreadSafeDatabaseItems(items);
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			LibraryDataProvider.ActOnItems((IList)threadSafeItems, (BulkItemAction)4, (EventArgs)(object)args);
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				BeginSync(userInitiated: true, syncOnNextNotify: false);
			}, (object)null);
		}, null);
	}

	public void Exclude(IList items)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Invalid comparison between Unknown and I4
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		if (items == null || items.Count <= 0 || !IsValid)
		{
			return;
		}
		SyncEventArgs args = new SyncEventArgs();
		args.Device = _device;
		MediaIdAndType[] threadSafeItems = GenerateThreadSafeDatabaseItems(items);
		bool shouldBeginSyncAfter = false;
		foreach (object item in items)
		{
			LibraryDataProviderItemBase val = (LibraryDataProviderItemBase)((item is LibraryDataProviderItemBase) ? item : null);
			if (val != null)
			{
				object property = ((DataProviderObject)val).GetProperty("SyncState");
				if (property != null)
				{
					ESyncState val2 = (ESyncState)property;
					if ((int)val2 != 1 && (int)val2 != 0)
					{
						continue;
					}
				}
			}
			shouldBeginSyncAfter = true;
			break;
		}
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Expected O, but got Unknown
			DeferredInvokeHandler val3 = null;
			LibraryDataProvider.ActOnItems((IList)threadSafeItems, (BulkItemAction)5, (EventArgs)(object)args);
			if (shouldBeginSyncAfter)
			{
				if (val3 == null)
				{
					val3 = (DeferredInvokeHandler)delegate
					{
						BeginSync(userInitiated: true, syncOnNextNotify: false);
					};
				}
				Application.DeferredInvoke(val3, (object)null);
			}
		}, null);
	}

	public void Unexclude(IList items)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if (items == null || items.Count <= 0 || !IsValid)
		{
			return;
		}
		SyncEventArgs args = new SyncEventArgs();
		args.Device = _device;
		MediaIdAndType[] threadSafeItems = GenerateThreadSafeDatabaseItems(items);
		ThreadPool.QueueUserWorkItem(delegate
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Expected O, but got Unknown
			LibraryDataProvider.ActOnItems((IList)threadSafeItems, (BulkItemAction)6, (EventArgs)(object)args);
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				BeginSync(userInitiated: true, syncOnNextNotify: false);
			}, (object)null);
		}, null);
	}

	public void DeleteAndExclude(IList items)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		if (items != null && items.Count > 0 && IsReadyForSync)
		{
			DeferrableSyncEventArgs args = new DeferrableSyncEventArgs();
			((SyncEventArgs)args).Device = _device;
			MediaIdAndType[] threadSafeItems = GenerateThreadSafeDatabaseItems(items);
			ThreadPool.QueueUserWorkItem(delegate
			{
				LibraryDataProvider.ActOnItems((IList)threadSafeItems, (BulkItemAction)1, (EventArgs)(object)args);
			}, null);
		}
	}

	public SyncGroupList GenerateSyncGroupList(IModelItemOwner owner, bool expandSyncAllEntries)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		SyncRulesView snapshot = null;
		if (IsValid)
		{
			HRESULT.op_Implicit(_device.Rules.GenerateSnapshot(expandSyncAllEntries, ref snapshot));
		}
		return new SyncGroupList(owner, this, snapshot, expandSyncAllEntries);
	}

	public PodcastSyncLimit GetPodcastSyncLimit(int podcastID)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected I4, but got Unknown
		EDeviceSyncRuleType val = (EDeviceSyncRuleType)(-1);
		if (IsValid)
		{
			_device.Rules.GetSyncRuleForMedia((EMediaTypes)18, podcastID, ref val);
		}
		return (PodcastSyncLimit)val;
	}

	public void SetPodcastSyncLimit(int podcastID, PodcastSyncLimit limit)
	{
		if (IsValid)
		{
			int[] array = new int[1] { podcastID };
			_device.Rules.Add(array, (EMediaTypes)18, (EDeviceSyncRuleType)limit);
		}
	}

	public int GetPodcastSyncLimitWithValue(int podcastID)
	{
		int result = -1;
		if (IsValid && _device != null && _device.Rules != null)
		{
			_device.Rules.GetSyncRuleValueForMedia(podcastID, ref result);
		}
		return result;
	}

	public void SetPodcastSyncLimitWithValue(int podcastID, int value)
	{
		if (IsValid)
		{
			int[] array = new int[1] { podcastID };
			if (_device != null && _device.Rules != null)
			{
				_device.Rules.AddDeviceSyncRuleWithValue(array, value);
			}
		}
	}

	public void BeginSync()
	{
		BeginSync(userInitiated: false, syncOnNextNotify: false);
	}

	public void BeginSync(bool userInitiated, bool syncOnNextNotify)
	{
		if (IsReadyForSync && !IsLockedAgainstSyncing && (userInitiated || !UserStoppedLastSync))
		{
			if (syncOnNextNotify)
			{
				_device.StartSyncNextNotify();
			}
			else
			{
				_device.StartSync();
			}
		}
	}

	public void EndSync()
	{
		EndSync(userInitiated: false);
	}

	public void EndSync(bool userInitiated)
	{
		if (SyncProgress != null)
		{
			SyncProgress.SyncCanceled = true;
		}
		if (IsConnectedToClient)
		{
			_device.StopSync();
		}
		if (userInitiated)
		{
			if (IsGuest)
			{
				_device.ClearRules();
			}
			else
			{
				_device.ClearManualModeRules();
			}
			UserStoppedLastSync = true;
		}
	}

	public bool ReverseSync(IList items)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Invalid comparison between Unknown and I4
		if (items != null && items.Count > 0 && IsConnectedToClient && IsReadyForSync)
		{
			DeferrableSyncEventArgs e = new DeferrableSyncEventArgs();
			((SyncEventArgs)e).Device = _device;
			LibraryDataProvider.ActOnItems(items, (BulkItemAction)2, (EventArgs)(object)e);
			if ((int)e.Status == 1)
			{
				return false;
			}
		}
		return true;
	}

	public HRESULT Enumerate()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.StartEnumeration());
	}

	public HRESULT Format()
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		IsFormatting = true;
		return HRESULT.op_Implicit(_device.Format());
	}

	public HRESULT DeleteAllGuestContent()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		ESyncOperationStatus val = (ESyncOperationStatus)(-1);
		return HRESULT.op_Implicit(_device.DeleteAllGuestContent(ref val));
	}

	public HRESULT ForceAppUpdate()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.ForceAppUpdate());
	}

	public HRESULT ClearAccountAssociation()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = HRESULT._E_UNEXPECTED;
		if (IsConnectedToClient)
		{
			result = HRESULT.op_Implicit(_device.ClearUserGuidandZuneTag());
			_ = HasFailedLogin;
			((ModelItem)this).FirePropertyChanged("UserId");
			((ModelItem)this).FirePropertyChanged("ZuneTag");
			PurchaseEnabled = false;
		}
		return result;
	}

	public HRESULT AssociateWithAccount(Guid guid, string tag)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = HRESULT._E_UNEXPECTED;
		if (IsConnectedToClient)
		{
			result = HRESULT.op_Implicit(_device.SetUserGuidandZuneTag(guid, tag));
			if (((HRESULT)(ref result)).IsSuccess)
			{
				result = SetGeoId();
			}
			_ = HasFailedLogin;
			((ModelItem)this).FirePropertyChanged("UserId");
			((ModelItem)this).FirePropertyChanged("ZuneTag");
		}
		return result;
	}

	public void SendMarketplaceCredentials(string username, string password)
	{
		if (IsConnectedToClient)
		{
			SecureString secureUsername = Shell.MakeSecureString(username, readOnly: true);
			SecureString securePassword = Shell.MakeSecureString(password, readOnly: true);
			ThreadPool.QueueUserWorkItem(delegate
			{
				_device.SetMarketplaceCredentials(secureUsername, securePassword);
			});
		}
	}

	public HRESULT SetGeoId()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = HRESULT._E_UNEXPECTED;
		if (IsValid)
		{
			uint geoId = CultureHelper.GeoId();
			if (!CultureHelper.IsValidGeoId(geoId))
			{
				geoId = 0u;
			}
			result = HRESULT.op_Implicit(_device.SetGeoId(geoId));
		}
		return result;
	}

	public HRESULT RemoveWiFiAssociation()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.UnassociateWlanDevice());
	}

	public HRESULT SetWiFiProfileList(WlanProfileList list)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.SetWlanProfileList(list));
	}

	public HRESULT GetWiFiProfileList(ref WlanProfileList list)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.GetWlanProfileList(ref list));
	}

	public HRESULT SendWiFiProfiles()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.SetDeviceWlanProfiles());
	}

	public HRESULT ReceiveWiFiProfiles()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.GetDeviceWlanProfiles());
	}

	public HRESULT TestWiFi()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.TestDeviceWlan());
	}

	public HRESULT CancelWiFiTest()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.CancelTestDeviceWlan());
	}

	public HRESULT AssociateWiFi()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.AssociateWlanDevice());
	}

	public HRESULT LoadComputerWiFiProfiles()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.GetWlanProfiles());
	}

	public HRESULT ScanForWiFiNetworks()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.GetDeviceWlanNetworks());
	}

	public HRESULT GetDisconnectedWiFiUUID(ref string uuid)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsValid)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.GetDisconnectedWlanDeviceUuid(ref uuid));
	}

	public HRESULT UnassociateWiFiUUID(string uuid)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsValid)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.UnassociateWlanDeviceUuid(uuid));
	}

	public bool WirelessEnableRequiresElevation()
	{
		bool flag = true;
		if (IsValid)
		{
			_device.IsWlanFirewallEnabled(ref flag);
		}
		if (Environment.OSVersion.Version.Major < 6)
		{
			return false;
		}
		return !flag;
	}

	public HRESULT GetWiFiAuthorizationCipherList(ref WlanAuthCipherPairList list)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.GetWlanDeviceAuthCipherPairList(ref list));
	}

	public HRESULT GetWifiConnectedSSID(ref string connectedSSID)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.GetDeviceWlanConnectedSSID(ref connectedSSID));
	}

	public HRESULT IsWlanDeviceDisabled(ref bool disabled)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.IsWlanDeviceDisabled(ref disabled));
	}

	public HRESULT GetWifiMediaSyncSSID(ref string mediaSyncSSID)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.GetDeviceWlanMediaSyncSSID(ref mediaSyncSSID));
	}

	public HRESULT SetWifiMediaSyncSSID(string mediaSyncSSID)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		if (!IsConnectedToClient)
		{
			return HRESULT._E_UNEXPECTED;
		}
		return HRESULT.op_Implicit(_device.SetDeviceWlanMediaSyncSSID(mediaSyncSSID));
	}

	public override bool Equals(object obj)
	{
		if (obj is UIDevice uIDevice)
		{
			return _device == uIDevice._device;
		}
		return ((object)this).Equals(obj);
	}

	public override int GetHashCode()
	{
		if (_device == null)
		{
			return 0;
		}
		return ((object)_device).GetHashCode();
	}

	public bool SupportsRentalOfVideo(bool isHD)
	{
		if (SupportsRental)
		{
			if (isHD)
			{
				return SupportsHD;
			}
			return true;
		}
		return false;
	}

	public bool SupportsBrandingType(DeviceBranding brand)
	{
		bool result = false;
		switch (Class)
		{
		case DeviceClass.WindowsPhone:
			result = brand == DeviceBranding.WindowsPhone;
			break;
		case DeviceClass.Kin:
			result = brand == DeviceBranding.Kin;
			break;
		default:
			result = brand == DeviceBranding.Zune;
			break;
		case DeviceClass.Invalid:
		case DeviceClass.Reserved:
			break;
		}
		return result;
	}

	private void OnNameChange(Device device, string newName)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed)
			{
				((ModelItem)this).FirePropertyChanged("Name");
				((ModelItem)this).FirePropertyChanged("NameXMLEscaped");
			}
		}, (object)null);
	}

	private void OnDeviceOverfill(GasGauge gasGauge)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f1: Expected O, but got Unknown
			if (!((ModelItem)this).IsDisposed)
			{
				EndSync();
				if (IsGuest)
				{
					_device.ClearRules();
				}
				ZuneShell shell = ZuneShell.DefaultInstance;
				if (shell.CurrentPage is PlaybackPage)
				{
					shell.NavigateBack();
				}
				if (!(shell.CurrentPage is DialogPage) && !_isOosDialogVisible)
				{
					_isOosDialogVisible = true;
					if (IsGuest)
					{
						MessageBox.Show(Shell.LoadString(StringId.IDS_DEVICE_OUT_OF_SPACE_TITLE), string.Format(Shell.LoadString(StringId.IDS_GUEST_OUT_OF_SPACE_MESSAGE), Name), (EventHandler)null, (EventHandler)null, (EventHandler)null, (EventHandler)delegate
						{
							_isOosDialogVisible = false;
						});
					}
					else
					{
						Command val = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_HANDLE_DEVICE_OUT_OF_SPACE_BUTTON), (EventHandler)delegate
						{
							shell.NavigateToPage(new DeviceOverfillLand(this));
							_isOosDialogVisible = false;
						});
						MessageBox.Show(Shell.LoadString(StringId.IDS_DEVICE_OUT_OF_SPACE_TITLE), string.Format(Shell.LoadString(StringId.IDS_DEVICE_OUT_OF_SPACE_MESSAGE), Name), val, (string)null, (EventHandler)delegate
						{
							_isOosDialogVisible = false;
						}, false);
					}
				}
			}
		}, (object)null);
	}

	private void OnDeviceStatusChanged(Device device, int rawHR, EEndpointStatus endpointStatus)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			if (!((ModelItem)this).IsDisposed)
			{
				HandleDeviceState(endpointStatus, HRESULT.op_Implicit(rawHR));
			}
		}, (object)null);
	}

	private void OnFormatPhase1Completed(Device device, int rawHR)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			if (!((ModelItem)this).IsDisposed)
			{
				HRESULT hr = HRESULT.op_Implicit(rawHR);
				if (((HRESULT)(ref hr)).IsError)
				{
					OnFormatCompleted(hr);
				}
				else
				{
					FormatSanityTimer.Start();
				}
			}
		}, (object)null);
	}

	private void OnSyncBegun(Device device)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed)
			{
				SyncProgress = new SyncNotification(this);
				IsSyncing = true;
				SyncBegun.Invoke();
				SyncProgressed.Invoke();
				if (IsLockedAgainstSyncing)
				{
					EndSync();
				}
				else
				{
					UserStoppedLastSync = false;
				}
				LastSyncStartTimestring = GenerateSyncTimestring();
				((ModelItem)this).FirePropertyChanged("LastSyncTime");
			}
		}, (object)null);
	}

	private void OnSyncProgressed(Device device, uint percent, uint percentItem, uint percentTitle, string group, string title, ESyncEngineState engineState)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			if (!((ModelItem)this).IsDisposed)
			{
				if (SyncProgress != null)
				{
					SyncProgress.UpdateProgress((int)percent, (int)percentItem, (int)percentTitle, group, title, engineState);
				}
				SyncProgressed.Invoke();
			}
		}, (object)null);
	}

	private void OnSyncCompleted(Device device, ESyncEventReason reason)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_004d: Unknown result type (might be due to invalid IL or missing references)
			if (!((ModelItem)this).IsDisposed)
			{
				if (SyncProgress != null)
				{
					SyncProgress.Complete(reason);
					SyncProgress = null;
					if ((int)reason == 0)
					{
						LastSyncError = HRESULT._S_OK;
					}
					else
					{
						LastSyncError = HRESULT._E_FAIL;
					}
				}
				IsSyncing = false;
				SyncProgressed.Invoke();
				SyncCompleted.Invoke();
				((ModelItem)this).FirePropertyChanged("LastSyncTime");
			}
		}, (object)null);
	}

	private void FormatSanityFailed(object sender, EventArgs e)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if (IsFormatting)
		{
			OnFormatCompleted(HRESULT._E_FAIL);
			SingletonModelItem<UIDeviceList>.Instance.HideDevice(this);
		}
	}

	private void OnUnassociateWlanCompleted(Device device, int hr)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			if (!((ModelItem)this).IsDisposed)
			{
				UpdateWirelessSyncEnabled();
				if (this.WiFiRemovalCompletedEvent != null)
				{
					this.WiFiRemovalCompletedEvent(this, new FallibleEventArgs(HRESULT.op_Implicit(hr)));
				}
			}
		}, (object)null);
	}

	private void OnSetDeviceWlanProfilesCompleted(Device device, int hr)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			if (!((ModelItem)this).IsDisposed)
			{
				UpdateWirelessSyncEnabled();
				if (this.WiFiProfilesSentEvent != null)
				{
					this.WiFiProfilesSentEvent(this, new FallibleEventArgs(HRESULT.op_Implicit(hr)));
				}
			}
		}, (object)null);
	}

	private void OnGetDeviceWlanProfilesCompleted(Device device, int hr)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			if (!((ModelItem)this).IsDisposed && this.WiFiProfilesReceivedEvent != null)
			{
				this.WiFiProfilesReceivedEvent(this, new FallibleEventArgs(HRESULT.op_Implicit(hr)));
			}
		}, (object)null);
	}

	private void OnTestWlanCompleted(Device device, WlanTestResultCode result, int hr)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			UpdateWirelessSyncEnabled();
			if (!((ModelItem)this).IsDisposed && this.WiFiTestCompletedEvent != null)
			{
				this.WiFiTestCompletedEvent(this, new FallibleEventArgs(HRESULT.op_Implicit(hr)));
			}
		}, (object)null);
	}

	private void OnAssociateWlanCompleted(Device device, int hr)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			UpdateWirelessSyncEnabled();
			if (!((ModelItem)this).IsDisposed && this.WiFiAssociationCompletedEvent != null)
			{
				this.WiFiAssociationCompletedEvent(this, new FallibleEventArgs(HRESULT.op_Implicit(hr)));
			}
		}, (object)null);
	}

	private void OnGetWlanProfilesCompleted(Device device, int hr)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			if (!((ModelItem)this).IsDisposed && this.ComputerWiFiProfilesLoadedEvent != null)
			{
				this.ComputerWiFiProfilesLoadedEvent(this, new FallibleEventArgs(HRESULT.op_Implicit(hr)));
			}
		}, (object)null);
	}

	private void OnGetDeviceWlanVisibleNetworksCompleted(Device device, int hr)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			if (!((ModelItem)this).IsDisposed && this.WiFiScanCompletedEvent != null)
			{
				this.WiFiScanCompletedEvent(this, new FallibleEventArgs(HRESULT.op_Implicit(hr)));
			}
		}, (object)null);
	}

	private void ReleaseFirmwareObjects()
	{
		UIFirmwareUpdater = null;
		UIFirmwareRestorer = null;
	}

	private void MaterializeMarketplaceMedia(IList items)
	{
		new List<int>(items.Count);
		bool flag = default(bool);
		bool flag2 = default(bool);
		for (int i = 0; i < items.Count; i++)
		{
			object? obj = items[i];
			DataProviderObject val = (DataProviderObject)((obj is DataProviderObject) ? obj : null);
			if (val == null)
			{
				continue;
			}
			if (val is Track)
			{
				Track track = (Track)(object)val;
				int num = -1;
				if (!ZuneApplication.Service.InCompleteCollection(track.Id, (EContentType)0, ref num, ref flag) && (track.IsDownloading || track.CanDownload || track.CanPurchase))
				{
					num = ZuneApplication.ZuneLibrary.AddTrack(track.Id, track.AlbumId, track.TrackNumber, track.Title, track.Duration, track.AlbumTitle, track.Artist, track.PrimaryGenre.Title);
				}
				if (num >= 0)
				{
					track.LibraryId = num;
				}
			}
			else if (val is Album)
			{
				Album album = (Album)(object)val;
				int num2 = ZuneApplication.ZuneLibrary.AddAlbum(album.Id, album.Title, album.Artist);
				if (num2 >= 0)
				{
					album.LibraryId = num2;
				}
			}
			else if (val is PodcastSeries)
			{
				PodcastSeries podcastSeries = (PodcastSeries)(object)val;
				string sourceUrl = podcastSeries.SourceUrl;
				if (!string.IsNullOrEmpty(sourceUrl))
				{
					SubscriptionState subscriptionState = ZuneShell.DefaultInstance.Management.GetSubscriptionState(sourceUrl, (EMediaTypes)18);
					if (subscriptionState != null && !subscriptionState.IsSubscribed)
					{
						subscriptionState = ZuneShell.DefaultInstance.Management.SubscribeToPodcastFeed(sourceUrl, podcastSeries.Title, podcastSeries.Id, (ESubscriptionSource)0);
					}
				}
			}
			else if (val is MusicVideo)
			{
				MusicVideo musicVideo = (MusicVideo)(object)val;
				int num3 = -1;
				if (!ZuneApplication.Service.InCompleteCollection(musicVideo.Id, (EContentType)3, ref num3, ref flag2) && (musicVideo.IsDownloading || musicVideo.CanPurchase))
				{
					num3 = ZuneApplication.ZuneLibrary.AddVideo(musicVideo.Id, musicVideo.Title, musicVideo.Duration);
				}
				if (num3 >= 0)
				{
					musicVideo.LibraryId = num3;
				}
			}
		}
	}

	private void HandleDeviceState(EEndpointStatus state, HRESULT hr)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Expected O, but got Unknown
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Invalid comparison between Unknown and I4
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Expected I4, but got Unknown
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		bool isValid = IsValid;
		bool isConnectedToPC = IsConnectedToPC;
		bool isConnectedToClient = IsConnectedToClient;
		bool isConnectedToSideloader = IsConnectedToSideloader;
		bool isReadyForSync = IsReadyForSync;
		_lastDeviceState = state;
		if (isValid != IsValid)
		{
			ReverseSyncPicsFromPhone = true;
			((ModelItem)this).FirePropertyChanged("IsValid");
		}
		if (isConnectedToPC != IsConnectedToPC)
		{
			((ModelItem)this).FirePropertyChanged("IsConnectedToPC");
			if (!FeatureEnablement.IsFeatureEnabled((Features)1))
			{
				ClientConfiguration.FeaturesOverride.Device = 1;
				FeatureEnablement.ForceFeatureOn((Features)1);
			}
			Application.DeferredInvoke((DeferredInvokeHandler)delegate
			{
				Shell.MainFrame.Device.UpdateShowDevice();
				Shell.SettingsFrame.Settings.ShowDevice(show: true);
			}, (object)null);
		}
		if (isConnectedToClient != IsConnectedToClient)
		{
			if (IsConnectedToClient)
			{
				ReloadIconSet();
				if (IsFormatting)
				{
					OnFormatCompleted(HRESULT._S_OK);
				}
			}
			if (!ShouldSuppressConnectionNotifications())
			{
				OnConnectivityChanged();
				((ModelItem)this).FirePropertyChanged("IsConnectedToClient");
				((ModelItem)this).FirePropertyChanged("IsConnectedToClientWirelessly");
				((ModelItem)this).FirePropertyChanged("IsConnectedToClientPhysically");
				if (isReadyForSync != IsReadyForSync)
				{
					((ModelItem)this).FirePropertyChanged("IsReadyForSync");
				}
			}
		}
		if (isConnectedToSideloader != IsConnectedToSideloader)
		{
			((ModelItem)this).FirePropertyChanged("IsConnectedToSideloader");
			((ModelItem)this).FirePropertyChanged("OwnerApplicationName");
		}
		if (!IsConnectedToPC)
		{
			IsReadyForSync = false;
		}
		else if ((int)state >= 14)
		{
			IsReadyForSync = true;
		}
		switch (state - 6)
		{
		case 9:
			MessageBox.Show(Shell.LoadString(StringId.IDS_PHONE_NO_IP_TITLE), Shell.LoadString(StringId.IDS_PHONE_NO_IP_TEXT), (EventHandler)null);
			break;
		case 1:
			ShowPinUnlockDialog();
			break;
		case 0:
			HidePinUnlockDialog(unlockSucceeded: false);
			MessageBox.Show(Shell.LoadString(StringId.IDS_PHONE_TLS_ERROR_TITLE), Shell.LoadString(StringId.IDS_PHONE_TLS_ERROR_TEXT), (EventHandler)null);
			break;
		case 2:
			HidePinUnlockDialog(unlockSucceeded: true);
			break;
		case 7:
			if (!((HRESULT)(ref hr)).IsSuccess)
			{
				MessageBox.Show(Shell.LoadString(StringId.IDS_DEVICE_ENUMERATION_FAILED_TITLE), Shell.LoadString(StringId.IDS_DEVICE_ENUMERATION_FAILED_BODY), (EventHandler)null);
				SingletonModelItem<UIDeviceList>.Instance.HideDevice(this);
			}
			if (this.EnumeratedEvent != null)
			{
				this.EnumeratedEvent(this, new FallibleEventArgs(hr));
			}
			break;
		case 8:
			if (IsFormatting)
			{
				OnFormatCompleted(HRESULT._S_OK);
				break;
			}
			BeginSync();
			if (SupportsPaidApplications)
			{
				ForceAppUpdate();
			}
			break;
		case 3:
		case 4:
		case 5:
		case 6:
			break;
		}
	}

	private void OnConnectivityChanged()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		if (IsConnectedToClient)
		{
			HRESULT lastFailedLoginError = HRESULT._S_OK;
			if (UserGuid != Guid.Empty)
			{
				int num = 0;
				HRESULT val = HRESULT.op_Implicit(_device.GetAndResetLastLoginError(ref num));
				if (((HRESULT)(ref val)).IsSuccess)
				{
					lastFailedLoginError = HRESULT.op_Implicit(num);
				}
			}
			LastFailedLoginError = lastFailedLoginError;
		}
		else
		{
			if (SyncProgress != null)
			{
				SyncProgress.SyncCanceled = true;
				SyncProgress.Complete((ESyncEventReason)0);
				SyncProgress = null;
			}
			ReleaseFirmwareObjects();
		}
		if (ActualGasGauge != null)
		{
			((ModelItem)ActualGasGauge).Dispose();
		}
		if (PredictedGasGauge != null)
		{
			((ModelItem)PredictedGasGauge).Dispose();
		}
		ActualGasGauge = new UIGasGauge((IModelItemOwner)(object)this, _device.ActualGasGauge);
		PredictedGasGauge = new UIGasGauge((IModelItemOwner)(object)this, _device.PredictedGasGauge);
		if (_device.PredictedGasGauge != null)
		{
			_device.PredictedGasGauge.DeviceOverflowEvent -= new DeviceOverflowHandler(OnDeviceOverfill);
			_device.PredictedGasGauge.DeviceOverflowEvent += new DeviceOverflowHandler(OnDeviceOverfill);
		}
		UpdateWirelessSyncEnabled();
		((ModelItem)this).FirePropertyChanged("CanonicalName");
		((ModelItem)this).FirePropertyChanged("ZuneTag");
		((ModelItem)this).FirePropertyChanged("RequiresClientUpdate");
		((ModelItem)this).FirePropertyChanged("RequiresFirmwareUpdate");
		((ModelItem)this).FirePropertyChanged("FirmwareVersion");
		UserStoppedLastSync = false;
		LastConnectTimestring = GenerateSyncTimestring();
	}

	private void OnFormatCompleted(HRESULT hr)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		FormatSanityTimer.Stop();
		IsFormatting = false;
		if (this.FormatCompletedEvent != null)
		{
			this.FormatCompletedEvent(this, new FallibleEventArgs(hr));
		}
	}

	private bool ShouldSuppressConnectionNotifications()
	{
		if (!IsFormatting && (UIFirmwareUpdater == null || !UIFirmwareUpdater.UpdateInProgress))
		{
			if (UIFirmwareRestorer != null)
			{
				return UIFirmwareRestorer.RestoreInProgress;
			}
			return false;
		}
		return true;
	}

	private void UpdateWirelessSyncEnabled()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		bool isWirelessSyncEnabled = false;
		if (IsConnectedToClient)
		{
			string mediaSyncSSID = null;
			bool disabled = false;
			HRESULT val = IsWlanDeviceDisabled(ref disabled);
			if (((HRESULT)(ref val)).IsSuccess)
			{
				val = GetWifiMediaSyncSSID(ref mediaSyncSSID);
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				isWirelessSyncEnabled = !disabled && !string.IsNullOrEmpty(mediaSyncSSID);
			}
		}
		IsWirelessSyncEnabled = isWirelessSyncEnabled;
	}

	private void ShowPinUnlockDialog()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		if (_pinUnlockMessageBox == null)
		{
			_pinUnlockMessageBox = MessageBox.Show(Shell.LoadString(StringId.IDS_PHONE_PIN_UNLOCK_TITLE), Shell.LoadString(StringId.IDS_PHONE_PIN_UNLOCK_TEXT), (Command)null, Shell.LoadString(StringId.IDS_CANCEL_BUTTON), (EventHandler)PinUnlockCancelled, false);
			return;
		}
		if ((int)Application.RenderingType == 0 && SingletonModelItem<TransportControls>.Instance.PlayingVideo)
		{
			SingletonModelItem<TransportControls>.Instance.Stop.Invoke();
		}
		((DialogHelper)_pinUnlockMessageBox).Show();
	}

	private void HidePinUnlockDialog(bool unlockSucceeded)
	{
		if (_pinUnlockMessageBox != null)
		{
			if (unlockSucceeded)
			{
				((DialogHelper)_pinUnlockMessageBox).Hide();
			}
			else
			{
				((DialogHelper)_pinUnlockMessageBox).Cancel.Invoke();
			}
			_pinUnlockMessageBox = null;
		}
	}

	private void PinUnlockCancelled(object sender, EventArgs args)
	{
		SingletonModelItem<UIDeviceList>.Instance.HideDevice(this);
	}

	private string GenerateSyncTimestring()
	{
		CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
		return DateTime.UtcNow.ToString("yyyy-MM-dd HH\\:mm\\:ss.fff", cultureInfo);
	}

	private bool SupportsSyncCategory(SyncCategory category)
	{
		return category switch
		{
			SyncCategory.Application => SupportsSyncApplications, 
			SyncCategory.Channel => SupportsChannels, 
			SyncCategory.Friend => SupportsUserCards, 
			_ => true, 
		};
	}

	private MediaIdAndType[] GenerateThreadSafeDatabaseItems(IList source)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		MediaIdAndType[] array = new MediaIdAndType[source.Count];
		int mediaId = default(int);
		EMediaTypes type = default(EMediaTypes);
		for (int i = 0; i < source.Count; i++)
		{
			object? obj = source[i];
			IDatabaseMedia val = (IDatabaseMedia)((obj is IDatabaseMedia) ? obj : null);
			if (val != null)
			{
				val.GetMediaIdAndType(ref mediaId, ref type);
				array[i] = new MediaIdAndType(mediaId, type);
			}
		}
		return array;
	}

	public static void WarnUserAboutFriendSyncSize()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Expected O, but got Unknown
		if (ClientConfiguration.MediaStore.AlertSyncAllFriendsBehavior)
		{
			BooleanChoice neverAlertSyncAllFriendsBehavior = new BooleanChoice((IModelItemOwner)(object)ZuneShell.DefaultInstance, Shell.LoadString(StringId.IDS_DONT_SHOW_THIS_MESSAGE_AGAIN));
			neverAlertSyncAllFriendsBehavior.Value = false;
			((Choice)neverAlertSyncAllFriendsBehavior).ChosenChanged += delegate
			{
				ClientConfiguration.MediaStore.AlertSyncAllFriendsBehavior = !neverAlertSyncAllFriendsBehavior.Value;
			};
			MessageBox.Show(Shell.LoadString(StringId.IDS_SYNC_FRIENDS_NOTICE_TITLE), Shell.LoadString(StringId.IDS_SYNC_FRIENDS_NOTICE), (Command)null, neverAlertSyncAllFriendsBehavior);
		}
	}

	public static string FormatSyncStatus(object title, IList metadata)
	{
		string text = (title ?? string.Empty).ToString();
		bool flag = false;
		foreach (object item in metadata)
		{
			string text2 = (item ?? string.Empty).ToString();
			if (!string.IsNullOrEmpty(text2))
			{
				if (flag)
				{
					text += _syncStatusDatumDivider;
				}
				else
				{
					text += _syncStatusTitleDivider;
					flag = true;
				}
				text += text2;
			}
		}
		return text;
	}

	public static string FormatDeviceClass(DeviceClass deviceClass)
	{
		return deviceClass switch
		{
			DeviceClass.ZuneHD => Shell.LoadString(StringId.IDS_AppsZuneHDDeviceName), 
			DeviceClass.WindowsPhone => Shell.LoadString(StringId.IDS_AppsWindowsPhoneDeviceName), 
			_ => Shell.LoadString(StringId.IDS_GENERIC_ERROR), 
		};
	}

	public static DeviceClass ToDeviceClass(int deviceClass)
	{
		return (DeviceClass)deviceClass;
	}

	public static DeviceClass ToDeviceClass(object deviceClass)
	{
		DeviceClass? deviceClass2 = (DeviceClass?)deviceClass;
		if (!deviceClass2.HasValue)
		{
			return DeviceClass.Invalid;
		}
		return deviceClass2.Value;
	}

	public static int ToInt32(DeviceClass deviceClass)
	{
		return (int)deviceClass;
	}
}
