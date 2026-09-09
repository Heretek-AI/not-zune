using System;
using Microsoft.Iris;
using Microsoft.Zune.Util;
using UIXControls;

namespace ZuneUI;

public class UpdateDialogInfo
{
	internal static void Show(bool updateFound, bool fIsCritical, bool isUserInitiated)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		if (updateFound)
		{
			EventHandler eventHandler = null;
			string text = ((!fIsCritical) ? Shell.LoadString(StringId.IDS_UPDATE_AVAILABLE) : Shell.LoadString(StringId.IDS_CRITICAL_UPDATE_AVAILABLE));
			string text2 = Shell.LoadString(StringId.IDS_UPDATE_INSTALL_LATER);
			Command val = new Command((IModelItemOwner)null, Shell.LoadString(StringId.IDS_UPDATE_INSTALL_NOW), (EventHandler)null);
			val.Invoked += delegate
			{
				SoftwareUpdates.Instance.InstallUpdates();
			};
			if (!isUserInitiated)
			{
				eventHandler = delegate
				{
					SQMLog.Log((SQMDataId)44, 1);
				};
			}
			MessageBox.Show((string)null, text, val, text2, eventHandler, true);
		}
		else
		{
			string text3 = Shell.LoadString(StringId.IDS_UPDATE_NOT_REQUIRED);
			MessageBox.Show((string)null, text3, (EventHandler)null);
		}
	}
}
