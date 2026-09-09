using System;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.UserCredential;
using UIXControls;

namespace ZuneUI;

public class ProxyCredentials : NetworkSignInCredentials
{
	private const int _maxCancelCount = 3;

	private const int _maxFailedCount = 3;

	private UserCredentialRequestArguments _credentials;

	private static ProxyCredentials _instance;

	private ProxySignIn _inputDialog;

	private string _lastUIPathPrompted;

	private string _lastSignInTargetUrl;

	public static ProxyCredentials Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new ProxyCredentials();
			}
			return _instance;
		}
	}

	public static bool HasInstance => _instance != null;

	private ProxyCredentials()
	{
	}

	public void Reset()
	{
		_lastUIPathPrompted = "";
		_signInCount = 0u;
		_cancelCount = 0u;
	}

	public override void Phase2Init()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		UserCredentialManager.Instance.SetCredentialHandler(new UserCredentialHandler(GetCredentials));
	}

	private bool GetCredentials(UserCredentialRequestArguments args)
	{
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Expected O, but got Unknown
		_completed = false;
		if (base.CancelCount >= 3 || base.SignInCount >= 3 || (_lastUIPathPrompted == ZuneShell.DefaultInstance.CurrentPage.UIPath && _lastSignInTargetUrl != args.TargetUrl))
		{
			if (args != null && args.Credential != null)
			{
				args.Credential.UserName = string.Empty;
				args.Credential.Password = string.Empty;
			}
		}
		else if (!Application.IsApplicationThread && _dialogClosed == null)
		{
			_dialogClosed = new EventWaitHandle(initialState: false, EventResetMode.ManualReset);
			Application.DeferredInvoke(new DeferredInvokeHandler(ShowDialog), (object)args);
			_dialogClosed.WaitOne();
			_dialogClosed.Close();
			_dialogClosed = null;
		}
		return _completed;
	}

	private void ShowDialog(object args)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		if (Application.IsApplicationThread && args is UserCredentialRequestArguments)
		{
			if (_inputDialog != null)
			{
				((ModelItem)_inputDialog).Dispose();
				_inputDialog = null;
			}
			_credentials = (UserCredentialRequestArguments)args;
			_inputDialog = new ProxySignIn(_credentials, OnDialogSignIn, OnDialogCanceled);
			((DialogHelper)_inputDialog).Show();
			_lastUIPathPrompted = ZuneShell.DefaultInstance.CurrentPage.UIPath;
		}
	}

	protected override void OnDialogSignIn(object sender, EventArgs args)
	{
		if (_credentials != null)
		{
			_lastSignInTargetUrl = _credentials.TargetUrl;
		}
		base.OnDialogSignIn(sender, args);
	}

	public override void Dispose()
	{
		UserCredentialManager.Instance.SetCredentialHandler((UserCredentialHandler)null);
		if (_inputDialog != null)
		{
			((ModelItem)_inputDialog).Dispose();
			_inputDialog = null;
		}
		base.Dispose();
	}
}
