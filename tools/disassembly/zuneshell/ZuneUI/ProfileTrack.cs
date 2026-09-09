using Microsoft.Iris;

namespace ZuneUI;

public class ProfileTrack
{
	private Category _category;

	private DataProviderObject _track;

	public Category Category => _category;

	public DataProviderObject Track => _track;

	public ProfileTrack(Category category, DataProviderObject track)
	{
		_category = category;
		_track = track;
	}
}
