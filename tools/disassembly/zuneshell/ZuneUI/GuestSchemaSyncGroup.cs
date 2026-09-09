using Microsoft.Iris;

namespace ZuneUI;

public class GuestSchemaSyncGroup : SchemaSyncGroup
{
	private long _size;

	private bool _active;

	public override long Size => _size;

	public override int Count => 0;

	public override SyncGroupState State => SyncGroupState.Calculated;

	public override bool IsActive
	{
		get
		{
			return _active;
		}
		set
		{
			if (_active != value)
			{
				_active = value;
				((ModelItem)this).FirePropertyChanged("IsActive");
				base.ParentList.GasGauge.HideGuestSpace = !_active;
			}
		}
	}

	public override bool IsVisible => true;

	public GuestSchemaSyncGroup(SyncGroupList list, long size)
		: base(list, SyncCategory.Guest)
	{
		_size = size;
		_active = true;
	}

	public override void CommitChanges()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (!_active)
		{
			base.ParentList.Device.DeleteAllGuestContent();
		}
	}

	public override void CancelChanges()
	{
	}

	public override void DataUpdated()
	{
	}
}
