using System;
using System.Globalization;
using System.Text;
using System.Threading;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Util;
using UIXControls;

namespace ZuneUI;

public class CultureHelper
{
	private static bool s_marketplaceCultureChecked = false;

	private static bool? useAlternateStyling;

	public static void CheckMarketplaceCulture()
	{
		if (s_marketplaceCultureChecked)
		{
			return;
		}
		string marketplaceCulture = FeatureEnablement.GetMarketplaceCulture();
		if (!string.IsNullOrEmpty(marketplaceCulture))
		{
			s_marketplaceCultureChecked = true;
			string strB = null;
			if (marketplaceCulture.Length > 1)
			{
				strB = marketplaceCulture.Substring(0, 2);
			}
			string text = CultureInfo.CurrentUICulture.ToString();
			string strA = null;
			if (text.Length > 1)
			{
				strA = text.Substring(0, 2);
			}
			bool flag = 0 == string.Compare(strA, strB, StringComparison.InvariantCultureIgnoreCase);
			bool flag2 = false;
			bool flag3 = false;
			if (!flag)
			{
				string lastMarketplaceCulture = ClientConfiguration.Shell.LastMarketplaceCulture;
				flag2 = 0 != string.Compare(marketplaceCulture, lastMarketplaceCulture, StringComparison.InvariantCultureIgnoreCase);
				string lastClientCulture = ClientConfiguration.Shell.LastClientCulture;
				flag3 = 0 != string.Compare(text, lastClientCulture, StringComparison.InvariantCultureIgnoreCase);
			}
			ClientConfiguration.Shell.LastMarketplaceCulture = marketplaceCulture;
			ClientConfiguration.Shell.LastClientCulture = text;
			if (!flag && (flag2 || flag3))
			{
				MessageBox.Show(Shell.LoadString(StringId.IDS_MARKETPLACE_CULTURE_MISMATCH_TITLE), Shell.LoadString(StringId.IDS_MARKETPLACE_CULTURE_MISMATCH), (EventHandler)null);
			}
		}
	}

	public static void CheckValidRegionAndLanguage()
	{
		if (!FeatureEnablement.HasValidRegionAndLanguage())
		{
			ErrorDialogInfo.Show(((HRESULT)(ref HRESULT._ZUNE_E_UNKNOWN_REGION_OR_LANGUAGE)).Int, Shell.LoadString(StringId.IDS_UNKNOWN_REGION_OR_LANGUAGE_TITLE));
		}
	}

	public static bool ShowYomiSortFields()
	{
		string twoLetterISOLanguageName = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
		string region = FeatureEnablement.GetRegion();
		if (!twoLetterISOLanguageName.Equals("ja", StringComparison.OrdinalIgnoreCase))
		{
			return region.Equals("jp", StringComparison.OrdinalIgnoreCase);
		}
		return true;
	}

	public static string GetCurrentRegionDisplayName()
	{
		string region = FeatureEnablement.GetRegion();
		try
		{
			RegionInfo regionInfo = new RegionInfo(FeatureEnablement.GetRegion());
			return regionInfo.DisplayName;
		}
		catch (ArgumentException)
		{
			return region;
		}
	}

	public static bool IsEnUs()
	{
		bool flag = Thread.CurrentThread.CurrentUICulture.Equals(new CultureInfo("en-US"));
		bool result = StringHelper.IsEqualCaseInsensitive(FeatureEnablement.GetRegion(), "US");
		if (flag)
		{
			return result;
		}
		return false;
	}

	public static bool UseAlternateStyling()
	{
		if (!useAlternateStyling.HasValue)
		{
			switch (CultureInfo.CurrentUICulture.LCID)
			{
			case 4:
			case 17:
			case 18:
			case 1028:
			case 1041:
			case 1042:
			case 2052:
			case 3076:
			case 4100:
			case 5124:
			case 31748:
				useAlternateStyling = true;
				break;
			default:
				useAlternateStyling = false;
				break;
			}
		}
		return useAlternateStyling.Value;
	}

	public static uint GeoId()
	{
		return FeatureEnablement.GetGeoId();
	}

	public static bool IsValidGeoId(uint geoId)
	{
		return FeatureEnablement.GetInvalidGeoId() != geoId;
	}

