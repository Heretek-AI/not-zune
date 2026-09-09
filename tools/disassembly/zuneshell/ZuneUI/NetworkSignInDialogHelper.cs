using System;
using System.Net;
using Microsoft.Iris;
using Microsoft.Zune.ErrorMapperApi;
using UIXControls;

namespace ZuneUI;

public abstract class NetworkSignInDialogHelper : DialogHelper
{
	private string _helpUrl;

	protected string _error;

	protected string _title;

	protected string _warning;

	protected string _hostName;

	protected string _realmName;

	protected Command _signInCommand;

	protected BooleanChoice _rememberPassword;

	public string Title => _title;

	public Command SignIn => _signInCommand;

	public string Error => _error;

	public string HelpUrl => _helpUrl;

	public string Warning => _warning;

	public string RealmName => _realmName;

	public string HostName => _hostName;

	public BooleanChoice RememberPassword => _rememberPassword;

	public abstract string UserName { get; set; }

	public abstract string Password { get; set; }

	public NetworkSignInDialogHelper(EventHandler signInHandler, EventHandler cancelHandler, string contentUI)
		: base(contentUI)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		_signInCommand = new Command((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_DIALOG_OK), (EventHandler)OnInvoked);
		if (signInHandler != null)
		{
			_signInCommand.Invoked += signInHandler;
		}
		if (cancelHandler != null)
		{
			((DialogHelper)this).Cancel.Invoked += cancelHandler;
		}
		_rememberPassword = new BooleanChoice((IModelItemOwner)(object)this, Shell.LoadString(StringId.IDS_PODCAST_SIGN_IN_SAVE));
	}

	protected virtual void OnInvoked(object sender, EventArgs args)
	{
		((DialogHelper)this).Hide();
	}

	protected override void OnDispose(bool disposing)
	{
		((ModelItem)this).OnDispose(disposing);
		if (disposing)
		{
			if (_rememberPassword != null)
			{
				((ModelItem)_rememberPassword).Dispose();
				_rememberPassword = null;
			}
			if (_signInCommand != null)
			{
				((ModelItem)_signInCommand).Dispose();
				_signInCommand = null;
			}
		}
	}

	protected void SetError(HRESULT hrError)
	{
		if (((HRESULT)(ref hrError)).IsError)
		{
			ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hrError)).Int);
			if (mappedErrorDescriptionAndUrl != null)
			{
				_error = mappedErrorDescriptionAndUrl.Description;
				_helpUrl = mappedErrorDescriptionAndUrl.WebHelpUrl;
			}
		}
	}

	protected string AuthSchemeToString(AuthenticationSchemes authScheme)
	{
		return authScheme switch
		{
			AuthenticationSchemes.Basic => Shell.LoadString(StringId.IDS_PODCAST_SIGN_IN_BASIC_AUTH), 
			AuthenticationSchemes.Digest => Shell.LoadString(StringId.IDS_PODCAST_SIGN_IN_DIGEST_AUTH), 
			AuthenticationSchemes.Negotiate => Shell.LoadString(StringId.IDS_PODCAST_SIGN_IN_NEGOTIATE_AUTH), 
			AuthenticationSchemes.Ntlm => Shell.LoadString(StringId.IDS_PODCAST_SIGN_IN_NTLM_AUTH), 
			_ => null, 
		};
	}
}
