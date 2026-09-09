namespace ZuneUI;

public class EmailDomainPropertyDescriptor : PropertyDescriptor
{
	public EmailDomainPropertyDescriptor(string name, string multiValueString, string unknownString)
		: base(name, multiValueString, unknownString, required: true)
	{
	}

	public override bool IsValidInternal(string value)
	{
		return EmailHelper.IsValidDomain(value);
	}
}
