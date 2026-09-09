using System;
using System.Collections.Generic;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class DetailsBackedSchemaSyncGroup : SchemaSyncGroup
{
	private SyncRulesView _view;

	private int _index;

	private SyncRuleDetails _model;

	private bool _inOverfill;

	private List<SyncGroup> _masterList;

	private List<SyncGroup> _sortedList;

	private Timer _resortTimer;

	private bool _active;

	private bool _updatingActive;

	public override long Size
	{
		get
		{
			if (State != SyncGroupState.Calculated)
			{
				return -1L;
			}
			return _model.totalSize;
		}
	}

	public override int Count
	{
		get
		{
			if (State != SyncGroupState.Calculated)
			{
				return -1;
			}
			return (int)_model.totalItems;
		}
	}

	public override SyncGroupState State
	{
		get
		{
			if (_model == null || !_model.calculated)
			{
				return SyncGroupState.Uncalculated;
			}
			return SyncGroupState.Calculated;
		}
	}

	public override bool IsActive
	{
		get
		{
			return _active;
		}
		set
		{
			if (_active == value)
			{
				return;
			}
			_active = value;
			_updatingActive = true;
			foreach (SyncGroup master in _masterList)
			{
				if (master.IsVisible)
				{
					master.IsActive = _active;
				}
			}
			_updatingActive = false;
			Sort(immediately: true);
			((ModelItem)this).FirePropertyChanged("IsActive");
		}
	}

	public override bool IsVisible => true;

	public List<SyncGroup> List
	{
		get
		{
			return _sortedList;
		}
		private set
		{
			if (_sortedList != value)
			{
				_sortedList = value;
				((ModelItem)this).FirePropertyChanged("List");
			}
		}
	}

	public DetailsBackedSchemaSyncGroup(SyncGroupList list, SyncRulesView view, SyncCategory type, bool inOverfill)
		: base(list, type)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		_view = view;
		_index = -1;
		_inOverfill = inOverfill;
		_resortTimer = new Timer((IModelItemOwner)(object)this);
		_resortTimer.AutoRepeat = false;
		_resortTimer.Tick += OnResortTimerTick;
		_resortTimer.Interval = 527;
		_masterList = new List<SyncGroup>();
		_sortedList = new List<SyncGroup>();
		_active = true;
	}

	public override void CommitChanges()
	{
		foreach (SyncGroup master in _masterList)
		{
			master.CommitChanges();
		}
	}

	public override void CancelChanges()
	{
		foreach (SyncGroup master in _masterList)
		{
			master.CancelChanges();
		}
	}

	public override void DataUpdated()
	{
		if (_index != -1)
		{
			_model = _view.GetItem(_index);
			((ModelItem)this).FirePropertyChanged("Size");
			((ModelItem)this).FirePropertyChanged("Count");
			((ModelItem)this).FirePropertyChanged("State");
			((ModelItem)this).FirePropertyChanged("IsActive");
		}
	}

	public void AssignDetails(int index)
	{
		_index = index;
		DataUpdated();
	}

	public void Add(SyncGroup group)
	{
		_masterList.Add(group);
		_sortedList.Add(group);
		Sort(immediately: false);
	}

	public void Sort(bool immediately)
	{
		if (!immediately)
		{
			if (!_resortTimer.Enabled)
			{
				_resortTimer.Start();
			}
			return;
		}
		_resortTimer.Stop();
		List<SyncGroup> list = new List<SyncGroup>(_masterList.Count);
		foreach (SyncGroup master in _masterList)
		{
			if ((master.IsActive || _inOverfill) && master.IsVisible)
			{
				list.Add(master);
			}
		}
		list.Sort();
		List = list;
		if (List.Count == 0)
		{
			base.IsExpanded = false;
		}
	}

	public void UpdateActiveState()
	{
		if (_updatingActive)
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		foreach (SyncGroup master in _masterList)
		{
			if (master.IsVisible)
			{
				flag2 = true;
				if (master.IsActive)
				{
					flag = true;
					break;
				}
			}
		}
		_active = flag || !flag2;
		((ModelItem)this).FirePropertyChanged("IsActive");
	}

	private void OnResortTimerTick(object sender, EventArgs e)
	{
		Sort(immediately: true);
	}
}
