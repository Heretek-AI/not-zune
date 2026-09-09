using System;
using Microsoft.Iris;
using ZuneXml;

namespace ZuneUI;

public class MixResultAlbum : MixResult
{
	public static int KaraokePriorityBump => 100;

	public static int NonActionablePriorityBump => 200;

	protected MixResultAlbum()
	{
	}

	public static MixResultAlbum CreateInstance(DataProviderObject dataProviderObject, string reason)
	{
		MixResultAlbum mixResultAlbum = new MixResultAlbum();
		Album album = (Album)(object)dataProviderObject;
		MiniArtist primaryArtist = album.PrimaryArtist;
		string secondaryText = ((primaryArtist != null) ? primaryArtist.Title : string.Empty);
		mixResultAlbum.Initialize(MixResultType.Album, reason, album.Title ?? string.Empty, secondaryText, album.Id.ToString(), string.Empty, album.ImageId, null);
		return mixResultAlbum;
	}

	public static MixResultAlbum CreateInstance(LibraryAlbumInfo libraryAlbumInfo)
	{
		MixResultAlbum mixResultAlbum = new MixResultAlbum();
		mixResultAlbum.Initialize(MixResultType.Album, string.Empty, libraryAlbumInfo.AlbumTitle, libraryAlbumInfo.ArtistName, libraryAlbumInfo.ZuneMediaId.ToString(), libraryAlbumInfo.AlbumArtUrl, Guid.Empty, null);
		return mixResultAlbum;
	}

	internal static int GetItemPriority(DataProviderObject item, int startPriority)
	{
		int num = startPriority;
		Album album = (Album)(object)item;
		if (!album.Actionable)
		{
			num += NonActionablePriorityBump;
		}
		if ((album.Title ?? string.Empty).ToLowerInvariant().Contains("karaoke"))
		{
			num += KaraokePriorityBump;
		}
		return num;
	}

	internal override bool IsDuplicate(MixResult compareTo)
	{
		if (base.IsDuplicate(compareTo))
		{
			return true;
		}
		if (string.Compare(base.SecondaryText, compareTo.SecondaryText, StringComparison.InvariantCultureIgnoreCase) == 0 && string.Compare(GetComparableAlbumName(base.PrimaryText), GetComparableAlbumName(compareTo.PrimaryText), StringComparison.InvariantCultureIgnoreCase) == 0)
		{
			return true;
		}
		return false;
	}

	public static string GetComparableAlbumName(string albumName)
	{
		string text = albumName.Trim();
		int num = text.IndexOf('(');
		if (num > 3)
		{
			text = text.Substring(0, num).Trim();
		}
		text.ToLowerInvariant();
		return text;
	}
}
