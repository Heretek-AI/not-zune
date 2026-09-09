using Microsoft.Iris;

namespace ZuneUI;

public class MessageNotification : Notification
{
	private string _message;

	private string _subMessage;

	public string Message
	{
		get
		{
			return _message;
		}
		set
		{
			if (_message != value)
			{
				_message = value;
				((ModelItem)this).FirePropertyChanged("Message");
			}
		}
	}

	public string SubMessage
	{
		get
		{
			return _subMessage;
		}
		set
		{
			if (_subMessage != value)
			{
				_subMessage = value;
				((ModelItem)this).FirePropertyChanged("SubMessage");
			}
		}
	}

	public MessageNotification(NotificationTask taskType, NotificationState notificationType)
		: this(null, null, taskType, notificationType, 0)
	{
	}

	public MessageNotification(NotificationTask taskType, NotificationState notificationType, int displayTime)
		: this(null, null, taskType, notificationType, displayTime)
	{
	}

	public MessageNotification(string message, NotificationTask taskType, NotificationState notificationType)
		: this(message, null, taskType, notificationType, 0)
	{
	}

	public MessageNotification(string message, NotificationTask taskType, NotificationState notificationType, int displayTime)
		: this(message, null, taskType, notificationType, displayTime)
	{
	}

	public MessageNotification(string message, string subMessage, NotificationTask taskType, NotificationState notificationType)
		: this(message, subMessage, taskType, notificationType, 0)
	{
	}

	public MessageNotification(string message, string subMessage, NotificationTask taskType, NotificationState notificationType, int displayTime)
		: base(taskType, notificationType, displayTime)
	{
		_message = message;
		_subMessage = subMessage;
	}
}
