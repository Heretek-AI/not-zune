namespace ZuneUI;

public class EmailPropertyDescriptor : PropertyDescriptor
{
	public EmailPropertyDescriptor(string name, string multiValueString, string unknownString, bool required)
		: base(name, multiValueString, unknownString, required)
	{
	}

	public override bool IsValidInternal(string value)
	{
		if (string.IsNullOrEmpty(value) && !base.Required)
		{
			return true;
		}
		return EmailHelper.IsValid(value);
	}
}
