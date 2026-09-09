using System;
using System.IO;
using Microsoft.Zune.Shell;

namespace ZuneUI;

public static class LocalizationHelper
{
	public static string GetLocalizedFolderName(string path)
	{
		try
		{
			string text = GetLocalizedFolderPath(path);
			FileInfo fileInfo = new FileInfo(text);
			if (!string.IsNullOrEmpty(fileInfo.Name))
			{
				text = fileInfo.Name;
			}
			return text;
		}
		catch (Exception)
		{
			return path;
		}
	}

	public static string GetLocalizedFolderPath(string path)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		if (!string.IsNullOrEmpty(path))
		{
			string result = default(string);
			HRESULT val = HRESULT.op_Implicit(ZuneApplication.ZuneLibrary.GetLocalizedPathOfFolder(path, false, ref result));
			if (((HRESULT)(ref val)).IsSuccess)
			{
				return result;
			}
		}
		return path;
	}
}
