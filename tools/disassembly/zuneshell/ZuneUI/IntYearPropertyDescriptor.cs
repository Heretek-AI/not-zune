namespace ZuneUI;

public class IntYearPropertyDescriptor : YearPropertyDescriptor
{
	public IntYearPropertyDescriptor(string name, string multiValueString, string unknownString, int maxTextLength)
		: base(name, multiValueString, unknownString, maxTextLength)
	{
	}

	public override string ConvertToString(object value)
	{
		if (value != null)
		{
			int num = (int)value;
			if (num < 0)
			{
				return null;
			}
			return num.ToString();
		}
		return null;
	}

	public override object ConvertFromString(string value)
	{
		if (int.TryParse(value, out var result))
		{
			return result;
		}
		return 0;
	}
}
