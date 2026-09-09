namespace ZuneUI;

public class FamilySetting
{
	private int _ratingId;

	private string _ratingSystem;

	private int _ratingLevel;

	private bool _blockUnrated;

	private bool _changed;

	public int RatingId
	{
		get
		{
			return _ratingId;
		}
		set
		{
			_ratingId = value;
		}
	}

	public string RatingSystem
	{
		get
		{
			return _ratingSystem;
		}
		set
		{
			_ratingSystem = value;
			_changed = true;
		}
	}

	public int RatingLevel
	{
		get
		{
			return _ratingLevel;
		}
		set
		{
			_ratingLevel = value;
			_changed = true;
		}
	}

	public bool BlockUnrated
	{
		get
		{
			return _blockUnrated;
		}
		set
		{
			_blockUnrated = value;
			_changed = true;
		}
	}

	public bool HasChanged => _changed;

	public FamilySetting(int ratingId, string ratingSystem, int ratingLevel, bool blockUnrated)
	{
		_ratingId = ratingId;
		_ratingSystem = ratingSystem;
		_ratingLevel = ratingLevel;
		_blockUnrated = blockUnrated;
		_changed = false;
	}

	public FamilySetting(string ratingSystem, int ratingLevel, bool blockUnrated)
	{
		_ratingId = -1;
		_ratingSystem = ratingSystem;
		_ratingLevel = ratingLevel;
		_blockUnrated = blockUnrated;
		_changed = true;
	}
}
