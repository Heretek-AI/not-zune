namespace ZuneUI;

public class SyncModeOptionPair
{
	private string _name;

	private SyncMode _mode;

	public string Name
	{
		get
		{
			return _name;
		}
		set
		{
			_name = value;
		}
	}

	public SyncMode Mode
	{
		get
		{
			return _mode;
		}
		set
		{
			_mode = value;
		}
	}

	public SyncModeOptionPair(string name, SyncMode mode)
	{
		_name = name;
		_mode = mode;
	}

	public override string ToString()
	{
		return Name;
	}

	public static implicit operator string(SyncModeOptionPair pair)
	{
		return pair.ToString();
	}
}
