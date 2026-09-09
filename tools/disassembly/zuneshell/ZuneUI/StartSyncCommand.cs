namespace ZuneUI;

public class StartSyncCommand : SyncCommandBase
{
	public StartSyncCommand()
	{
		_availableWhenSyncing = false;
	}

	protected override void OnInvoked()
	{
		if (base.Device != null)
		{
			base.Device.BeginSync(userInitiated: true, syncOnNextNotify: false);
		}
		base.OnInvoked();
	}
}
