using ZuneXml;

namespace ZuneUI;

public class MessageListFilter : FilterList
{
	private bool _wishlistItems;

	public MessageListFilter(bool wishlistItems)
	{
		_wishlistItems = wishlistItems;
	}

	protected override bool ShouldIncludeItem(int sourceIndex, int targetIndex, object item)
	{
		bool result = false;
		if (item is MessageRoot messageRoot && messageRoot.Wishlist == _wishlistItems)
		{
			result = messageRoot.IsSupported;
		}
		return result;
	}
}
