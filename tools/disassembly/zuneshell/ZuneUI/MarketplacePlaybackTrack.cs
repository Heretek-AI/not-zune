using System;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;
using MicrosoftZunePlayback;

namespace ZuneUI;

[Serializable]
public class MarketplacePlaybackTrack : PlaybackTrack
{
	private bool _subscriptionPlay;

	private Guid _zuneMediaId;

	private string _title;

	private string _album;

	private string _artist;

	private TimeSpan _duration;

	private int _trackNumber;

	private string _genre;

	private Guid _albumId;

	private string _context;

	private long _incrementPlayCountAfter;

	private bool _hasReportedStreamPlayback;

	private bool _hasReportedStreamPlaySkip;

	public override string Title => _title;

	public string Album => _album;

	public string Artist => _artist;

	public override TimeSpan Duration => _duration;

	public override Guid ZuneMediaId => _zuneMediaId;

	public override bool IsMusic => true;

	public override bool IsStreaming => true;

	public int TrackNumber => _trackNumber;

	public string Genre => _genre;

	public Guid AlbumId => _albumId;

	public override MediaType MediaType => MediaType.Track;

	public override string ServiceContext => _context;

	public MarketplacePlaybackTrack(bool subscriptionPlay, Guid zuneMediaId, string title, TimeSpan duration, string album, string artist, int trackNumber, string genre, Guid albumId, string context)
	{
		_subscriptionPlay = subscriptionPlay;
		_zuneMediaId = zuneMediaId;
		_title = title;
		_duration = duration;
		_album = album;
		_artist = artist;
		_trackNumber = trackNumber;
		_genre = genre;
		_albumId = albumId;
		if (!string.IsNullOrEmpty(context))
		{
			_context = context;
		}
		_incrementPlayCountAfter = (subscriptionPlay ? 200000000 : 1);
	}

	public override HRESULT GetURI(out string uri)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		string text = null;
		if (ZuneApplication.Service.InCompleteCollection(_zuneMediaId, (EContentType)0))
		{
			Guid guid = default(Guid);
			ZuneApplication.Service.GetContentUri(_zuneMediaId, (EContentType)0, (EContentUriFlags)2, ref text, ref guid);
		}
		if (string.IsNullOrEmpty(text))
		{
			text = "vnd.ms.zunecp://CP/?ContentPartnerKeyName=zune&StreamType=Music&TrackID=" + _zuneMediaId;
		}
		uri = text;
		return HRESULT._S_OK;
	}

	internal override void OnBeginPlayback(PlayerInterop playbackWrapper)
	{
		base.OnBeginPlayback(playbackWrapper);
		_hasReportedStreamPlayback = false;
		_hasReportedStreamPlaySkip = false;
		Notification.BroadcastNowPlaying((EMediaTypes)3, Album, Artist, Title, TrackNumber, ZuneMediaId);
	}

	internal override void OnEndPlayback(bool endOfMedia)
	{
		base.OnEndPlayback(endOfMedia);
		Notification.ResetNowPlaying();
	}

	internal override void OnPositionChanged(long position)
	{
		if (_hasReportedStreamPlayback || position < _incrementPlayCountAfter)
		{
			return;
		}
		_hasReportedStreamPlayback = true;
		if (_subscriptionPlay)
		{
			UsageDataService.ReportTrackSubscriptionPlayback(_zuneMediaId, _context);
			if (MediaType == MediaType.Track)
			{
				Shell.MainFrame.Social.PlayCount++;
			}
		}
		else
		{
			UsageDataService.ReportTrackPreviewPlayback(_zuneMediaId, _context);
		}
	}

	internal override void OnSkip()
	{
		if (!_hasReportedStreamPlayback && !_hasReportedStreamPlaySkip)
		{
			_hasReportedStreamPlaySkip = true;
			if (_subscriptionPlay)
			{
				UsageDataService.ReportTrackSubscriptionSkipPlay(_zuneMediaId, _context);
			}
			else
			{
				UsageDataService.ReportTrackPreviewSkipPlay(_zuneMediaId, _context);
			}
		}
	}
}