	public static int GetLCIDFromCultureString(string culture, bool useDefault)
	{
		int num = -1;
		if (!string.IsNullOrEmpty(culture))
		{
			try
			{
				CultureInfo cultureInfo = new CultureInfo(culture);
				num = cultureInfo.LCID;
			}
			catch (Exception)
			{
				if (culture.Equals("es-US", StringComparison.InvariantCultureIgnoreCase))
				{
					num = 21514;
				}
			}
		}
		if (num == -1 && useDefault)
		{
			num = CultureInfo.CurrentUICulture.LCID;
		}
		return num;
	}

	public static string GetHelpUrl()
	{
		string text = Shell.LoadString(StringId.IDS_WWW_ZUNE_NET_SUPPORT_URL);
		string lynxCulture = FeatureEnablement.GetLynxCulture();
		if (!string.IsNullOrEmpty(lynxCulture))
		{
			text = AppendFwlinkCulture(text, lynxCulture);
		}
		StringBuilder stringBuilder = new StringBuilder(text);
		string uIPath = ZuneShell.DefaultInstance.CurrentPage.UIPath;
		if (!string.IsNullOrEmpty(uIPath))
		{
			UrlHelper.AppendParam(first: false, stringBuilder, "path", uIPath);
		}
		foreach (UIDevice item in SingletonModelItem<UIDeviceList>.Instance)
		{
			if (item.IsConnectedToPC || (SignIn.Instance.SignedIn && item.UserId == SignIn.Instance.LastSignedInUserId))
			{
				UrlHelper.AppendParam(first: false, stringBuilder, "mfr", item.Manufacturer);
				UrlHelper.AppendParam(first: false, stringBuilder, "mdl", item.ModelName);
			}
		}
		return stringBuilder.ToString();
	}

	public static string GetPrivacyUrl()
	{
		string text = Shell.LoadString(StringId.IDS_PRIVACY_STATEMENT_URL);
		string lynxCulture = FeatureEnablement.GetLynxCulture();
		if (!string.IsNullOrEmpty(lynxCulture))
		{
			text = AppendFwlinkCulture(text, lynxCulture);
		}
		return text;
	}

	public static string AppendFwlinkCulture(string uri, int lcid)
	{
		if (lcid >= 0)
		{
			uri = $"{uri}&clcid=0x{lcid:x}";
		}
		return uri;
	}

	public static string AppendFwlinkCulture(string uri, string culture)
	{
		if (!string.IsNullOrEmpty(culture))
		{
			uri = AppendFwlinkCulture(uri, GetLCIDFromCultureString(culture, useDefault: false));
		}
		return uri;
	}

	public static string AppendFwlinkCulture(string uri)
	{
		uri = AppendFwlinkCulture(uri, GetLCIDFromCultureString(null, useDefault: true));
		return uri;
	}

	public static string AppendFwlinkLynxCulture(string uri)
	{
		string lynxCulture = FeatureEnablement.GetLynxCulture();
		if (!string.IsNullOrEmpty(lynxCulture))
		{
			uri = AppendFwlinkCulture(uri, lynxCulture);
		}
		return uri;
	}

	public static string AppendLynxCultureQueryString(string uri)
	{
		return AppendLynxCultureQueryString(uri, first: false);
	}

	public static string AppendLynxCultureQueryString(string uri, bool first)
	{
		string lynxCulture = FeatureEnablement.GetLynxCulture();
		if (string.IsNullOrEmpty(lynxCulture))
		{
			return uri;
		}
		if (first)
		{
			return $"{uri}?culture={lynxCulture}";
		}
		return $"{uri}&culture={lynxCulture}";
	}

	internal static string GetDefaultCountry()
	{
		return FeatureEnablement.GetRegion();
	}

	internal static string GetDefaultLanguage()
	{
		string result = null;
		CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
		if (currentUICulture != null)
		{
			result = currentUICulture.TwoLetterISOLanguageName;
		}
		return result;
	}

	public static CultureInfo GetCulture(string cultureString)
	{
		CultureInfo cultureInfo = null;
		if (!string.IsNullOrEmpty(cultureString))
		{
			try
			{
				cultureInfo = new CultureInfo(cultureString, useUserOverride: false);
			}
			catch
			{
			}
		}
		if (cultureInfo == null)
		{
			cultureInfo = CultureInfo.CurrentCulture;
		}
		return cultureInfo;
	}
}
