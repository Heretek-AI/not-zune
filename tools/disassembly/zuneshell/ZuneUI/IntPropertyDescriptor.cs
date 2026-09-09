namespace ZuneUI;

public class IntPropertyDescriptor : PropertyDescriptor
{
	public IntPropertyDescriptor(string name, string multiValueString, string unknownString, int maxTextLength)
		: base(name, multiValueString, unknownString, maxTextLength)
	{
	}

	public IntPropertyDescriptor(string name, string multiValueString, string unknownString)
		: base(name, multiValueString, unknownString)
	{
	}

	public IntPropertyDescriptor(string name, string multiValueString, string unknownString, bool required)
		: base(name, multiValueString, unknownString, required)
	{
	}

	public override object ConvertFromString(string value)
	{
		if (int.TryParse(value, out var result))
		{
			return result;
		}
		return 0;
	}

	public override bool IsValidInternal(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return true;
		}
		if (int.TryParse(value, out var result) && result >= 0)
		{
			return true;
		}
		return false;
	}
}
