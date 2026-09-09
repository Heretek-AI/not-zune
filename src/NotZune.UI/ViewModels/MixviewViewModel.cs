using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Interfaces;
using NotZune.Domain.Models;

namespace NotZune.UI.ViewModels;

public class MixviewViewModel : ViewModelBase
{
    private readonly IMixviewService _mixviewService;
    private readonly IPlayerCoordinator _playerCoordinator;
    private readonly ISmartDJService _smartDJService;
    private readonly IMediaLibraryService _libraryService;

    private readonly Stack<MixStackEntry> _mixStack = new();

    private MixNode _centerSeed = new() { Title = "Zune Mixview", Subtitle = "CONSTELLATION" };
    public MixNode CenterSeed
    {
        get => _centerSeed;
        set => SetProperty(ref _centerSeed, value);
    }

    public ObservableCollection<MixNode> Satellites { get; } = new();

    private MixNode? _hoveredNode;
    public MixNode? HoveredNode
    {
        get => _hoveredNode;
        set
        {
            if (SetProperty(ref _hoveredNode, value))
            {
                OnPropertyChanged(nameof(HasHoveredNode));
                OnPropertyChanged(nameof(HoveredTitle));
                OnPropertyChanged(nameof(HoveredSubtitle));
            }
        }
    }

    public bool HasHoveredNode => HoveredNode != null;
    public string HoveredTitle => HoveredNode?.Title ?? CenterSeed.Title;
    public string HoveredSubtitle => HoveredNode?.Subtitle ?? CenterSeed.Subtitle;

    public bool CanGoBack => _mixStack.Count > 0;
    public string HistoryBreadcrumbText => _mixStack.Count > 0 
        ? $"← BACK TO {_mixStack.Peek().SeedNode.Title.ToUpperInvariant()}" 
        : string.Empty;

    public ICommand SelectNodeCommand { get; }
    public ICommand GoBackCommand { get; }
    public ICommand PlayNodeCommand { get; }
    public ICommand SmartDJNodeCommand { get; }
    public ICommand SetHoveredNodeCommand { get; }

    public MixviewViewModel(
        IMixviewService mixviewService,
        IPlayerCoordinator playerCoordinator,
        ISmartDJService smartDJService,
        IMediaLibraryService libraryService)
    {
        _mixviewService = mixviewService;
        _playerCoordinator = playerCoordinator;
        _smartDJService = smartDJService;
        _libraryService = libraryService;

        SelectNodeCommand = new AsyncRelayCommand<MixNode>(OnSelectNodeAsync);
        GoBackCommand = new AsyncRelayCommand(OnGoBackAsync);
        PlayNodeCommand = new AsyncRelayCommand<MixNode>(OnPlayNodeAsync);
        SmartDJNodeCommand = new AsyncRelayCommand<MixNode>(OnSmartDJNodeAsync);
        SetHoveredNodeCommand = new RelayCommand<MixNode>(node => HoveredNode = node);
    }

    public async Task InitializeSeedAsync(string seedName, MixNodeType seedType = MixNodeType.Artist, Guid? seedId = null)
    {
        _mixStack.Clear();
        OnPropertyChanged(nameof(CanGoBack));
        OnPropertyChanged(nameof(HistoryBreadcrumbText));

        await LoadConstellationAsync(seedName, seedType, seedId);
    }

    private async Task OnSelectNodeAsync(MixNode? node)
    {
        if (node == null || node.IsCenterSeed) return;

        // Push current view onto MixStack
        _mixStack.Push(new MixStackEntry(CenterSeed, Satellites));
        OnPropertyChanged(nameof(CanGoBack));
        OnPropertyChanged(nameof(HistoryBreadcrumbText));

        await LoadConstellationAsync(node.Title, node.NodeType, node.EntityId);
    }

    private Task OnGoBackAsync()
    {
        if (_mixStack.Count == 0) return Task.CompletedTask;

        var previous = _mixStack.Pop();
        CenterSeed = previous.SeedNode;
        Satellites.Clear();
        foreach (var s in previous.Satellites)
        {
            Satellites.Add(s);
        }

        OnPropertyChanged(nameof(CanGoBack));
        OnPropertyChanged(nameof(HistoryBreadcrumbText));
        return Task.CompletedTask;
    }

    private async Task LoadConstellationAsync(string seedName, MixNodeType seedType, Guid? seedId)
    {
        var constellation = await _mixviewService.GenerateConstellationAsync(seedName, seedType, seedId);
        CenterSeed = constellation.CenterSeed;

        Satellites.Clear();
        foreach (var sat in constellation.Satellites)
        {
            Satellites.Add(sat);
        }

        HoveredNode = null;
    }

    private async Task OnPlayNodeAsync(MixNode? node)
    {
        var target = node ?? CenterSeed;
        var allTracks = await _libraryService.GetAllTracksAsync();

        if (target.NodeType == MixNodeType.Track && target.EntityId.HasValue)
        {
            var trk = allTracks.FirstOrDefault(t => t.Id == target.EntityId.Value);
            if (trk != null)
            {
                await _playerCoordinator.PlayTrackAsync(trk, allTracks);
                return;
            }
        }

        if (target.NodeType == MixNodeType.Album && target.EntityId.HasValue)
        {
            var albumTracks = allTracks.Where(t => t.AlbumId == target.EntityId.Value).ToList();
            if (albumTracks.Count > 0)
            {
                await _playerCoordinator.PlayTrackAsync(albumTracks[0], albumTracks);
                return;
            }
        }

        // Default or Artist: generate Smart DJ mix
        await OnSmartDJNodeAsync(target);
    }

    private async Task OnSmartDJNodeAsync(MixNode? node)
    {
        var target = node ?? CenterSeed;
        var allTracks = await _libraryService.GetAllTracksAsync();

        var seed = new SmartDJSeed
        {
            SeedArtistId = target.NodeType == MixNodeType.Artist ? target.EntityId : null,
            SeedAlbumId = target.NodeType == MixNodeType.Album ? target.EntityId : null,
            SeedGenre = target.Title
        };

        var mix = await _smartDJService.GenerateMixAsync(seed, allTracks);
        if (mix.Count > 0)
        {
            await _playerCoordinator.PlayTrackAsync(mix[0], mix);
        }
    }
}
