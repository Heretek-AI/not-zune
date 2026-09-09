using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Interfaces;

namespace NotZune.UI.ViewModels;

public record AccentColorOption(string Name, string HexCode);
public record BackgroundThemeOption(string Name, string? AssetUri);

public enum SettingsTopLevelPivot
{
    Software,
    Device
}

public enum SoftwareSubPivot
{
    Collection,
    Playback,
    Rip,
    Burn,
    Metadata,
    Display,
    About
}

public enum DeviceSubPivot
{
    SyncOptions,
    SpaceReservation,
    WirelessSync,
    DeviceInfo
}

public class SettingsViewModel : ViewModelBase
{
    private readonly ISoundEffectService? _soundService;
    private readonly IFolderPickerService? _folderPicker;
    private readonly IMediaLibraryService? _libraryService;
    private readonly IPlayerCoordinator? _playerCoordinator;
    private readonly IDeviceSyncService? _deviceSyncService;

    public event EventHandler<string?>? BackgroundArtChanged;

    // ==========================================
    // TWO-TIER PIVOT NAVIGATION
    // ==========================================
    private SettingsTopLevelPivot _topLevelPivot = SettingsTopLevelPivot.Software;
    public SettingsTopLevelPivot TopLevelPivot
    {
        get => _topLevelPivot;
        set
        {
            if (SetProperty(ref _topLevelPivot, value))
            {
                OnPropertyChanged(nameof(IsSoftwarePivotActive));
                OnPropertyChanged(nameof(IsDevicePivotActive));
            }
        }
    }

    public bool IsSoftwarePivotActive => TopLevelPivot == SettingsTopLevelPivot.Software;
    public bool IsDevicePivotActive => TopLevelPivot == SettingsTopLevelPivot.Device;

    private SoftwareSubPivot _softwarePivot = SoftwareSubPivot.Collection;
    public SoftwareSubPivot SoftwarePivot
    {
        get => _softwarePivot;
        set
        {
            if (SetProperty(ref _softwarePivot, value))
            {
                OnPropertyChanged(nameof(IsCollectionSubPivotActive));
                OnPropertyChanged(nameof(IsPlaybackSubPivotActive));
                OnPropertyChanged(nameof(IsRipSubPivotActive));
                OnPropertyChanged(nameof(IsBurnSubPivotActive));
                OnPropertyChanged(nameof(IsMetadataSubPivotActive));
                OnPropertyChanged(nameof(IsDisplaySubPivotActive));
                OnPropertyChanged(nameof(IsAboutSubPivotActive));
            }
        }
    }

    public bool IsCollectionSubPivotActive => SoftwarePivot == SoftwareSubPivot.Collection;
    public bool IsPlaybackSubPivotActive => SoftwarePivot == SoftwareSubPivot.Playback;
    public bool IsRipSubPivotActive => SoftwarePivot == SoftwareSubPivot.Rip;
    public bool IsBurnSubPivotActive => SoftwarePivot == SoftwareSubPivot.Burn;
    public bool IsMetadataSubPivotActive => SoftwarePivot == SoftwareSubPivot.Metadata;
    public bool IsDisplaySubPivotActive => SoftwarePivot == SoftwareSubPivot.Display;
    public bool IsAboutSubPivotActive => SoftwarePivot == SoftwareSubPivot.About;

    private DeviceSubPivot _devicePivot = DeviceSubPivot.SyncOptions;
    public DeviceSubPivot DevicePivot
    {
        get => _devicePivot;
        set
        {
            if (SetProperty(ref _devicePivot, value))
            {
                OnPropertyChanged(nameof(IsSyncOptionsSubPivotActive));
                OnPropertyChanged(nameof(IsSpaceReservationSubPivotActive));
                OnPropertyChanged(nameof(IsWirelessSyncSubPivotActive));
                OnPropertyChanged(nameof(IsDeviceInfoSubPivotActive));
            }
        }
    }

    public bool IsSyncOptionsSubPivotActive => DevicePivot == DeviceSubPivot.SyncOptions;
    public bool IsSpaceReservationSubPivotActive => DevicePivot == DeviceSubPivot.SpaceReservation;
    public bool IsWirelessSyncSubPivotActive => DevicePivot == DeviceSubPivot.WirelessSync;
    public bool IsDeviceInfoSubPivotActive => DevicePivot == DeviceSubPivot.DeviceInfo;

