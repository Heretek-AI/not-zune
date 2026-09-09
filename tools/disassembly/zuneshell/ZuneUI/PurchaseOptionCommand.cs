using Microsoft.Iris;

namespace ZuneUI;

public class PurchaseOptionCommand : Command
{
	private string _text = string.Empty;

	private int _points;

	public string Text
	{
		get
		{
			return _text;
		}
		set
		{
			if (_text != value)
			{
				_text = value;
				((ModelItem)this).FirePropertyChanged("Text");
			}
		}
	}

	public int Points
	{
		get
		{
			return _points;
		}
		set
		{
			if (_points != value)
			{
				_points = value;
				((ModelItem)this).FirePropertyChanged("Points");
			}
		}
	}
}
