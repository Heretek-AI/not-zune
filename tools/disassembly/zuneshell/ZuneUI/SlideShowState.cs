using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneUI;

public class SlideShowState : ModelItem
{
	private int _index;

	private string _sort;

	private bool _play;

	private bool _canPlay;

	private bool _usePhotoIds;

	private bool _startWithFirstPhotoId;

	private Command _navigate;

	private int _folderId;

	private List<int> _photoIds;

	public int Index
	{
		get
		{
			return _index;
		}
		set
		{
			if (_index != value)
			{
				_index = value;
				((ModelItem)this).FirePropertyChanged("Index");
			}
		}
	}

	public string Sort
	{
		get
		{
			return _sort;
		}
		set
		{
			if (_sort != value)
			{
				_sort = value;
				((ModelItem)this).FirePropertyChanged("Sort");
			}
		}
	}

	public bool Play
	{
		get
		{
			return _play;
		}
		set
		{
			if (_play != value)
			{
				_play = value;
				((ModelItem)this).FirePropertyChanged("Play");
			}
		}
	}

	public bool CanPlay
	{
		get
		{
			return _canPlay;
		}
		set
		{
			if (_canPlay != value)
			{
				_canPlay = value;
				((ModelItem)this).FirePropertyChanged("CanPlay");
			}
		}
	}

	public Command Navigate => _navigate;

	public int FolderId
	{
		get
		{
			return _folderId;
		}
		set
		{
			if (_folderId != value)
			{
				_folderId = value;
				((ModelItem)this).FirePropertyChanged("FolderId");
			}
		}
	}

	public List<int> PhotoIds
	{
		get
		{
			return _photoIds;
		}
		set
		{
			if (_photoIds != value)
			{
				_photoIds = value;
				((ModelItem)this).FirePropertyChanged("PhotoIds");
			}
		}
	}

	public bool UsePhotoIds
	{
		get
		{
			return _usePhotoIds;
		}
		set
		{
			if (_usePhotoIds != value)
			{
				_usePhotoIds = value;
				((ModelItem)this).FirePropertyChanged("UsePhotoIds");
			}
		}
	}

	public bool StartWithFirstPhotoId
	{
		get
		{
			return _startWithFirstPhotoId;
		}
		set
		{
			if (_startWithFirstPhotoId != value)
			{
				_startWithFirstPhotoId = value;
				((ModelItem)this).FirePropertyChanged("StartWithFirstPhotoId");
			}
		}
	}

	public SlideShowState(IModelItemOwner owner)
		: base(owner)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		_navigate = new Command((IModelItemOwner)(object)this);
		_photoIds = new List<int>();
	}
}
