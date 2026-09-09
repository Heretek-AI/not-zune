using Microsoft.Iris;

namespace ZuneUI;

public class MessageDetailsPanel : LibraryPanel
{
	private DataProviderObject _selectedItem;

	private string _title;

	public DataProviderObject SelectedItem
	{
		get
		{
			return _selectedItem;
		}
		set
		{
			if (_selectedItem != value)
			{
				_selectedItem = value;
				((ModelItem)this).FirePropertyChanged("SelectedItem");
				Title = null;
			}
		}
	}

	public string Title
	{
		get
		{
			return _title;
		}
		set
		{
			if (_title != value)
			{
				_title = value;
				((ModelItem)this).FirePropertyChanged("Title");
			}
		}
	}

	internal MessageDetailsPanel(LibraryPage page, bool showHeaderAndFooter)
		: base((IModelItemOwner)(object)page)
	{
	}
}
