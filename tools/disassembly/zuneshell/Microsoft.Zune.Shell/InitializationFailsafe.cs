using System.Threading;
using Microsoft.Iris;

namespace Microsoft.Zune.Shell;

internal class InitializationFailsafe
{
	private bool _complete;

	private DeferredInvokeHandler _onCompleteHandler;

	private Timer _failsafeTimer;

	public void Initialize(DeferredInvokeHandler onCompleteHandler)
	{
		if (_complete)
		{
			Application.DeferredInvoke(onCompleteHandler, (DeferredInvokePriority)1);
			return;
		}
		int dueTime = 30000;
		_onCompleteHandler = onCompleteHandler;
		_failsafeTimer = new Timer(FailsafeCallback, null, dueTime, -1);
	}

	public void Complete()
	{
		if (!_complete)
		{
			_complete = true;
			if (_onCompleteHandler != null)
			{
				Application.DeferredInvoke(_onCompleteHandler, (DeferredInvokePriority)1);
			}
			if (_failsafeTimer != null)
			{
				_failsafeTimer.Change(-1, -1);
			}
		}
	}

	private void FailsafeCallback(object state)
	{
		if (!_complete)
		{
			_complete = true;
			Application.DeferredInvoke(_onCompleteHandler, (DeferredInvokePriority)1);
		}
	}
}
