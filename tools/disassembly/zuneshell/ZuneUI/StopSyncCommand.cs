namespace ZuneUI;

public class StopSyncCommand : SyncCommandBase
{
	public StopSyncCommand()
	{
		_availableWhenSyncing = true;
	}

	protected override void OnInvoked()
	{
		if (base.Device != null)
		{
			base.Device.EndSync(userInitiated: true);
		}
		base.OnInvoked();
	}
}
