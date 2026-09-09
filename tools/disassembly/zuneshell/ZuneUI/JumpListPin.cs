namespace ZuneUI;

public class JumpListPin
{
	private const string _formatString = "{0}~{1}~{2}~{3}~{4}";

	private const int _numberOfFormatStringParameters = 5;

	private string _name;

	private bool _isQuickMix;

	private bool _isMarketplace;

	private MediaType _type;

	private string _id;

	private int _userId;

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

	public bool IsQuickMix
	{
		get
		{
			return _isQuickMix;
		}
		set
		{
			_isQuickMix = value;
		}
	}

	public bool IsMarketplace
	{
		get
		{
			return _isMarketplace;
		}
		set
		{
			_isMarketplace = value;
		}
	}

	public MediaType Type
	{
		get
		{
			return _type;
		}
		set
		{
			_type = value;
		}
	}

	public string ID
	{
		get
		{
			return _id;
		}
		set
		{
			_id = value;
		}
	}

	public int UserID
	{
		get
		{
			return _userId;
		}
		set
		{
			_userId = value;
		}
	}

	public JumpListPin()
	{
	}

	public JumpListPin(JumpListPin source)
	{
		_name = source._name;
		_isQuickMix = source._isQuickMix;
		_isMarketplace = source._isMarketplace;
		_type = source._type;
		_id = source._id;
		_userId = source._userId;
	}

	public override string ToString()
	{
		return string.Format("{0}~{1}~{2}~{3}~{4}", new object[5]
		{
			IsQuickMix,
			IsMarketplace,
			(int)Type,
			UserID,
			ID
		});
	}

	public static JumpListPin Parse(string pinString)
	{
		JumpListPin jumpListPin = null;
		string[] array = pinString.Split(new char[1] { '~' });
		if (array.Length == 5 && bool.TryParse(array[0], out var result) && bool.TryParse(array[1], out var result2) && int.TryParse(array[2], out var result3) && int.TryParse(array[3], out var result4))
		{
			jumpListPin = new JumpListPin();
			jumpListPin.IsQuickMix = result;
			jumpListPin.IsMarketplace = result2;
			jumpListPin.Type = (MediaType)result3;
			jumpListPin.UserID = result4;
			jumpListPin.ID = array[4];
		}
		return jumpListPin;
	}
}
