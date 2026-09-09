using System;
using System.Runtime.Serialization;
using Microsoft.Iris;
using MicrosoftZunePlayback;

namespace ZuneUI;

[Serializable]
public abstract class PlaybackTrack
{
	public const long c_TicksPerSecond = 10000000L;

	public const long c_TicksPerMinute = 600000000L;

	public const long c_incrementPlayCountAfter = 200000000L;

	private static int s_nextPlaybackID;

	protected ContainerPlayMarker _containerPlayMarker;

	[NonSerialized]
	private int _playbackID;

	[NonSerialized]
	private Command _ratingChanged;

	public int PlaybackID => _playbackID;

	public Command RatingChanged
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Expected O, but got Unknown
			if (_ratingChanged == null)
			{
				_ratingChanged = new Command();
			}
			return _ratingChanged;
		}
	}

	public abstract string Title { get; }

	public abstract TimeSpan Duration { get; }

	public abstract Guid ZuneMediaId { get; }

	public virtual Guid ZuneMediaInstanceId => Guid.Empty;

	public virtual bool CanRate => false;

	public virtual int UserRating
	{
		get
		{
			return 0;
		}
		set
		{
		}
	}

	public virtual bool IsInCollection => false;

	public virtual bool IsInVisibleCollection => false;

	public virtual bool IsMusic => false;

	public virtual bool IsVideo => false;

	public virtual bool IsStreaming => false;

	public virtual bool IsHD => false;

	public virtual MediaType MediaType => MediaType.Undefined;

	public virtual string ServiceContext => null;

	public PlaybackTrack()
	{
		AcquirePlaybackID();
	}

	public abstract HRESULT GetURI(out string uri);

	internal virtual void OnBeginPlayback(PlayerInterop playbackWrapper)
	{
		SingletonModelItem<TransportControls>.Instance.ClearError(this);
	}

	internal virtual void OnPositionChanged(long position)
	{
	}

	internal virtual void OnSkip()
	{
	}

	internal virtual void OnEndPlayback(bool endOfMedia)
	{
	}

	public override string ToString()
	{
		return $"TRACK[{_playbackID}]: {Title}";
	}

	private void AcquirePlaybackID()
	{
		_playbackID = ++s_nextPlaybackID;
		if (_playbackID == 0)
		{
			_playbackID = ++s_nextPlaybackID;
		}
	}

	[OnDeserialized]
	internal void HandleDeserialization(StreamingContext context)
	{
		AcquirePlaybackID();
	}
}
