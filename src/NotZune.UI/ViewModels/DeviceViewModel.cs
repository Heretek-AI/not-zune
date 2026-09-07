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
                OnPropertyChanged(nameof(StorageText));
                OnPropertyChanged(nameof(StorageUsedPercentage));
            }
        }
    }

    public bool HasDevice => SelectedDevice != null;
    public string DeviceName => SelectedDevice?.ModelName ?? "No Zune Connected";

    public string StorageText
    {
        get
        {
            if (SelectedDevice == null) return "Connect a Zune device via USB cable.";
            double usedGb = (SelectedDevice.CapacityBytes - SelectedDevice.FreeSpaceBytes) / (1024.0 * 1024 * 1024);
            double totalGb = SelectedDevice.CapacityBytes / (1024.0 * 1024 * 1024);
            return $"{usedGb:F1} GB free of {totalGb:F1} GB";
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
        var progressReporter = new Progress<double>(p => SyncProgress = p);
        await _deviceSyncService.SyncDeviceAsync(SelectedDevice.SerialNumber, progressReporter);
    }
}
