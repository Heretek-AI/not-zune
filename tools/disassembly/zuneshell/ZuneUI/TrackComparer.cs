using System.Collections.Generic;

namespace ZuneUI;

internal class TrackComparer : IComparer<MetadataEditTrack>
{
	public int Compare(MetadataEditTrack x, MetadataEditTrack y)
	{
		int result = 0;
		int result2 = 0;
		if (x != null && y != null)
		{
			int.TryParse(x.GetProperty(MetadataEditMedia.TrackNumberDescriptor).Value, out result);
			int.TryParse(y.GetProperty(MetadataEditMedia.TrackNumberDescriptor).Value, out result2);
		}
		return result - result2;
	}
}
