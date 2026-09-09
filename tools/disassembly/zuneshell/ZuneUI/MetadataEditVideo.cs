using System.Collections;

namespace ZuneUI;

public class MetadataEditVideo : MetadataEditMedia
{
	private static PropertyDescriptor[] s_properties = new PropertyDescriptor[12]
	{
		MetadataEditMedia.s_Title,
		MetadataEditMedia.s_TitleYomi,
		MetadataEditMedia.s_Artist,
		MetadataEditMedia.s_Genre,
		MetadataEditMedia.s_Conductor,
		MetadataEditMedia.s_Composer,
		MetadataEditMedia.s_Category,
		MetadataEditMedia.s_SeriesTitle,
		MetadataEditMedia.s_SeasonNumber,
		MetadataEditMedia.s_EpisodeNumber,
		MetadataEditMedia.s_ReleaseDate,
		MetadataEditMedia.s_Description
	};

	public MetadataEditVideo(IList videoList)
	{
		Initialize(videoList, s_properties);
	}
}
