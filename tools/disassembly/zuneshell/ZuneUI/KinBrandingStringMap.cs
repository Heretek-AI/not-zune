namespace ZuneUI;

internal class KinBrandingStringMap : PhoneBrandingStringMap
{
	private static readonly KinBrandingStringMap _instance;

	public new static KinBrandingStringMap Instance => _instance;

	private KinBrandingStringMap()
	{
	}

	static KinBrandingStringMap()
	{
		_instance = new KinBrandingStringMap();
		_instance.Initialize();
	}

	public override void Initialize()
	{
		base.Initialize();
		if (_stringMap.ContainsKey(StringId.IDS_EULA_DIALOG_TEXTAREA_TITLE))
		{
			_stringMap.Remove(StringId.IDS_EULA_DIALOG_TEXTAREA_TITLE);
		}
	}
}
