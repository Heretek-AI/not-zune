using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class UIGasGauge : ModelItem
{
	private GasGauge _gauge;

	private int _dividerCount;

	private bool _hideGuestSpace;

	public long TotalSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return (long)_gauge.Capacity;
		}
	}

	public long MusicSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)0);
		}
	}

	public long VideoSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)1);
		}
	}

	public long PhotoSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)2);
		}
	}

	public long PodcastSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)3);
		}
	}

	public long FriendSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)4);
		}
	}

	public long ChannelSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)6);
		}
	}

	public long AudiobookSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)5);
		}
	}

	public long ApplicationSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)7);
		}
	}

	public long GuestSpace
	{
		get
		{
			if (_gauge == null || _hideGuestSpace)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)8);
		}
	}

	public long InboxSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.GetSpaceUsedByCategory((ESyncCategory)9);
		}
	}

	public long ReservedSpace
	{
		get
		{
			if (_gauge == null)
			{
				return 0L;
			}
			return _gauge.SpaceReserved;
		}
	}

	public long OtherSpace
	{
		get
		{
			long result = 0L;
			if (_gauge != null)
			{
				long num = ReservedSpace - AudiobookSpace;
				long num2 = ((num > InboxSpace) ? num : InboxSpace);
				result = num2 + GuestSpace;
			}
			return result;
		}
	}

	public long FreeSpace
	{
		get
		{
			long num = 0L;
			if (_gauge != null)
			{
				num = _gauge.SpaceAvailable;
				if (_hideGuestSpace)
				{
					num += _gauge.GetSpaceUsedByCategory((ESyncCategory)8);
				}
			}
			return num;
		}
	}

	public long UsedSpace => TotalSpace - FreeSpace;

	public int DividerCount => _dividerCount;

	public bool HideGuestSpace
	{
		get
		{
			return _hideGuestSpace;
		}
		set
		{
			if (_hideGuestSpace != value)
			{
				_hideGuestSpace = value;
				((ModelItem)this).FirePropertyChanged("HideGuestSpace");
				((ModelItem)this).FirePropertyChanged("GuestSpace");
				((ModelItem)this).FirePropertyChanged("OtherSpace");
				((ModelItem)this).FirePropertyChanged("UsedSpace");
				((ModelItem)this).FirePropertyChanged("FreeSpace");
			}
		}
	}

	public UIGasGauge(IModelItemOwner owner, GasGauge gauge)
		: base(owner)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		_gauge = gauge;
		if (_gauge != null)
		{
			_gauge.CategorySpaceUsedUpdatedEvent += new CategorySpaceUsedUpdatedHandler(OnCategorySpaceUpdated);
			_gauge.ReservedSpaceUpdatedEvent += new ReservedSpaceUpdatedHandler(OnReservedSpaceUpdated);
		}
		UIDevice uIDevice = owner as UIDevice;
		if (uIDevice == null && owner is SyncGroupList syncGroupList)
		{
			uIDevice = syncGroupList.Device;
		}
		if (uIDevice != null)
		{
			int num;
			for (num = uIDevice.AdvertisedCapacity; num > 8; num = ((num % 10 != 0) ? ((num % 5 != 0) ? ((num % 3 != 0) ? (num / 2) : (num / 3)) : (num / 5)) : (num / 10)))
			{
			}
			_dividerCount = num;
		}
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		if (disposing && _gauge != null)
		{
			_gauge.CategorySpaceUsedUpdatedEvent -= new CategorySpaceUsedUpdatedHandler(OnCategorySpaceUpdated);
			_gauge.ReservedSpaceUpdatedEvent -= new ReservedSpaceUpdatedHandler(OnReservedSpaceUpdated);
		}
		((ModelItem)this).OnDispose(disposing);
	}

	private void OnCategorySpaceUpdated(GasGauge gasGauge, ESyncCategory category, long llNewSchemaSpace, long llNewFreeSpace)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0071: Expected I4, but got Unknown
			if (!((ModelItem)this).IsDisposed)
			{
				((ModelItem)this).FirePropertyChanged("UsedSpace");
				((ModelItem)this).FirePropertyChanged("FreeSpace");
				((ModelItem)this).FirePropertyChanged("OtherSpace");
				ESyncCategory val = category;
				switch ((int)val)
				{
				case 0:
					((ModelItem)this).FirePropertyChanged("MusicSpace");
					break;
				case 1:
					((ModelItem)this).FirePropertyChanged("VideoSpace");
					break;
				case 2:
					((ModelItem)this).FirePropertyChanged("PhotoSpace");
					break;
				case 3:
					((ModelItem)this).FirePropertyChanged("PodcastSpace");
					break;
				case 4:
					((ModelItem)this).FirePropertyChanged("FriendSpace");
					break;
				case 6:
					((ModelItem)this).FirePropertyChanged("ChannelSpace");
					break;
				case 7:
					((ModelItem)this).FirePropertyChanged("ApplicationSpace");
					break;
				case 5:
					((ModelItem)this).FirePropertyChanged("AudiobookSpace");
					break;
				case 8:
					((ModelItem)this).FirePropertyChanged("GuestSpace");
					break;
				}
			}
		}, (object)null);
	}

	private void OnReservedSpaceUpdated(GasGauge gasGauge, long llNewReservedSpace, long llNewFreeSpace)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed)
			{
				((ModelItem)this).FirePropertyChanged("ReservedSpace");
				((ModelItem)this).FirePropertyChanged("UsedSpace");
				((ModelItem)this).FirePropertyChanged("FreeSpace");
				((ModelItem)this).FirePropertyChanged("OtherSpace");
			}
		}, (object)null);
	}
}
