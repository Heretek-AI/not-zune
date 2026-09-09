using System;
using Microsoft.Zune.Util;

namespace ZuneUI;

[Serializable]
public class StreamingRadioPlaybackTrack : PlaybackTrack
{
	private string _originalUri;

	private string _title;

	private bool _isVideo;

	private MediaType _mediaType;

	[NonSerialized]
	private RadioPlaylist _playlist;

	public override Guid ZuneMediaId => Guid.Empty;

	public override string Title => _title;

	public override TimeSpan Duration => TimeSpan.Zero;

	public override bool IsVideo => _isVideo;

	public override bool IsStreaming => true;

	public override MediaType MediaType => _mediaType;

	public StreamingRadioPlaybackTrack(string uri, string title, MediaType mediaType)
	{
		_originalUri = uri;
		_title = title;
		_isVideo = mediaType == MediaType.Video;
		_mediaType = mediaType;
	}

	public override HRESULT GetURI(out string uri)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		string text = null;
		if (_playlist == null && _originalUri != null)
		{
			_playlist = RadioStationManager.Instance.GetRadioPlaylist(_originalUri);
		}
		if (_playlist != null)
		{
			text = _playlist.GetNextUri();
		}
		uri = text;
		return HRESULT._S_OK;
	}
}
