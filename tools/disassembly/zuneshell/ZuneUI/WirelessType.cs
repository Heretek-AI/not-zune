using MicrosoftZuneLibrary;

namespace ZuneUI;

internal class WirelessType
{
	private WlanAuthCipherPair _type;

	private string _description;

	private bool _newGroup;

	private bool _alwaysSupported;

	private bool _displayType;

	public bool NewGroup => _newGroup;

	public bool AlwaysSupported => _alwaysSupported;

	public WlanAuthCipherPair Type => _type;

	public string Description => _description;

	public bool DisplayType => _displayType;

	public WirelessType(WlanAuthCipherPair type, string description, bool displayType, bool alwaysSupported, bool newGroup)
	{
		_type = type;
		_description = description;
		_alwaysSupported = alwaysSupported;
		_newGroup = newGroup;
		_displayType = displayType;
	}
}
