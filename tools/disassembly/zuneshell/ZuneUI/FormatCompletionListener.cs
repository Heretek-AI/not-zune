using System.ComponentModel;
using Microsoft.Iris;

namespace ZuneUI;

public class FormatCompletionListener : ModelItem
{
	private Command _completed;

	private UIDevice _device;

	public bool IsFormatting => _device.IsFormatting;

	public Command Completed => _completed;

	public FormatCompletionListener()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		_completed = new Command((IModelItemOwner)(object)this);
		((ModelItem)SyncControls.Instance).PropertyChanged += OnDeviceChanged;
		_device = SyncControls.Instance.CurrentDevice;
		SetCurrentDevice();
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)this).OnDispose(disposing);
		if (disposing && _device.IsValid)
		{
			_device.FormatCompletedEvent -= FormatCompleted;
		}
	}

	private void SetCurrentDevice()
	{
		if (_device.IsValid)
		{
			_device.FormatCompletedEvent -= FormatCompleted;
		}
		_device = SyncControls.Instance.CurrentDevice;
		if (_device.IsValid)
		{
			_device.FormatCompletedEvent += FormatCompleted;
		}
	}

	private void FormatCompleted(object sender, FallibleEventArgs args)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (_device.IsConnectedToClient)
		{
			_device.EnumeratedEvent -= EnumerationCompleted;
			_device.EnumeratedEvent += EnumerationCompleted;
			_device.Enumerate();
		}
		else
		{
			FinishFormat(HRESULT._S_OK);
		}
	}

	private void EnumerationCompleted(object sender, FallibleEventArgs args)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		FinishFormat(args.HR);
	}

	private void FinishFormat(HRESULT hResult)
	{
		_device.EnumeratedEvent -= EnumerationCompleted;
		Completed.Invoke();
	}

	private void OnDeviceChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "CurrentDevice")
		{
			SetCurrentDevice();
		}
	}
}
