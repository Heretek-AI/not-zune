using System;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using ZuneXml;

namespace ZuneUI;

[Serializable]
public class VideoPlaybackTrack : PlaybackTrack
{
	private Guid _zuneMediaId;

	private Guid _zuneMediaInstanceId;

	private string _title;

	private string _artist;

	private string _uri;

	private bool _isDownloading;

	private bool _fallbackToPreview;

	private bool _forcePreview;

	private bool _ignoreCollection;

	private bool _isStreaming;

	private VideoDefinitionEnum _videoDefinition;

	public override Guid ZuneMediaId => _zuneMediaId;

	public Guid Id => _zuneMediaId;

	public override Guid ZuneMediaInstanceId => _zuneMediaInstanceId;

	public override string Title => _title;

	public string Artist => _artist;

	public override TimeSpan Duration => TimeSpan.Zero;

	public override bool IsVideo => true;

	public override MediaType MediaType => MediaType.Video;

	public bool IsDownloading => _isDownloading;

	public bool FallbackToPreview => _fallbackToPreview;

	public bool ForcePreview => _forcePreview;

	public bool IgnoreCollection => _ignoreCollection;

	public override bool IsStreaming => _isStreaming;

	public override bool IsHD
	{
		get
		{
			if (_videoDefinition != VideoDefinitionEnum.HD)
			{
				return false;
			}
			return true;
		}
	}

	public VideoPlaybackTrack(Guid zuneMediaId, string title, string artist, string uri, bool isDownloading, bool isStreaming, bool ignoreCollection, bool fallbackToPreview, bool forcePreview, VideoDefinitionEnum videoDefinition)
	{
		_title = title;
		_zuneMediaId = zuneMediaId;
		_artist = artist;
		_uri = uri;
		_isDownloading = isDownloading;
		_isStreaming = isStreaming;
		_ignoreCollection = ignoreCollection;
		_fallbackToPreview = fallbackToPreview;
		_forcePreview = forcePreview;
		_videoDefinition = videoDefinition;
	}

	public override HRESULT GetURI(out string uri)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		HRESULT result = HRESULT._S_OK;
		string text = null;
		if (!ZuneApplication.Service.InCompleteCollection(_zuneMediaId, (EContentType)3) && !string.IsNullOrEmpty(_uri))
		{
			text = _uri;
		}
		else
		{
			EContentUriFlags val = (EContentUriFlags)0;
			if (IgnoreCollection)
			{
				val = (EContentUriFlags)(val | 1);
			}
			if (FallbackToPreview)
			{
				val = (EContentUriFlags)(val | 2);
			}
			if (ForcePreview)
			{
				val = (EContentUriFlags)(val | 4);
			}
			result = ZuneApplication.Service.GetContentUri(_zuneMediaId, (EContentType)3, val, ref text, ref _zuneMediaInstanceId);
		}
		if (!string.IsNullOrEmpty(text) && text.Contains(".ism/manifest"))
		{
			_isStreaming = true;
		}
		uri = text;
		return result;
	}
}
