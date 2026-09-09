using Microsoft.Iris;

namespace ZuneUI;

public class BooleanInputChoice : BooleanChoice
{
	private bool available = true;

	public bool Available
	{
		get
		{
			return available;
		}
		set
		{
			if (available != value)
			{
				available = value;
				((ModelItem)this).FirePropertyChanged("Available");
			}
		}
	}

	internal BooleanInputChoice(ModelItem owner, string description, bool isAvailable)
		: base((IModelItemOwner)(object)owner, description)
	{
		Available = isAvailable;
	}
}
