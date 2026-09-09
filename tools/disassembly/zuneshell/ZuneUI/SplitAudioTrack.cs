using System;
using System.Collections;
using System.Runtime.InteropServices;
using Microsoft.Iris;
using MicrosoftZuneLibrary;
using UIXControls;

namespace ZuneUI;

public static class SplitAudioTrack
{
	public static void Split(IList tracks)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		bool flag = false;
		foreach (DataProviderObject track in tracks)
		{
			DataProviderObject val = track;
			long num = (long)(val.GetProperty("FileCount") ?? ((object)0));
			if (num > 1)
			{
				int libraryId = (int)(val.GetProperty("LibraryId") ?? ((object)(-1)));
				HRESULT val2 = Split(libraryId);
				flag |= ((HRESULT)(ref val2)).IsError;
			}
		}
		if (flag)
		{
			MessageBox.Show(Shell.LoadString(StringId.IDS_GENERIC_ERROR), Shell.LoadString(StringId.IDS_SHOW_DUPLICATES_FAILED), (EventHandler)null);
		}
	}

	private static HRESULT Split(int libraryId)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		try
		{
			ZuneLibrary.SplitAudioTrack(libraryId);
		}
		catch (COMException ex)
		{
			return HRESULT.op_Implicit(ex.ErrorCode);
		}
		return HRESULT._S_OK;
	}
}
