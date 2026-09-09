using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Subscription;
using UIXControls;

namespace ZuneUI;

public class PodcastCredentials : NetworkSignInCredentials
{
	private static PodcastCredentials _instance;

	private PodcastSignIn _inputDialog;

	public static PodcastCredentials Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new PodcastCredentials();
			}
			return _instance;
		}
	}

	public static bool HasInstance => _instance != null;

	private PodcastCredentials()
	{
	}

	public override void Phase2Init()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		SubscriptionManager.Instance.SetCredentialHandler((EMediaTypes)18, new SubscriptionCredentialHandler(GetCredentials));
	}

	private bool GetCredentials(SubscriptonCredentialRequestArguments args)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		_completed = false;
		if (!Application.IsApplicationThread && _dialogClosed == null)
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
		//IL_004f: Expected O, but got Unknown
		if (Application.IsApplicationThread && args is SubscriptonCredentialRequestArguments)
		{
			if (_inputDialog != null)
			{
				((ModelItem)_inputDialog).Dispose();
				_inputDialog = null;
			}
			_inputDialog = new PodcastSignIn((SubscriptonCredentialRequestArguments)args, OnDialogSignIn, OnDialogCanceled);
			((DialogHelper)_inputDialog).Show();
		}
	}

	public override void Dispose()
	{
		SubscriptionManager.Instance.SetCredentialHandler((EMediaTypes)18, (SubscriptionCredentialHandler)null);
		if (_inputDialog != null)
		{
			((ModelItem)_inputDialog).Dispose();
			_inputDialog = null;
		}
		base.Dispose();
	}
}
