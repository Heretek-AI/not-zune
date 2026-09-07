using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace NotZune.UI.ViewModels;

public record AccentColorOption(string Name, string HexCode);

public class SettingsViewModel : ViewModelBase
{
    public ObservableCollection<AccentColorOption> AccentColors { get; } = new()
    {
        new("Zune Pink (Signature)", "#FA2A55"),
        new("Zune Orange", "#F09609"),
        new("Zune Electric Cyan", "#1BA1E2"),
        new("Zune Vivid Lime", "#339933"),
        new("Zune Deep Purple", "#A200FF")
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

    public SettingsViewModel()
    {
        _selectedAccent = AccentColors[0];
        SelectFolderCommand = new RelayCommand(() => { });
        SelectAccentCommand = new RelayCommand<AccentColorOption>(accent =>
        {
            if (accent != null)
            {
                SelectedAccent = accent;
            }
        });
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
