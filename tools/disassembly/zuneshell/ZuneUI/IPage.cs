namespace ZuneUI;

public interface IPage
{
	IPageState SaveAndRelease();

	void Release();

	void OnNavigatedTo();

	void OnNavigatedAway(IPage destination);
}
