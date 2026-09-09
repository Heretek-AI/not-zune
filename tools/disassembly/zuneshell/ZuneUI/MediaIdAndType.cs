using MicrosoftZuneLibrary;

namespace ZuneUI;

public class MediaIdAndType : IDatabaseMedia
{
	private int _id;

	private EMediaTypes _type;

	public MediaIdAndType(int mediaId, MediaType type)
		: this(mediaId, (EMediaTypes)type)
	{
	}

	public MediaIdAndType(int mediaId, EMediaTypes type)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		_id = mediaId;
		_type = type;
	}

	public void GetMediaIdAndType(out int mediaId, out EMediaTypes mediaType)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected I4, but got Unknown
		mediaId = _id;
		mediaType = (EMediaTypes)(int)_type;
	}
}
