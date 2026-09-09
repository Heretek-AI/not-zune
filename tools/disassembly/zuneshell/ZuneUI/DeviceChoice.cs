using System.Collections;
using System.ComponentModel;
using Microsoft.Iris;

namespace ZuneUI;

public class DeviceChoice : Choice
{
	public DeviceChoice()
		: this(null)
	{
	}

	public DeviceChoice(IModelItemOwner owner)
		: base(owner)
	{
		UIDeviceList instance = SingletonModelItem<UIDeviceList>.Instance;
		instance.DeviceAddedEvent += ListChanged;
		instance.DeviceRemovedEvent += ListChanged;
		((ModelItem)SyncControls.Instance).PropertyChanged += OnSyncPropertyChanged;
		BuildOptions();
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			((ModelItem)SyncControls.Instance).PropertyChanged -= OnSyncPropertyChanged;
			UIDeviceList instance = SingletonModelItem<UIDeviceList>.Instance;
			instance.DeviceAddedEvent -= ListChanged;
			instance.DeviceRemovedEvent -= ListChanged;
		}
		((Choice)this).OnDispose(disposing);
	}

	private void ListChanged(object sender, DeviceListEventArgs args)
	{
		BuildOptions();
	}

	private void OnSyncPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (!(args.PropertyName == "CurrentDevice"))
		{
			return;
		}
		for (int i = 0; i < ((Choice)this).Options.Count; i++)
		{
			if ((UIDevice)((Choice)this).Options[i] == SyncControls.Instance.CurrentDevice)
			{
				((Choice)this).ChosenIndex = i;
				break;
			}
		}
	}

	private void BuildOptions()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		UIDeviceList instance = SingletonModelItem<UIDeviceList>.Instance;
		((Choice)this).Options = (IList)new ArrayListDataSet((IModelItemOwner)(object)this);
		foreach (UIDevice item in instance)
		{
			((Choice)this).Options.Add(item);
			if (item == SyncControls.Instance.CurrentDevice)
			{
				((Choice)this).ChosenValue = item;
			}
		}
	}

	protected override void OnChosenChanged()
	{
		((Choice)this).OnChosenChanged();
		if (((Choice)this).ChosenValue != null)
		{
			SyncControls.Instance.SetCurrentDevice((UIDevice)((Choice)this).ChosenValue);
		}
	}
}
