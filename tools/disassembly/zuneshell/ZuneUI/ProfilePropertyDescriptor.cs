namespace ZuneUI;

public class ProfilePropertyDescriptor : PropertyDescriptor
{
	private string _serviceName;

	private StringId _displayNameId;

	public string ServiceName => _serviceName;

	public StringId DisplayNameId => _displayNameId;

	public ProfilePropertyDescriptor(string name, string serviceName, StringId displayNameId, int maxTextLength)
		: base(name, string.Empty, string.Empty, maxTextLength)
	{
		_serviceName = serviceName;
		_displayNameId = displayNameId;
	}

	public virtual string GetServiceValue(string value)
	{
		return value;
	}
}
