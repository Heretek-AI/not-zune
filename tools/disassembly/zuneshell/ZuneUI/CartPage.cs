using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class CartPage : LibraryPage
{
	private Command _refreshPageCommand;

	private CartPanel _cartPanel;

	private bool _isEmptyCart;

	public static readonly string CartPageTemplate = "res://ZuneMarketplaceResources!Cart.uix#CartLibrary";

	public CartPanel CartPanel => _cartPanel;

	public bool IsEmptyCart
	{
		get
		{
			return _isEmptyCart;
		}
		set
		{
			if (_isEmptyCart != value)
			{
				_isEmptyCart = value;
				((ModelItem)this).FirePropertyChanged("IsEmptyCart");
			}
		}
	}

	public Command RefreshPageCommand => _refreshPageCommand;

	public CartPage()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		base.PivotPreference = Shell.MainFrame.Marketplace.Cart;
		base.IsRootPage = true;
		base.UI = CartPageTemplate;
		_refreshPageCommand = new Command((IModelItemOwner)(object)this);
		_cartPanel = new CartPanel(this);
	}

	public override IPageState SaveAndRelease()
	{
		if (IsEmptyCart)
		{
			return null;
		}
		if (CartPanel.SelectedItem != null)
		{
			if (base.NavigationArguments == null)
			{
				base.NavigationArguments = new Hashtable(1);
			}
			base.NavigationArguments["MessageId"] = CartPanel.SelectedItem.MessagingId;
			CartPanel.SelectedItem = null;
		}
		else
		{
			base.NavigationArguments = null;
		}
		_cartPanel.Release();
		return base.SaveAndRelease();
	}
}
