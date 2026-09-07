using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Interfaces;

namespace NotZune.UI.ViewModels;

public record AccentColorOption(string Name, string HexCode);
public record BackgroundThemeOption(string Name, string? AssetUri);

public class SettingsViewModel : ViewModelBase
{
    private readonly ISoundEffectService? _soundService;
    public event EventHandler<string?>? BackgroundArtChanged;

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

    private string _musicFolderPath = "~/Music";
    public string MusicFolderPath
    {
        get => _musicFolderPath;
        set => SetProperty(ref _musicFolderPath, value);
    }

    public string PlatformInfo => $"{System.Runtime.InteropServices.RuntimeInformation.OSDescription} ({System.Runtime.InteropServices.RuntimeInformation.ProcessArchitecture})";
    public string VersionInfo => "Not-Zune v0.1.0-alpha";

    public ICommand SelectFolderCommand { get; }
    public ICommand SelectAccentCommand { get; }
    public ICommand SelectBackgroundCommand { get; }
    public ICommand TestSoundCommand { get; }

    public SettingsViewModel(ISoundEffectService? soundService = null)
    {
        _soundService = soundService;
        _selectedAccent = AccentColors[0];
        _selectedBackground = BackgroundThemes[1]; // Default to authentic Zune Vector Ribbon
        SelectFolderCommand = new RelayCommand(() => { });
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
