using Microsoft.Iris;

namespace ZuneUI;

public class CartPanel : LibraryPanel
{
	private CartItem _selectedItem;

	public CartItem SelectedItem
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
			}
		}
	}

	public CartPanel(CartPage page)
		: base((IModelItemOwner)(object)page)
	{
	}
}
