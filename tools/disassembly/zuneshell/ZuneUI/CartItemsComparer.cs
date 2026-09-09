using System;
using System.Collections;

namespace ZuneUI;

public class CartItemsComparer : IComparer
{
	public readonly bool SortAscending;

	internal readonly GetCartItemPropertyDelegate PropertyDelegate;

	internal CartItemsComparer(bool sortAscending, GetCartItemPropertyDelegate propertyDelegate)
	{
		SortAscending = sortAscending;
		PropertyDelegate = propertyDelegate;
	}

	public static string GetCartItemSortTitle(CartItem item)
	{
		return item.SortTitle;
	}

	public static string GetCartItemArtistName(CartItem item)
	{
		return item.ArtistName;
	}

	public static string GetCartItemDisplayType(CartItem item)
	{
		return item.DisplayType;
	}

	public int Compare(object x, object y)
	{
		CartItem cartItem = x as CartItem;
		CartItem cartItem2 = y as CartItem;
		if (cartItem != null && cartItem2 != null)
		{
			int num = ((IComparable)cartItem.AvailableInMarketplace).CompareTo((object?)cartItem2.AvailableInMarketplace);
			if (num != 0)
			{
				return -num;
			}
			if (PropertyDelegate != null)
			{
				num = string.Compare(PropertyDelegate(cartItem), PropertyDelegate(cartItem2));
			}
			if (!SortAscending)
			{
				return -num;
			}
			return num;
		}
		return 0;
	}
}
