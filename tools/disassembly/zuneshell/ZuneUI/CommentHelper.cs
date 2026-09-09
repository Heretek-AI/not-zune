using System;
using System.Text;
using Microsoft.Iris;
using Microsoft.Zune.Messaging;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class CommentHelper : NotifyPropertyChangedImpl
{
	private string _author;

	private string _message;

	private DateTime _updated;

	private Guid _commentId;

	private bool _pendingAddComplete;

	public bool AddingComment
	{
		get
		{
			return _pendingAddComplete;
		}
		private set
		{
			if (_pendingAddComplete != value)
			{
				_pendingAddComplete = value;
				FirePropertyChanged("AddingComment");
			}
		}
	}

	public string CommentMessage
	{
		get
		{
			return _message;
		}
		private set
		{
			if (_message != value)
			{
				_message = value;
				FirePropertyChanged("CommentMessage");
			}
		}
	}

	public Guid CommentId
	{
		get
		{
			return _commentId;
		}
		private set
		{
			if (_commentId != value)
			{
				_commentId = value;
				FirePropertyChanged("CommentId");
			}
		}
	}

	public string CommentAuthor
	{
		get
		{
			return _author;
		}
		private set
		{
			if (_author != value)
			{
				_author = value;
				FirePropertyChanged("CommentAuthor");
			}
		}
	}

	public DateTime CommentUpdated
	{
		get
		{
			return _updated;
		}
		private set
		{
			if (_updated != value)
			{
				_updated = value;
				FirePropertyChanged("CommentUpdated");
			}
		}
	}

	public void AddComment(string recipient, string message)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		if (!AddingComment && !string.IsNullOrEmpty(recipient) && !string.IsNullOrEmpty(message) && SignIn.Instance.SignedIn)
		{
			AddingComment = true;
			CommentAuthor = SignIn.Instance.ZuneTag;
			CommentMessage = message;
			CommentId = Guid.Empty;
			CommentUpdated = DateTime.UtcNow;
			StringBuilder stringBuilder = new StringBuilder(Service.GetEndPointUri((EServiceEndpointId)28));
			stringBuilder.Append("/members/");
			stringBuilder.Append(Uri.EscapeDataString(recipient));
			stringBuilder.Append("/comments");
			if (!MessagingService.Instance.AddComment(stringBuilder.ToString(), recipient, _message, new CommentCallback(OnAddCommentCompleted)))
			{
				OnAddCommentCompleted(HRESULT._E_FAIL, Guid.Empty);
			}
		}
	}

	public void DeleteComment(string profileTag, Guid commentId)
	{
		if (commentId != Guid.Empty && SignIn.Instance.SignedIn)
		{
			StringBuilder stringBuilder = new StringBuilder(Service.GetEndPointUri((EServiceEndpointId)28));
			stringBuilder.Append("/members/");
			stringBuilder.Append(Uri.EscapeDataString(profileTag));
			stringBuilder.Append("/comments/");
			stringBuilder.Append(Uri.EscapeDataString(commentId.ToString()));
			MessagingService.Instance.DeleteComment(stringBuilder.ToString(), profileTag, (MessagingCallback)null);
		}
	}

	private void OnAddCommentCompleted(HRESULT hr, Guid commentId)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(OnAddCommentCompletedDeferred), (object)new object[2] { hr, commentId });
	}

	private void OnAddCommentCompletedDeferred(object args)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		object[] array = (object[])args;
		HRESULT val = (HRESULT)array[0];
		Guid commentId = (Guid)array[1];
		if (((HRESULT)(ref val)).IsSuccess)
		{
			CommentId = commentId;
			CommentUpdated = DateTime.UtcNow;
		}
		else
		{
			Shell.ShowErrorDialog(((HRESULT)(ref val)).Int, "Error");
		}
		AddingComment = false;
	}
}
