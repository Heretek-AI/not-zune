using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneUI;

public class NewComplexSyncGroup : SyncGroup
{
	private string _title;

	private int _id;

	private bool _isActive;

	private SyncCategory _type;

	public override string Title => _title;

	public override int ID => _id;

	public override SyncCategory Type => _type;

	public override long Size => -1L;

	public override int Count => -1;

	public override SyncGroupState State => SyncGroupState.Pending;

	public override bool IsActive
	{
		get
		{
			return _isActive;
		}
		set
		{
			if (_isActive != value)
			{
				_isActive = value;
				((ModelItem)this).FirePropertyChanged("IsActive");
			}
		}
	}

	public override bool IsVisible => true;

	public override bool IsComplex => true;

	public NewComplexSyncGroup(SyncGroupList parentList, int id)
		: base(parentList)
	{
		_id = id;
		_isActive = true;
		UpdateInfo();
	}

	public override void CommitChanges()
	{
		if (IsActive)
		{
			MediaIdAndType[] items = new MediaIdAndType[1]
			{
				new MediaIdAndType(ID, MediaType.Playlist)
			};
			base.ParentList.Device.AddSyncRule(items);
		}
		else
		{
			CancelChanges();
		}
	}

	public override void CancelChanges()
	{
		List<MediaIdAndType> list = new List<MediaIdAndType>(1);
		list.Add(new MediaIdAndType(ID, MediaType.Playlist));
		Shell.DeleteMedia(list, deleteFileOnDisk: false);
	}

	public override void DataUpdated()
	{
	}

	public override void DataEdited()
	{
		UpdateInfo();
	}

	private void UpdateInfo()
	{
		_title = PlaylistManager.GetPlaylistName(ID);
		((ModelItem)this).FirePropertyChanged("Title");
		_type = UIDeviceList.MapMediaTypeToSyncCategory(PlaylistManager.GetAutoPlaylistSchema(ID));
		((ModelItem)this).FirePropertyChanged("Type");
	}
}
