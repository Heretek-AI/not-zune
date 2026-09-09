namespace ZuneUI;

public class VideoViewCategory
{
	private string _view;

	private string _category;

	public string View => _view;

	public string Category => _category;

	public VideoViewCategory(string view, string category)
	{
		_view = view;
		_category = category;
	}
}
