namespace ZuneUI;

public abstract class DialogPage : ZunePage
{
	public abstract bool IsWizard { get; }

	public abstract bool AllowAdvance { get; set; }

	public abstract bool AllowCancel { get; set; }

	public abstract bool RequireSecurityIcon { get; set; }

	public abstract void Save();

	public abstract void Exit();

	public abstract void SaveAndExit();

	public abstract void CancelAndExit();

	public abstract void NavigatePage(bool forward);

	public abstract bool NavigationAvailable(bool forward);
}
