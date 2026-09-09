using System;
using System.Collections.Generic;
using Microsoft.Iris;

namespace ZuneUI;

public class NotificationArea : ModelItem
{
	private bool _paused;

	private List<Notification> _notificationList;

	private Notification _currentNotification;

	private Notification _overrideNotification;

	private Timer _timer;

	private static NotificationArea _singletonInstance;

	public static NotificationArea Instance
	{
		get
		{
			if (_singletonInstance == null)
			{
				_singletonInstance = new NotificationArea((IModelItemOwner)(object)ZuneShell.DefaultInstance);
			}
			return _singletonInstance;
		}
	}

	public Notification CurrentNotification
	{
		get
		{
			if (_overrideNotification != null)
			{
				return _overrideNotification;
			}
			return _currentNotification;
		}
		private set
		{
			if (_currentNotification != value)
			{
				_currentNotification = value;
				if (_currentNotification != null)
				{
					TickTimer.Interval = _currentNotification.DisplayTime;
				}
				if (_overrideNotification == null)
				{
					((ModelItem)this).FirePropertyChanged("CurrentNotification");
				}
			}
		}
	}

	public bool NotificationsReady => _notificationList.Count > 0;

	public int NotificationCount => _notificationList.Count;

	private Timer TickTimer
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Expected O, but got Unknown
			if (_timer == null)
			{
				_timer = new Timer((IModelItemOwner)(object)this, "Notification Area Change Timer");
				_timer.Interval = 600000;
				_timer.AutoRepeat = true;
				_timer.Tick += OnTick;
			}
			return _timer;
		}
	}

	public bool Paused
	{
		get
		{
			return _paused;
		}
		set
		{
			if (_paused != value)
			{
				_paused = value;
				if (_paused)
				{
					TickTimer.Enabled = false;
				}
				else if (NotificationsReady)
				{
					TickTimer.Enabled = true;
				}
			}
		}
	}

	private NotificationArea(IModelItemOwner owner)
		: base(owner)
	{
		_notificationList = new List<Notification>();
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			if (_timer != null)
			{
				_timer.Tick -= OnTick;
			}
			for (int i = 0; i < _notificationList.Count; i++)
			{
				if (_notificationList[i] != null)
				{
					((ModelItem)_notificationList[i]).Dispose();
				}
			}
			_currentNotification = null;
			if (_overrideNotification != null)
			{
				((ModelItem)_overrideNotification).Dispose();
				_overrideNotification = null;
			}
		}
		((ModelItem)this).OnDispose(disposing);
		_singletonInstance = null;
	}

	public void Add(Notification notification)
	{
		if (notification == null || _notificationList.Contains(notification))
		{
			return;
		}
		if (!NotificationsReady)
		{
			_notificationList.Add(notification);
			if (!Paused)
			{
				TickTimer.Start();
			}
		}
		else
		{
			int num = _notificationList.IndexOf(_currentNotification);
			if (num >= 0 && num < _notificationList.Count)
			{
				_notificationList.Insert(num, notification);
			}
			else
			{
				_notificationList.Add(notification);
			}
		}
		((ModelItem)this).FirePropertyChanged("NotificationCount");
		if (!Paused || CurrentNotification == null)
		{
			CurrentNotification = notification;
		}
	}

	public void Override(Notification notification)
	{
		if (_overrideNotification != notification)
		{
			if (_overrideNotification != null)
			{
				((ModelItem)_overrideNotification).Dispose();
			}
			_overrideNotification = notification;
			((ModelItem)this).FirePropertyChanged("CurrentNotification");
			if (TickTimer.Enabled)
			{
				TickTimer.Stop();
			}
		}
	}

	public void EndOverride(Notification notification)
	{
		if (notification != null && _overrideNotification == notification)
		{
			Override(null);
			if (NotificationsReady)
			{
				TickTimer.Start();
			}
		}
	}

	public void ClearAll()
	{
		TickTimer.Stop();
		_notificationList.Clear();
		CurrentNotification = null;
	}

	public void RemoveAll(NotificationTask taskType, NotificationState notificationType)
	{
		for (int num = _notificationList.Count - 1; num >= 0; num--)
		{
			if (_notificationList[num].Task == taskType && _notificationList[num].Type == notificationType)
			{
				if (_notificationList[num] == _currentNotification)
				{
					IncrementNotification();
				}
				((ModelItem)_notificationList[num]).Dispose();
				_notificationList.RemoveAt(num);
				if (!NotificationsReady)
				{
					CurrentNotification = null;
					TickTimer.Stop();
				}
			}
		}
	}

	public void Remove(Notification notification)
	{
		if (notification != null && !((ModelItem)notification).IsDisposed)
		{
			if (notification == _currentNotification)
			{
				IncrementNotification();
			}
			if (_notificationList.Remove(notification))
			{
				((ModelItem)notification).Dispose();
			}
			if (!NotificationsReady)
			{
				CurrentNotification = null;
				TickTimer.Stop();
			}
		}
	}

	public void Replace(Notification oldNotification, Notification newNotification)
	{
		if (oldNotification == null || newNotification == null || ((ModelItem)oldNotification).IsDisposed || ((ModelItem)newNotification).IsDisposed)
		{
			return;
		}
		int num = _notificationList.IndexOf(oldNotification);
		if (num >= 0)
		{
			_notificationList.RemoveAt(num);
			_notificationList.Insert(num, newNotification);
			if (oldNotification == _currentNotification)
			{
				CurrentNotification = newNotification;
			}
			((ModelItem)oldNotification).Dispose();
		}
	}

	public void ForceToFront(Notification notification)
	{
		int num = _notificationList.IndexOf(notification);
		if (num >= 0)
		{
			if (notification == _currentNotification)
			{
				TickTimer.Stop();
				TickTimer.Interval = _currentNotification.DisplayTime;
				TickTimer.Start();
			}
			else
			{
				CurrentNotification = notification;
			}
		}
	}

	private void OnTick(object sender, EventArgs args)
	{
		if (_currentNotification != null)
		{
			Notification currentNotification = _currentNotification;
			IncrementNotification();
			currentNotification.IncrementDisplayCount();
		}
	}

	public void IncrementNotification()
	{
		int num = _notificationList.IndexOf(_currentNotification);
		num++;
		if (num >= _notificationList.Count)
		{
			num = 0;
		}
		if (num >= 0 && num < _notificationList.Count)
		{
			CurrentNotification = _notificationList[num];
		}
	}
}
