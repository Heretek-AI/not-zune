using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.UI.ViewModels;

public class DeviceViewModel : ViewModelBase
{
    private readonly IDeviceSyncService _deviceSyncService;

    public ObservableCollection<ZuneDevice> Devices { get; } = new();

    private ZuneDevice? _selectedDevice;
    public ZuneDevice? SelectedDevice
    {
        get => _selectedDevice;
        set
        {
            if (SetProperty(ref _selectedDevice, value))
            {
                OnPropertyChanged(nameof(HasDevice));
                OnPropertyChanged(nameof(DeviceName));
                OnPropertyChanged(nameof(SerialNumber));
                OnPropertyChanged(nameof(FirmwareVersion));
                OnPropertyChanged(nameof(StorageText));
                OnPropertyChanged(nameof(StorageUsedPercentage));
                OnPropertyChanged(nameof(TotalGb));
                OnPropertyChanged(nameof(FreeGb));
                OnPropertyChanged(nameof(MusicGb));
                OnPropertyChanged(nameof(VideoGb));
                OnPropertyChanged(nameof(PhotoGb));
                OnPropertyChanged(nameof(PodcastGb));
                OnPropertyChanged(nameof(SystemGb));
                OnPropertyChanged(nameof(MusicText));
                OnPropertyChanged(nameof(VideoText));
                OnPropertyChanged(nameof(PhotoText));
                OnPropertyChanged(nameof(PodcastText));
                OnPropertyChanged(nameof(SystemText));
                OnPropertyChanged(nameof(FreeText));
                OnPropertyChanged(nameof(GasGaugeColumns));
            }
        }
    }

    public bool HasDevice => SelectedDevice != null;
    public string DeviceName => SelectedDevice?.ModelName ?? "No Zune Connected";
    public string SerialNumber => SelectedDevice?.SerialNumber ?? "Unknown";
    public string FirmwareVersion => SelectedDevice?.FirmwareVersion ?? "4.8";

    public double TotalGb => (SelectedDevice?.CapacityBytes ?? 0) / (1024.0 * 1024 * 1024);
    public double FreeGb => (SelectedDevice?.FreeSpaceBytes ?? 0) / (1024.0 * 1024 * 1024);
    public double MusicGb => (SelectedDevice?.MusicBytes ?? 0) / (1024.0 * 1024 * 1024);
    public double VideoGb => (SelectedDevice?.VideoBytes ?? 0) / (1024.0 * 1024 * 1024);
    public double PhotoGb => (SelectedDevice?.PhotoBytes ?? 0) / (1024.0 * 1024 * 1024);
    public double PodcastGb => (SelectedDevice?.PodcastBytes ?? 0) / (1024.0 * 1024 * 1024);
    public double SystemGb => (SelectedDevice?.SystemBytes ?? 0) / (1024.0 * 1024 * 1024);

    public string MusicText => $"MUSIC: {MusicGb:F1} GB";
    public string VideoText => $"VIDEO: {VideoGb:F1} GB";
    public string PhotoText => $"PICTURES: {PhotoGb:F1} GB";
    public string PodcastText => $"PODCASTS: {PodcastGb:F1} GB";
    public string SystemText => $"SYSTEM: {SystemGb:F1} GB";
    public string FreeText => $"FREE: {FreeGb:F1} GB";

    public string GasGaugeColumns
    {
        get
        {
            double m = Math.Max(0.001, (double)(SelectedDevice?.MusicBytes ?? 0));
            double v = Math.Max(0.001, (double)(SelectedDevice?.VideoBytes ?? 0));
            double p = Math.Max(0.001, (double)(SelectedDevice?.PhotoBytes ?? 0));
            double pod = Math.Max(0.001, (double)(SelectedDevice?.PodcastBytes ?? 0));
            double s = Math.Max(0.001, (double)(SelectedDevice?.SystemBytes ?? 0));
            double f = Math.Max(0.001, (double)(SelectedDevice?.FreeSpaceBytes ?? 1));
            return FormattableString.Invariant($"{m:F3}*,{v:F3}*,{p:F3}*,{pod:F3}*,{s:F3}*,{f:F3}*");
        }
    }

    public string StorageText
    {
        get
        {
            if (SelectedDevice == null) return "Connect a Zune device via USB cable.";
            return $"{FreeGb:F1} GB free of {TotalGb:F1} GB";
        }
    }

    public double StorageUsedPercentage
    {
        get
        {
            if (SelectedDevice == null || SelectedDevice.CapacityBytes == 0) return 0.0;
            return (double)(SelectedDevice.CapacityBytes - SelectedDevice.FreeSpaceBytes) / SelectedDevice.CapacityBytes;
        }
    }

    private bool _isSyncing;
    public bool IsSyncing
    {
        get => _isSyncing;
        set => SetProperty(ref _isSyncing, value);
    }

    private double _syncProgress;
    public double SyncProgress
    {
        get => _syncProgress;
        set => SetProperty(ref _syncProgress, value);
    }

    public ICommand SyncCommand { get; }

    public DeviceViewModel(IDeviceSyncService deviceSyncService)
    {
        _deviceSyncService = deviceSyncService;

        _deviceSyncService.DeviceConnected += OnDeviceConnected;
        _deviceSyncService.DeviceDisconnected += OnDeviceDisconnected;

        SyncCommand = new AsyncRelayCommand(OnSyncAsync);

        RefreshDevices();
    }

    private void RefreshDevices()
    {
        Devices.Clear();
        foreach (var dev in _deviceSyncService.ConnectedDevices)
        {
            Devices.Add(dev);
        }
        SelectedDevice = Devices.FirstOrDefault();
    }

    private void OnDeviceConnected(object? sender, ZuneDevice dev)
    {
        Devices.Add(dev);
        if (SelectedDevice == null) SelectedDevice = dev;
    }

    private void OnDeviceDisconnected(object? sender, string serial)
    {
        var existing = Devices.FirstOrDefault(d => d.SerialNumber == serial);
        if (existing != null) Devices.Remove(existing);
        if (SelectedDevice?.SerialNumber == serial) SelectedDevice = Devices.FirstOrDefault();
    }

    private async Task OnSyncAsync()
    {
        if (SelectedDevice == null) return;
        try
        {
            IsSyncing = true;
            var progressReporter = new Progress<double>(p => SyncProgress = p);
            await _deviceSyncService.SyncDeviceAsync(SelectedDevice.SerialNumber, progressReporter);
            SyncProgress = 1.0;
        }
        finally
        {
            IsSyncing = false;
        }
    }
}
