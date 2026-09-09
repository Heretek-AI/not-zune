namespace ZuneUI;

public class TypePropertyDescriptor : PropertyDescriptor
{
	public TypePropertyDescriptor(string name, string multiValueString, string unknownString)
		: base(name, multiValueString, unknownString)
	{
	}

	public override string ConvertToString(object value)
	{
		return VideoDescriptions.GetDescription((int)value);
	}

	public override object ConvertFromString(string value)
	{
		return (int)VideoDescriptions.GetCategory(value);
	}
}
