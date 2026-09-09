namespace ZuneUI;

public static class VideoDefinitionHelper
{
	public static bool IsHD(VideoDefinition def)
	{
		if (def != VideoDefinition.High1080p && def != VideoDefinition.High1080i)
		{
			return def == VideoDefinition.High720p;
		}
		return true;
	}
}
