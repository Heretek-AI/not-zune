using Microsoft.Iris;

namespace ZuneUI;

public class BackgroundOption : NamedStringOption
{
	private WindowColor _color;

	public WindowColor Color
	{
		get
		{
			return _color;
		}
		set
		{
			_color = value;
		}
	}
}
