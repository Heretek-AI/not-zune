using System.Collections;
using Microsoft.Iris;

namespace ZuneXml;

internal class ArtistEventList : ListResult
{
	protected IList _concerts;

	internal IList Concerts
	{
		get
		{
			if (_concerts == null)
			{
				_concerts = FilterConcerts();
			}
			return _concerts;
		}
	}

	internal override IList Items => (IList)base.GetProperty("Items");

	private IList FilterConcerts()
	{
		IList list = null;
		if (Items != null && Items.Count > 0)
		{
			list = new ArrayList(Items.Count);
			foreach (ArtistEvent item in Items)
			{
				if (item.Type == "Concert")
				{
					list.Add(item);
				}
			}
		}
		return list;
	}

	internal static XmlDataProviderObject ConstructArtistEventListObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new ArtistEventList(owner, objectTypeCookie);
	}

	internal ArtistEventList(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		string text;
		if ((text = propertyName) != null && text == "Concerts")
		{
			return Concerts;
		}
		return base.GetProperty(propertyName);
	}
}
