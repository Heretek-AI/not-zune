using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public class SyncGroupList : ModelItem
{
	private class SyncRuleDetailsHasher : IEqualityComparer<SyncRuleDetails>
	{
		public bool Equals(SyncRuleDetails x, SyncRuleDetails y)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			if (x.mediaId == y.mediaId && x.mediaType == y.mediaType)
			{
				return x.syncCategory == y.syncCategory;
			}
			return false;
		}

		public int GetHashCode(SyncRuleDetails obj)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected I4, but got Unknown
			return obj.mediaId ^ (obj.mediaType << 24) ^ (obj.syncCategory << 16);
		}
	}

	private UIDevice _device;

	private SyncRulesView _snapshot;

	private bool _inOverfill;

	private bool _inManagement;

	private UIGasGauge _gauge;

	private Dictionary<SyncRuleDetails, SyncGroup> _existingRulesList;

	private Dictionary<int, SyncGroup> _complexRulesList;

	private ProxySettingDelegate _commitDelegate;

	private bool _committed;

	private SchemaSyncGroup _music;

	private SchemaSyncGroup _video;

	private SchemaSyncGroup _photo;

	private SchemaSyncGroup _podcast;

	private SchemaSyncGroup _friend;

	private SchemaSyncGroup _channel;

	private SchemaSyncGroup _application;

	private SchemaSyncGroup _audiobook;

	private SchemaSyncGroup _guest;

	private List<SchemaSyncGroup> _schemas;

	public UIDevice Device => _device;

	public bool InOverfill => _inOverfill;

	public List<SchemaSyncGroup> List
	{
		get
		{
			return _schemas;
		}
		private set
		{
			if (_schemas != value)
			{
				_schemas = value;
				((ModelItem)this).FirePropertyChanged("List");
			}
		}
	}

	public UIGasGauge GasGauge => _gauge;

	public SyncGroupList(IModelItemOwner parent, UIDevice device, SyncRulesView snapshot, bool inOverfill)
		: base(parent)
	{
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Expected O, but got Unknown
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Expected O, but got Unknown
		_device = device;
		_snapshot = snapshot;
		_inOverfill = inOverfill;
		_inManagement = parent is DeviceManagement;
		_commitDelegate = CommitChanges;
		if (_snapshot != null)
		{
			_gauge = new UIGasGauge((IModelItemOwner)(object)this, _snapshot.PredictedGasGauge);
		}
		else
		{
			_gauge = new UIGasGauge((IModelItemOwner)(object)this, null);
		}
		_existingRulesList = new Dictionary<SyncRuleDetails, SyncGroup>(new SyncRuleDetailsHasher());
		_complexRulesList = new Dictionary<int, SyncGroup>();
		_schemas = new List<SchemaSyncGroup>(8);
		_music = new DetailsBackedSchemaSyncGroup(this, _snapshot, SyncCategory.Music, _inOverfill);
		_schemas.Add(_music);
		_video = new DetailsBackedSchemaSyncGroup(this, _snapshot, SyncCategory.Video, _inOverfill);
		_schemas.Add(_video);
		_photo = new DetailsBackedSchemaSyncGroup(this, _snapshot, SyncCategory.Photo, _inOverfill);
		_schemas.Add(_photo);
		_podcast = new DetailsBackedSchemaSyncGroup(this, _snapshot, SyncCategory.Podcast, _inOverfill);
		_schemas.Add(_podcast);
		if (Device.UserId != 0)
		{
			if (FeatureEnablement.IsFeatureEnabled((Features)5) && device.SupportsUserCards)
			{
				_friend = new DetailsBackedSchemaSyncGroup(this, _snapshot, SyncCategory.Friend, _inOverfill);
				_schemas.Add(_friend);
			}
			if (FeatureEnablement.IsFeatureEnabled((Features)8) && device.SupportsChannels)
			{
				_channel = new DetailsBackedSchemaSyncGroup(this, _snapshot, SyncCategory.Channel, _inOverfill);
				_schemas.Add(_channel);
			}
		}
		if (FeatureEnablement.IsFeatureEnabled((Features)10) && Device.SupportsSyncApplications)
		{
			_application = new DetailsBackedSchemaSyncGroup(this, _snapshot, SyncCategory.Application, _inOverfill);
			_schemas.Add(_application);
		}
		if (InOverfill)
		{
			_guest = new GuestSchemaSyncGroup(this, GasGauge.GuestSpace);
			_audiobook = new DetailsBackedSchemaSyncGroup(this, _snapshot, SyncCategory.Audiobook, _inOverfill);
			_schemas.Add(_audiobook);
		}
		if (_snapshot == null)
		{
			return;
		}
		_snapshot.ItemAddedEvent += new SyncRulesViewItemAddedHandler(ItemAdded);
		_snapshot.ItemUpdatedEvent += new SyncRulesViewItemUpdatedHandler(ItemUpdated);
		if (_snapshot.Count > 0)
		{
			for (int i = 0; i < _snapshot.Count; i++)
			{
				AddExistingSyncGroup(i);
			}
		}
	}

	protected override void OnDispose(bool disposing)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		if (disposing)
		{
			if (_snapshot != null)
			{
				_snapshot.ItemAddedEvent -= new SyncRulesViewItemAddedHandler(ItemAdded);
				_snapshot.ItemUpdatedEvent -= new SyncRulesViewItemUpdatedHandler(ItemUpdated);
				_snapshot.Dispose();
			}
			if (!_committed)
			{
				if (_inManagement)
				{
					ZuneShell.DefaultInstance.Management.CommitList.Remove(_commitDelegate);
				}
				CancelChanges();
			}
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public SchemaSyncGroup GetGroupForSchema(SyncCategory schema)
	{
		return schema switch
		{
			SyncCategory.Music => _music, 
			SyncCategory.Video => _video, 
			SyncCategory.Photo => _photo, 
			SyncCategory.Podcast => _podcast, 
			SyncCategory.Friend => _friend, 
			SyncCategory.Channel => _channel, 
			SyncCategory.Application => _application, 
			SyncCategory.Audiobook => _audiobook, 
			SyncCategory.Guest => _guest, 
			_ => null, 
		};
	}

	public void AddNewComplexSyncGroup(int playlistID)
	{
		NewComplexSyncGroup newComplexSyncGroup = new NewComplexSyncGroup(this, playlistID);
		AddSyncGroup(newComplexSyncGroup);
		if (GetGroupForSchema(newComplexSyncGroup.Type) is DetailsBackedSchemaSyncGroup detailsBackedSchemaSyncGroup)
		{
			detailsBackedSchemaSyncGroup.IsExpanded = true;
		}
		if (_inManagement)
		{
			ZuneShell.DefaultInstance.Management.CommitList[_commitDelegate] = _device.ID;
		}
	}

	public void DeleteGroup(SyncGroup group)
	{
		DeleteGroups(new SyncGroup[1] { group });
	}

	public void DeleteGroups(IList groupList)
	{
		if (groupList == null || groupList.Count < 1)
		{
			return;
		}
		if (groupList.Count == 1)
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_REMOVE_SYNC_GROUP_DIALOG_TITLE), string.Format(Shell.LoadString(StringId.IDS_REMOVE_SINGLE_SYNC_GROUP_DIALOG_TEXT), ((SyncGroup)groupList[0]).Title), (EventHandler)delegate
			{
				DeleteGroupsConfirmed(groupList);
			});
		}
		else if (groupList.Count > 1)
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_REMOVE_SYNC_GROUP_DIALOG_TITLE), Shell.LoadString(StringId.IDS_REMOVE_MULTIPLE_SYNC_GROUP_DIALOG_TEXT), (EventHandler)delegate
			{
				DeleteGroupsConfirmed(groupList);
			});
		}
	}

	private void DeleteGroupsConfirmed(IList groupList)
	{
		if (groupList == null)
		{
			return;
		}
		List<SyncCategory> list = new List<SyncCategory>();
		foreach (object group in groupList)
		{
			if (group is SyncGroup syncGroup)
			{
				syncGroup.IsActive = false;
				if (!list.Contains(syncGroup.Type))
				{
					list.Add(syncGroup.Type);
				}
			}
		}
		if (_inManagement)
		{
			ZuneShell.DefaultInstance.Management.CommitList[_commitDelegate] = _device.ID;
		}
		foreach (SyncCategory item in list)
		{
			if (GetGroupForSchema(item) is DetailsBackedSchemaSyncGroup detailsBackedSchemaSyncGroup)
			{
				detailsBackedSchemaSyncGroup.Sort(immediately: true);
			}
		}
	}

	public void ComplexGroupEdited(int playlistID)
	{
		if (_complexRulesList.ContainsKey(playlistID))
		{
			SyncGroup syncGroup = _complexRulesList[playlistID];
			syncGroup.DataEdited();
			if (GetGroupForSchema(syncGroup.Type) is DetailsBackedSchemaSyncGroup detailsBackedSchemaSyncGroup)
			{
				detailsBackedSchemaSyncGroup.Sort(immediately: true);
			}
		}
		if (_inManagement)
		{
			ZuneShell.DefaultInstance.Management.CommitList[_commitDelegate] = _device.ID;
		}
	}

	public void CommitChanges(object throwaway)
	{
		if (((ModelItem)this).IsDisposed)
		{
			return;
		}
		foreach (SchemaSyncGroup schema in _schemas)
		{
			schema.CommitChanges();
		}
		_committed = true;
	}

	public void CancelChanges()
	{
		foreach (SchemaSyncGroup schema in _schemas)
		{
			schema.CancelChanges();
		}
	}

	private void ItemUpdated(SyncRulesView syncRulesView, int iItem)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed)
			{
				SyncRuleDetails item = syncRulesView.GetItem(iItem);
				if (_existingRulesList.ContainsKey(item))
				{
					SyncGroup syncGroup = _existingRulesList[item];
					syncGroup.DataUpdated();
					if (!item.allMedia && GetGroupForSchema(syncGroup.Type) is DetailsBackedSchemaSyncGroup detailsBackedSchemaSyncGroup)
					{
						detailsBackedSchemaSyncGroup.Sort(immediately: false);
					}
				}
			}
		}, (object)null);
	}

	private void ItemAdded(SyncRulesView syncRulesView, int iItem)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		Application.DeferredInvoke((DeferredInvokeHandler)delegate
		{
			if (!((ModelItem)this).IsDisposed && !_existingRulesList.ContainsKey(syncRulesView.GetItem(iItem)))
			{
				AddExistingSyncGroup(iItem);
			}
		}, (object)null);
	}

	private void AddExistingSyncGroup(int indexInRulesSnapshot)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected I4, but got Unknown
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Expected I4, but got Unknown
		SyncRuleDetails item = _snapshot.GetItem(indexInRulesSnapshot);
		if (_existingRulesList.ContainsKey(item))
		{
			return;
		}
		if (item.allMedia)
		{
			if (GetGroupForSchema((SyncCategory)item.syncCategory) is DetailsBackedSchemaSyncGroup detailsBackedSchemaSyncGroup)
			{
				_existingRulesList.Add(item, detailsBackedSchemaSyncGroup);
				detailsBackedSchemaSyncGroup.AssignDetails(indexInRulesSnapshot);
			}
			return;
		}
		ExistingSyncGroup existingSyncGroup = null;
		SyncCategory syncCategory = ((!item.complex) ? ((SyncCategory)item.syncCategory) : UIDeviceList.MapMediaTypeToSyncCategory(PlaylistManager.GetAutoPlaylistSchema(item.mediaId)));
		bool flag = Device.IsSyncAllFor(syncCategory) || Device.IsManualFor(syncCategory);
		if (InOverfill || !flag)
		{
			existingSyncGroup = new ExistingSyncGroup(this, _snapshot, indexInRulesSnapshot, flag);
			_existingRulesList.Add(item, existingSyncGroup);
			AddSyncGroup(existingSyncGroup);
		}
	}

	private void AddSyncGroup(SyncGroup group)
	{
		if (group.IsComplex)
		{
			_complexRulesList.Add(group.ID, group);
		}
		if (GetGroupForSchema(group.Type) is DetailsBackedSchemaSyncGroup detailsBackedSchemaSyncGroup)
		{
			detailsBackedSchemaSyncGroup.Add(group);
		}
	}
}
