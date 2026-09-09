using System;

namespace ZuneUI;

[Serializable]
public class StreamingPlaybackTrack : PlaybackTrack
{
	private string _uri;

	private string _title;

	private bool _isVideo;

	private MediaType _mediaType;

	public override Guid ZuneMediaId => Guid.Empty;

	public override string Title => _title;

	public override TimeSpan Duration => TimeSpan.Zero;

	public override bool IsVideo => _isVideo;

	public override MediaType MediaType => _mediaType;

	public StreamingPlaybackTrack(string uri, string title, MediaType mediaType)
	{
		_uri = uri;
		_title = title;
		_isVideo = mediaType == MediaType.Video;
		_mediaType = mediaType;
	}

	public override HRESULT GetURI(out string uri)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		uri = _uri;
		return HRESULT._S_OK;
	}
}