    // ==========================================
    // THEMES & ACCENTS
    // ==========================================
    public ObservableCollection<AccentColorOption> AccentColors { get; } = new()
    {
        new("Zune Pink (Signature)", "#FA2A55"),
        new("Zune Orange", "#F09609"),
        new("Zune Electric Cyan", "#1BA1E2"),
        new("Zune Vivid Lime", "#339933"),
        new("Zune Deep Purple", "#A200FF")
    };

    public ObservableCollection<BackgroundThemeOption> BackgroundThemes { get; } = new()
    {
        new("Classic Minimal (Matte Black)", null),
        new("Vector Ribbon 10", "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-10.JPG"),
        new("Abstract Aurora 15", "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-15.JPG"),
        new("Geometric Mesh 20", "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-20.JPG"),
        new("Cosmic Gradient 25", "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-25.JPG"),
        new("Circuit Flow 30", "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-30.JPG"),
        new("Prism Waves 35", "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-35.JPG"),
        new("Retro Horizon 40", "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-40.JPG"),
        new("Radiant Bloom 45", "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-45.JPG"),
        new("Neon Drift 47", "avares://NotZune.UI/Assets/Zune/Backgrounds/USERBACKGROUND-ART-536X196-47.JPG")
    };

    private AccentColorOption _selectedAccent;
    public AccentColorOption SelectedAccent
    {
        get => _selectedAccent;
        set
        {
            if (SetProperty(ref _selectedAccent, value))
            {
                ApplyAccent(value);
            }
        }
    }

    private BackgroundThemeOption _selectedBackground;
    public BackgroundThemeOption SelectedBackground
    {
        get => _selectedBackground;
        set
        {
            if (SetProperty(ref _selectedBackground, value))
            {
                BackgroundArtChanged?.Invoke(this, value.AssetUri);
            }
        }
    }

    // ==========================================
    // AUDIO & PLAYBACK SETTINGS
    // ==========================================
    private bool _crossfadeEnabled = true;
    public bool CrossfadeEnabled
    {
        get => _crossfadeEnabled;
        set
        {
            if (SetProperty(ref _crossfadeEnabled, value))
            {
                if (_playerCoordinator != null)
                {
                    _playerCoordinator.CrossfadeDurationSeconds = value ? _crossfadeDurationSeconds : 0.0;
                }
            }
        }
    }

    private double _crossfadeDurationSeconds = 2.0;
    public double CrossfadeDurationSeconds
    {
        get => _crossfadeDurationSeconds;
        set
        {
            var clamped = Math.Clamp(value, 0.0, 10.0);
            if (SetProperty(ref _crossfadeDurationSeconds, clamped))
            {
                if (_playerCoordinator != null && _crossfadeEnabled)
                {
                    _playerCoordinator.CrossfadeDurationSeconds = clamped;
                }
                OnPropertyChanged(nameof(CrossfadeDurationText));
            }
        }
    }

    public string CrossfadeDurationText => $"{CrossfadeDurationSeconds:0.0} seconds";

    private bool _gaplessPlaybackEnabled = true;
    public bool GaplessPlaybackEnabled
    {
        get => _gaplessPlaybackEnabled;
        set
        {
            if (SetProperty(ref _gaplessPlaybackEnabled, value))
            {
                if (_playerCoordinator != null)
                {
                    _playerCoordinator.GaplessEnabled = value;
                }
            }
        }
    }

