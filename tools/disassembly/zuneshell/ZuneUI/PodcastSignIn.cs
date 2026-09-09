using System;
using System.Net;
using Microsoft.Zune.Subscription;
using UIXControls;

namespace ZuneUI;

public class PodcastSignIn : NetworkSignInDialogHelper
{
	private SubscriptonCredentialRequestArguments _credentials;

	private string _seriesTitle;

	public string TargetUrl => _credentials.TargetUrl;

	public override string UserName
	{
		get
		{
			return _credentials.Credential.UserName;
		}
		set
		{
			_credentials.Credential.UserName = value;
		}
	}

	public override string Password
	{
		get
		{
			return _credentials.Credential.Password;
		}
		set
		{
			_credentials.Credential.Password = value;
		}
	}

	public string SeriesTitle => _seriesTitle;

	public PodcastSignIn(SubscriptonCredentialRequestArguments credentials, EventHandler signInHandler, EventHandler cancelHandler)
		: base(signInHandler, cancelHandler, "res://ZuneShellResources!PodcastDialogs.uix#PodcastSignInDialogUI")
	{
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		_credentials = credentials;
		((DialogHelper)this).Cancel.Invoked += OnInvoked;
		if (_credentials.SubscriptionMediaId > 0)
		{
			_seriesTitle = PlaylistManager.GetFieldValue(_credentials.SubscriptionMediaId, (EListType)6, 344, string.Empty);
		}
		else
		{
			_seriesTitle = string.Empty;
		}
		_title = Shell.LoadString(StringId.IDS_PODCAST_SIGN_IN_TITLE);
		Uri uri = new Uri(_credentials.TargetUrl);
		_hostName = uri.Scheme + Uri.SchemeDelimiter + uri.Host;
		_realmName = _credentials.Realm;
		_credentials.Credential = new NetworkCredential();
		_credentials.Credential.UserName = _credentials.LastUserName;
		if (_credentials.LastError < 0)
		{
			SetError(HRESULT.op_Implicit(_credentials.LastError));
		}
		SetWarning(credentials);
	}

	protected override void OnInvoked(object sender, EventArgs args)
	{
		_credentials.Save = _rememberPassword.Value;
		base.OnInvoked(sender, args);
	}

	private void SetWarning(SubscriptonCredentialRequestArguments credentials)
	{
		string text = null;
		if (!credentials.IsAuthenticationSchemeSafe)
		{
			text = AuthSchemeToString(credentials.AuthScheme);
		}
		if (text != null)
		{
			_warning = string.Format(Shell.LoadString(StringId.IDS_PODCAST_SIGN_IN_INSECURE_AUTH_WARNING), text);
		}
		else
		{
			_warning = null;
		}
	}
}
