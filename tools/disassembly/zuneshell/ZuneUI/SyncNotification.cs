using MicrosoftZuneLibrary;

namespace ZuneUI;

public class SyncNotification : ProgressNotification
{
	private bool _syncCanceled;

	private UIDevice _device;

	private static string _syncing = Shell.LoadString(StringId.IDS_SYNC_STATUS);

	private static string _transcoding = Shell.LoadString(StringId.IDS_TRANSCODE_STATUS);

	private static string _deleting = Shell.LoadString(StringId.IDS_DELETING_STATUS);

	private static string _updating = Shell.LoadString(StringId.IDS_UPDATING_STATUS);

	private static string _tunnelling = Shell.LoadString(StringId.IDS_TUNNELLING_STATUS);

	private static string _group = Shell.LoadString(StringId.IDS_SYNC_STATUS_GROUP);

	private static string _ellipsis = Shell.LoadString(StringId.IDS_GENERIC_ELLIPSIS);

	public bool SyncCanceled
	{
		get
		{
			return _syncCanceled;
		}
		set
		{
			if (_syncCanceled != value)
			{
				_syncCanceled = value;
			}
		}
	}

	public UIDevice Device => _device;

	public SyncNotification(UIDevice device)
		: base(Shell.LoadString(StringId.IDS_SYNC_HEADER), NotificationTask.Sync, NotificationState.Normal)
	{
		_device = device;
		UpdateProgress(0, 0, 0, "", "", (ESyncEngineState)0);
	}

	public void UpdateProgress(int percent, int percentItem, int percentTranscode, string group, string title, ESyncEngineState engineState)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Invalid comparison between Unknown and I4
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Invalid comparison between Unknown and I4
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Invalid comparison between Unknown and I4
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Invalid comparison between Unknown and I4
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Invalid comparison between Unknown and I4
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Invalid comparison between Unknown and I4
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Invalid comparison between Unknown and I4
		base.Percentage = percent;
		string format = (((int)engineState == 11) ? _transcoding : (((int)engineState != 9 && (int)engineState != 10) ? (((int)engineState == 6) ? _updating : (((int)engineState != 17 && (int)engineState != 16 && (int)engineState != 15) ? _syncing : ((!string.IsNullOrEmpty(title) || !string.IsNullOrEmpty(group)) ? _syncing : _tunnelling))) : _deleting));
		string arg;
		if (string.IsNullOrEmpty(group))
		{
			arg = title ?? string.Empty;
		}
		else if (string.IsNullOrEmpty(title))
		{
			arg = group;
		}
		else
		{
			string text = group;
			if (text.Length > 15)
			{
				text = string.Format(_ellipsis, text.Substring(0, 15).Trim());
			}
			arg = string.Format(_group, text, title);
		}
		base.SubMessage = string.Format(format, arg);
	}

	public void Complete(ESyncEventReason reason)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Invalid comparison between Unknown and I4
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Invalid comparison between Unknown and I4
		base.Percentage = 100;
		if ((int)reason == 1 || (int)reason == -1)
		{
			base.Message = Shell.LoadString(StringId.IDS_SYNC_ERROR_NOTIFICATION);
			base.SubMessage = null;
			return;
		}
		if (SyncCanceled)
		{
			base.Message = Shell.LoadString(StringId.IDS_SYNC_CANCELLED);
		}
		else
		{
			base.Message = Shell.LoadString(StringId.IDS_SYNC_COMPLETED);
		}
		long freeSpace = _device.ActualGasGauge.FreeSpace;
		if (freeSpace > 0)
		{
			if (freeSpace < SyncControls.DevicelandGigabyte)
			{
				base.SubMessage = string.Format(Shell.LoadString(StringId.IDS_FREE_SPACE_REMAINING_IN_MB), (float)freeSpace / (float)SyncControls.DevicelandMegabyte);
			}
			else
			{
				base.SubMessage = string.Format(Shell.LoadString(StringId.IDS_FREE_SPACE_REMAINING_IN_GB), (float)freeSpace / (float)SyncControls.DevicelandGigabyte);
			}
		}
	}
}
