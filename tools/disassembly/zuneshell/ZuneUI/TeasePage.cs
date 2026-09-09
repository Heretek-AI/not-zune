namespace ZuneUI;

public class TeasePage : TestPage
{
	protected override void OnNavigatedToWorker()
	{
		base.OnNavigatedToWorker();
		ZuneShell defaultInstance = ZuneShell.DefaultInstance;
		defaultInstance.NavigateBack();
	}
}
