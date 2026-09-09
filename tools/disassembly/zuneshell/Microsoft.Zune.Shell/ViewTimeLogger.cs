using System;
using Microsoft.Zune.Util;

namespace Microsoft.Zune.Shell;

public class ViewTimeLogger
{
	private class LogHelper
	{
		private readonly SQMDataId _logId;

		private DateTime _start;

		public SQMDataId LogId => _logId;

		public LogHelper(SQMDataId logId)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			_logId = logId;
		}

		public void Start()
		{
			_start = DateTime.Now;
		}

		public void Stop()
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			if (_start != DateTime.MinValue)
			{
				TimeSpan timeSpan = DateTime.Now.Subtract(_start);
				SQMLog.Log(_logId, (int)timeSpan.TotalSeconds);
				_start = DateTime.MinValue;
			}
		}
	}

	private bool _inCompactMode;

	private bool _shutdown;

	private LogHelper _compactModeLog = new LogHelper((SQMDataId)209);

	private LogHelper _mixViewLog = new LogHelper((SQMDataId)185);

	private LogHelper _collectionViewLog;

	private static ViewTimeLogger _viewTimeLogger;

	public static ViewTimeLogger Instance
	{
		get
		{
			if (_viewTimeLogger == null)
			{
				_viewTimeLogger = new ViewTimeLogger();
			}
			return _viewTimeLogger;
		}
	}

	private ViewTimeLogger()
	{
	}

	public void Shutdown()
	{
		_compactModeLog.Stop();
		_mixViewLog.Stop();
		if (_collectionViewLog != null)
		{
			_collectionViewLog.Stop();
		}
		_shutdown = true;
	}

	public void ViewChanged(SQMDataId viewSQMId)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		if (_collectionViewLog != null && viewSQMId != _collectionViewLog.LogId)
		{
			_collectionViewLog.Stop();
			_collectionViewLog = null;
		}
		if ((int)viewSQMId != 0 && _collectionViewLog == null)
		{
			_collectionViewLog = new LogHelper(viewSQMId);
			if (!_inCompactMode)
			{
				_collectionViewLog.Start();
			}
		}
	}

	public void InCompactMode(bool inCompactMode)
	{
		_inCompactMode = inCompactMode;
		if (_inCompactMode)
		{
			_compactModeLog.Start();
			if (_collectionViewLog != null)
			{
				_collectionViewLog.Stop();
			}
		}
		else
		{
			_compactModeLog.Stop();
			if (_collectionViewLog != null)
			{
				_collectionViewLog.Start();
			}
		}
	}

	public void EnterMixView()
	{
		_mixViewLog.Start();
	}

	public void LeaveMixView()
	{
		_mixViewLog.Stop();
	}
}
