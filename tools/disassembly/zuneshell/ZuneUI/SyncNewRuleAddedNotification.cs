using System;

namespace ZuneUI;

public class SyncNewRuleAddedNotification : MessageNotification
{
	private long _oldSize;

	private UIGasGauge _gauge;

	public long OldSize => Math.Min(Math.Max(_oldSize, 0L), PredictedGauge.TotalSpace);

	public UIGasGauge PredictedGauge => _gauge;

	public SyncNewRuleAddedNotification(string message, long startingSize, UIGasGauge gasGauge)
		: base(message, NotificationTask.Sync, NotificationState.OneShot)
	{
		_gauge = gasGauge;
		_oldSize = startingSize;
	}
}
