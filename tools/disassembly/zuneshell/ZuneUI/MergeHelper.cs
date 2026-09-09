using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Shell;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class MergeHelper
{
	public static void MergeArtistsToArtist(string targetArtist, IList sourceArtists)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		IList list = new ArrayList(sourceArtists.Count);
		foreach (LibraryDataProviderListItem sourceArtist in sourceArtists)
		{
			LibraryDataProviderListItem val = sourceArtist;
			list.Add((int)((DataProviderObject)val).GetProperty("LibraryId"));
		}
		ZuneQueryList albumsByArtists = ZuneApplication.ZuneLibrary.GetAlbumsByArtists(list, (string)null);
		albumsByArtists.AddRef();
		uint count = (uint)albumsByArtists.Count;
		for (uint num = 0u; num < count; num++)
		{
			albumsByArtists.SetFieldValue(num, 380u, (object)targetArtist);
		}
		albumsByArtists.Release();
		albumsByArtists.Dispose();
	}

	public static void MergeAlbumsToArtist(string targetArtist, IList sourceAlbums)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		foreach (LibraryDataProviderListItem sourceAlbum in sourceAlbums)
		{
			LibraryDataProviderListItem val = sourceAlbum;
			((DataProviderObject)val).SetProperty("ArtistName", (object)targetArtist);
		}
	}

	public static void MergeTracksToArtist(string targetArtist, IList sourceTracks)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		foreach (LibraryDataProviderListItem sourceTrack in sourceTracks)
		{
			LibraryDataProviderListItem val = sourceTrack;
			((DataProviderObject)val).SetProperty("AlbumArtistName", (object)targetArtist);
		}
	}

	public static void MergeAlbumsToAlbum(string targetAlbumTitle, string targetAlbumArtistName, IList sourceAlbums)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		foreach (LibraryDataProviderListItem sourceAlbum in sourceAlbums)
		{
			LibraryDataProviderListItem val = sourceAlbum;
			((DataProviderObject)val).SetProperty("ArtistName", (object)targetAlbumArtistName);
			((DataProviderObject)val).SetProperty("Title", (object)targetAlbumTitle);
		}
	}

	public static void MergeTracksToAlbum(string targetAlbumTitle, string targetAlbumArtistName, IList sourceTracks)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		foreach (LibraryDataProviderListItem sourceTrack in sourceTracks)
		{
			LibraryDataProviderListItem val = sourceTrack;
			((DataProviderObject)val).SetProperty("AlbumArtistName", (object)targetAlbumArtistName);
			((DataProviderObject)val).SetProperty("AlbumName", (object)targetAlbumTitle);
		}
	}

	public static void MergeTracksToGenre(string targetGenre, IList sourceTracks)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		foreach (LibraryDataProviderListItem sourceTrack in sourceTracks)
		{
			LibraryDataProviderListItem val = sourceTrack;
			((DataProviderObject)val).SetProperty("Genre", (object)targetGenre);
		}
	}

	public static void MergeAlbumsToGenre(string targetGenre, IList sourceAlbums)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		IList list = new ArrayList(sourceAlbums.Count);
		foreach (LibraryDataProviderListItem sourceAlbum in sourceAlbums)
		{
			LibraryDataProviderListItem val = sourceAlbum;
			list.Add((int)((DataProviderObject)val).GetProperty("LibraryId"));
		}
		ZuneQueryList tracksByAlbums = ZuneApplication.ZuneLibrary.GetTracksByAlbums(list, (string)null);
		tracksByAlbums.AddRef();
		uint count = (uint)tracksByAlbums.Count;
		for (uint num = 0u; num < count; num++)
		{
			tracksByAlbums.SetFieldValue(num, 398u, (object)targetGenre);
		}
		tracksByAlbums.Release();
		tracksByAlbums.Dispose();
	}

	public static void MergeGenresToGenre(string targetGenre, IList sourceGenres)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Expected O, but got Unknown
		foreach (LibraryDataProviderListItem sourceGenre in sourceGenres)
		{
			LibraryDataProviderListItem val = sourceGenre;
			((DataProviderObject)val).SetProperty("Title", (object)targetGenre);
		}
	}
}
