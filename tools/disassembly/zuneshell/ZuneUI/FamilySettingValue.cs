namespace ZuneUI;

public class FamilySettingValue
{
	private string _text;

	private int _value;

	public int Value => _value;

	public string Text => _text;

	public FamilySettingValue(string text, int value)
	{
		_text = text;
		_value = value;
	}
}
