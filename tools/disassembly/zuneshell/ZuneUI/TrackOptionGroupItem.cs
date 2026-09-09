using MicrosoftZuneLibrary;

namespace ZuneUI;

public class TrackOptionGroupItem
{
	private TrackMetadata _trackMetadata;

	private bool _original;

	public TrackMetadata TrackMetadata
	{
		get
		{
			return _trackMetadata;
		}
		set
		{
			_trackMetadata = value;
		}
	}

	public bool Original
	{
		get
		{
			return _original;
		}
		set
		{
			_original = value;
		}
	}
}
