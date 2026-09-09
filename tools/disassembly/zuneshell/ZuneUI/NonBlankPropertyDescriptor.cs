namespace ZuneUI;

public class NonBlankPropertyDescriptor : PropertyDescriptor
{
	private int _minLength = 1;

	public int MinLength
	{
		set
		{
			_minLength = value;
		}
	}

	public NonBlankPropertyDescriptor(string name, string multiValueString, string unknownString)
		: this(name, multiValueString, unknownString, 1)
	{
	}

	public NonBlankPropertyDescriptor(string name, string multiValueString, string unknownString, int minLength)
		: this(name, multiValueString, unknownString, minLength, required: false)
	{
	}

	public NonBlankPropertyDescriptor(string name, string multiValueString, string unknownString, int minLength, bool required)
		: base(name, multiValueString, unknownString, required)
	{
		_minLength = minLength;
	}

	public override bool IsValidInternal(string value)
	{
		if (value != null)
		{
			return value.Length >= _minLength;
		}
		return false;
	}
}
