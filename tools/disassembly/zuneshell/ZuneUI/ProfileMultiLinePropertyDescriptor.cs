namespace ZuneUI;

public class ProfileMultiLinePropertyDescriptor : ProfilePropertyDescriptor
{
	private int _maxTrimmedLength;

	public int MaxTrimmedLength => _maxTrimmedLength;

	public ProfileMultiLinePropertyDescriptor(string name, string serviceName, StringId displayNameId, int maxTrimmedLength)
		: base(name, serviceName, displayNameId, maxTrimmedLength * 2)
	{
		_maxTrimmedLength = maxTrimmedLength;
	}

	public override string GetServiceValue(string value)
	{
		if (value != null)
		{
			value = value.Replace("\r\n", "\n");
		}
		return value;
	}

	public override bool IsValidInternal(string value)
	{
		value = GetServiceValue(value);
		if (value != null && _maxTrimmedLength < value.Length)
		{
			return false;
		}
		return base.IsValidInternal(value);
	}

	public override object ConvertFromString(string value)
	{
		string value2 = base.ConvertFromString(value) as string;
		return GetServiceValue(value2);
	}
}
