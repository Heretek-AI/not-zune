using System;
using System.Threading;

namespace ZuneUI;

public abstract class NetworkSignInCredentials : IDisposable
{
	protected EventWaitHandle _dialogClosed;

	protected bool _completed;

	protected uint _cancelCount;

	protected uint _signInCount;

	protected uint CancelCount => _cancelCount;

	protected uint SignInCount => _signInCount;

	protected NetworkSignInCredentials()
	{
		_cancelCount = 0u;
	}

	public virtual void Dispose()
	{
		if (_dialogClosed != null)
		{
			_dialogClosed.Close();
		}
	}

	public abstract void Phase2Init();

	protected virtual void OnDialogSignIn(object sender, EventArgs args)
	{
		if (_dialogClosed != null)
		{
			_dialogClosed.Set();
		}
		_signInCount++;
		_completed = true;
	}

	protected virtual void OnDialogCanceled(object sender, EventArgs args)
	{
		if (_dialogClosed != null)
		{
			_dialogClosed.Set();
		}
		_cancelCount++;
		_completed = false;
	}
}
