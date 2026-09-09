using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class SongMatchData
{
	private AlbumMetadata _mergedAlbum;

	private TrackMetadata _track;

	private GroupedList _matchOptions;

	private int _originalMergedIndex;

	private int _selectedMatchIndex;

	public AlbumMetadata MergedAlbum => _mergedAlbum;

	public TrackMetadata Track => _track;

	public GroupedList MatchOptions => _matchOptions;

	public int OriginalMergedIndex => _originalMergedIndex;

	public int SelectedMatchIndex
	{
		get
		{
			return _selectedMatchIndex;
		}
		set
		{
			_selectedMatchIndex = value;
		}
	}

	internal SongMatchData(AlbumMetadata mergedAlbum, TrackMetadata track, GroupedList matchOptions, int originalMergedIndex, int selectedMatchIndex)
	{
		_mergedAlbum = mergedAlbum;
		_track = track;
		_matchOptions = matchOptions;
		_originalMergedIndex = originalMergedIndex;
		_selectedMatchIndex = selectedMatchIndex;
	}
}
