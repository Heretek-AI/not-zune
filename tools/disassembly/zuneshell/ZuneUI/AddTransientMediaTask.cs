using System;
using System.Threading;
using Microsoft.Zune.Shell;

namespace ZuneUI;

internal class AddTransientMediaTask
{
	private string _filePath;

	private MediaType _mediaType;

	private int _dbMediaId;

	private bool _fFileAlreadyExists;

	private bool _fAddSuccessful;

	private ManualResetEvent _event;

	private AddTransientMediaTask(string filePath, MediaType mediaType)
	{
		_filePath = filePath;
		_mediaType = mediaType;
	}

	private void TaskFunction(object obj)
	{
		_dbMediaId = -1;
		_fFileAlreadyExists = false;
		_fAddSuccessful = false;
		if (!string.IsNullOrEmpty(_filePath))
		{
			_fAddSuccessful = ZuneApplication.AddTransientMedia(_filePath, _mediaType, out _dbMediaId, out _fFileAlreadyExists);
		}
		if (_event != null)
		{
			_event.Set();
		}
	}

	private bool RunSyncTask(TimeSpan timeout, out int dbMediaId, out bool fFileAlreadyExists, out bool fTimedout)
	{
		dbMediaId = -1;
		fFileAlreadyExists = false;
		fTimedout = false;
		bool result = false;
		if (!string.IsNullOrEmpty(_filePath))
		{
			if (timeout.TotalSeconds > 0.0)
			{
				_event = new ManualResetEvent(initialState: false);
				ThreadPool.QueueUserWorkItem(TaskFunction);
				if (_event.WaitOne(timeout, exitContext: false))
				{
					dbMediaId = _dbMediaId;
					fFileAlreadyExists = _fFileAlreadyExists;
					result = _fAddSuccessful;
				}
				else
				{
					fTimedout = true;
				}
			}
			else
			{
				TaskFunction(null);
				dbMediaId = _dbMediaId;
				fFileAlreadyExists = _fFileAlreadyExists;
				result = _fAddSuccessful;
			}
		}
		return result;
	}

	public static bool AddTransientMediaWithTimeout(string filePath, MediaType mediaType, TimeSpan timeout, out int dbMediaId, out bool fFileAlreadyExists, out bool fTimedout)
	{
		AddTransientMediaTask addTransientMediaTask = new AddTransientMediaTask(filePath, mediaType);
		return addTransientMediaTask.RunSyncTask(timeout, out dbMediaId, out fFileAlreadyExists, out fTimedout);
	}
}
