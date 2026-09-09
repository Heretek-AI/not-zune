using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class AppReviewHelper : NotifyPropertyChangedImpl
{
	private static float s_maxRating = 10f;

	private static float s_minRating = 0f;

	private HRESULT _lastError;

	private string _lastAuthor;

	private AccountManagement _accountManagement;

	public static float MaxRating => s_maxRating;

	public static float MinRating => s_minRating;

	public HRESULT LastError
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _lastError;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (_lastError != value)
			{
				_lastError = value;
				FirePropertyChanged("LastError");
			}
		}
	}

	public string LastAuthor
	{
		get
		{
			return _lastAuthor;
		}
		private set
		{
			if (_lastAuthor != value)
			{
				_lastAuthor = value;
				FirePropertyChanged("LastAuthor");
			}
		}
	}

	public event EventHandler ReviewPosted;

	public event EventHandler ReviewPostFailed;

	public AppReviewHelper()
	{
		Reset();
	}

	public void AddReview(Guid mediaId, float rating, string title, string text)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Expected O, but got Unknown
		if (mediaId == Guid.Empty || rating < MinRating || rating > MaxRating)
		{
			OnReviewPostFailed(HRESULT._ZUNE_E_ADD_REVIEW_FAILED);
		}
		else
		{
			Service.Instance.PostAppReview(mediaId, title, text, (int)rating, new AsyncCompleteHandler(OnPostAddReviewComplete));
		}
	}

	private void OnPostAddReviewComplete(HRESULT hr)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(HandleAddReviewResponse), (object)hr, (DeferredInvokePriority)0);
	}

	private void Reset()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		LastAuthor = string.Empty;
		LastError = HRESULT._S_OK;
	}

	private void HandleAddReviewResponse(object args)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		HRESULT? val = (HRESULT?)args;
		if (!val.HasValue)
		{
			OnReviewPostFailed(HRESULT._ZUNE_E_ADD_REVIEW_FAILED);
			return;
		}
		HRESULT value = val.Value;
		if (((HRESULT)(ref value)).IsSuccess)
		{
			GetAuthorName();
		}
		else
		{
			OnReviewPostFailed(val.Value);
		}
	}

	private void GetAuthorName()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
		//IL_0037: Expected O, but got Unknown
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Expected O, but got Unknown
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Expected O, but got Unknown
		if (_accountManagement == null)
		{
			_accountManagement = new AccountManagement();
		}
		HRESULT account = _accountManagement.GetAccount((PassportIdentity)null, new GetAccountCompleteCallback(OnGetAccountSuccess), new AccountManagementErrorCallback(OnGetAccountError));
		if (((HRESULT)(ref account)).IsError)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(HandleGetAccountComplete), (object)string.Empty, (DeferredInvokePriority)0);
		}
	}

	private void OnGetAccountSuccess(AccountUser accountUser)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		string text = string.Empty;
		if (accountUser != null)
		{
			text = string.Format(Shell.LoadString(StringId.IDS_ACCOUNT_CREATION_NAME_FORMAT), accountUser.FirstName, accountUser.LastName);
		}
		Application.DeferredInvoke(new DeferredInvokeHandler(HandleGetAccountComplete), (object)text, (DeferredInvokePriority)0);
	}

	private void OnGetAccountError(HRESULT hr, ServiceError serviceError)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(HandleGetAccountComplete), (object)string.Empty, (DeferredInvokePriority)0);
	}

	private void HandleGetAccountComplete(object args)
	{
		LastAuthor = args as string;
		OnReviewPosted();
	}

	private void OnReviewPosted()
	{
		if (this.ReviewPosted != null)
		{
			this.ReviewPosted(this, null);
		}
		FirePropertyChanged("ReviewPosted");
	}

	private void OnReviewPostFailed(HRESULT hr)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		LastError = hr;
		if (this.ReviewPostFailed != null)
		{
			this.ReviewPostFailed(this, null);
		}
		FirePropertyChanged("ReviewPostFailed");
	}
}
