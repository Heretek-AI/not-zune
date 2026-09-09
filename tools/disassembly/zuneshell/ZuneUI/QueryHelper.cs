using Microsoft.Iris;

namespace ZuneUI;

public class QueryHelper
{
	public static bool HasCompletedOrFailed(DataProviderQueryStatus status)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Invalid comparison between Unknown and I4
		if ((int)status != 3)
		{
			return (int)status == 4;
		}
		return true;
	}

	public static bool HasFailed(DataProviderQueryStatus status)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		return (int)status == 4;
	}

	public static bool HasCompleted(DataProviderQueryStatus status)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		return (int)status == 3;
	}

	public static bool IsBusy(DataProviderQueryStatus status)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Invalid comparison between Unknown and I4
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Invalid comparison between Unknown and I4
		if ((int)status != 2)
		{
			return (int)status == 1;
		}
		return true;
	}
}
