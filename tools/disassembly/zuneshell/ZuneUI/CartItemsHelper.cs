using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using ZuneXml;

namespace ZuneUI;

public class CartItemsHelper
{
	public static EContentType ExtractContentType(object obj)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		EContentType result = (EContentType)(-1);
		if (obj is MessageRoot messageRoot)
		{
			result = messageRoot.ContentType;
		}
		return result;
	}

	public static void Sort(ListDataSet list, CartItemSortColumn column, bool sortAscending)
	{
		if (list != null && list.Count != 0)
		{
			IComparer comparer = null;
			switch (column)
			{
			case CartItemSortColumn.AvailableInMarketplace:
				comparer = new CartItemsAvailableInMarketplaceComparer(sortAscending);
				break;
			case CartItemSortColumn.SortTitle:
				comparer = new CartItemsComparer(sortAscending, CartItemsComparer.GetCartItemSortTitle);
				break;
			case CartItemSortColumn.ArtistName:
				comparer = new CartItemsComparer(sortAscending, CartItemsComparer.GetCartItemArtistName);
				break;
			case CartItemSortColumn.DisplayType:
				comparer = new CartItemsComparer(sortAscending, CartItemsComparer.GetCartItemDisplayType);
				break;
			}
			if (comparer != null)
			{
				int count = list.Count;
				ArrayList arrayList = new ArrayList(count);
				arrayList.AddRange((ICollection)list);
				arrayList.Sort(comparer);
				list.Source = arrayList;
			}
		}
	}

	public static void ErrorMessageCartAlreadyFull()
	{
		ErrorDialogInfo.Show(((HRESULT)(ref HRESULT._NS_E_CART_FULL)).Int, Shell.LoadString(StringId.IDS_CART_FULL));
	}

	public static void ErrorMessageTooManyNewItems()
	{
		ErrorDialogInfo.Show(((HRESULT)(ref HRESULT._NS_E_CART_TOO_MANY_NEW_ITEMS)).Int, Shell.LoadString(StringId.IDS_CART_FULL));
	}

	public static void ErrorMessageMoreCartItemsAvailable(int extraCartItems)
	{
		StringId stringId = ((extraCartItems == 1) ? StringId.IDS_CART_FULL_MORE_DEVICE_ITEMS_SINGULAR : StringId.IDS_CART_FULL_MORE_DEVICE_ITEMS_PLURAL);
		ErrorDialogInfo.Show(((HRESULT)(ref HRESULT._NS_E_CART_MORE_ITEMS_AVAILABLE)).Int, Shell.LoadString(StringId.IDS_CART_FULL), string.Format(Shell.LoadString(stringId), extraCartItems));
	}
}
