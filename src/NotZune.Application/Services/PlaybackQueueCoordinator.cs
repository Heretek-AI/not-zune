using NotZune.Application.Events;
using NotZune.Application.Interfaces;
using NotZune.Domain.Enums;
using NotZune.Domain.Models;

namespace NotZune.Application.Services;

public class PlaybackQueueCoordinator : IPlayerCoordinator
{
    private readonly List<Track> _queue = new();
    private readonly List<Track> _history = new();
    private int _currentIndex = -1;
    private PlaybackState _state = PlaybackState.Stopped;
    private TimeSpan _currentPosition = TimeSpan.Zero;
    private double _volume = 1.0;
    private bool _isMuted;
    private bool _shuffle;
    private bool _repeat;
    private readonly Random _random = new();

    public PlaybackState State => _state;
    public Track? CurrentTrack => (_currentIndex >= 0 && _currentIndex < _queue.Count) ? _queue[_currentIndex] : null;
    public TimeSpan CurrentPosition => _currentPosition;
    public TimeSpan Duration => CurrentTrack?.Duration ?? TimeSpan.Zero;

    public double Volume
    {
        get => _volume;
        set => _volume = Math.Clamp(value, 0.0, 1.0);
    }

    public bool IsMuted
    {
        get => _isMuted;
        set => _isMuted = value;
    }

    public bool Shuffle
    {
        get => _shuffle;
        set => _shuffle = value;
    }

    public bool Repeat
    {
        get => _repeat;
        set => _repeat = value;
    }

    public IReadOnlyList<Track> Queue
    {
        get
        {
            lock (_queue)
            {
                return _queue.ToList();
            }
        }
    }

    public event EventHandler<TrackChangedEventArgs>? TrackChanged;
    public event EventHandler<PlaybackStateChangedEventArgs>? StateChanged;
    public event EventHandler<HeartRatingChangedEventArgs>? RatingChanged;

    public Task PlayTrackAsync(Track track, IEnumerable<Track>? contextQueue = null)
    {
        lock (_queue)
        {
            if (contextQueue != null)
            {
                _queue.Clear();
                _queue.AddRange(contextQueue);
                _currentIndex = _queue.FindIndex(t => t.Id == track.Id);
                if (_currentIndex == -1)
                {
                    _queue.Insert(0, track);
                    _currentIndex = 0;
                }
            }
            else
            {
                _currentIndex = _queue.FindIndex(t => t.Id == track.Id);
                if (_currentIndex == -1)
                {
                    _queue.Add(track);
                    _currentIndex = _queue.Count - 1;
                }
            }

            _currentPosition = TimeSpan.Zero;
            _state = PlaybackState.Playing;
        }

        TrackChanged?.Invoke(this, new TrackChangedEventArgs(CurrentTrack, _currentPosition));
        StateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(_state, _currentPosition));
        return Task.CompletedTask;
    }

    public Task PlayPauseAsync()
    {
        if (CurrentTrack == null && _queue.Count > 0)
        {
            _currentIndex = 0;
            _state = PlaybackState.Playing;
        }
        else if (_state == PlaybackState.Playing)
        {
            _state = PlaybackState.Paused;
        }
        else if (_state == PlaybackState.Paused || _state == PlaybackState.Stopped)
        {
            if (CurrentTrack != null)
            {
                _state = PlaybackState.Playing;
            }
        }

        StateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(_state, _currentPosition));
        return Task.CompletedTask;
    }

    public Task StopAsync()
    {
        _state = PlaybackState.Stopped;
        _currentPosition = TimeSpan.Zero;
        StateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(_state, _currentPosition));
        return Task.CompletedTask;
    }

    public Task NextAsync()
    {
        lock (_queue)
        {
            if (_queue.Count == 0) return Task.CompletedTask;

            if (_shuffle)
            {
                _currentIndex = _random.Next(0, _queue.Count);
            }
            else if (_currentIndex + 1 < _queue.Count)
            {
                _currentIndex++;
            }
            else if (_repeat)
            {
                _currentIndex = 0;
            }
            else
            {
                _state = PlaybackState.Stopped;
                _currentPosition = TimeSpan.Zero;
                StateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(_state, _currentPosition));
                return Task.CompletedTask;
            }

            _currentPosition = TimeSpan.Zero;
            _state = PlaybackState.Playing;
        }

        TrackChanged?.Invoke(this, new TrackChangedEventArgs(CurrentTrack, _currentPosition));
        StateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(_state, _currentPosition));
        return Task.CompletedTask;
    }

    public Task PreviousAsync()
    {
        lock (_queue)
        {
            if (_queue.Count == 0) return Task.CompletedTask;

            // If more than 3 seconds in, restart track
            if (_currentPosition.TotalSeconds > 3)
            {
                _currentPosition = TimeSpan.Zero;
            }
            else if (_currentIndex > 0)
            {
                _currentIndex--;
                _currentPosition = TimeSpan.Zero;
            }
            else if (_repeat)
            {
                _currentIndex = _queue.Count - 1;
                _currentPosition = TimeSpan.Zero;
            }
        }

        TrackChanged?.Invoke(this, new TrackChangedEventArgs(CurrentTrack, _currentPosition));
        StateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(_state, _currentPosition));
        return Task.CompletedTask;
    }

    public Task SeekAsync(TimeSpan position)
    {
        if (CurrentTrack == null) return Task.CompletedTask;

        _currentPosition = position < TimeSpan.Zero ? TimeSpan.Zero : 
                           position > Duration ? Duration : position;

        StateChanged?.Invoke(this, new PlaybackStateChangedEventArgs(_state, _currentPosition));
        return Task.CompletedTask;
    }

    public Task SetRatingAsync(Guid trackId, HeartRating rating)
    {
        lock (_queue)
        {
            foreach (var t in _queue.Where(t => t.Id == trackId))
            {
                t.Rating = rating;
            }
        }

        RatingChanged?.Invoke(this, new HeartRatingChangedEventArgs(trackId, rating));
        return Task.CompletedTask;
    }

    public void Enqueue(IEnumerable<Track> tracks)
    {
        lock (_queue)
        {
            _queue.AddRange(tracks);
        }
    }

    public void PlayNext(IEnumerable<Track> tracks)
    {
        lock (_queue)
        {
            var insertPos = _currentIndex >= 0 ? _currentIndex + 1 : 0;
            _queue.InsertRange(insertPos, tracks);
        }
    }
}
