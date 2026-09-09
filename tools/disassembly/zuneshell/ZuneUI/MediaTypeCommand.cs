using Microsoft.Iris;

namespace ZuneUI;

public class MediaTypeCommand : Command
{
	private MediaType _type;

	public MediaType Type
	{
		get
		{
			return _type;
		}
		set
		{
			if (_type != value)
			{
				_type = value;
				((ModelItem)this).FirePropertyChanged("Type");
			}
		}
	}
}
