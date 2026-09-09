using System.Collections;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class MetadataEditTrack : MetadataEditMedia
{
	private static PropertyDescriptor[] s_dataProviderProperties = new PropertyDescriptor[14]
	{
		MetadataEditMedia.s_Title,
		MetadataEditMedia.s_TrackArtist,
		MetadataEditMedia.s_TrackArtistList,
		MetadataEditMedia.s_Genre,
		MetadataEditMedia.s_Conductor,
		MetadataEditMedia.s_Composer,
		MetadataEditMedia.s_TrackReleaseYear,
		MetadataEditMedia.s_AlbumTitle,
		MetadataEditMedia.s_AlbumArtist,
		MetadataEditMedia.s_TrackNumber,
		MetadataEditMedia.s_DiscNumber,
		MetadataEditMedia.s_MediaId,
		MetadataEditMedia.s_TitleYomi,
		MetadataEditMedia.s_ArtistYomi
	};

	private static PropertyDescriptor[] s_trackMetadataProperties = new PropertyDescriptor[14]
	{
		MetadataEditMedia.s_Title,
		MetadataEditMedia.s_TrackArtist,
		MetadataEditMedia.s_TrackArtistList,
		MetadataEditMedia.s_Genre,
		MetadataEditMedia.s_Conductor,
		MetadataEditMedia.s_Composer,
		MetadataEditMedia.s_ReleaseYear,
		MetadataEditMedia.s_AlbumTitle,
		MetadataEditMedia.s_AlbumArtist,
		MetadataEditMedia.s_TrackNumber,
		MetadataEditMedia.s_DiscNumber,
		MetadataEditMedia.s_MediaId,
		MetadataEditMedia.s_TitleYomi,
		MetadataEditMedia.s_ArtistYomi
	};

	public MetadataEditTrack(TrackMetadata trackMetadata)
	{
		_source = TrackMetadataPropertySource.Instance;
		Initialize(new object[1] { trackMetadata }, s_trackMetadataProperties);
	}

	public MetadataEditTrack(IList trackList)
	{
		Initialize(trackList, s_dataProviderProperties);
	}
}
