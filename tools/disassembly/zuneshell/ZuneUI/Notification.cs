using Microsoft.Iris;

namespace ZuneUI;

public class Notification : ModelItem
{
	private NotificationTask _task;

	private NotificationState _type;

	private uint _appearances;

	private int _displayTime;

	public NotificationTask Task
	{
		get
		{
			return _task;
		}
		set
		{
			if (_task != value)
			{
				_task = value;
				((ModelItem)this).FirePropertyChanged("Task");
			}
		}
	}

	public NotificationState Type
	{
		get
		{
			return _type;
		}
		set
		{
			if (_type != value)
			{
				_type = value;
				((ModelItem)this).FirePropertyChanged("Type");
			}
		}
	}

	public uint Appearances => _appearances;

	public int DisplayTime => _displayTime;

	public Notification(NotificationTask taskType, NotificationState notificationType)
		: this(taskType, notificationType, 0)
	{
	}

	public Notification(NotificationTask taskType, NotificationState notificationType, int displayTime)
		: base((IModelItemOwner)null)
	{
		_task = taskType;
		_type = notificationType;
		_appearances = 0u;
		_displayTime = ((displayTime <= 0) ? 5000 : displayTime);
	}

	public void IncrementDisplayCount()
	{
		if (_type == NotificationState.OneShot)
		{
			NotificationArea.Instance.Remove(this);
		}
		if (_type == NotificationState.Completed)
		{
			_appearances++;
			if (_appearances >= 5)
			{
				NotificationArea.Instance.Remove(this);
			}
		}
	}
}
