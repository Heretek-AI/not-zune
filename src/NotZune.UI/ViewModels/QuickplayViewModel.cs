using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.UI.ViewModels;

public enum QuickplayDeck
{
    Pins,
    History,
    New
}

public class QuickplayViewModel : ViewModelBase
{
    private readonly IPlayerCoordinator _playerCoordinator;
    private readonly IMediaLibraryService _libraryService;
    private readonly ISmartDJService _smartDJService;

    private QuickplayDeck _activeDeck = QuickplayDeck.History;
    private string _smartDjSeedText = "Pick an artist or track...";

    public ObservableCollection<Album> Pins { get; } = new();
    public ObservableCollection<PlayHistoryEntry> History { get; } = new();
    public ObservableCollection<Album> NewAlbums { get; } = new();

    public QuickplayDeck ActiveDeck
    {
        get => _activeDeck;
        set
        {
            if (SetProperty(ref _activeDeck, value))
            {
                OnPropertyChanged(nameof(IsPinsActive));
                OnPropertyChanged(nameof(IsHistoryActive));
                OnPropertyChanged(nameof(IsNewActive));
            }
        }
    }

    public bool IsPinsActive => _activeDeck == QuickplayDeck.Pins;
    public bool IsHistoryActive => _activeDeck == QuickplayDeck.History;
    public bool IsNewActive => _activeDeck == QuickplayDeck.New;

    public string SmartDjSeedText
    {
        get => _smartDjSeedText;
        set => SetProperty(ref _smartDjSeedText, value);
    }

    public ICommand SelectDeckCommand { get; }
    public ICommand LaunchSmartDjCommand { get; }
    public ICommand PlayHistoryItemCommand { get; }

    public QuickplayViewModel(
        IPlayerCoordinator playerCoordinator,
        IMediaLibraryService libraryService,
        ISmartDJService smartDJService)
    {
        _playerCoordinator = playerCoordinator;
        _libraryService = libraryService;
        _smartDJService = smartDJService;

        SelectDeckCommand = new RelayCommand<QuickplayDeck>(deck => ActiveDeck = deck);
        LaunchSmartDjCommand = new AsyncRelayCommand(OnLaunchSmartDjAsync);
        PlayHistoryItemCommand = new AsyncRelayCommand<PlayHistoryEntry>(OnPlayHistoryItemAsync);

        _ = LoadInitialDataAsync();
    }

    public async Task LoadInitialDataAsync()
    {
        var historyItems = await _libraryService.GetRecentHistoryAsync(10);
        History.Clear();
        foreach (var item in historyItems)
        {
            History.Add(item);
        }

        var newItems = await _libraryService.GetRecentlyAddedAlbumsAsync(8);
        NewAlbums.Clear();
        foreach (var album in newItems)
        {
            NewAlbums.Add(album);
        }
    }

    private async Task OnLaunchSmartDjAsync()
    {
        var allTracks = await _libraryService.GetAllTracksAsync();
        if (allTracks.Count == 0) return;

        var seed = new SmartDJSeed
        {
            TargetTrackCount = 25,
            ExcludeDisliked = true
        };

        var mix = await _smartDJService.GenerateMixAsync(seed, allTracks);
        if (mix.Count > 0)
        {
            await _playerCoordinator.PlayTrackAsync(mix[0], mix);
        }
    }

    private async Task OnPlayHistoryItemAsync(PlayHistoryEntry? entry)
    {
        if (entry == null) return;
        var allTracks = await _libraryService.GetAllTracksAsync();
        var target = allTracks.FirstOrDefault(t => t.Id == entry.TrackId);
        if (target != null)
        {
            await _playerCoordinator.PlayTrackAsync(target);
        }
    }
}
