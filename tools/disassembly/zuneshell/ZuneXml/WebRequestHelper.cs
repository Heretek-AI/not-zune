using System;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneXml;

internal class WebRequestHelper
{
	private static string AcceptLanguage;

	public static string Locale
	{
		get
		{
			if (AcceptLanguage == null)
			{
				AcceptLanguage = CultureInfo.CurrentUICulture.Name;
			}
			return AcceptLanguage;
		}
	}

	private static void SetCommonHeaders(HttpWebRequest request, EPassportPolicyId passportTicketType, HttpRequestCachePolicy cachePolicy, bool acceptGZipEncoding)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		request.CachePolicy = cachePolicy;
		if ((int)passportTicketType != 0)
		{
			string passportTicket = ZuneApplication.Service.GetPassportTicket(passportTicketType);
			if (!string.IsNullOrEmpty(passportTicket))
			{
				request.Authorization = "WLID1.0 " + passportTicket;
			}
		}
		request.AcceptGZipEncoding = acceptGZipEncoding;
		request.AcceptLanguage = Locale;
	}

	public static HttpWebRequest ConstructWebRequest(string requestUri, EPassportPolicyId passportTicketType, HttpRequestCachePolicy cachePolicy, bool fKeepAlive, bool acceptGZipEncoding)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Invalid comparison between Unknown and I4
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Uri uri = new Uri(requestUri);
		string absoluteUri = uri.AbsoluteUri;
		if (absoluteUri != requestUri)
		{
			_ = TraceSwitches.DataProviderSwitch.TraceWarning;
			requestUri = absoluteUri;
		}
		if ((int)cachePolicy == 1 && UriResourceTracker.Instance.IsResourceModified(requestUri))
		{
			cachePolicy = (HttpRequestCachePolicy)2;
			UriResourceTracker.Instance.SetResourceModified(requestUri, false);
		}
		HttpWebRequest val = HttpWebRequest.Create(requestUri);
		val.KeepAlive = fKeepAlive;
		val.CancelOnShutdown = true;
		SetCommonHeaders(val, passportTicketType, cachePolicy, acceptGZipEncoding);
		_ = TraceSwitches.DataProviderSwitch.TraceWarning;
		return val;
	}

	public static HttpWebRequest ConstructWebPostRequest(string requestUri, string requestBody, EPassportPolicyId passportTicketType, HttpRequestCachePolicy cachePolicy, bool fKeepAlive, bool acceptGZipEncoding)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		HttpWebRequest val = HttpWebRequest.Create(requestUri);
		val.KeepAlive = fKeepAlive;
		val.CachePolicy = cachePolicy;
		val.Method = "POST";
		val.ContentType = "application/x-www-form-urlencoded";
		SetCommonHeaders(val, passportTicketType, cachePolicy, acceptGZipEncoding);
		ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
		byte[] bytes = aSCIIEncoding.GetBytes(requestBody);
		val.ContentLength = bytes.Length;
		Stream requestStream = val.GetRequestStream();
		requestStream.Write(bytes, 0, bytes.Length);
		_ = TraceSwitches.DataProviderSwitch.TraceWarning;
		return val;
	}
}
