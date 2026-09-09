using System.Collections.Generic;

namespace ZuneUI;

public static class MediaDescriptions
{
	private static Dictionary<MediaType, string> _mappings;

	static MediaDescriptions()
	{
		_mappings = new Dictionary<MediaType, string>();
		_mappings[MediaType.AudioMP4] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_AUDIOMP4);
		_mappings[MediaType.AudioMP3] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_AUDIOMP3);
		_mappings[MediaType.AudioWMA] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_AUDIOWMA);
		_mappings[MediaType.AudioWAV] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_AUDIOWAV);
		_mappings[MediaType.ImageJPEG] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_IMAGEJPEG);
		_mappings[MediaType.VideoAVI] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_VIDEOAVI);
		_mappings[MediaType.VideoMP4] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_VIDEOMP4);
		_mappings[MediaType.VideoMPG] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_VIDEOMPG);
		_mappings[MediaType.VideoWMV] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_VIDEOWMV);
		_mappings[MediaType.VideoQT] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_VIDEOQT);
		_mappings[MediaType.VideoDVRMS] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_VIDEODVRMS);
		_mappings[MediaType.VideoMBR] = Shell.LoadString(StringId.IDS_LIBRARY_MEDIATYPE_VIDEOMBR);
	}

	public static string Map(MediaType mediaType)
	{
		if (!_mappings.TryGetValue(mediaType, out var value))
		{
			return "";
		}
		return value;
	}
}
