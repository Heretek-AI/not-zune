using Microsoft.Iris;
using ZuneXml;

namespace ZuneUI;

public class MixResultArtist : MixResult
{
	protected MixResultArtist()
	{
	}

	public static MixResultArtist CreateInstance(DataProviderObject dataProviderObject, string reason)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		MixResultArtist mixResultArtist = new MixResultArtist();
		if (dataProviderObject.TypeName == "ArtistData")
		{
			dataProviderObject = (DataProviderObject)dataProviderObject.GetProperty("Item");
		}
		Artist artist = (Artist)(object)dataProviderObject;
		mixResultArtist.Initialize(MixResultType.Artist, reason, artist.Title ?? string.Empty, string.Empty, artist.Id.ToString(), string.Empty, artist.ImageId, null);
		return mixResultArtist;
	}

	internal static int GetItemPriority(DataProviderObject item, int startPriority)
	{
		return startPriority;
	}
}
