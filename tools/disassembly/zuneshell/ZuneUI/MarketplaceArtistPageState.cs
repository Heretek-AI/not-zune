namespace ZuneUI;

public class MarketplaceArtistPageState
{
	private string _pivot;

	private string _album;

	private bool _showSongs;

	public string SelectedPivotId
	{
		get
		{
			return _pivot;
		}
		set
		{
			_pivot = value;
		}
	}

	public string SelectedAlbumId
	{
		get
		{
			return _album;
		}
		set
		{
			_album = value;
		}
	}

	public bool ShowSongs
	{
		get
		{
			return _showSongs;
		}
		set
		{
			_showSongs = value;
		}
	}

	public MarketplaceArtistPageState(string selectedPivotId, string selectedAlbumId, bool showSongs)
	{
		_pivot = selectedPivotId;
		_album = selectedAlbumId;
		_showSongs = showSongs;
	}
}
