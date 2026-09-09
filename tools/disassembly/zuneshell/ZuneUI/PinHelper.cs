using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class PinHelper
{
	public static int AddPin(int id, EMediaTypes mediaType, int userId)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		return AddPin((EPinType)0, -1, id, mediaType, userId);
	}

	public static int AddPin(EPinType pinType, int pinOrdinal, int id, EMediaTypes mediaType, int userId)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		int result = default(int);
		PinManager.Instance.AddPin(pinType, id, mediaType, userId, pinOrdinal, ref result);
		SingletonModelItem<JumpListManager>.Instance.JumpListPinUpdateRequested.Invoke();
		return result;
	}

	public static int AddPin(string moniker, EServiceMediaType mediaType, string description, int userId)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		return AddPin((EPinType)0, description, -1, moniker, mediaType, userId);
	}

	public static int AddPin(EPinType pinType, string description, int pinOrdinal, string moniker, EServiceMediaType mediaType, int userId)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		int result = default(int);
		PinManager.Instance.AddPin(pinType, moniker, description, mediaType, userId, pinOrdinal, ref result);
		SingletonModelItem<JumpListManager>.Instance.JumpListPinUpdateRequested.Invoke();
		return result;
	}

	public static int FindPin(int id, EMediaTypes type, int userId, int maxAge)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		int result = default(int);
		PinManager.Instance.FindPin((EPinType)0, id, type, userId, maxAge, ref result);
		return result;
	}

	public static int FindPin(string moniker, EServiceMediaType type, int userId, int maxAge)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		int result = default(int);
		PinManager.Instance.FindPin((EPinType)0, moniker, type, userId, maxAge, ref result);
		return result;
	}

	public static void DeletePin(int id)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PinManager.Instance.DeletePin(id);
		SingletonModelItem<JumpListManager>.Instance.JumpListPinUpdateRequested.Invoke();
	}

	public static EServiceMediaType MapMarketplaceObjectToServiceMediaType(DataProviderObject dataProviderObject)
	{
		return (EServiceMediaType)(dataProviderObject.TypeName switch
		{
			"Album" => 0, 
			"Artist" => 3, 
			_ => -1, 
		});
	}
}
