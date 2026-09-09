using System.Collections;
using System.ComponentModel;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class DeviceExperience : CollectionExperience
{
	private ArrayListDataSet _nodes;

	private Node _status;

	private Node _friends;

	public override IList NodesList
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_nodes == null)
			{
				_nodes = new ArrayListDataSet((IModelItemOwner)(object)this);
				((ListDataSet)_nodes).Add((object)Status);
				((ListDataSet)_nodes).Add((object)base.Music);
				((ListDataSet)_nodes).Add((object)base.Videos);
				((ListDataSet)_nodes).Add((object)base.Photos);
				((ListDataSet)_nodes).Add((object)base.Podcasts);
				UpdateDeviceDependentPivots();
			}
			return (IList)_nodes;
		}
	}

	public Node Status
	{
		get
		{
			if (_status == null)
			{
				_status = new Node(this, StringId.IDS_SYNC_STATUS_PIVOT, "Device\\Status", (SQMDataId)110);
			}
			return _status;
		}
	}

	public Node Friends
	{
		get
		{
			if (_friends == null)
			{
				_friends = new Node(this, StringId.IDS_FRIENDS_PIVOT, "Device\\Friends", (SQMDataId)115);
			}
			return _friends;
		}
	}

	public override string DefaultUIPath => "Device\\Status";

	public DeviceExperience(Frame frameOwner)
		: base(frameOwner, isDevice: true)
	{
		((ModelItem)SyncControls.Instance).PropertyChanged += OnSyncPropertyChanged;
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)SyncControls.Instance).PropertyChanged -= OnSyncPropertyChanged;
		((ModelItem)this).OnDispose(disposing);
	}

	protected override void OnInvoked()
	{
		((MainFrame)base.Frame).ShowDevice(show: true);
		base.OnInvoked();
	}

	protected override void OnIsCurrentChanged()
	{
		UpdateShowDevice();
		base.OnIsCurrentChanged();
	}

	public void UpdateShowDevice()
	{
		((MainFrame)base.Frame).ShowDevice(base.IsCurrent || AreAnyDevicesConnected());
	}

	public bool AreAnyDevicesConnected()
	{
		foreach (UIDevice item in SingletonModelItem<UIDeviceList>.Instance)
		{
			if (item.IsConnectedToPC)
			{
				return true;
			}
		}
		return false;
	}

	private void OnSyncPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "CurrentDevice")
		{
			UpdateDeviceDependentPivots();
			((ModelItem)this).Description = SyncControls.Instance.CurrentDevice.PivotDescription;
		}
	}

	private void UpdateDeviceDependentPivots()
	{
		UIDevice currentDevice = SyncControls.Instance.CurrentDevice;
		if (NodesList.Contains(base.Applications))
		{
			NodesList.Remove(base.Applications);
		}
		if (NodesList.Contains(Friends))
		{
			NodesList.Remove(Friends);
		}
		if (NodesList.Contains(base.Channels))
		{
			NodesList.Remove(base.Channels);
		}
		if (FeatureEnablement.IsFeatureEnabled((Features)5) && currentDevice.SupportsUserCards)
		{
			NodesList.Add(Friends);
		}
		if (FeatureEnablement.IsFeatureEnabled((Features)8) && currentDevice.SupportsChannels)
		{
			NodesList.Add(base.Channels);
		}
		if (FeatureEnablement.IsFeatureEnabled((Features)10) && currentDevice.SupportsSyncApplications)
		{
			NodesList.Add(base.Applications);
		}
	}
}
