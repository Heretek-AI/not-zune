using System;
using System.Collections;
using System.Text;
using Microsoft.Iris;
using Microsoft.Zune.ErrorMapperApi;
using Microsoft.Zune.Messaging;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public class ComposerHelper : DialogHelper
{
	private class SendCompletedParams
	{
		private readonly HRESULT _hr;

		private readonly bool _wishlist;

		public HRESULT HR => _hr;

		public bool Wishlist => _wishlist;

		public SendCompletedParams(HRESULT hr, bool wishlist)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			_hr = hr;
			_wishlist = wishlist;
		}
	}

	private class FavoritesManagementState
	{
		public FavoritesAction Action;

		public int FavoritesCount;

		public HRESULT MgmtOperationResult;

		public FavoritesManagementState(FavoritesAction action, int favoritesCount)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			Action = action;
			FavoritesCount = favoritesCount;
		}
	}

	private const int c_maxMessageLength = 230;

	private RecipientHelper _recipientHelper;

	private Command _sendFailed;

	private Command _sendSucceeded;

	private string _message;

	private string _serviceContext;

	public RecipientHelper RecipientHelper => _recipientHelper;

	public int MaxMessageLength => 230;

	public Command SendFailed => _sendFailed;

	public Command SendSucceeded => _sendSucceeded;

	public string Message
	{
		get
		{
			return _message;
		}
		set
		{
			if (_message != value)
			{
				if (value != null)
				{
					_message = value.Replace("\r\n", "\n");
				}
				else
				{
					_message = value;
				}
				((ModelItem)this).FirePropertyChanged("Message");
			}
		}
	}

	public string ServiceContext
	{
		get
		{
			return _serviceContext;
		}
		set
		{
			if (_serviceContext != value)
			{
				_serviceContext = value;
				((ModelItem)this).FirePropertyChanged("ServiceContext");
			}
		}
	}

	public ComposerHelper()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		_recipientHelper = new RecipientHelper();
		_sendFailed = new Command();
		_sendSucceeded = new Command();
	}

	public void ClearState()
	{
		_recipientHelper.ClearState();
		Message = string.Empty;
	}

	public bool Send(Attachment attachment)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		string endPointUri = Service.GetEndPointUri((EServiceEndpointId)5);
		string zuneTag = SignIn.Instance.ZuneTag;
		Uri.EscapeDataString(",");
		string errorMessage = null;
		HRESULT val = HRESULT._S_OK;
		bool flag = true;
		bool wishlist = false;
		bool allowEmailRecipients = true;
		if (attachment != null)
		{
			wishlist = attachment.Wishlist;
			allowEmailRecipients = attachment.AllowEmailRecipients;
			if (!attachment.IsValid(out errorMessage))
			{
				flag = false;
				val = HRESULT._NS_E_MESSAGING_CLIENT_ERROR;
			}
		}
		if (flag && !RecipientHelper.ValidateAll(allowEmailRecipients))
		{
			flag = false;
			val = HRESULT._NS_E_MESSAGING_RECIPIENT_ERROR;
			errorMessage = RecipientHelper.ErrorMessage;
		}
		if (!SignIn.Instance.SignedIn || string.IsNullOrEmpty(endPointUri) || string.IsNullOrEmpty(zuneTag))
		{
			flag = false;
		}
		if (flag)
		{
			string requestUrl = $"{endPointUri}/messaging/{Uri.EscapeDataString(zuneTag)}/send";
			flag = Send(requestUrl, attachment, wishlist);
		}
		if (!flag)
		{
			val = (((HRESULT)(ref val)).IsError ? val : HRESULT._NS_E_MESSAGING_CLIENT_ERROR);
			OnSendCompleted(val, errorMessage, wishlist);
		}
		return flag;
	}

	private bool Send(string requestUrl, Attachment attachment, bool wishlist)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Expected O, but got Unknown
		bool flag = false;
		if (attachment is PropertySetAttachment)
		{
			PropertySetAttachment propertySetAttachment = (PropertySetAttachment)attachment;
			string text = CreateRecipientString(uriEscape: false);
			flag = MessagingService.Instance.Compose(requestUrl, Message, text, propertySetAttachment.RequestType, propertySetAttachment.PropertySet, new MessagingCallback(OnSendCompletedAsync), (object)wishlist);
		}
		else
		{
			string text2 = null;
			try
			{
				text2 = CreateUrlEncodedString(attachment);
			}
			catch (ArgumentNullException)
			{
			}
			if (text2 != null)
			{
				flag = MessagingService.Instance.Compose(requestUrl, text2, new MessagingCallback(OnSendCompletedAsync), (object)wishlist);
			}
		}
		if (flag)
		{
			attachment?.LogSend();
		}
		return flag;
	}

	public static bool ManageFriend(FriendAction action, string zuneTag)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		bool flag = !string.IsNullOrEmpty(zuneTag);
		string text = null;
		if (flag)
		{
			text = CreateOperationUri("friends");
			flag = text != null;
		}
		if (flag)
		{
			flag = MessagingService.Instance.ManageFriend(action, text, zuneTag);
		}
		return flag;
	}

	public bool ManageFavorites(FavoritesAction action, MediaType mediaType, IList favorites)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected I4, but got Unknown
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		string value = null;
		string value2 = null;
		string text = null;
		StringBuilder stringBuilder = null;
		bool flag = true;
		text = CreateOperationUri("playlists/BuiltIn-FavoriteTracks");
		flag = text != null;
		switch (action - 1)
		{
		case 0:
			value = "add";
			break;
		case 1:
			value = "delete";
			break;
		default:
			flag = false;
			break;
		}
		if (mediaType == MediaType.Track)
		{
			value2 = "track";
		}
		else
		{
			flag = false;
		}
		if (flag)
		{
			stringBuilder = new StringBuilder();
			stringBuilder.Append(value);
			stringBuilder.Append(' ');
			stringBuilder.Append(value2);
			stringBuilder.Append(' ');
			for (int i = 0; i < favorites.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(favorites[i].ToString());
			}
			flag = MessagingService.Instance.ManageFavorites(action, text, stringBuilder.ToString(), new MessagingCallback(OnFavoritesManagementCompletedAsync), (object)new FavoritesManagementState(action, favorites.Count));
		}
		return flag;
	}

	public static bool ManageProfile(string fieldName, string fieldValue, MessagingCallback callback, object state)
	{
		bool flag = !string.IsNullOrEmpty(fieldName);
		string text = null;
		if (flag)
		{
			text = CreateOperationUri(fieldName);
			flag = text != null;
		}
		if (flag)
		{
			flag = MessagingService.Instance.ManageProfile(text, fieldValue, callback, state);
		}
		return flag;
	}

	public static bool ManageProfileImage(ProfileImage image, MessagingCallback callback, object state)
	{
		bool flag = !string.IsNullOrEmpty(image.TypeName);
		string text = null;
		if (flag)
		{
			text = CreateOperationUri(image.TypeName);
			flag = text != null;
		}
		if (flag)
		{
			flag = ((image.Image != null) ? MessagingService.Instance.ManageProfileImage(text, (SafeBitmap)(object)image.Image, callback, state) : (image.ResourceId != null && MessagingService.Instance.ManageProfileImage(text, image.ResourceId, callback, state)));
		}
		if (flag)
		{
			if (image.Type == ProfileImageType.Background)
			{
				SQMLog.Log((SQMDataId)70, 1);
			}
			else if (image.Type == ProfileImageType.Tile)
			{
				SQMLog.Log((SQMDataId)69, 1);
			}
		}
		return flag;
	}

	public static string CreateOperationUri(string operation)
	{
		string zuneTagOrGuid = null;
		Guid userGuid = SignIn.Instance.UserGuid;
		if (!GuidHelper.IsEmpty(userGuid))
		{
			zuneTagOrGuid = userGuid.ToString();
		}
		return CreateOperationUri(operation, zuneTagOrGuid);
	}

	public static string GetUserTileUri(string zuneTag)
	{
		return CreateOperationUri("usertile", zuneTag);
	}

	public static string CreateOperationUri(string operation, string zuneTagOrGuid)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (string.IsNullOrEmpty(zuneTagOrGuid))
		{
			return null;
		}
		EServiceEndpointId val = (EServiceEndpointId)((operation == "comments") ? 28 : 3);
		string endPointUri = Service.GetEndPointUri(val);
		if (string.IsNullOrEmpty(endPointUri))
		{
			return null;
		}
		zuneTagOrGuid = Uri.EscapeUriString(zuneTagOrGuid);
		StringBuilder stringBuilder = new StringBuilder(endPointUri);
		stringBuilder.Append("/members");
		if (operation == "search")
		{
			return UrlHelper.MakeUrl(stringBuilder.ToString(), "q", zuneTagOrGuid);
		}
		stringBuilder.Append("/");
		stringBuilder.Append(zuneTagOrGuid);
		if (!string.IsNullOrEmpty(operation))
		{
			stringBuilder.Append("/");
			stringBuilder.Append(operation);
		}
		return stringBuilder.ToString();
	}

	private string CreateUrlEncodedString(Attachment attachment)
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (attachment != null)
		{
			string[] properties = attachment.Properties;
			for (int i = 0; i < properties.Length; i++)
			{
				if (properties[i] == null)
				{
					throw new ArgumentNullException();
				}
			}
		}
		stringBuilder.Append("recipients=");
		stringBuilder.Append(CreateRecipientString(uriEscape: true));
		if (!string.IsNullOrEmpty(_message) && _message.Length <= 230)
		{
			stringBuilder.Append("&text=");
			stringBuilder.Append(Uri.EscapeDataString(_message));
		}
		if (attachment != null)
		{
			string[] properties2 = attachment.Properties;
			int num = properties2.Length - 1;
			for (int j = 0; j < num; j += 2)
			{
				stringBuilder.Append("&");
				stringBuilder.Append(Uri.EscapeDataString(properties2[j]));
				stringBuilder.Append("=");
				stringBuilder.Append(Uri.EscapeDataString(properties2[j + 1]));
			}
		}
		else
		{
			stringBuilder.Append("&type=message");
		}
		if (!string.IsNullOrEmpty(_serviceContext))
		{
			stringBuilder.Append("&context=");
			stringBuilder.Append(Uri.EscapeDataString(_serviceContext));
		}
		return stringBuilder.ToString();
	}

	private string CreateRecipientString(bool uriEscape)
	{
		StringBuilder stringBuilder = new StringBuilder();
		IList validRecipients = RecipientHelper.ValidRecipients;
		string value = ",";
		if (validRecipients != null)
		{
			for (int i = 0; i < validRecipients.Count; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(value);
				}
				if (uriEscape)
				{
					stringBuilder.Append(Uri.EscapeDataString((string)validRecipients[i]));
				}
				else
				{
					stringBuilder.Append((string)validRecipients[i]);
				}
			}
		}
		return stringBuilder.ToString();
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing && _recipientHelper != null)
		{
			((ModelItem)_recipientHelper).Dispose();
			_recipientHelper = null;
		}
		((ModelItem)this).OnDispose(disposing);
	}

	private void OnSendCompleted(HRESULT hr, string dialogMessage, bool wishlist)
	{
		if (((HRESULT)(ref hr)).IsError)
		{
			if (wishlist)
			{
				ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(((HRESULT)(ref hr)).Int, (eErrorCondition)6);
				ErrorDialogInfo.Show(mappedErrorDescriptionAndUrl.Hr, Shell.LoadString(StringId.IDS_CART_CANT_ADD_ITEMS), mappedErrorDescriptionAndUrl.Description);
			}
			else
			{
				ErrorDialogInfo.Show(((HRESULT)(ref hr)).Int, ((ModelItem)this).Description, dialogMessage);
			}
			_sendFailed.Invoke();
		}
		else
		{
			_sendSucceeded.Invoke();
			if (!wishlist)
			{
				NotificationArea.Instance.Add(new MessageNotification(Shell.LoadString(StringId.IDS_MESSAGE_SENT_NOTIFICATION), NotificationTask.Messaging, NotificationState.OneShot));
			}
			else
			{
				Shell.MainFrame.Marketplace.CartItemsCount = Shell.MainFrame.Marketplace.CartItemsCount + 1;
			}
		}
	}

	private void OnSendCompleted(object obj)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		SendCompletedParams sendCompletedParams = (SendCompletedParams)obj;
		OnSendCompleted(sendCompletedParams.HR, null, sendCompletedParams.Wishlist);
	}

	private void OnSendCompletedAsync(HRESULT hr, object state)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		bool wishlist = (bool)state;
		Application.DeferredInvoke(new DeferredInvokeHandler(OnSendCompleted), (object)new SendCompletedParams(hr, wishlist));
	}

	private void OnFavoritesManagementCompleted(object obj)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Invalid comparison between Unknown and I4
		if (obj is FavoritesManagementState favoritesManagementState && (int)favoritesManagementState.Action == 1 && ((HRESULT)(ref favoritesManagementState.MgmtOperationResult)).IsSuccess)
		{
			StringId stringId = ((favoritesManagementState.FavoritesCount == 1) ? StringId.IDS_FAVORITE_MGMT_NOTIFICATION : StringId.IDS_FAVORITES_MGMT_NOTIFICATION);
			string message = string.Format(Shell.LoadString(stringId), favoritesManagementState.FavoritesCount);
			NotificationArea.Instance.Add(new MessageNotification(message, NotificationTask.Messaging, NotificationState.OneShot));
		}
	}

	private void OnFavoritesManagementCompletedAsync(HRESULT hr, object state)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		if (state is FavoritesManagementState favoritesManagementState)
		{
			favoritesManagementState.MgmtOperationResult = hr;
			Application.DeferredInvoke(new DeferredInvokeHandler(OnFavoritesManagementCompleted), (object)favoritesManagementState);
		}
	}
}
