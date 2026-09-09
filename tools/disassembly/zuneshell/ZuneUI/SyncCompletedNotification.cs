namespace ZuneUI;

public class SyncCompletedNotification : MessageNotification
{
	private int _count;

	private string _pluralMessage;

	private string _deviceName;

	public string PluralMessage => _pluralMessage;

	public int Count => _count;

	public string DeviceName => _deviceName;

	public SyncCompletedNotification(string message, string pluralMessage, int count, string deviceName)
		: base(message, NotificationTask.Sync, NotificationState.OneShot)
	{
		_pluralMessage = pluralMessage;
		_count = count;
		_deviceName = deviceName;
	}
}
