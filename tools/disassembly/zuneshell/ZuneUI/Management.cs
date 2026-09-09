using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Subscription;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public class Management : ModelItem
{
	private struct MonitoredFolder(string path, EMediaTypes schema)
	{
		public readonly string Path = path;

		public readonly EMediaTypes Schema = schema;
	}

	private static CategoryPage _currentCategoryPage;

	private Category _alertedDeviceCategory;

	private DeviceManagement _deviceManagement;

	private bool _deviceManagementLocked;

	private CommitListHashtable _commitList;

	private bool _hasPendingCommits;

	private bool _changeRequiresElevation;

	private BooleanChoice _sqmChoice;

	private ListDataSet _monitoredAudioFolders;

	private ListDataSet _monitoredPhotoFolders;

	private ListDataSet _monitoredPodcastFolders;

	private ListDataSet _monitoredVideoFolders;

	private BooleanChoice _mediaInfoChoice;

	private BooleanChoice _metadataChoice;

	private List<MonitoredFolder> _removedMonitoredFoldersToRemoveFromCollection;

	private ProxySettingDelegate[] _actionsToCommitOnLibraryIntegrate;

	private Choice _podcastDefaultKeepEpisodesChoice;

	private Choice _podcastPlaybackChoice;

	private string[] _defaultFileTypeExtensions = new string[7] { ".mp3", ".m4a", ".mp4", ".m4b", ".m4v", ".mbr", ".zpl" };

	private IFileAssociationHandler _fileAssocHandler;

	private IList<BooleanInputChoice> _allFileTypes;

	private IList<BooleanInputChoice> _audioFileTypes;

	private IList<BooleanInputChoice> _videoFileTypes;

	private IList<FileAssociationInfo> _fileAssociationInfoList;

	private bool _canFileAssociationBeChanged;

	private ITunerInfoHandler _tunerHandler;

	private ArrayListDataSet _registeredComputersModelList;

	private ArrayListDataSet _registeredDevicesModelList;

	private ArrayListDataSet _registeredAppStoreDevicesModelList;

	private string _nextPCDeregistrationDate;

	private string _nextSubscriptionDeviceDeregistrationDate;

	private string _nextAppStoreDeviceDeregistrationDate;

	private IntRangedValue _slideShowSpeed;

	private Choice _burnDiscFormat;

	private BooleanChoice _autoEjectCDAfterBurn;

	private Choice _burnSpeed;

	private Choice _recordMode;

	private Choice _recordRate;

	private BooleanChoice _autoCopyCD;

	private BooleanChoice _autoEjectCD;

	private string _mediaFolder;

	private string _videoMediaFolder;

	private string _photoMediaFolder;

	private string _podcastMediaFolder;

	private Choice _wmaRate;

	private Choice _wmavRate;

	private Choice _mp3Rate;

	private string _sharingError;

	private string _sharingDisplayName;

	private BooleanChoice _sharingEnableMusic;

	private BooleanChoice _sharingEnableVideo;

	private BooleanChoice _sharingEnablePhoto;

	private Choice _sharingSelectDeviceChoice;

	private IList<Command> _sharingSelectDeviceOptions;

	private bool _sharingAllDevicesEnabled;

	private IList<BooleanInputChoice> _sharingDeviceList;

	private uint _sharingDeviceIndex;

	private HMESettings _HME;

	private bool _nssDeviceListChangeEventAdded;

	private string _backgroundImage;

	private WindowColor _backgroundColor;

	private BooleanChoice _showNowPlayingBackgroundOnIdle;

	private Choice _screenGraphicsSlider;

	private BooleanChoice _playSounds;

	private BooleanChoice _compactModeAlwaysOnTop;

	private BooleanChoice _ratingsChoice;

	private BooleanChoice _applyRatingsChoice;

	private Choice _startupPageChoice;

	private bool _autoLaunchZuneOnConnect;

	private object mylock = new object();

	private IFileAssociationHandler FileAssocHandler
	{
		get
		{
			if (_fileAssocHandler == null)
			{
				_fileAssocHandler = FileAssociationHandlerFactory.CreateFileAssociationHandler();
			}
			return _fileAssocHandler;
		}
	}

	public DeviceManagement DeviceManagement
	{
		get
		{
			if (_deviceManagement == null && !DeviceManagementLocked)
			{
				_deviceManagement = new DeviceManagement();
			}
			return _deviceManagement;
		}
	}

	public bool DeviceManagementLocked
	{
		get
		{
			return _deviceManagementLocked;
		}
		private set
		{
			if (_deviceManagementLocked != value)
			{
				_deviceManagementLocked = value;
				((ModelItem)this).FirePropertyChanged("DeviceManagementLocked");
			}
		}
	}

	public bool DeviceManagementChanged => true;

	public CommitListHashtable CommitList
	{
		get
		{
			if (_commitList == null)
			{
				_commitList = new CommitListHashtable();
			}
			return _commitList;
		}
		set
		{
			if (_commitList != value)
			{
				_commitList = value;
				((ModelItem)this).FirePropertyChanged("CommitList");
				if (value == null)
				{
					HasPendingCommits = false;
				}
			}
		}
	}

	public bool HasPendingCommits
	{
		get
		{
			return _hasPendingCommits;
		}
		internal set
		{
			if (_hasPendingCommits != value)
			{
				_hasPendingCommits = value;
				((ModelItem)this).FirePropertyChanged("HasPendingCommits");
			}
		}
	}

	public bool ActiveDeviceHasPendingCommits => CommitList.ContainsIntValue(SyncControls.Instance.CurrentDevice.ID);

	public bool ChangeRequiresElevation
	{
		get
		{
			return _changeRequiresElevation;
		}
		set
		{
			if (_changeRequiresElevation != value)
			{
				_changeRequiresElevation = value;
				((ModelItem)this).FirePropertyChanged("ChangeRequiresElevation");
			}
		}
	}

	public string BuildNumber => VersionInfo.BuildNumber;

	public Choice RecordMode
	{
		get
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Expected O, but got Unknown
			//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
			//IL_0107: Expected O, but got Unknown
			//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_01c7: Expected O, but got Unknown
			//IL_0273: Unknown result type (might be due to invalid IL or missing references)
			//IL_027d: Expected O, but got Unknown
			if (_recordMode == null)
			{
				_wmaRate = new Choice((IModelItemOwner)(object)this);
				_wmaRate.Options = new NamedIntOption[6]
				{
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMA_48), 48000),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMA_64), 64000),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMA_96), 96000),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMA_128), 128000),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMA_160), 160000),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMA_192), 192000)
				};
				NamedIntOption.SelectOptionByValue(_wmaRate, ClientConfiguration.Recorder.WMARecordRate);
				_wmaRate.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnWmaRateCommit)] = null;
				};
				_wmavRate = new Choice((IModelItemOwner)(object)this);
				_wmavRate.Options = new NamedIntOption[5]
				{
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMAV_25), 25),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMAV_50), 50),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMAV_75), 75),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMAV_90), 90),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_WMAV_98), 98)
				};
				NamedIntOption.SelectOptionByValue(_wmavRate, ClientConfiguration.Recorder.WMAVBRRecordQuality);
				_wmavRate.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnWmavRateCommit)] = null;
				};
				_mp3Rate = new Choice((IModelItemOwner)(object)this);
				_mp3Rate.Options = new NamedIntOption[4]
				{
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_MP3_128), 128000),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_MP3_192), 192000),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_MP3_256), 256000),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_RIP_MP3_320), 320000)
				};
				NamedIntOption.SelectOptionByValue(_mp3Rate, ClientConfiguration.Recorder.MP3RecordRate);
				_mp3Rate.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnMp3RateCommit)] = null;
				};
				_recordMode = new Choice((IModelItemOwner)(object)this);
				_recordMode.Options = new RecordModeOption[4]
				{
					new RecordModeOption(null, Shell.LoadString(StringId.IDS_WMA_OPTION), 0, _wmaRate),
					new RecordModeOption(null, Shell.LoadString(StringId.IDS_WMA_VARIABLE_OPTION), 3, _wmavRate),
					new RecordModeOption(null, Shell.LoadString(StringId.IDS_WMA_LOSSLESS_OPTION), 1, null),
					new RecordModeOption(null, Shell.LoadString(StringId.IDS_MP3_OPTION), 2, _mp3Rate)
				};
				NamedIntOption.SelectOptionByValue(_recordMode, ClientConfiguration.Recorder.RecordMode);
				_recordMode.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnRecordModeCommit)] = null;
					RecordRate = ((RecordModeOption)_recordMode.ChosenValue).BitRate;
				};
				RecordRate = ((RecordModeOption)_recordMode.ChosenValue).BitRate;
			}
			return _recordMode;
		}
	}

	public Choice RecordRate
	{
		get
		{
			return _recordRate;
		}
		private set
		{
			if (_recordRate != value)
			{
				_recordRate = value;
				((ModelItem)this).FirePropertyChanged("RecordRate");
			}
		}
	}

	public Category AlertedDeviceCategory
	{
		get
		{
			return _alertedDeviceCategory;
		}
		set
		{
			if (_alertedDeviceCategory != value)
			{
				if (_currentCategoryPage != null && value != null && _alertedDeviceCategory != null)
				{
					_currentCategoryPage.CurrentCategory = _alertedDeviceCategory;
					_alertedDeviceCategory = null;
				}
				else
				{
					_alertedDeviceCategory = value;
					((ModelItem)this).FirePropertyChanged("AlertedDeviceCategory");
				}
			}
		}
	}

	public CategoryPage CurrentCategoryPage
	{
		get
		{
			return _currentCategoryPage;
		}
		set
		{
			if (_currentCategoryPage != value)
			{
				_currentCategoryPage = value;
				((ModelItem)this).FirePropertyChanged("CurrentCategoryPage");
			}
		}
	}

	public bool CanFileAssociationBeChanged
	{
		get
		{
			return _canFileAssociationBeChanged;
		}
		private set
		{
			if (_canFileAssociationBeChanged != value)
			{
				_canFileAssociationBeChanged = value;
				((ModelItem)this).FirePropertyChanged("CanFileAssociationBeChanged");
			}
		}
	}

	public ListDataSet MonitoredAudioFolders
	{
		get
		{
			if (_monitoredAudioFolders == null)
			{
				if (UsingWin7Libraries)
				{
					_monitoredAudioFolders = StringsToListDataSet(ClientConfiguration.Groveler.RipDirectory, ClientConfiguration.Groveler.MonitoredAudioFolders);
				}
				else
				{
					_monitoredAudioFolders = StringsToListDataSet(ClientConfiguration.Groveler.MonitoredAudioFolders);
				}
			}
			return _monitoredAudioFolders;
		}
	}

	public ListDataSet MonitoredPhotoFolders
	{
		get
		{
			if (_monitoredPhotoFolders == null)
			{
				if (UsingWin7Libraries)
				{
					_monitoredPhotoFolders = StringsToListDataSet(ClientConfiguration.Groveler.PhotoMediaFolder, ClientConfiguration.Groveler.MonitoredPhotoFolders);
				}
				else
				{
					_monitoredPhotoFolders = StringsToListDataSet(ClientConfiguration.Groveler.MonitoredPhotoFolders);
				}
			}
			return _monitoredPhotoFolders;
		}
	}

	public ListDataSet MonitoredPodcastFolders
	{
		get
		{
			if (_monitoredPodcastFolders == null)
			{
				if (UsingWin7Libraries)
				{
					_monitoredPodcastFolders = StringsToListDataSet(ClientConfiguration.Groveler.PodcastMediaFolder, ClientConfiguration.Groveler.MonitoredPodcastFolders);
				}
				else
				{
					_monitoredPodcastFolders = StringsToListDataSet(ClientConfiguration.Groveler.MonitoredPodcastFolders);
				}
			}
			return _monitoredPodcastFolders;
		}
	}

	public ListDataSet MonitoredVideoFolders
	{
		get
		{
			if (_monitoredVideoFolders == null)
			{
				if (UsingWin7Libraries)
				{
					_monitoredVideoFolders = StringsToListDataSet(ClientConfiguration.Groveler.VideoMediaFolder, ClientConfiguration.Groveler.MonitoredVideoFolders);
				}
				else
				{
					_monitoredVideoFolders = StringsToListDataSet(ClientConfiguration.Groveler.MonitoredVideoFolders);
				}
			}
			return _monitoredVideoFolders;
		}
	}

	public bool Win7LibrariesAreAvailable => OSVersion.IsWin7();

	public bool UsingWin7Libraries
	{
		get
		{
			if (Win7LibrariesAreAvailable)
			{
				return ClientConfiguration.Groveler.LibrarySync != -1;
			}
			return false;
		}
	}

	public BooleanChoice AutoCopyCD
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_autoCopyCD == null)
			{
				_autoCopyCD = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_AUTO_RIP));
				_autoCopyCD.Value = ClientConfiguration.Recorder.AutoCopyCD != 0;
				((Choice)_autoCopyCD).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnAutoCopyCDCommit)] = null;
				};
			}
			return _autoCopyCD;
		}
	}

	public BooleanChoice AutoEjectCD
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_autoEjectCD == null)
			{
				_autoEjectCD = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_EJECT_AFTER_RIP));
				_autoEjectCD.Value = ClientConfiguration.Recorder.AutoEjectCD != 0;
				((Choice)_autoEjectCD).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnAutoEjectCDCommit)] = null;
				};
			}
			return _autoEjectCD;
		}
	}

	public string MediaFolder
	{
		get
		{
			if (_mediaFolder == null)
			{
				_mediaFolder = LocalizationHelper.GetLocalizedFolderPath(ClientConfiguration.Groveler.RipDirectory);
			}
			return _mediaFolder;
		}
		set
		{
			if (_mediaFolder != value)
			{
				CommitList[new ProxySettingDelegate(OnMediaFolderCommit)] = null;
				_mediaFolder = value;
				((ModelItem)this).FirePropertyChanged("MediaFolder");
			}
		}
	}

	public string VideoMediaFolder
	{
		get
		{
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			if (_videoMediaFolder == null)
			{
				_videoMediaFolder = ClientConfiguration.Groveler.VideoMediaFolder;
				if (string.IsNullOrEmpty(_videoMediaFolder))
				{
					string[] array = default(string[]);
					string text = default(string);
					string path = default(string);
					HRESULT val = HRESULT.op_Implicit(ZuneApplication.ZuneLibrary.GetKnownFolders(ref array, ref array, ref array, ref array, ref array, ref text, ref path, ref text, ref text, ref text));
					if (((HRESULT)(ref val)).IsSuccess)
					{
						_videoMediaFolder = LocalizationHelper.GetLocalizedFolderPath(path);
					}
				}
			}
			return _videoMediaFolder;
		}
		set
		{
			if (_videoMediaFolder != value)
			{
				CommitList[new ProxySettingDelegate(OnVideoMediaFolderCommit)] = null;
				_videoMediaFolder = value;
				((ModelItem)this).FirePropertyChanged("VideoMediaFolder");
			}
		}
	}

	public string PhotoMediaFolder
	{
		get
		{
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			if (_photoMediaFolder == null)
			{
				_photoMediaFolder = ClientConfiguration.Groveler.PhotoMediaFolder;
				if (string.IsNullOrEmpty(_photoMediaFolder))
				{
					string[] array = default(string[]);
					string text = default(string);
					string path = default(string);
					HRESULT val = HRESULT.op_Implicit(ZuneApplication.ZuneLibrary.GetKnownFolders(ref array, ref array, ref array, ref array, ref array, ref text, ref text, ref path, ref text, ref text));
					if (((HRESULT)(ref val)).IsSuccess)
					{
						_photoMediaFolder = LocalizationHelper.GetLocalizedFolderPath(path);
					}
				}
			}
			return _photoMediaFolder;
		}
		set
		{
			if (_photoMediaFolder != value)
			{
				CommitList[new ProxySettingDelegate(OnPhotoMediaFolderCommit)] = null;
				_photoMediaFolder = value;
				((ModelItem)this).FirePropertyChanged("PhotoMediaFolder");
			}
		}
	}

	public string PodcastMediaFolder
	{
		get
		{
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			if (_podcastMediaFolder == null)
			{
				_podcastMediaFolder = ClientConfiguration.Groveler.PodcastMediaFolder;
				if (string.IsNullOrEmpty(_podcastMediaFolder))
				{
					string[] array = default(string[]);
					string text = default(string);
					string path = default(string);
					HRESULT val = HRESULT.op_Implicit(ZuneApplication.ZuneLibrary.GetKnownFolders(ref array, ref array, ref array, ref array, ref array, ref text, ref text, ref text, ref path, ref text));
					if (((HRESULT)(ref val)).IsSuccess)
					{
						_podcastMediaFolder = LocalizationHelper.GetLocalizedFolderPath(path);
					}
				}
			}
			return _podcastMediaFolder;
		}
		set
		{
			if (_podcastMediaFolder != value)
			{
				CommitList[new ProxySettingDelegate(OnPodcastMediaFolderCommit)] = null;
				_podcastMediaFolder = value;
				((ModelItem)this).FirePropertyChanged("PodcastMediaFolder");
			}
		}
	}

	public BooleanChoice AutoEjectCDAfterBurn
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_autoEjectCDAfterBurn == null)
			{
				_autoEjectCDAfterBurn = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_BURN_AUTO_EJECT_CHECK));
				_autoEjectCDAfterBurn.Value = ClientConfiguration.CDBurn.AutoEject;
				((Choice)_autoEjectCDAfterBurn).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnAutoEjectCDAfterBurnCommit)] = null;
				};
			}
			return _autoEjectCDAfterBurn;
		}
	}

	public Choice BurnFormat
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (_burnDiscFormat == null)
			{
				_burnDiscFormat = new Choice((IModelItemOwner)(object)this);
				_burnDiscFormat.Options = new NamedIntOption[2]
				{
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_BURN_AUDIO_OPTION), 0),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_BURN_DATA_OPTION), 1)
				};
				NamedIntOption.SelectOptionByValue(_burnDiscFormat, ClientConfiguration.CDBurn.DiscFormat);
				_burnDiscFormat.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnBurnDiscFormatCommit)] = null;
				};
			}
			return _burnDiscFormat;
		}
	}

	public Choice BurnSpeed
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (_burnSpeed == null)
			{
				_burnSpeed = new Choice((IModelItemOwner)(object)this);
				_burnSpeed.Options = new NamedIntOption[4]
				{
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_BURN_FASTEST_OPTION), 0),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_BURN_FAST_OPTION), 1),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_BURN_MEDIUM_OPTION), 2),
					new NamedIntOption(null, Shell.LoadString(StringId.IDS_BURN_SLOW_OPTION), 3)
				};
				NamedIntOption.SelectOptionByValue(_burnSpeed, ClientConfiguration.CDBurn.BurnSpeed);
				_burnSpeed.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnBurnSpeedCommit)] = null;
				};
			}
			return _burnSpeed;
		}
	}

	public BooleanChoice MediaInfoChoice
	{
		get
		{
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Expected O, but got Unknown
			if (_mediaInfoChoice == null)
			{
				StringId stringId = StringId.IDS_UPDATE_METADATA_CHECK;
				if (FeatureEnablement.IsFeatureEnabled((Features)27) || FeatureEnablement.IsFeatureEnabled((Features)26))
				{
					stringId = ((!FeatureEnablement.IsFeatureEnabled((Features)29)) ? StringId.IDS_UPDATE_METADATA_QUICKMIX_CHECK : StringId.IDS_UPDATE_METADATA_FEATURES_CHECK);
				}
				_mediaInfoChoice = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(stringId));
				_mediaInfoChoice.Value = ClientConfiguration.MediaStore.ConnectToInternetForAlbumMetadata;
				((Choice)_mediaInfoChoice).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnMediaInfoChoiceCommit)] = null;
				};
			}
			return _mediaInfoChoice;
		}
	}

	public BooleanChoice SqmChoice
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_sqmChoice == null)
			{
				_sqmChoice = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_USAGE_DATA_CHECK));
				_sqmChoice.Value = ClientConfiguration.SQM.UsageTracking;
				((Choice)_sqmChoice).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnSqmChoiceCommit)] = null;
				};
			}
			return _sqmChoice;
		}
	}

	public Choice PodcastDefaultKeepEpisodesChoice
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			if (_podcastDefaultKeepEpisodesChoice == null)
			{
				Choice val = new Choice((IModelItemOwner)(object)this);
				val.Options = NamedIntOption.PodcastKeepOptions;
				NamedIntOption.SelectOptionByValue(val, ClientConfiguration.Series.PodcastDefaultKeepEpisodes);
				_podcastDefaultKeepEpisodesChoice = val;
				val.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnPodcastDefaultKeepEpisodesChoiceCommit)] = null;
				};
			}
			return _podcastDefaultKeepEpisodesChoice;
		}
	}

	public Choice PodcastPlaybackChoice
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (_podcastPlaybackChoice == null)
			{
				_podcastPlaybackChoice = new Choice((IModelItemOwner)(object)this);
				_podcastPlaybackChoice.Options = NamedIntOption.PodcastPlaybackOptions;
				NamedIntOption.SelectOptionByValue(_podcastPlaybackChoice, ClientConfiguration.Series.PodcastDefaultPlaybackOrder);
				_podcastPlaybackChoice.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnPodcastPlaybackChoiceCommit)] = null;
				};
			}
			return _podcastPlaybackChoice;
		}
	}

	public BooleanChoice MetadataChoice
	{
		get
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			//IL_003f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Expected O, but got Unknown
			if (_metadataChoice == null)
			{
				Command[] array = (Command[])(object)new Command[2];
				Command val = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_MISSING_METADATA), (EventHandler)null);
				array[0] = val;
				val = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_OVERWRITE_METADATA), (EventHandler)null);
				array[1] = val;
				_metadataChoice = new BooleanChoice((IModelItemOwner)(object)this);
				((Choice)_metadataChoice).Options = array;
				_metadataChoice.Value = ClientConfiguration.MediaStore.OverwriteAllMetadata;
				((Choice)_metadataChoice).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnMetadataChoiceCommit)] = null;
				};
			}
			return _metadataChoice;
		}
	}

	public IList AudioFileTypes
	{
		get
		{
			if (_audioFileTypes == null)
			{
				PopulateFileTypes();
			}
			return (IList)_audioFileTypes;
		}
	}

	public IList VideoFileTypes
	{
		get
		{
			if (_videoFileTypes == null)
			{
				PopulateFileTypes();
			}
			return (IList)_videoFileTypes;
		}
	}

	public bool InhibitSubscriptionMachineCountExceededSignInWarning
	{
		get
		{
			return ClientConfiguration.Service.InhibitSubscriptionMachineCountExceededSignInWarning;
		}
		set
		{
			ClientConfiguration.Service.InhibitSubscriptionMachineCountExceededSignInWarning = value;
		}
	}

	public bool InhibitSubscriptionBillingViolationSignInWarning
	{
		get
		{
			return ClientConfiguration.Service.InhibitSubscriptionBillingViolationSignInWarning;
		}
		set
		{
			ClientConfiguration.Service.InhibitSubscriptionBillingViolationSignInWarning = value;
		}
	}

	public bool InhibitSubscriptionEndingWarning
	{
		get
		{
			return ClientConfiguration.Service.InhibitSubscriptionEndingWarning;
		}
		set
		{
			ClientConfiguration.Service.InhibitSubscriptionEndingWarning = value;
		}
	}

	public bool InhibitSubscriptionFreePurchasePrompt
	{
		get
		{
			return ClientConfiguration.Service.InhibitSubscriptionFreePurchasePrompt;
		}
		set
		{
			ClientConfiguration.Service.InhibitSubscriptionFreePurchasePrompt = value;
		}
	}

	public int LibraryDefaultDeleteChoice
	{
		get
		{
			return ClientConfiguration.MediaStore.LibraryDefaultDeleteChoice;
		}
		set
		{
			ClientConfiguration.MediaStore.LibraryDefaultDeleteChoice = value;
		}
	}

	public int PlaylistDefaultDeleteChoice
	{
		get
		{
			return ClientConfiguration.MediaStore.PlaylistDefaultDeleteChoice;
		}
		set
		{
			ClientConfiguration.MediaStore.PlaylistDefaultDeleteChoice = value;
		}
	}

	public bool ConfirmAcceptFriend
	{
		get
		{
			return ClientConfiguration.Social.ConfirmAcceptFriend;
		}
		set
		{
			ClientConfiguration.Social.ConfirmAcceptFriend = value;
		}
	}

	public bool ConfirmDeleteFriend
	{
		get
		{
			return ClientConfiguration.Social.ConfirmDeleteFriend;
		}
		set
		{
			ClientConfiguration.Social.ConfirmDeleteFriend = value;
		}
	}

	public bool ConfirmAccountDevicePCDeletion
	{
		get
		{
			return ClientConfiguration.MediaStore.ConfirmAccountDevicePCDeletion;
		}
		set
		{
			ClientConfiguration.MediaStore.ConfirmAccountDevicePCDeletion = value;
		}
	}

	public bool ConfirmAccountDevicePortableDeletion
	{
		get
		{
			return ClientConfiguration.MediaStore.ConfirmAccountDevicePortableDeletion;
		}
		set
		{
			ClientConfiguration.MediaStore.ConfirmAccountDevicePortableDeletion = value;
		}
	}

	public bool ConfirmDeviceMediaDeletion
	{
		get
		{
			return ClientConfiguration.MediaStore.ConfirmDeviceMediaDeletion;
		}
		set
		{
			ClientConfiguration.MediaStore.ConfirmDeviceMediaDeletion = value;
		}
	}

	public bool ConfirmMultiAlbumEdit
	{
		get
		{
			return ClientConfiguration.MediaStore.ConfirmMultiAlbumEdit;
		}
		set
		{
			ClientConfiguration.MediaStore.ConfirmMultiAlbumEdit = value;
		}
	}

	public bool ConfirmMultiSongEdit
	{
		get
		{
			return ClientConfiguration.MediaStore.ConfirmMultiSongEdit;
		}
		set
		{
			ClientConfiguration.MediaStore.ConfirmMultiSongEdit = value;
		}
	}

	public bool ConfirmMultiVideoEdit
	{
		get
		{
			return ClientConfiguration.MediaStore.ConfirmMultiVideoEdit;
		}
		set
		{
			ClientConfiguration.MediaStore.ConfirmMultiVideoEdit = value;
		}
	}

	public bool ConfirmPasteAlbumArt
	{
		get
		{
			return ClientConfiguration.MediaStore.ConfirmPasteAlbumArt;
		}
		set
		{
			ClientConfiguration.MediaStore.ConfirmPasteAlbumArt = value;
		}
	}

	public ArrayListDataSet DeviceList
	{
		get
		{
			if (_registeredDevicesModelList == null)
			{
				InitRegisteredTuners();
			}
			return _registeredDevicesModelList;
		}
	}

	public ArrayListDataSet AppStoreDeviceList
	{
		get
		{
			if (_registeredAppStoreDevicesModelList == null)
			{
				InitRegisteredTuners();
			}
			return _registeredAppStoreDevicesModelList;
		}
	}

	public ArrayListDataSet ComputerList
	{
		get
		{
			if (_registeredComputersModelList == null)
			{
				InitRegisteredTuners();
			}
			return _registeredComputersModelList;
		}
	}

	public string NextPCDeregistrationDate
	{
		get
		{
			return _nextPCDeregistrationDate;
		}
		private set
		{
			if (_nextPCDeregistrationDate != value)
			{
				_nextPCDeregistrationDate = value;
				((ModelItem)this).FirePropertyChanged("NextPCDeregistrationDate");
			}
		}
	}

	public string NextSubscriptionDeviceDeregistrationDate
	{
		get
		{
			return _nextSubscriptionDeviceDeregistrationDate;
		}
		private set
		{
			if (_nextSubscriptionDeviceDeregistrationDate != value)
			{
				_nextSubscriptionDeviceDeregistrationDate = value;
				((ModelItem)this).FirePropertyChanged("NextSubscriptionDeviceDeregistrationDate");
			}
		}
	}

	public string NextAppStoreDeviceDeregistrationDate
	{
		get
		{
			return _nextAppStoreDeviceDeregistrationDate;
		}
		private set
		{
			if (_nextAppStoreDeviceDeregistrationDate != value)
			{
				_nextAppStoreDeviceDeregistrationDate = value;
				((ModelItem)this).FirePropertyChanged("NextAppStoreDeviceDeregistrationDate");
			}
		}
	}

	public bool CanShowDeviceList
	{
		get
		{
			if (_tunerHandler == null)
			{
				InitRegisteredTuners();
			}
			return _tunerHandler.CanQueryTunerList();
		}
	}

	public IntRangedValue SlideShowSpeed
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (_slideShowSpeed == null)
			{
				_slideShowSpeed = new IntRangedValue((IModelItemOwner)(object)this);
				_slideShowSpeed.MinValue = 3000;
				_slideShowSpeed.MaxValue = 10000;
				_slideShowSpeed.Step = 1000;
				_slideShowSpeed.Value = ClientConfiguration.GeneralSettings.SlideShowSpeed;
				((ModelItem)_slideShowSpeed).PropertyChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnSlideShowSpeedCommit)] = null;
				};
			}
			return _slideShowSpeed;
		}
	}

	public HMESettings HME
	{
		get
		{
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Expected O, but got Unknown
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			if (_HME == null)
			{
				lock (mylock)
				{
					if (_HME == null)
					{
						HMESettings val = new HMESettings();
						HRESULT val2 = HRESULT.op_Implicit(val.Init());
						_ = ((HRESULT)(ref val2)).IsError;
						_sharingAllDevicesEnabled = val.GetAllDevicesEnabled();
						_HME = val;
					}
				}
			}
			return _HME;
		}
	}

	public bool UserCanModifySharing
	{
		get
		{
			if (Environment.OSVersion.Version.Major < 6 && SharingEnableRequiresElevation)
			{
				return SharingEnabled;
			}
			return true;
		}
	}

	public bool SharingEnableRequiresElevation
	{
		get
		{
			if (Environment.OSVersion.Version.Major < 6)
			{
				return HME.SharingEnableRequiresLoginAsAdmin;
			}
			return HME.SharingEnableRequiresElevation;
		}
	}

	public bool SharingEnabled => HME.SharingEnabled;

	public string SharingDisplayName
	{
		get
		{
			if (_sharingDisplayName == null)
			{
				HME.GetDisplayName(ref _sharingDisplayName);
			}
			return _sharingDisplayName;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				SharingError = Shell.LoadString(StringId.IDS_SHARE_NAME_EMPTY);
			}
			else if (_sharingDisplayName != value)
			{
				SharingError = string.Empty;
				_sharingDisplayName = value;
				CommitList[new ProxySettingDelegate(OnMediaSharingUpdate)] = null;
			}
		}
	}

	public string SharingError
	{
		get
		{
			return _sharingError;
		}
		set
		{
			if (_sharingError != value)
			{
				_sharingError = value;
				((ModelItem)this).FirePropertyChanged("SharingError");
			}
		}
	}

	public BooleanChoice SharingEnableMusic
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_sharingEnableMusic == null)
			{
				_sharingEnableMusic = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_SHARE_MUSIC_CHECK));
				_sharingEnableMusic.Value = HME.GetSharingEnabledForMediaType((EMediaTypes)3);
				((Choice)_sharingEnableMusic).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnMediaSharingUpdate)] = null;
					if (SharingEnableRequiresElevation)
					{
						ChangeRequiresElevation = true;
					}
				};
			}
			return _sharingEnableMusic;
		}
	}

	public BooleanChoice SharingEnableVideo
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_sharingEnableVideo == null)
			{
				_sharingEnableVideo = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_SHARE_VIDEOS_CHECK));
				_sharingEnableVideo.Value = HME.GetSharingEnabledForMediaType((EMediaTypes)4);
				((Choice)_sharingEnableVideo).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnMediaSharingUpdate)] = null;
					if (SharingEnableRequiresElevation)
					{
						ChangeRequiresElevation = true;
					}
				};
			}
			return _sharingEnableVideo;
		}
	}

	public BooleanChoice SharingEnablePhoto
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_sharingEnablePhoto == null)
			{
				_sharingEnablePhoto = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_SHARE_PICTURES_CHECK));
				_sharingEnablePhoto.Value = HME.GetSharingEnabledForMediaType((EMediaTypes)5);
				((Choice)_sharingEnablePhoto).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnMediaSharingUpdate)] = null;
					if (SharingEnableRequiresElevation)
					{
						ChangeRequiresElevation = true;
					}
				};
			}
			return _sharingEnablePhoto;
		}
	}

	public Choice SharingSelectDeviceChoice
	{
		get
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Expected O, but got Unknown
			//IL_0058: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Expected O, but got Unknown
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0098: Expected O, but got Unknown
			if (_sharingSelectDeviceChoice == null)
			{
				_sharingSelectDeviceOptions = new List<Command>();
				Command val = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_GLOBAL_SHARING_OPTION), (EventHandler)null);
				((ModelItem)val).Data.Add("value", true);
				_sharingSelectDeviceOptions.Add(val);
				val = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_SELECTIVE_SHARING_OPTION), (EventHandler)null);
				((ModelItem)val).Data.Add("value", false);
				val.Available = SharingEnabled;
				_sharingSelectDeviceOptions.Add(val);
				_sharingSelectDeviceChoice = new Choice((IModelItemOwner)(object)this);
				_sharingSelectDeviceChoice.Options = (IList)_sharingSelectDeviceOptions;
				_sharingSelectDeviceChoice.ChosenChanged += delegate(object sender, EventArgs args)
				{
					//IL_0001: Unknown result type (might be due to invalid IL or missing references)
					//IL_0007: Expected O, but got Unknown
					Choice val2 = (Choice)sender;
					SharingAllDevicesEnabled = (bool)((ModelItem)_sharingSelectDeviceOptions[val2.ChosenIndex]).Data["value"];
				};
			}
			return _sharingSelectDeviceChoice;
		}
	}

	public bool SharingAllDevicesEnabled
	{
		get
		{
			return _sharingAllDevicesEnabled;
		}
		set
		{
			if (_sharingAllDevicesEnabled != value)
			{
				_sharingAllDevicesEnabled = value;
				CommitList[new ProxySettingDelegate(OnMediaSharingUpdate)] = null;
				((ModelItem)this).FirePropertyChanged("SharingAllDevicesEnabled");
			}
		}
	}

	public IList SharingDeviceList
	{
		get
		{
			if (_sharingDeviceList == null)
			{
				_sharingDeviceList = CreateSharingDeviceList();
			}
			return (IList)_sharingDeviceList;
		}
	}

	public bool ReevaluateVideoSettings
	{
		get
		{
			return ClientConfiguration.GeneralSettings.ReevaluateVideoSettings;
		}
		set
		{
			ClientConfiguration.GeneralSettings.ReevaluateVideoSettings = value;
			((ModelItem)this).FirePropertyChanged("ReevaluateVideoSettings");
		}
	}

	public RenderingType RequestedRenderingType
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Invalid comparison between Unknown and I4
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			RenderingType val = (RenderingType)ClientConfiguration.GeneralSettings.RenderingType;
			if ((int)val == 2)
			{
				val = Application.RenderingType;
			}
			return val;
		}
		set
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected I4, but got Unknown
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Invalid comparison between Unknown and I4
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Invalid comparison between Unknown and I4
			ClientConfiguration.GeneralSettings.RenderingType = (int)value;
			ReevaluateVideoSettings = (int)Application.RenderingType != 1 && (int)value == 1;
			((ModelItem)this).FirePropertyChanged("RequestedRenderingType");
		}
	}

	public RenderingQuality RequestedRenderingQuality
	{
		get
		{
			return (RenderingQuality)ClientConfiguration.GeneralSettings.RenderingQuality;
		}
		set
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Expected I4, but got Unknown
			ClientConfiguration.GeneralSettings.RenderingQuality = (int)value;
			((ModelItem)this).FirePropertyChanged("RequestedRenderingQuality");
		}
	}

	public bool AnimationsEnabled
	{
		get
		{
			return ClientConfiguration.GeneralSettings.AnimationsEnabled;
		}
		set
		{
			ClientConfiguration.GeneralSettings.AnimationsEnabled = value;
			((ModelItem)this).FirePropertyChanged("AnimationsEnabled");
		}
	}

	public Choice ScreenGraphicsSlider
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Expected O, but got Unknown
			//IL_001e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Invalid comparison between Unknown and I4
			if (_screenGraphicsSlider == null)
			{
				Choice val = new Choice((IModelItemOwner)(object)this);
				val.Options = NamedIntOption.ScreenGraphicsOptions;
				ScreenGraphics screenGraphics = ScreenGraphics.Basic;
				screenGraphics = (((int)Application.RenderingType != 0) ? (((int)Application.RenderingQuality == 1) ? ScreenGraphics.Premium : ((!Application.AnimationsEnabled) ? ScreenGraphics.Advanced : ScreenGraphics.AdvancedWithAnimation)) : ScreenGraphics.Basic);
				NamedIntOption.SelectOptionByValue(val, (int)screenGraphics);
				_screenGraphicsSlider = val;
				val.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnScreenGraphicsSliderCommit)] = null;
				};
			}
			return _screenGraphicsSlider;
		}
	}

	public string BackgroundImage
	{
		get
		{
			if (_backgroundImage == null)
			{
				_backgroundImage = ClientConfiguration.Shell.BackgroundImage;
			}
			return _backgroundImage;
		}
		set
		{
			if (_backgroundImage != value)
			{
				CommitList[new ProxySettingDelegate(OnBackgroundImageCommit)] = null;
				_backgroundImage = value;
				((ModelItem)this).FirePropertyChanged("BackgroundImage");
			}
		}
	}

	public WindowColor BackgroundColor
	{
		get
		{
			return _backgroundColor;
		}
		set
		{
			_backgroundColor = value;
		}
	}

	public BooleanChoice ShowNowPlayingBackgroundOnIdle
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_showNowPlayingBackgroundOnIdle == null)
			{
				_showNowPlayingBackgroundOnIdle = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_SHOW_NOWPLAYING_ON_IDLE_DESCRIPTION));
				_showNowPlayingBackgroundOnIdle.Value = ClientConfiguration.Shell.ShowNowPlayingBackgroundOnIdleTimeout > 0;
				((Choice)_showNowPlayingBackgroundOnIdle).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnShowNowPlayingBackgroundOnIdleCommit)] = null;
				};
			}
			return _showNowPlayingBackgroundOnIdle;
		}
	}

	public BooleanChoice PlaySounds
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_playSounds == null)
			{
				_playSounds = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_SOUNDS_DESCRIPTION));
				_playSounds.Value = ClientConfiguration.Shell.Sounds;
				((Choice)_playSounds).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnPlaySoundsCommit)] = null;
				};
			}
			return _playSounds;
		}
	}

	public BooleanChoice CompactModeAlwaysOnTop
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_compactModeAlwaysOnTop == null)
			{
				_compactModeAlwaysOnTop = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_COMPACT_MODE_ALWAYS_ON_TOP));
				_compactModeAlwaysOnTop.Value = ClientConfiguration.GeneralSettings.CompactModeAlwaysOnTop;
				((Choice)_compactModeAlwaysOnTop).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnCompactModeAlwaysOnTopCommit)] = null;
				};
			}
			return _compactModeAlwaysOnTop;
		}
	}

	public BooleanChoice RatingsChoice
	{
		get
		{
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Expected O, but got Unknown
			if (_ratingsChoice == null)
			{
				Command[] options = (Command[])(object)new Command[2]
				{
					new RichLayoutCommand((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_COMMON_RATINGS_ALL_USERS_OPTION), hasRichLayout: true),
					new RichLayoutCommand((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PERSONAL_RATINGS_EACH_USER_OPTION), hasRichLayout: false)
				};
				_ratingsChoice = new BooleanChoice((IModelItemOwner)(object)this);
				((Choice)_ratingsChoice).Options = options;
				_ratingsChoice.Value = !ClientConfiguration.MediaStore.SharedUserRatings;
				((Choice)_ratingsChoice).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnRatingsCommit)] = null;
				};
			}
			return _ratingsChoice;
		}
	}

	public BooleanChoice ApplyRatingsChoice
	{
		get
		{
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Expected O, but got Unknown
			if (_applyRatingsChoice == null)
			{
				_applyRatingsChoice = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_APPLY_RATINGS_DIALOG_DESCRIPTION));
				_applyRatingsChoice.Value = false;
				((Choice)_applyRatingsChoice).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnRatingsCommit)] = null;
				};
			}
			return _applyRatingsChoice;
		}
	}

	public Choice StartupPageChoice
	{
		get
		{
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Expected O, but got Unknown
			if (_startupPageChoice == null)
			{
				List<Command> list = new List<Command>();
				if (FeatureEnablement.IsFeatureEnabled((Features)0))
				{
					list.Add((Command)(object)new NamedStringOption(Shell.LoadString(StringId.IDS_VIEW_STARTUPPAGE_QUICKPLAY_CHOICE), Shell.MainFrame.Quickplay.DefaultUIPath));
				}
				list.Add((Command)(object)new NamedStringOption(Shell.LoadString(StringId.IDS_VIEW_STARTUPPAGE_COLLECTION_CHOICE), Shell.MainFrame.Collection.DefaultUIPath));
				if (FeatureEnablement.IsFeatureEnabled((Features)2))
				{
					list.Add((Command)(object)new NamedStringOption(Shell.LoadString(StringId.IDS_VIEW_STARTUPPAGE_MARKETPLACE_CHOICE), Shell.MainFrame.Marketplace.DefaultUIPath));
				}
				if (FeatureEnablement.IsFeatureEnabled((Features)5))
				{
					list.Add((Command)(object)new NamedStringOption(Shell.LoadString(StringId.IDS_VIEW_STARTUPPAGE_SOCIAL_CHOICE), Shell.MainFrame.Social.DefaultUIPath));
				}
				_startupPageChoice = new Choice((IModelItemOwner)(object)this);
				_startupPageChoice.Options = list;
				foreach (NamedStringOption item in list)
				{
					if (item.Value == ClientConfiguration.Shell.StartupPage)
					{
						_startupPageChoice.ChosenValue = item;
						break;
					}
				}
				_startupPageChoice.ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnStartupPageCommit)] = null;
				};
			}
			return _startupPageChoice;
		}
	}

	public bool AutoLaunchZuneOnConnect
	{
		get
		{
			return _autoLaunchZuneOnConnect;
		}
		set
		{
			if (_autoLaunchZuneOnConnect != value)
			{
				_autoLaunchZuneOnConnect = value;
				CommitList[new ProxySettingDelegate(OnAutoLaunchZuneOnConnectCommit)] = null;
				((ModelItem)this).FirePropertyChanged("AutoLaunchZuneOnConnect");
			}
		}
	}

	public Management()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		_autoLaunchZuneOnConnect = ClientConfiguration.Devices.AutoLaunchZuneOnConnect;
		((CConfigurationManagedBase)ClientConfiguration.Groveler).OnConfigurationChanged += new ConfigurationChangeEventHandler(OnGrovelerConfigurationChanged);
		_actionsToCommitOnLibraryIntegrate = new ProxySettingDelegate[5] { OnMonitoredFoldersCommit, OnMediaFolderCommit, OnVideoMediaFolderCommit, OnPhotoMediaFolderCommit, OnPodcastMediaFolderCommit };
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		if (disposing)
		{
			DisposeDeviceManagement(deviceManagementLocked: true);
			((CConfigurationManagedBase)ClientConfiguration.Groveler).OnConfigurationChanged -= new ConfigurationChangeEventHandler(OnGrovelerConfigurationChanged);
		}
		if (_fileAssocHandler != null)
		{
			((IDisposable)_fileAssocHandler).Dispose();
			_fileAssocHandler = null;
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public void DisposeDeviceManagement(bool deviceManagementLocked)
	{
		DeviceManagementLocked = deviceManagementLocked;
		if (_deviceManagement != null)
		{
			UIDevice currentDeviceOverride = SyncControls.Instance.CurrentDeviceOverride;
			if (currentDeviceOverride.IsValid)
			{
				int removeValue = (currentDeviceOverride.IsGuest ? (-1) : currentDeviceOverride.ID);
				CommitList.RemoveByIntValue(removeValue);
				CommitList.RemoveByStringValue("OnSyncPartnershipCommit");
			}
			else
			{
				CommitList.RemoveByIntValue(-1);
			}
			((ModelItem)_deviceManagement).Dispose();
			_deviceManagement = null;
			((ModelItem)this).FirePropertyChanged("DeviceManagementChanged");
		}
	}

	private void OnWmaRateCommit(object data)
	{
		ClientConfiguration.Recorder.WMARecordRate = ((NamedIntOption)_wmaRate.ChosenValue).Value;
	}

	private void OnWmavRateCommit(object data)
	{
		ClientConfiguration.Recorder.WMAVBRRecordQuality = ((NamedIntOption)_wmavRate.ChosenValue).Value;
	}

	private void OnMp3RateCommit(object data)
	{
		ClientConfiguration.Recorder.MP3RecordRate = ((NamedIntOption)_mp3Rate.ChosenValue).Value;
	}

	private void OnRecordModeCommit(object data)
	{
		ClientConfiguration.Recorder.RecordMode = ((RecordModeOption)_recordMode.ChosenValue).Value;
	}

	public static void NavigateToSetupLandWizard(SetupLandPage page)
	{
		SetupLandWizardNavigationCommand confirmed = new SetupLandWizardNavigationCommand(page);
		NavigateAwayFromCategory((Command)(object)confirmed);
	}

	public static void NavigateToCategory(Category category)
	{
		if (ZuneShell.DefaultInstance.Management.CurrentCategoryPage != null)
		{
			ZuneShell.DefaultInstance.Management.CurrentCategoryPage.CurrentCategory = category;
		}
	}

	public static void NavigateAwayFromCategory(Command confirmed)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Expected O, but got Unknown
		Management management = ZuneShell.DefaultInstance.Management;
		if (management.HasPendingCommits)
		{
			Command val = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_DIALOG_YES), (EventHandler)null);
			val.Invoked += delegate
			{
				management.CommitListSave();
				NavigateAwayFromCategory(confirmed);
			};
			Command val2 = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_DIALOG_NO), (EventHandler)null);
			val2.Invoked += delegate
			{
				management.CommitList = null;
				NavigateAwayFromCategory(confirmed);
			};
			MessageBox.Show(Shell.LoadString(StringId.IDS_SAVE_CHANGES_DIALOG_TITLE), Shell.LoadString(StringId.IDS_SAVE_CHANGES_ON_BACK_DIALOG_TEXT), val, val2, (BooleanChoice)null);
			return;
		}
		if (Shell.SettingsFrame.IsCurrent && !Shell.SettingsFrame.Wizard.FUE.IsCurrent && management.CurrentCategoryPage != null)
		{
			management.CurrentCategoryPage.CancelAndExit();
		}
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (confirmed != null)
			{
				confirmed.Invoke();
			}
		}, (object)null);
	}

	public void CommitListSave()
	{
		CheckForAutomatedRequirements();
		CommitList.Save();
	}

	public void CheckForAutomatedRequirements()
	{
		if (DeviceManagement.SetupDevice != null)
		{
			DeviceManagement.CheckForAutomatedRequirements();
		}
	}

	public SubscriptionState SubscribeToChannelFeed(bool isPersonalChannel, Guid channelId, string feedUrl, string title, ESubscriptionSource source)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return SubscribeToFeed(feedUrl, title, channelId, isPersonalChannel, source, (EMediaTypes)9, Shell.LoadString(StringId.IDS_PLAYLIST_SUBSCRIPTION_ERROR));
	}

	public SubscriptionState SubscribeToPodcastFeed(string feedUrl, string title, ESubscriptionSource source)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		return SubscribeToPodcastFeed(feedUrl, title, Guid.Empty, source);
	}

	public SubscriptionState SubscribeToPodcastFeed(string feedUrl, string title, Guid serviceId, ESubscriptionSource source)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		return SubscribeToFeed(feedUrl, title, serviceId, isPersonalChannel: false, source, (EMediaTypes)18, Shell.LoadString(StringId.IDS_PODCAST_SUBSCRIPTION_ERROR));
	}

	private SubscriptionState SubscribeToFeed(string feedUrl, string title, Guid serviceId, bool isPersonalChannel, ESubscriptionSource source, EMediaTypes mediaType, string errorDialogHeader)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		int seriesId = 0;
		SubscriptionState result = null;
		SubscriptionManager instance = SubscriptionManager.Instance;
		HRESULT val = HRESULT.op_Implicit(instance.Subscribe(feedUrl, title, serviceId, isPersonalChannel, mediaType, source, ref seriesId));
		if (((HRESULT)(ref val)).IsSuccess)
		{
			result = new SubscriptionState(isSubscribed: true, seriesFound: true, seriesId);
		}
		else
		{
			ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, errorDialogHeader);
		}
		return result;
	}

	public SubscriptionState GetSubscriptionState(string feedURL, EMediaTypes subscriptionType)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(feedURL))
		{
			return null;
		}
		try
		{
			int seriesId = -1;
			SubscriptionManager instance = SubscriptionManager.Instance;
			bool isSubscribed = default(bool);
			bool seriesFound = instance.FindByUrl(feedURL, subscriptionType, ref seriesId, ref isSubscribed);
			return new SubscriptionState(isSubscribed, seriesFound, seriesId);
		}
		catch (ApplicationException)
		{
		}
		return null;
	}

	public SubscriptionState GetSubscriptionState(Guid serviceId, EMediaTypes subscriptionType)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		if (serviceId == Guid.Empty)
		{
			return null;
		}
		try
		{
			int seriesId = -1;
			SubscriptionManager instance = SubscriptionManager.Instance;
			bool isSubscribed = default(bool);
			bool seriesFound = instance.FindByServiceId(serviceId, subscriptionType, ref seriesId, ref isSubscribed);
			return new SubscriptionState(isSubscribed, seriesFound, seriesId);
		}
		catch (ApplicationException)
		{
		}
		return null;
	}

	private void OnGrovelerConfigurationChanged(object sender, ConfigurationChangeEventArgs e)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (UsingWin7Libraries)
			{
				if (e.PropertyName == "RipDirectory" || e.PropertyName == "MonitoredAudioFolders")
				{
					if (_monitoredAudioFolders != null)
					{
						_monitoredAudioFolders = null;
						((ModelItem)this).FirePropertyChanged("MonitoredAudioFolders");
					}
					if (_mediaFolder != null)
					{
						_mediaFolder = null;
						((ModelItem)this).FirePropertyChanged("MediaFolder");
					}
				}
				else if (e.PropertyName == "PhotoMediaFolder" || e.PropertyName == "MonitoredPhotoFolders")
				{
					if (_monitoredPhotoFolders != null)
					{
						_monitoredPhotoFolders = null;
						((ModelItem)this).FirePropertyChanged("MonitoredPhotoFolders");
					}
					if (_photoMediaFolder != null)
					{
						_photoMediaFolder = null;
						((ModelItem)this).FirePropertyChanged("PhotoMediaFolder");
					}
				}
				else if (e.PropertyName == "PodcastMediaFolder" || e.PropertyName == "MonitoredPodcastFolders")
				{
					if (_monitoredPodcastFolders != null)
					{
						_monitoredPodcastFolders = null;
						((ModelItem)this).FirePropertyChanged("MonitoredPodcastFolders");
					}
					if (_podcastMediaFolder != null)
					{
						_podcastMediaFolder = null;
						((ModelItem)this).FirePropertyChanged("PodcastMediaFolder");
					}
				}
				else if (e.PropertyName == "VideoMediaFolder" || e.PropertyName == "MonitoredVideoFolders")
				{
					if (_monitoredVideoFolders != null)
					{
						_monitoredVideoFolders = null;
						((ModelItem)this).FirePropertyChanged("MonitoredVideoFolders");
					}
					if (_videoMediaFolder != null)
					{
						_videoMediaFolder = null;
						((ModelItem)this).FirePropertyChanged("VideoMediaFolder");
					}
				}
			}
		}, (object)null);
	}

	public void UseWin7Libraries()
	{
		ProxySettingDelegate[] actionsToCommitOnLibraryIntegrate = _actionsToCommitOnLibraryIntegrate;
		foreach (ProxySettingDelegate proxySettingDelegate in actionsToCommitOnLibraryIntegrate)
		{
			if (CommitList.ContainsKey(proxySettingDelegate))
			{
				CommitList.Remove(proxySettingDelegate);
				proxySettingDelegate(null);
			}
		}
		SQMLog.Log((SQMDataId)222, 0);
		SetWin7LibrariesUsage(Win7LibrariesUsage.BeginIntegration);
	}

	public void DoNotUseWin7Libraries()
	{
		SQMLog.Log((SQMDataId)222, 1);
		SetWin7LibrariesUsage(Win7LibrariesUsage.DoNotIntegrate);
	}

	private void SetWin7LibrariesUsage(Win7LibrariesUsage usage)
	{
		ClientConfiguration.Groveler.LibrarySync = (int)usage;
		((ModelItem)this).FirePropertyChanged("UsingWin7Libraries");
	}

	private ListDataSet StringsToListDataSet(params object[] source)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		ListDataSet val = (ListDataSet)new ArrayListDataSet((IModelItemOwner)(object)this);
		if (source != null && source.Length > 0)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (object obj in source)
			{
				if (obj == null)
				{
					continue;
				}
				IEnumerable<string> enumerable = obj as IEnumerable<string>;
				if (enumerable == null)
				{
					enumerable = new string[1] { obj.ToString() };
				}
				if (enumerable == null)
				{
					continue;
				}
				foreach (string item in enumerable)
				{
					if (!string.IsNullOrEmpty(item))
					{
						string key = item.ToLower();
						if (!dictionary.ContainsKey(key))
						{
							dictionary.Add(key, null);
							val.Add((object)item);
						}
					}
				}
			}
		}
		val.Sort();
		return val;
	}

	private IList<string> ListDataSetToIList(ListDataSet listDataSet)
	{
		if (listDataSet == null)
		{
			return new List<string>();
		}
		IList<string> list = new List<string>(listDataSet.Count);
		for (int i = 0; i < listDataSet.Count; i++)
		{
			list.Add((string)listDataSet[i]);
		}
		return list;
	}

	internal bool IsMonitored(ListDataSet monitoredFolders, string path)
	{
		new DirectoryInfo(path);
		foreach (string monitoredFolder in monitoredFolders)
		{
			if (IsSubfolder(monitoredFolder, path))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsSubfolder(string root, string subfolder)
	{
		try
		{
			while (subfolder != null)
			{
				if (subfolder.Equals(root, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
				subfolder = Path.GetDirectoryName(subfolder);
			}
		}
		catch (ArgumentException)
		{
		}
		catch (PathTooLongException)
		{
		}
		return false;
	}

	public void AddMonitoredFolder(ListDataSet monitoredFolders)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Expected O, but got Unknown
		FolderBrowseDialog.Show(Shell.LoadString(StringId.IDS_ADD_MONITORED_FOLDER_DIALOG_TITLE), (DeferredInvokeHandler)delegate(object args)
		{
			if (args != null)
			{
				string text = (string)args;
				if (ZuneApplication.ZuneLibrary.CanAddFromFolder(text))
				{
					AddMonitoredFolder(monitoredFolders, text, commit: false);
				}
				else
				{
					MessageBox.Show(Shell.LoadString(StringId.IDS_INVALID_MONITORED_FOLDER_TITLE), Shell.LoadString(StringId.IDS_INVALID_MONITORED_FOLDER_MESSAGE), (EventHandler)null);
				}
			}
		});
	}

	public void AddMonitoredFolder(ListDataSet monitoredFolders, string path, bool commit)
	{
		bool flag = false;
		for (int i = 0; i < monitoredFolders.Count; i++)
		{
			if (monitoredFolders[i].Equals(path))
			{
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			monitoredFolders.Add((object)path);
		}
		SaveMonitoredFolders(commit);
	}

	public void OpenMediaFile()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		FileOpenDialog.Show(Shell.LoadString(StringId.IDS_OPEN_FILE_DIALOG_TITLE), MediaFolder, (DeferredInvokeHandler)delegate
		{
		});
	}

	public bool RemoveChildMonitoredFolders(string path, bool commit)
	{
		bool flag = false;
		flag |= RemoveChildMonitoredFolders(MonitoredAudioFolders, (EMediaTypes)3, path);
		flag |= RemoveChildMonitoredFolders(MonitoredPhotoFolders, (EMediaTypes)5, path);
		flag |= RemoveChildMonitoredFolders(MonitoredPodcastFolders, (EMediaTypes)17, path);
		flag |= RemoveChildMonitoredFolders(MonitoredVideoFolders, (EMediaTypes)4, path);
		SaveMonitoredFolders(commit);
		return flag;
	}

	private bool RemoveChildMonitoredFolders(ListDataSet monitoredFolders, EMediaTypes type, string path)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected I4, but got Unknown
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Invalid comparison between Unknown and I4
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		EWin7LibraryKind val;
		switch (type - 3)
		{
		default:
			if ((int)type == 17)
			{
				val = (EWin7LibraryKind)3;
				break;
			}
			return false;
		case 0:
			val = (EWin7LibraryKind)0;
			break;
		case 1:
			val = (EWin7LibraryKind)1;
			break;
		case 2:
			val = (EWin7LibraryKind)2;
			break;
		}
		List<int> list = new List<int>();
		for (int i = 0; i < monitoredFolders.Count; i++)
		{
			if (IsSubfolder(path, (string)monitoredFolders[i]))
			{
				list.Add(i);
			}
		}
		bool flag = default(bool);
		foreach (int item in list)
		{
			if (UsingWin7Libraries)
			{
				Win7ShellManager.Instance.RemoveLocationFromLibrary(val, ref flag, (string)monitoredFolders[item]);
			}
			else
			{
				RemoveMonitoredFolder(monitoredFolders, item, type);
			}
		}
		return list.Count > 0;
	}

	public void RemoveMonitoredFolder(ListDataSet monitoredFolders, string path, bool commit)
	{
		for (int i = 0; i < monitoredFolders.Count; i++)
		{
			if (path.Equals((string)monitoredFolders[i], StringComparison.OrdinalIgnoreCase))
			{
				RemoveMonitoredFolder(monitoredFolders, i, commit);
				break;
			}
		}
	}

	private void RemoveMonitoredFolder(ListDataSet monitoredFolders, int index)
	{
		RemoveMonitoredFolder(monitoredFolders, index, commit: false);
	}

	public void RemoveMonitoredFolder(ListDataSet monitoredFolders, int index, EMediaTypes mediaType)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		if (_removedMonitoredFoldersToRemoveFromCollection == null)
		{
			_removedMonitoredFoldersToRemoveFromCollection = new List<MonitoredFolder>();
		}
		_removedMonitoredFoldersToRemoveFromCollection.Add(new MonitoredFolder((string)monitoredFolders[index], mediaType));
		RemoveMonitoredFolder(monitoredFolders, index, commit: false);
	}

	private void RemoveMonitoredFolder(ListDataSet monitoredFolders, int index, bool commit)
	{
		monitoredFolders.RemoveAt(index);
		SaveMonitoredFolders(commit);
	}

	private void SaveMonitoredFolders()
	{
		SaveMonitoredFolders(commit: false);
	}

	public void SaveMonitoredFolders(bool commit)
	{
		if (commit)
		{
			OnMonitoredFoldersCommit(null);
		}
		else
		{
			CommitList[new ProxySettingDelegate(OnMonitoredFoldersCommit)] = null;
		}
	}

	public void OpenLibraryDialog(EMediaTypes mediaType)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		IntPtr winHandle = Application.Window.Handle;
		Thread thread = new Thread((ParameterizedThreadStart)delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0003: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Expected I4, but got Unknown
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Invalid comparison between Unknown and I4
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			EWin7LibraryKind val = (EWin7LibraryKind)0;
			EMediaTypes val2 = mediaType;
			switch (val2 - 3)
			{
			default:
				if ((int)val2 == 17)
				{
					val = (EWin7LibraryKind)3;
				}
				break;
			case 0:
				val = (EWin7LibraryKind)0;
				break;
			case 2:
				val = (EWin7LibraryKind)2;
				break;
			case 1:
				val = (EWin7LibraryKind)1;
				break;
			}
			Win7ShellManager.Instance.ShowLibraryDialog(val, winHandle, (string)null, (string)null);
		});
		thread.TrySetApartmentState(ApartmentState.STA);
		thread.Start();
	}

	private void OnMonitoredFoldersCommit(object data)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		SQMLog.LogToStream((SQMDataId)94, (uint)MonitoredAudioFolders.Count);
		SQMLog.LogToStream((SQMDataId)95, (uint)MonitoredPhotoFolders.Count);
		SQMLog.LogToStream((SQMDataId)96, (uint)MonitoredPodcastFolders.Count);
		SQMLog.LogToStream((SQMDataId)97, (uint)MonitoredVideoFolders.Count);
		if (!UsingWin7Libraries)
		{
			if (_removedMonitoredFoldersToRemoveFromCollection != null)
			{
				foreach (MonitoredFolder item in _removedMonitoredFoldersToRemoveFromCollection)
				{
					ZuneApplication.ZuneLibrary.DeleteRootFolder(item.Path, item.Schema);
				}
			}
			ClientConfiguration.Groveler.MonitoredAudioFolders = ListDataSetToIList(MonitoredAudioFolders);
			ClientConfiguration.Groveler.MonitoredPhotoFolders = ListDataSetToIList(MonitoredPhotoFolders);
			ClientConfiguration.Groveler.MonitoredPodcastFolders = ListDataSetToIList(MonitoredPodcastFolders);
			ClientConfiguration.Groveler.MonitoredVideoFolders = ListDataSetToIList(MonitoredVideoFolders);
			_removedMonitoredFoldersToRemoveFromCollection = null;
			_monitoredAudioFolders = null;
			_monitoredPhotoFolders = null;
			_monitoredPodcastFolders = null;
			_monitoredVideoFolders = null;
		}
		if (HME.SharingEnabled)
		{
			HME.SetSharedFoldersList(true);
		}
	}

	private void OnAutoCopyCDCommit(object data)
	{
		ClientConfiguration.Recorder.AutoCopyCD = (_autoCopyCD.Value ? 1 : 0);
	}

	private void OnAutoEjectCDCommit(object data)
	{
		ClientConfiguration.Recorder.AutoEjectCD = (_autoEjectCD.Value ? 1 : 0);
	}

	public bool MediaFolderHasSharedPathWithMonitoredFolder(string monitoredFolder, string mediaFolder)
	{
		bool result = false;
		if (!string.IsNullOrEmpty(monitoredFolder) && !string.IsNullOrEmpty(mediaFolder))
		{
			string text = LocalizationHelper.GetLocalizedFolderPath(monitoredFolder);
			string text2 = LocalizationHelper.GetLocalizedFolderPath(mediaFolder);
			if (text[text.Length - 1] != Path.PathSeparator)
			{
				text += Path.PathSeparator;
			}
			if (text2[text2.Length - 1] != Path.PathSeparator)
			{
				text2 += Path.PathSeparator;
			}
			result = text.ToLower().IndexOf(text2.ToLower()) == 0;
		}
		return result;
	}

	private void OnMediaFolderCommit(object data)
	{
		if (!UsingWin7Libraries)
		{
			ClientConfiguration.Groveler.RipDirectory = _mediaFolder;
			UpdateSharedFoldersList();
			_mediaFolder = null;
		}
	}

	private void OnVideoMediaFolderCommit(object data)
	{
		if (!UsingWin7Libraries)
		{
			ClientConfiguration.Groveler.VideoMediaFolder = _videoMediaFolder;
			UpdateSharedFoldersList();
			_videoMediaFolder = null;
		}
	}

	private void OnPhotoMediaFolderCommit(object data)
	{
		if (!UsingWin7Libraries)
		{
			ClientConfiguration.Groveler.PhotoMediaFolder = _photoMediaFolder;
			UpdateSharedFoldersList();
			_photoMediaFolder = null;
		}
	}

	private void OnPodcastMediaFolderCommit(object data)
	{
		if (!UsingWin7Libraries)
		{
			ClientConfiguration.Groveler.PodcastMediaFolder = _podcastMediaFolder;
			UpdateSharedFoldersList();
			_podcastMediaFolder = null;
		}
	}

	private void UpdateSharedFoldersList()
	{
		if (HME.SharingEnabled)
		{
			HME.SetSharedFoldersList(true);
		}
	}

	public void ChooseMediaFolder(MediaType mediaType)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Expected O, but got Unknown
		FolderBrowseDialog.Show(Shell.LoadString(StringId.IDS_CHANGE_MEDIA_FOLDER_DIALOG_TITLE), (DeferredInvokeHandler)delegate(object args)
		{
			string text = (string)args;
			if (text != null)
			{
				if (FolderBrowseDialog.CanWriteToFolder(text) && ZuneApplication.ZuneLibrary.CanAddFromFolder(text))
				{
					if (mediaType == MediaType.Track)
					{
						MediaFolder = text;
					}
					else if (mediaType == MediaType.Video)
					{
						VideoMediaFolder = text;
					}
					else if (mediaType == MediaType.Photo)
					{
						PhotoMediaFolder = text;
					}
					else if (mediaType == MediaType.Podcast)
					{
						PodcastMediaFolder = text;
					}
				}
				else
				{
					MessageBox.Show(Shell.LoadString(StringId.IDS_INVALID_MEDIA_FOLDER_TITLE), Shell.LoadString(StringId.IDS_INVALID_MEDIA_FOLDER_MESSAGE), (EventHandler)null);
				}
			}
		}, validate: true);
	}

	private void OnAutoEjectCDAfterBurnCommit(object data)
	{
		ClientConfiguration.CDBurn.AutoEject = _autoEjectCDAfterBurn.Value;
	}

	private void OnBurnDiscFormatCommit(object data)
	{
		ClientConfiguration.CDBurn.DiscFormat = ((NamedIntOption)_burnDiscFormat.ChosenValue).Value;
		CDAccess.Instance.UpdateIsAudioBurn();
	}

	private void OnBurnSpeedCommit(object data)
	{
		ClientConfiguration.CDBurn.BurnSpeed = ((NamedIntOption)_burnSpeed.ChosenValue).Value;
	}

	private void OnMediaInfoChoiceCommit(object data)
	{
		ClientConfiguration.MediaStore.ConnectToInternetForAlbumMetadata = _mediaInfoChoice.Value;
	}

	public void ScanAndClearDeletedMedia()
	{
		ZuneLibrary.ScanAndClearDeletedMedia();
	}

	private void OnSqmChoiceCommit(object data)
	{
		ClientConfiguration.SQM.UsageTracking = _sqmChoice.Value;
		ClientConfiguration.FUE.AcceptedPrivacyStatement = _sqmChoice.Value;
	}

	private void OnPodcastDefaultKeepEpisodesChoiceCommit(object data)
	{
		ClientConfiguration.Series.PodcastDefaultKeepEpisodes = ((NamedIntOption)_podcastDefaultKeepEpisodesChoice.ChosenValue).Value;
	}

	private void OnPodcastPlaybackChoiceCommit(object data)
	{
		ClientConfiguration.Series.PodcastDefaultPlaybackOrder = ((NamedIntOption)_podcastPlaybackChoice.ChosenValue).Value;
	}

	private void OnMetadataChoiceCommit(object data)
	{
		ClientConfiguration.MediaStore.OverwriteAllMetadata = _metadataChoice.Value;
	}

	private void PopulateFileTypes()
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Invalid comparison between Unknown and I4
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Invalid comparison between Unknown and I4
		if (_audioFileTypes != null && _videoFileTypes != null)
		{
			return;
		}
		_allFileTypes = new List<BooleanInputChoice>();
		_audioFileTypes = new List<BooleanInputChoice>();
		_videoFileTypes = new List<BooleanInputChoice>();
		HRESULT val = HRESULT.op_Implicit(FileAssocHandler.GetFileAssociationInfoList(ref _fileAssociationInfoList));
		if (((HRESULT)(ref val)).IsSuccess)
		{
			CanFileAssociationBeChanged = FileAssocHandler.CanAssociationBeChanged();
			string format = Shell.LoadString(StringId.IDS_FILE_TYPES_DESCRIPTION_FORMAT);
			for (int i = 0; i < _fileAssociationInfoList.Count; i++)
			{
				string extension = _fileAssociationInfoList[i].Extension;
				string description = string.Format(format, extension.Substring(1), _fileAssociationInfoList[i].Description);
				BooleanInputChoice booleanInputChoice = new BooleanInputChoice((ModelItem)(object)this, description, CanFileAssociationBeChanged);
				if (ClientConfiguration.FUE.ShowFUE && CanFileAssociationBeChanged && Array.IndexOf(_defaultFileTypeExtensions, extension) >= 0)
				{
					_fileAssociationInfoList[i].IsCurrentlyOwned = true;
				}
				((BooleanChoice)booleanInputChoice).Value = _fileAssociationInfoList[i].IsCurrentlyOwned;
				((Choice)booleanInputChoice).ChosenChanged += delegate
				{
					CommitList[new ProxySettingDelegate(OnFileTypesCommit)] = null;
				};
				EMediaTypes mediaType = _fileAssociationInfoList[i].MediaType;
				if ((int)mediaType == 3)
				{
					_audioFileTypes.Add(booleanInputChoice);
				}
				else if ((int)mediaType == 4)
				{
					_videoFileTypes.Add(booleanInputChoice);
				}
				_allFileTypes.Add(booleanInputChoice);
			}
		}
		else
		{
			ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, Shell.LoadString(StringId.IDS_FILE_TYPES_ERROR_DIALOG_TITLE));
		}
	}

	private void OnFileTypesCommit(object data)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		if (FileAssocHandler != null && CanFileAssociationBeChanged)
		{
			for (int i = 0; i < _allFileTypes.Count; i++)
			{
				_fileAssociationInfoList[i].IsCurrentlyOwned = ((BooleanChoice)_allFileTypes[i]).Value;
			}
			HRESULT val = HRESULT.op_Implicit(FileAssocHandler.SetFileAssociationInfo(_fileAssociationInfoList));
			if (((HRESULT)(ref val)).IsError)
			{
				ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, Shell.LoadString(StringId.IDS_FILE_TYPES_ERROR_DIALOG_TITLE));
			}
			if (Shell.SettingsFrame.Wizard.IsCurrent)
			{
				Fue.Instance.SetFileTypeAssociationsAreSet();
			}
		}
	}

	public void SelectAllFileTypes()
	{
		if (CanFileAssociationBeChanged)
		{
			for (int i = 0; i < _allFileTypes.Count; i++)
			{
				((BooleanChoice)_allFileTypes[i]).Value = true;
			}
		}
	}

	public void SaveFileTypesAsDefault()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (!FileAssocHandler.CanAssociationBeChanged())
		{
			return;
		}
		IList<FileAssociationInfo> list = default(IList<FileAssociationInfo>);
		HRESULT val = HRESULT.op_Implicit(FileAssocHandler.GetFileAssociationInfoList(ref list));
		if (((HRESULT)(ref val)).IsSuccess)
		{
			for (int i = 0; i < list.Count; i++)
			{
				string extension = list[i].Extension;
				if (Array.IndexOf(_defaultFileTypeExtensions, extension) >= 0)
				{
					list[i].IsCurrentlyOwned = true;
				}
			}
			val = HRESULT.op_Implicit(FileAssocHandler.SetFileAssociationInfo(list));
			if (((HRESULT)(ref val)).IsError)
			{
				ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, Shell.LoadString(StringId.IDS_FILE_TYPES_ERROR_DIALOG_TITLE));
			}
		}
		else
		{
			ErrorDialogInfo.Show(((HRESULT)(ref val)).Int, Shell.LoadString(StringId.IDS_FILE_TYPES_ERROR_DIALOG_TITLE));
		}
	}

	public void ResetWarningMessages()
	{
		ClientConfiguration.MediaStore.LibraryDefaultDeleteChoice = 0;
		ClientConfiguration.MediaStore.PlaylistDefaultDeleteChoice = 0;
		ClientConfiguration.Pictures.DisplayAutouploadNotification = true;
		ClientConfiguration.Series.PodcastDefaultUnsubscribeChoice = 0;
		ClientConfiguration.Service.InhibitSubscriptionMachineCountExceededSignInWarning = false;
		ClientConfiguration.Service.InhibitSubscriptionBillingViolationSignInWarning = false;
		ClientConfiguration.Service.InhibitSubscriptionEndingWarning = false;
		ClientConfiguration.Service.InhibitSubscriptionFreePurchasePrompt = false;
		ClientConfiguration.Service.InhibitWinPhoneAppPurchaseConfirmation = false;
		ClientConfiguration.Service.InhibitReviewRefreshWarning = false;
		ClientConfiguration.Shell.ShowAppsForZuneHDOnlyHeader = true;
		ClientConfiguration.Shell.ShowAppsForWindowsPhoneOnlyHeader = true;
		ClientConfiguration.MediaStore.ConfirmAccountDevicePCDeletion = true;
		ClientConfiguration.MediaStore.ConfirmAccountDevicePortableDeletion = true;
		ClientConfiguration.MediaStore.ConfirmDeviceMediaDeletion = true;
		ClientConfiguration.MediaStore.ConfirmMultiAlbumEdit = true;
		ClientConfiguration.MediaStore.ConfirmMultiSongEdit = true;
		ClientConfiguration.MediaStore.ConfirmMultiVideoEdit = true;
		ClientConfiguration.MediaStore.ConfirmPasteAlbumArt = true;
		ClientConfiguration.MediaStore.AlertSyncAllFriendsBehavior = true;
		ClientConfiguration.Social.ConfirmAcceptFriend = true;
		ClientConfiguration.Social.ConfirmDeleteFriend = true;
		ClientConfiguration.QuickMix.OnlyEnableItemsWithQuickMix = false;
		ClientConfiguration.Devices.ShowExcludeFromSyncWarning = true;
		ClientConfiguration.Devices.ShowSyncInstructionsToast = true;
		foreach (UIDevice item in SingletonModelItem<UIDeviceList>.Instance)
		{
			item.PromptForAccountLinkage = true;
		}
	}

	public void RemoveTuner(TunerInfo tunerInfo)
	{
		if (_tunerHandler == null)
		{
			InitRegisteredTuners();
		}
		_tunerHandler.DeregisterTuner(tunerInfo);
	}

	public void RefreshTunerList()
	{
		if (_tunerHandler.CanQueryTunerList())
		{
			_tunerHandler.RefreshTunerList();
		}
	}

	private void OnTunerInfoChanged(object oSenderUNUSED, EventArgs eargs)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Expected O, but got Unknown
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		if (eargs != null && (object)eargs.GetType() == typeof(EventArgsHR) && HRESULT.op_Implicit(((EventArgsHR)eargs).HResult) == HRESULT._ZEST_E_TOO_MANY_DEREGISTRATIONS_WITHIN_MONTH)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DisplayServiceErrorMessage), (object)eargs);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(UpdateRegisteredTunersList), (DeferredInvokePriority)0);
		}
	}

	private void DisplayServiceErrorMessage(object eargs)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		Shell.ShowErrorDialog(((EventArgsHR)eargs).HResult, StringId.IDS_REGDEVICES_CANT_REMOVE);
	}

	private void UpdateRegisteredTunersList(object argsUNUSED)
	{
		if (_registeredComputersModelList == null || _registeredDevicesModelList == null || _registeredAppStoreDevicesModelList == null)
		{
			return;
		}
		int count = ((ListDataSet)_registeredComputersModelList).Count;
		int count2 = ((ListDataSet)_registeredDevicesModelList).Count;
		int count3 = ((ListDataSet)_registeredAppStoreDevicesModelList).Count;
		((ListDataSet)_registeredComputersModelList).Clear();
		((ListDataSet)_registeredDevicesModelList).Clear();
		((ListDataSet)_registeredAppStoreDevicesModelList).Clear();
		foreach (TunerInfo pCs in _tunerHandler.GetPCsList())
		{
			((ListDataSet)_registeredComputersModelList).Add((object)pCs);
		}
		foreach (TunerInfo devices in _tunerHandler.GetDevicesList())
		{
			((ListDataSet)_registeredDevicesModelList).Add((object)devices);
		}
		foreach (TunerInfo appStoreDevices in _tunerHandler.GetAppStoreDevicesList())
		{
			((ListDataSet)_registeredAppStoreDevicesModelList).Add((object)appStoreDevices);
		}
		DateTime nextPCDeregistrationDate = _tunerHandler.GetNextPCDeregistrationDate();
		if (nextPCDeregistrationDate != DateTime.MinValue && nextPCDeregistrationDate > DateTime.Now)
		{
			NextPCDeregistrationDate = nextPCDeregistrationDate.AddDays(1.0).ToShortDateString();
		}
		else
		{
			NextPCDeregistrationDate = null;
		}
		nextPCDeregistrationDate = _tunerHandler.GetNextSubscriptionDeviceDeregistrationDate();
		if (nextPCDeregistrationDate != DateTime.MinValue && nextPCDeregistrationDate > DateTime.Now)
		{
			NextSubscriptionDeviceDeregistrationDate = nextPCDeregistrationDate.AddDays(1.0).ToShortDateString();
		}
		else
		{
			NextSubscriptionDeviceDeregistrationDate = null;
		}
		nextPCDeregistrationDate = _tunerHandler.GetNextAppStoreDeviceDeregistrationDate();
		if (nextPCDeregistrationDate != DateTime.MinValue && nextPCDeregistrationDate > DateTime.Now)
		{
			NextAppStoreDeviceDeregistrationDate = nextPCDeregistrationDate.AddDays(1.0).ToShortDateString();
		}
		else
		{
			NextAppStoreDeviceDeregistrationDate = null;
		}
		if (((ListDataSet)_registeredComputersModelList).Count < count || ((ListDataSet)_registeredDevicesModelList).Count < count2 || ((ListDataSet)_registeredAppStoreDevicesModelList).Count < count3)
		{
			SignIn.Instance.RefreshAccount();
		}
	}

	private void InitRegisteredTuners()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		_tunerHandler = TunerInfoHandlerFactory.CreateTunerInfoHandler();
		_tunerHandler.OnChanged += OnTunerInfoChanged;
		_registeredComputersModelList = new ArrayListDataSet();
		_registeredDevicesModelList = new ArrayListDataSet();
		_registeredAppStoreDevicesModelList = new ArrayListDataSet();
		_nextPCDeregistrationDate = null;
		_nextSubscriptionDeviceDeregistrationDate = null;
		_nextAppStoreDeviceDeregistrationDate = null;
		if (_tunerHandler.CanQueryTunerList())
		{
			_tunerHandler.RefreshTunerList();
		}
	}

	private void OnSlideShowSpeedCommit(object data)
	{
		ClientConfiguration.GeneralSettings.SlideShowSpeed = _slideShowSpeed.Value;
	}

	private void SetSharingEnabledForAllMediaTypes(bool music, bool video, bool pictures)
	{
		HME.SetSharingEnabledForMediaType((EMediaTypes)3, music);
		HME.SetSharingEnabledForMediaType((EMediaTypes)4, video);
		HME.SetSharingEnabledForMediaType((EMediaTypes)5, pictures);
	}

	private void OnMediaSharingUpdate(object data)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (HME == null)
		{
			return;
		}
		bool flag = _sharingEnableMusic.Value || _sharingEnableVideo.Value || _sharingEnablePhoto.Value;
		HRESULT val = HRESULT.op_Implicit(0);
		if (flag)
		{
			val = HRESULT.op_Implicit(HME.EnableSharingForUser());
			if (((HRESULT)(ref val)).IsSuccess)
			{
				SetSharingEnabledForAllMediaTypes(_sharingEnableMusic.Value, _sharingEnableVideo.Value, _sharingEnablePhoto.Value);
			}
			else
			{
				SetSharingEnabledForAllMediaTypes(music: false, video: false, pictures: false);
			}
		}
		else
		{
			val = HRESULT.op_Implicit(HME.DisableSharingForMachine());
			if (((HRESULT)(ref val)).IsSuccess)
			{
				val = HRESULT.op_Implicit(HME.DisableSharingForUser());
			}
			if (((HRESULT)(ref val)).IsSuccess)
			{
				SetSharingEnabledForAllMediaTypes(music: false, video: false, pictures: false);
			}
		}
		HME.SetAllDevicesEnabled(_sharingAllDevicesEnabled);
		if (!_sharingAllDevicesEnabled)
		{
			HME.EnableDevice(_sharingDeviceIndex, true);
		}
		HME.SetDisplayName(_sharingDisplayName);
	}

	private IList<BooleanInputChoice> CreateSharingDeviceList()
	{
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		IList<BooleanInputChoice> list = new List<BooleanInputChoice>();
		uint deviceCount = HME.GetDeviceCount();
		string text = "";
		string text2 = "";
		string arg = "";
		string text3 = "";
		for (uint num = 0u; num < deviceCount; num++)
		{
			HME.GetDeviceProps(num, ref text, ref text2, ref arg);
			text3 = ((deviceCount > 1) ? string.Format(Shell.LoadString(StringId.IDS_XBOX360_NAME_AND_SERIAL_NUMBER), text, arg) : text);
			BooleanInputChoice booleanInputChoice = new BooleanInputChoice((ModelItem)(object)this, text3, isAvailable: true);
			((ModelItem)booleanInputChoice).Data["index"] = num;
			((BooleanChoice)booleanInputChoice).Value = HME.GetDeviceEnabled(num);
			((Choice)booleanInputChoice).ChosenChanged += HandleSharingDeviceListValueChanged;
			list.Add(booleanInputChoice);
		}
		if (!_nssDeviceListChangeEventAdded)
		{
			HME.NSSDeviceListChangeEvent += new NSSDeviceListChangeHandler(HandleNSSDeviceListChangeEvent);
			_nssDeviceListChangeEventAdded = true;
		}
		return list;
	}

	public void RemoveNSSDeviceListChangeEvent()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		if (_nssDeviceListChangeEventAdded)
		{
			HME.NSSDeviceListChangeEvent -= new NSSDeviceListChangeHandler(HandleNSSDeviceListChangeEvent);
			_nssDeviceListChangeEventAdded = false;
			_sharingDeviceList = null;
		}
	}

	private void HandleNSSDeviceListChangeEvent()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (_sharingDeviceList != null)
			{
				_sharingDeviceList = CreateSharingDeviceList();
				((ModelItem)this).FirePropertyChanged("SharingDeviceList");
			}
		}, (object)null);
	}

	private void HandleSharingDeviceListValueChanged(object sender, EventArgs args)
	{
		BooleanInputChoice booleanInputChoice = (BooleanInputChoice)sender;
		_sharingDeviceIndex = (uint)((ModelItem)booleanInputChoice).Data["index"];
	}

	private void ReevaluateVideoAcceleration(object sender, EventArgs args)
	{
		RequestedRenderingType = (RenderingType)1;
	}

	private void OnScreenGraphicsSliderCommit(object data)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Invalid comparison between Unknown and I4
		int value = ((NamedIntOption)_screenGraphicsSlider.ChosenValue).Value;
		ScreenGraphics screenGraphics = (ScreenGraphics)value;
		RenderingType requestedRenderingType = RequestedRenderingType;
		switch (screenGraphics)
		{
		case ScreenGraphics.Basic:
			RequestedRenderingType = (RenderingType)0;
			RequestedRenderingQuality = (RenderingQuality)0;
			AnimationsEnabled = false;
			break;
		case ScreenGraphics.Advanced:
			RequestedRenderingType = (RenderingType)1;
			RequestedRenderingQuality = (RenderingQuality)0;
			AnimationsEnabled = false;
			break;
		case ScreenGraphics.AdvancedWithAnimation:
			RequestedRenderingType = (RenderingType)1;
			RequestedRenderingQuality = (RenderingQuality)0;
			AnimationsEnabled = true;
			break;
		case ScreenGraphics.Premium:
			RequestedRenderingType = (RenderingType)1;
			RequestedRenderingQuality = (RenderingQuality)1;
			AnimationsEnabled = true;
			break;
		}
		if ((int)requestedRenderingType == 0 && (int)RequestedRenderingType == 1)
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_SCREEN_GRAPHICS_TITLE), Shell.LoadString(StringId.IDS_ACCELERATION_PROMPT_TEXT), (EventHandler)null);
		}
		else
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_SCREEN_GRAPHICS_TITLE), Shell.LoadString(StringId.IDS_ACCELERATION_RESTART_TEXT), (EventHandler)null);
		}
	}

	private void OnBackgroundImageCommit(object data)
	{
		ClientConfiguration.Shell.BackgroundImage = _backgroundImage;
		ClientConfiguration.Shell.BackgroundColor = Shell.WindowColorToRGB(_backgroundColor);
		((Shell)ZuneShell.DefaultInstance).BackgroundImage = _backgroundImage;
		Application.Window.SetBackgroundColor(_backgroundColor);
	}

	private void OnShowNowPlayingBackgroundOnIdleCommit(object data)
	{
		int showNowPlayingBackgroundOnIdleTimeout = (_showNowPlayingBackgroundOnIdle.Value ? 90 : 0);
		ClientConfiguration.Shell.ShowNowPlayingBackgroundOnIdleTimeout = showNowPlayingBackgroundOnIdleTimeout;
		((Shell)ZuneShell.DefaultInstance).ShowNowPlayingBackgroundOnIdleTimeout = showNowPlayingBackgroundOnIdleTimeout;
	}

	private void OnPlaySoundsCommit(object data)
	{
		ClientConfiguration.Shell.Sounds = _playSounds.Value;
		((Shell)ZuneShell.DefaultInstance).PlaySounds = _playSounds.Value;
	}

	private void OnCompactModeAlwaysOnTopCommit(object data)
	{
		ClientConfiguration.GeneralSettings.CompactModeAlwaysOnTop = _compactModeAlwaysOnTop.Value;
		((Shell)ZuneShell.DefaultInstance).CompactModeAlwaysOnTop = _compactModeAlwaysOnTop.Value;
		SQMLog.Log((SQMDataId)164, 1);
	}

	private void OnRatingsCommit(object data)
	{
		ClientConfiguration.MediaStore.SharedUserRatings = !_ratingsChoice.Value;
		if (_applyRatingsChoice.Value && !_ratingsChoice.Value)
		{
			ZuneLibrary.ExportUserRatings(SignIn.Instance.LastSignedInUserId, (EMediaTypes)3);
		}
	}

	private void OnStartupPageCommit(object data)
	{
		ClientConfiguration.Shell.StartupPage = ((NamedStringOption)_startupPageChoice.ChosenValue).Value;
		ClientConfiguration.Quickplay.CheckUseCount = false;
	}

	private void OnAutoLaunchZuneOnConnectCommit(object data)
	{
		ClientConfiguration.Devices.AutoLaunchZuneOnConnect = _autoLaunchZuneOnConnect;
	}
}
