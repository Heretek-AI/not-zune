using System.ComponentModel;
using Microsoft.Iris;

namespace ZuneUI;

public class SyncCommandBase : Command
{
	private UIDevice _device;

	protected bool _availableWhenSyncing;

	public UIDevice Device
	{
		get
		{
			return _device;
		}
		set
		{
			if (_device != value)
			{
				if (_device != null)
				{
					((ModelItem)_device).PropertyChanged -= OnDevicePropertyChanged;
				}
				_device = value;
				if (_device != null)
				{
					((ModelItem)_device).PropertyChanged += OnDevicePropertyChanged;
				}
				UpdateAvailability();
				((ModelItem)this).FirePropertyChanged("Device");
			}
		}
	}

	private void OnDevicePropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "IsReadyForSync" || args.PropertyName == "IsSyncing")
		{
			UpdateAvailability();
		}
	}

	protected override void OnInvoked()
	{
		((Command)this).Available = false;
		((Command)this).OnInvoked();
	}

	private void UpdateAvailability()
	{
		((Command)this).Available = Device != null && Device.IsReadyForSync && Device.IsSyncing == _availableWhenSyncing;
	}
}
