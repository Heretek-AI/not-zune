using Microsoft.Iris;

namespace ZuneUI;

public class ProgressNotification : MessageNotification
{
	private int _percentage;

	public int Percentage
	{
		get
		{
			return _percentage;
		}
		set
		{
			if (_percentage != value)
			{
				_percentage = value;
				((ModelItem)this).FirePropertyChanged("Percentage");
			}
		}
	}

	public ProgressNotification(string message, NotificationTask taskType, NotificationState notificationType)
		: base(message, taskType, notificationType)
	{
	}

	public ProgressNotification(string message, NotificationTask taskType, NotificationState notificationType, int percentage)
		: base(message, taskType, notificationType)
	{
		_percentage = percentage;
	}
}
