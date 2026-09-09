using System;
using System.Net;
using Microsoft.Zune.UserCredential;
using UIXControls;

namespace ZuneUI;

public class ProxySignIn : NetworkSignInDialogHelper
{
	private UserCredentialRequestArguments _credentials;

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

	public ProxySignIn(UserCredentialRequestArguments credentials, EventHandler signInHandler, EventHandler cancelHandler)
		: base(signInHandler, cancelHandler, "res://ZuneShellResources!SignInDialog.uix#ProxySignInDialogUI")
	{
		_credentials = credentials;
		((DialogHelper)this).Cancel.Invoked += CancelInvoked;
		_credentials.Credential = new NetworkCredential();
		_credentials.Credential.UserName = _credentials.LastUserName;
		if (_credentials.LastError < 0)
		{
			_error = Shell.LoadString(StringId.IDS_PROXY_LOGIN_BAD_CREDENTIALS);
		}
		_title = Shell.LoadString(StringId.IDS_PROXY_LOGIN_TITLE);
		_realmName = _credentials.Realm;
		_hostName = _credentials.Host;
		SetWarning(credentials);
	}

	private void CancelInvoked(object sender, EventArgs args)
	{
		UserName = string.Empty;
		Password = string.Empty;
		((DialogHelper)this).Hide();
	}

	protected override void OnInvoked(object sender, EventArgs args)
	{
		_credentials.Save = _rememberPassword.Value;
		base.OnInvoked(sender, args);
	}

	private void SetWarning(UserCredentialRequestArguments credentials)
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
