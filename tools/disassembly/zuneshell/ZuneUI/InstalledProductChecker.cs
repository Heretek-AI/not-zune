using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ZuneUI;

public static class InstalledProductChecker
{
	public static bool IsInstalled(string upgradeCode, int minVersionMajor, int minVersionMinor)
	{
		Guid empty = Guid.Empty;
		try
		{
			empty = new Guid(upgradeCode);
		}
		catch
		{
			return false;
		}
		string empty2 = string.Empty;
		int num = 0;
		do
		{
			empty2 = EnumRelatedProducts(empty.ToString("B"), num);
			if (!string.IsNullOrEmpty(empty2) && GetProductVersionMajor(empty2) >= minVersionMajor && GetProductVersionMinor(empty2) >= minVersionMinor)
			{
				return true;
			}
			num++;
		}
		while (!string.IsNullOrEmpty(empty2));
		return false;
	}

	public static string GetProductName(string productCode)
	{
		return GetProductInfo(productCode, "ProductName");
	}

	public static int GetProductVersionMajor(string productCode)
	{
		int.TryParse(GetProductInfo(productCode, "VersionMajor"), out var result);
		return result;
	}

	public static int GetProductVersionMinor(string productCode)
	{
		int.TryParse(GetProductInfo(productCode, "VersionMinor"), out var result);
		return result;
	}

	public static string GetProductInfo(string productCode, string property)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		int size = 512;
		StringBuilder stringBuilder = new StringBuilder(size);
		HRESULT val = HRESULT.op_Implicit(MsiGetProductInfo(productCode, property, stringBuilder, ref size));
		if (val != HRESULT._S_OK)
		{
			return string.Empty;
		}
		return stringBuilder.ToString();
	}

	public static string EnumRelatedProducts(string upgradeCode, int index)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		StringBuilder stringBuilder = new StringBuilder(39);
		HRESULT val = HRESULT.op_Implicit(MsiEnumRelatedProducts(upgradeCode, 0, index, stringBuilder));
		if (val != HRESULT._S_OK)
		{
			return string.Empty;
		}
		return stringBuilder.ToString();
	}

	[DllImport("msi.dll")]
	private static extern int MsiGetProductInfo(string productCode, string property, [Out] StringBuilder valueBuffer, ref int size);

	[DllImport("msi.dll")]
	private static extern int MsiEnumRelatedProducts(string upgradeCode, int reserved, int index, [Out] StringBuilder productCodeBuffer);
}