    public bool SoundEffectsEnabled
    {
        get => _soundService?.SoundEffectsEnabled ?? true;
        set
        {
            if (_soundService != null)
            {
                _soundService.SoundEffectsEnabled = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _volumeLevelingEnabled = true;
    public bool VolumeLevelingEnabled
    {
        get => _volumeLevelingEnabled;
        set => SetProperty(ref _volumeLevelingEnabled, value);
    }

    private bool _compactModeAlwaysOnTop = true;
    public bool CompactModeAlwaysOnTop
    {
        get => _compactModeAlwaysOnTop;
        set => SetProperty(ref _compactModeAlwaysOnTop, value);
    }

    // ==========================================
    // RIP SETTINGS
    // ==========================================
    public ObservableCollection<string> RipAudioFormats { get; } = new()
    {
        "FLAC (Lossless Free Audio)",
        "MP3 (High Quality VBR)",
        "WMA (Windows Media Audio 9.2)",
        "AAC (Advanced Audio Coding)"
    };

    private string _selectedRipFormat = "FLAC (Lossless Free Audio)";
    public string SelectedRipFormat
    {
        get => _selectedRipFormat;
        set => SetProperty(ref _selectedRipFormat, value);
    }

    public ObservableCollection<string> RipBitrates { get; } = new()
    {
        "Lossless (Maximum Fidelity)",
        "320 kbps (Extreme)",
        "256 kbps (High Quality)",
        "192 kbps (Standard)",
        "128 kbps (Compact)"
    };

    private string _selectedRipBitrate = "Lossless (Maximum Fidelity)";
    public string SelectedRipBitrate
    {
        get => _selectedRipBitrate;
        set => SetProperty(ref _selectedRipBitrate, value);
    }

    private string _ripDestinationFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "NotZune Rips");
    public string RipDestinationFolder
    {
        get => _ripDestinationFolder;
        set => SetProperty(ref _ripDestinationFolder, value);
    }

    private bool _autoRipCdOnInsert = false;
    public bool AutoRipCdOnInsert
    {
        get => _autoRipCdOnInsert;
        set => SetProperty(ref _autoRipCdOnInsert, value);
    }

    private bool _ejectCdAfterRip = true;
    public bool EjectCdAfterRip
    {
        get => _ejectCdAfterRip;
        set => SetProperty(ref _ejectCdAfterRip, value);
    }

    // ==========================================
    // BURN SETTINGS
    // ==========================================
    public ObservableCollection<string> DiscTypes { get; } = new()
    {
        "Audio CD (Red Book standard, playable in car/home stereos)",
        "Data Disc (MP3/FLAC disc, holds up to 150+ tracks)"
    };

    private string _selectedDiscType = "Audio CD (Red Book standard, playable in car/home stereos)";
    public string SelectedDiscType
    {
        get => _selectedDiscType;
        set => SetProperty(ref _selectedDiscType, value);
    }

    public ObservableCollection<string> BurnSpeeds { get; } = new()
    {
        "Maximum Drive Speed",
        "24x",
        "16x (Recommended for Audio CD)",
        "8x",
        "4x"
    };

    private string _selectedBurnSpeed = "16x (Recommended for Audio CD)";
    public string SelectedBurnSpeed
    {
        get => _selectedBurnSpeed;
        set => SetProperty(ref _selectedBurnSpeed, value);
    }

    private bool _applyVolumeLevelingToBurn = true;
    public bool ApplyVolumeLevelingToBurn
    {
        get => _applyVolumeLevelingToBurn;
        set => SetProperty(ref _applyVolumeLevelingToBurn, value);
    }

    // ==========================================
    // METADATA SETTINGS
    // ==========================================
    private bool _autoFetchMetadata = true;
    public bool AutoFetchMetadata
    {
        get => _autoFetchMetadata;
        set => SetProperty(ref _autoFetchMetadata, value);
    }

    private bool _autoDownloadArtistArt = true;
    public bool AutoDownloadArtistArt
    {
        get => _autoDownloadArtistArt;
        set => SetProperty(ref _autoDownloadArtistArt, value);
    }

    private bool _writeTagsToFile = true;
    public bool WriteTagsToFile
    {
        get => _writeTagsToFile;
        set => SetProperty(ref _writeTagsToFile, value);
    }

    private bool _musicBrainzEnabled = true;
    public bool MusicBrainzEnabled
    {
        get => _musicBrainzEnabled;
        set => SetProperty(ref _musicBrainzEnabled, value);
    }

    private bool _lastFmEnabled = true;
    public bool LastFmEnabled
    {
        get => _lastFmEnabled;
        set => SetProperty(ref _lastFmEnabled, value);
    }

    // ==========================================
    // COLLECTION SETTINGS
    // ==========================================
    private bool _autoWatchFolder = true;
    public bool AutoWatchFolder
    {
        get => _autoWatchFolder;
        set
        {
            if (SetProperty(ref _autoWatchFolder, value))
            {
                if (value)
                {
                    _libraryService?.StartDirectoryWatcher(NormalizePath(MusicFolderPath));
                }
                else
                {
                    _libraryService?.StopDirectoryWatcher();
                }
            }
        }
    }

    private string _startupView = "Quickplay";
    public string StartupView
    {
        get => _startupView;
        set => SetProperty(ref _startupView, value);
    }

    private string _musicFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) is { Length: > 0 } myMusic
        ? myMusic
        : "~/Music";
    public string MusicFolderPath
    {
        get => _musicFolderPath;
        set => SetProperty(ref _musicFolderPath, value);
    }

    private bool _isScanning;
    public bool IsScanning
    {
        get => _isScanning;
        set => SetProperty(ref _isScanning, value);
    }

    private double _scanProgress;
    public double ScanProgress
    {
        get => _scanProgress;
        set => SetProperty(ref _scanProgress, value);
    }

    private string? _scanStatusText;
    public string? ScanStatusText
    {
        get => _scanStatusText;
        set
        {
            if (SetProperty(ref _scanStatusText, value))
            {
                OnPropertyChanged(nameof(HasScanStatus));
            }
        }
    }

    public bool HasScanStatus => !string.IsNullOrEmpty(ScanStatusText);

    // ==========================================
    // DEVICE SETTINGS (SYNC & SPACE RESERVATION)
    // ==========================================
    public bool IsDeviceConnected => _deviceSyncService?.ConnectedDevices.Count > 0;
    public string DeviceModelName => IsDeviceConnected 
        ? _deviceSyncService!.ConnectedDevices[0].ModelName 
        : "Zune HD (Simulated Standby)";
    public string DeviceSerialNumber => IsDeviceConnected 
        ? _deviceSyncService!.ConnectedDevices[0].SerialNumber 
        : "0001020304050607";
    public string FirmwareVersion => "v4.5 (3084)";
    public string DeviceBatteryText => "88% (Charging)";

    public double TotalCapacityGb => 32.0;

    private int _spaceReservationPercent = 10;
    public int SpaceReservationPercent
    {
        get => _spaceReservationPercent;
        set
        {
            var clamped = Math.Clamp(value, 0, 50);
            if (SetProperty(ref _spaceReservationPercent, clamped))
            {
                OnPropertyChanged(nameof(ReservedGbText));
                OnPropertyChanged(nameof(SyncSpaceGbText));
                OnPropertyChanged(nameof(SpaceReservationSummaryText));
            }
        }
    }

    public string ReservedGbText => $"{(TotalCapacityGb * (_spaceReservationPercent / 100.0)):0.0} GB";
    public string SyncSpaceGbText => $"{(TotalCapacityGb * (1.0 - (_spaceReservationPercent / 100.0))):0.0} GB";
    public string SpaceReservationSummaryText => $"{SpaceReservationPercent}% ({ReservedGbText}) reserved for device cache and non-sync files";

    public ObservableCollection<string> SyncRules { get; } = new()
    {
        "All Music (Automatic Sync)",
        "Selected Playlists, Artists & Genres",
        "Manual Sync Only (Drag and Drop)"
    };

    private string _musicSyncRule = "All Music (Automatic Sync)";
    public string MusicSyncRule
    {
        get => _musicSyncRule;
        set => SetProperty(ref _musicSyncRule, value);
    }

    public ObservableCollection<string> PodcastSyncRules { get; } = new()
    {
        "All Unplayed Episodes",
        "3 Newest Episodes",
        "5 Newest Episodes",
        "All Episodes"
    };

    private string _podcastSyncRule = "3 Newest Episodes";
    public string PodcastSyncRule
    {
        get => _podcastSyncRule;
        set => SetProperty(ref _podcastSyncRule, value);
    }

    private bool _wirelessSyncEnabled = true;
    public bool WirelessSyncEnabled
    {
        get => _wirelessSyncEnabled;
        set => SetProperty(ref _wirelessSyncEnabled, value);
    }

    private string _networkName = "Home-WiFi (WPA2)";
    public string NetworkName
    {
        get => _networkName;
        set => SetProperty(ref _networkName, value);
    }

    public string PlatformInfo => $"{System.Runtime.InteropServices.RuntimeInformation.OSDescription} ({System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture})";
    public string VersionInfo => "Not-Zune v1.0.0 (Phase 3 Fidelity Engine)";

    // ==========================================
    // COMMANDS
    // ==========================================
    public ICommand SelectTopLevelPivotCommand { get; }
    public ICommand SelectSoftwarePivotCommand { get; }
    public ICommand SelectDevicePivotCommand { get; }
    public ICommand SelectFolderCommand { get; }
    public ICommand RescanCommand { get; }
    public ICommand ClearDemoLibraryCommand { get; }
    public ICommand SelectAccentCommand { get; }
    public ICommand SelectBackgroundCommand { get; }
    public ICommand TestSoundCommand { get; }

    public SettingsViewModel(
        ISoundEffectService? soundService = null,
        IFolderPickerService? folderPicker = null,
        IMediaLibraryService? libraryService = null,
        IPlayerCoordinator? playerCoordinator = null,
        IDeviceSyncService? deviceSyncService = null)
    {
        _soundService = soundService;
        _folderPicker = folderPicker;
        _libraryService = libraryService;
        _playerCoordinator = playerCoordinator;
        _deviceSyncService = deviceSyncService;

        _selectedAccent = AccentColors[0];
        _selectedBackground = BackgroundThemes[1]; // Default to authentic Zune Vector Ribbon

        SelectTopLevelPivotCommand = new RelayCommand<string>(pivotStr =>
        {
            if (Enum.TryParse<SettingsTopLevelPivot>(pivotStr, true, out var p))
            {
                TopLevelPivot = p;
            }
        });

        SelectSoftwarePivotCommand = new RelayCommand<string>(pivotStr =>
        {
            if (Enum.TryParse<SoftwareSubPivot>(pivotStr, true, out var p))
            {
                SoftwarePivot = p;
            }
        });

        SelectDevicePivotCommand = new RelayCommand<string>(pivotStr =>
        {
            if (Enum.TryParse<DeviceSubPivot>(pivotStr, true, out var p))
            {
                DevicePivot = p;
            }
        });

        SelectFolderCommand = new AsyncRelayCommand(OnSelectFolderAsync);
        RescanCommand = new AsyncRelayCommand(OnRescanAsync);
        ClearDemoLibraryCommand = new AsyncRelayCommand(OnClearDemoLibraryAsync);

        SelectAccentCommand = new RelayCommand<AccentColorOption>(accent =>
        {
            if (accent != null)
            {
                SelectedAccent = accent;
            }
        });
        SelectBackgroundCommand = new RelayCommand<BackgroundThemeOption>(theme =>
        {
            if (theme != null)
            {
                SelectedBackground = theme;
            }
        });
        TestSoundCommand = new RelayCommand(() => _soundService?.PlaySyncComplete());
    }

    private async Task OnSelectFolderAsync()
    {
        if (_folderPicker == null) return;
        var selected = await _folderPicker.PickFolderAsync("Select Music Collection Folder");
        if (!string.IsNullOrWhiteSpace(selected))
        {
            MusicFolderPath = selected;
            await ScanFolderAsync(selected);
        }
    }

    private async Task OnRescanAsync()
    {
        await ScanFolderAsync(MusicFolderPath);
    }

    private async Task OnClearDemoLibraryAsync()
    {
        if (_libraryService != null)
        {
            await _libraryService.ClearDemoDataAsync();
            ScanStatusText = "Demo placeholder data cleared.";
        }
    }

    private static string NormalizePath(string path)
    {
        if (path.StartsWith("~/") || path.StartsWith("~\\"))
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, path.Substring(2));
        }
        return path;
    }

    private async Task ScanFolderAsync(string? rawPath)
    {
        if (string.IsNullOrWhiteSpace(rawPath))
        {
            ScanStatusText = "Please enter or select a valid music directory.";
            return;
        }

        var folderPath = NormalizePath(rawPath);
        if (!Directory.Exists(folderPath))
        {
            ScanStatusText = $"Directory not found: {folderPath}";
            return;
        }

        if (_libraryService == null)
        {
            ScanStatusText = "Library service unavailable.";
            return;
        }

        IsScanning = true;
        ScanProgress = 0.0;
        ScanStatusText = $"Scanning {folderPath}...";

        try
        {
            var progress = new Progress<double>(p =>
            {
                ScanProgress = p;
                ScanStatusText = $"Scanning audio files: {(int)(p * 100)}%";
            });

            await _libraryService.ScanDirectoryAsync(folderPath, progress);
            _soundService?.PlaySyncComplete();
            ScanStatusText = "Library scan complete.";
        }
        catch (Exception ex)
        {
            ScanStatusText = $"Scan failed: {ex.Message}";
        }
        finally
        {
            IsScanning = false;
        }
    }

    private void ApplyAccent(AccentColorOption accent)
    {
        // Dynamically updates the Application Resource dictionary accent brush
        if (Avalonia.Application.Current?.Resources != null)
        {
            if (Avalonia.Media.Color.TryParse(accent.HexCode, out var color))
            {
                Avalonia.Application.Current.Resources["ZuneAccentBrush"] = new Avalonia.Media.SolidColorBrush(color);
                var hoverColor = Avalonia.Media.Color.FromArgb(
                    255, 
                    (byte)Math.Min(255, color.R + 25), 
                    (byte)Math.Min(255, color.G + 25), 
                    (byte)Math.Min(255, color.B + 25));
                Avalonia.Application.Current.Resources["ZuneAccentHoverBrush"] = new Avalonia.Media.SolidColorBrush(hoverColor);
            }
        }
    }
}
