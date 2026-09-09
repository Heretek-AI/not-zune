using Microsoft.Iris;
using UIXControls;

namespace ZuneUI;

public class RateCommand : MenuItemCommand
{
	private int m_rating;

	public int Rating
	{
		get
		{
			return m_rating;
		}
		set
		{
			m_rating = value;
			((ModelItem)this).FirePropertyChanged("Rating");
		}
	}

	public RateCommand()
	{
		((ModelItem)this).Description = Shell.LoadString(StringId.IDS_LIBRARY_RATE_MENU_ITEM);
	}
}
