using System;
using System.Collections;
using Microsoft.Iris;
using ZuneXml;

namespace ZuneUI;

public class ProfileInterests : NotifyPropertyChangedImpl
{
	private const int c_maxIterests = 4;

	private IList _recentPlays;

	private IList _favorites;

	private IList _topArtists;

	private ArrayList _topAlbums;

	public IList RecentPlays
	{
		get
		{
			return _recentPlays;
		}
		set
		{
			if (_recentPlays != value)
			{
				if (_recentPlays == null)
				{
					_recentPlays = value;
					AddNewTopAlbums(ProfileCategories.RecentlyPlayed, value);
				}
				else
				{
					_recentPlays = value;
					RecreateTopAlbums();
				}
				FirePropertyChanged("RecentPlays");
			}
		}
	}

	public IList Favorites
	{
		get
		{
			return _favorites;
		}
		set
		{
			if (_favorites != value)
			{
				if (_favorites == null)
				{
					_favorites = value;
					AddNewTopAlbums(ProfileCategories.Favorites, value);
				}
				else
				{
					_favorites = value;
					RecreateTopAlbums();
				}
				FirePropertyChanged("Favorites");
			}
		}
	}

	public IList TopArtists
	{
		get
		{
			return _topArtists;
		}
		set
		{
			if (_topArtists != value)
			{
				if (_topArtists == null)
				{
					_topArtists = value;
					AddNewTopAlbums(ProfileCategories.TopArtists, value);
				}
				else
				{
					_topArtists = value;
					RecreateTopAlbums();
				}
				FirePropertyChanged("TopArtists");
			}
		}
	}

	public bool TopAlbumsFull => _topAlbums.Count >= 4;

	public IList TopAlbums => _topAlbums;

	public ProfileInterests()
	{
		_topAlbums = new ArrayList(4);
	}

	private void AddNewTopAlbums(Category category, IList newInterests)
	{
		if (newInterests == null)
		{
			return;
		}
		foreach (object newInterest in newInterests)
		{
			if (TopAlbumsFull)
			{
				FirePropertyChanged("TopAlbumsFull");
				break;
			}
			Track track = newInterest as Track;
			if (CanAddTrack(track))
			{
				_topAlbums.Add(new ProfileTrack(category, (DataProviderObject)(object)track));
				FirePropertyChanged("TopAlbums");
			}
		}
	}

	private bool CanAddTrack(Track track)
	{
		if (track == null)
		{
			return false;
		}
		string albumTitle = track.AlbumTitle;
		if (string.IsNullOrEmpty(albumTitle))
		{
			return false;
		}
		Guid albumId = track.AlbumId;
		if (Guid.Empty == albumId)
		{
			return false;
		}
		foreach (ProfileTrack topAlbum in _topAlbums)
		{
			Guid albumId2 = ((Track)(object)topAlbum.Track).AlbumId;
			if (albumId2 == albumId)
			{
				return false;
			}
		}
		return true;
	}

	private void RecreateTopAlbums()
	{
		_topAlbums.Clear();
		AddNewTopAlbums(ProfileCategories.RecentlyPlayed, _recentPlays);
		AddNewTopAlbums(ProfileCategories.Favorites, _favorites);
		AddNewTopAlbums(ProfileCategories.TopArtists, _topArtists);
	}
}
