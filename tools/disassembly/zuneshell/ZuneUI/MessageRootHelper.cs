using Microsoft.Iris;
using ZuneXml;

namespace ZuneUI;

public class MessageRootHelper
{
	public static bool IsUnread(DataProviderObject message)
	{
		return string.Compare(((MessageRoot)(object)message).Status, "unread") == 0;
	}

	public static void SetRead(DataProviderObject message, bool read)
	{
		((MessageRoot)(object)message).Status = (read ? "read" : "unread");
	}

	public static string UiType(DataProviderObject message)
	{
		return GetTypeInfo(message).UIText;
	}

	public static string DetailsTemplate(DataProviderObject message)
	{
		return GetTypeInfo(message).DetailsTemplate;
	}

	private static MessageTypeInfo GetTypeInfo(DataProviderObject message)
	{
		return MessageTypeInfo.GetMessageType(((MessageRoot)(object)message).Type);
	}
}
