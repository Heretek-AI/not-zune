namespace ZuneUI;

public class ZuneTagPropertyDescriptor : PropertyDescriptor
{
	public ZuneTagPropertyDescriptor(string name, string multiValueString, string unknownString)
		: base(name, multiValueString, unknownString, ZuneTagHelper.MaxLength, required: true)
	{
	}

	public override bool IsValidInternal(string value)
	{
		return ZuneTagHelper.IsValid(value);
	}
}
