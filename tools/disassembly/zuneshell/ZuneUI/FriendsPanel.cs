using Microsoft.Iris;

namespace ZuneUI;

public class FriendsPanel : ListPanel
{
	internal FriendsPanel(FriendsPage page)
		: base((IModelItemOwner)(object)page)
	{
	}

	public int GetIndexFromZuneTag(string tagToFind)
	{
		int result = -1;
		int num = 0;
		if (base.Content != null && !string.IsNullOrEmpty(tagToFind))
		{
			foreach (object item in base.Content)
			{
				object dataProviderObject = ProfileCardData.GetDataProviderObject(item);
				DataProviderObject val = (DataProviderObject)((dataProviderObject is DataProviderObject) ? dataProviderObject : null);
				if (val != null)
				{
					string zuneTag = val.GetProperty("ZuneTag") as string;
					if (SignIn.TagsMatch(zuneTag, tagToFind))
					{
						result = num;
						break;
					}
				}
				num++;
			}
		}
		return result;
	}
}
