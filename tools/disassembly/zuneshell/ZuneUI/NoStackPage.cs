namespace ZuneUI;

public class NoStackPage : ZunePage
{
	public override IPageState SaveAndRelease()
	{
		Release();
		return null;
	}
}
