using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.PerfTrace;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneXml;

public class XmlDataProviderQuery : DataProviderQuery
{
	private class GetDataFromResourceArgs
	{
		public readonly string Uri;

		public readonly string Body;

		public readonly bool NewGeneration;

		public GetDataFromResourceArgs(string uri, string body, bool fNewGeneration)
		{
			Uri = uri;
			Body = body;
			NewGeneration = fNewGeneration;
		}
	}

	private class RequestArgs
	{
		public readonly int m_requestGeneration;

		public readonly string m_requestUri;

		public readonly string m_requestBody;

		public readonly string m_localUri;

		public readonly XmlDataProviderObject m_result;

		public readonly bool m_fPaged;

		public readonly DateTime m_tmStartTime;

		public readonly int m_tcStart;

		public readonly HttpRequestCachePolicy m_cachePolicy;

		public RequestArgs(int requestGeneration, string requestUri, string requestBody, string localUri, XmlDataProviderObject result, bool fPaged, DateTime tmStartTime, int tcStart, HttpRequestCachePolicy cachePolicy)
		{
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			m_requestGeneration = requestGeneration;
			m_requestUri = requestUri;
			m_requestBody = requestBody;
			m_localUri = localUri;
			m_result = result;
			m_fPaged = fPaged;
			m_tmStartTime = tmStartTime;
			m_tcStart = tcStart;
			m_cachePolicy = cachePolicy;
		}
	}

	private class DeferredSetStatusArgs
	{
		public readonly DataProviderQueryStatus m_eStatus;

		public readonly int m_requestGeneration;

		public readonly object m_errorCode;

		public DeferredSetStatusArgs(int requestGeneration, DataProviderQueryStatus eStatus, object errorCode)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			m_eStatus = eStatus;
			m_requestGeneration = requestGeneration;
			m_errorCode = errorCode;
		}
	}

	private class DeferredSetResultArgs
	{
		public readonly XmlDataProviderObject m_result;

		public readonly int m_requestGeneration;

		public DeferredSetResultArgs(int requestGeneration, XmlDataProviderObject result)
		{
			m_requestGeneration = requestGeneration;
			m_result = result;
		}
	}

	public struct XPathMatch(XmlDataProviderObject instance, DataProviderMapping propertyMapping, string matchingAttributeName, bool encodedXml)
	{
		public XmlDataProviderObject instance = instance;

		public DataProviderMapping propertyMapping = propertyMapping;

		public string matchingAttributeName = matchingAttributeName;

		public bool encodedXml = encodedXml;
	}

	protected EPassportPolicyId _passportTicketType;

	protected HttpRequestCachePolicy _cachePolicy;

	private string _lastUri;

	internal int _requestGeneration;

	protected bool _keepAlive;

	protected bool _ignoreNamespacePrefix;

	protected bool _ignoreDuplicateGenerationRequests;

	protected bool _acceptGZipEncoding;

	private object _errorCode;

	private bool _refreshOnCacheExpire;

	private Timer _autoRefreshTimer;

	public object ErrorCode => _errorCode;

	public HttpRequestCachePolicy CachePolicy
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _cachePolicy;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_cachePolicy = value;
		}
	}

	public EPassportPolicyId PassportTicketType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _passportTicketType;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_passportTicketType = value;
		}
	}

	public bool IgnoreDuplicateGenerationRequests
	{
		get
		{
			return _ignoreDuplicateGenerationRequests;
		}
		set
		{
			_ignoreDuplicateGenerationRequests = value;
		}
	}

	public bool RefreshOnCacheExpire
	{
		get
		{
			return _refreshOnCacheExpire;
		}
		set
		{
			_refreshOnCacheExpire = value;
		}
	}

	protected XmlDataProviderQuery(object queryTypeCookie)
		: base(queryTypeCookie)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		_passportTicketType = (EPassportPolicyId)0;
		_cachePolicy = (HttpRequestCachePolicy)1;
		_keepAlive = true;
		_ignoreNamespacePrefix = true;
		_ignoreDuplicateGenerationRequests = true;
		((DataProviderQuery)this).Result = XmlDataProviderObjectFactory.CreateObject((DataProviderQuery)(object)this, ((DataProviderQuery)this).ResultTypeCookie);
	}

	protected override void OnDispose()
	{
		((DataProviderQuery)this).OnDispose();
		if (_autoRefreshTimer != null)
		{
			_autoRefreshTimer.Stop();
			((ModelItem)_autoRefreshTimer).Dispose();
			_autoRefreshTimer = null;
		}
	}

	protected override void BeginExecute()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		object property = ((DataProviderQuery)this).GetProperty("SendPassportTicket");
		if (property is bool)
		{
			_passportTicketType = (EPassportPolicyId)(((bool)property) ? 3 : 0);
		}
		object property2 = ((DataProviderQuery)this).GetProperty("SendSecurePassportTicket");
		if (property2 is bool)
		{
			_passportTicketType = (EPassportPolicyId)(((bool)property2) ? 2 : 0);
		}
		object property3 = ((DataProviderQuery)this).GetProperty("RefreshOnCacheExpire");
		if (property3 is bool)
		{
			_refreshOnCacheExpire = (bool)property3;
		}
		string resourceUri = GetResourceUri();
		string postBody = GetPostBody();
		if (!string.IsNullOrEmpty(resourceUri))
		{
			GetDataFromResource(resourceUri, postBody, fNewGeneration: true);
		}
	}

	protected virtual string GetResourceUri()
	{
		return null;
	}

	protected virtual string GetPostBody()
	{
		return null;
	}

	internal virtual bool FilterDataProviderObject(XmlDataProviderObject dataObject)
	{
		return false;
	}

	protected override void OnPropertyChanged(string propertyName)
	{
		if (propertyName == "LocalSortBy" && ((DataProviderQuery)this).Result != null)
		{
			((XmlDataProviderObject)((DataProviderQuery)this).Result).ChangeListSort((string)((DataProviderQuery)this).GetProperty(propertyName));
		}
		((DataProviderQuery)this).OnPropertyChanged(propertyName);
	}

	internal void GetDataFromResource(string uri, string body, bool fNewGeneration)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Expected O, but got Unknown
		GetDataFromResourceArgs getDataFromResourceArgs = new GetDataFromResourceArgs(uri, body, fNewGeneration);
		if (Application.IsApplicationThread)
		{
			GetDataFromResource(getDataFromResourceArgs);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(GetDataFromResource), (object)getDataFromResourceArgs);
		}
	}

	private void GetDataFromResource(object obj)
	{
		GetDataFromResourceArgs getDataFromResourceArgs = (GetDataFromResourceArgs)obj;
		string requestUri = getDataFromResourceArgs.Uri;
		string requestBody = getDataFromResourceArgs.Body;
		bool newGeneration = getDataFromResourceArgs.NewGeneration;
		string text = (string)((DataProviderQuery)this).GetProperty("LocalSortBy");
		object property = ((DataProviderQuery)this).GetProperty("Paged");
		bool fPaged = property is bool && (bool)property;
		XmlDataProviderObject result = null;
		if (newGeneration)
		{
			_requestGeneration++;
			result = XmlDataProviderObjectFactory.CreateObject((DataProviderQuery)(object)this, ((DataProviderQuery)this).ResultTypeCookie);
			if (text != null)
			{
				result.ChangeListSort(text);
			}
		}
		else
		{
			result = (XmlDataProviderObject)((DataProviderQuery)this).Result;
			if (_ignoreDuplicateGenerationRequests && string.Equals(requestUri, _lastUri, StringComparison.CurrentCultureIgnoreCase))
			{
				return;
			}
		}
		_lastUri = requestUri;
		SetWorkerStatus(_requestGeneration, (DataProviderQueryStatus)1);
		bool flag = false;
		string localUri = null;
		if (!flag)
		{
			ThreadPool.QueueUserWorkItem(delegate(object arg)
			{
				//IL_0049: Unknown result type (might be due to invalid IL or missing references)
				//IL_0061: Unknown result type (might be due to invalid IL or missing references)
				//IL_006c: Expected O, but got Unknown
				int requestGeneration = (int)arg;
				HttpWebRequest val = ConstructWebRequest(requestUri, requestBody);
				RequestArgs requestArgs = new RequestArgs(requestGeneration, requestUri, requestBody, localUri, result, fPaged, DateTime.Now, Environment.TickCount, val.CachePolicy);
				val.GetResponseAsync(new AsyncRequestComplete(OnRequestComplete), (object)requestArgs);
			}, _requestGeneration);
		}
		PerfTrace.TraceUICollectionEvent(UICollectionEvent.DataProviderQueryBegin, _lastUri);
	}

	private void OnRequestComplete(HttpWebResponse response, object requestArgs)
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Invalid comparison between Unknown and I4
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Invalid comparison between Unknown and I4
		RequestArgs requestArgs2 = (RequestArgs)requestArgs;
		Stream stream = null;
		try
		{
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new HttpWebException(response);
			}
			stream = response.GetResponseStream();
			ParseXml(stream, requestArgs2.m_requestGeneration, requestArgs2.m_requestUri, requestArgs2.m_requestBody, requestArgs2.m_result, requestArgs2.m_fPaged, requestArgs2.m_tmStartTime, requestArgs2.m_tcStart);
			if ((int)requestArgs2.m_cachePolicy == 2 && (int)_cachePolicy == 1 && requestArgs2.m_result.NextPage != null)
			{
				UriResourceTracker.Instance.SetResourceModified(requestArgs2.m_result.NextPage.GetPageUrl(0), true);
			}
			SetAutoRefreshTimer(response.Expires);
		}
		catch (Exception ex)
		{
			if (ex is HttpWebException)
			{
				object errorCode = ((HttpWebException)ex).Response.StatusCode;
				SetWorkerStatus(requestArgs2.m_requestGeneration, (DataProviderQueryStatus)4, errorCode);
			}
			else
			{
				SetWorkerStatus(requestArgs2.m_requestGeneration, (DataProviderQueryStatus)4, null);
			}
			_ = TraceSwitches.DataProviderSwitch.TraceWarning;
		}
		finally
		{
			stream?.Close();
			response.Close();
			PerfTrace.TraceUICollectionEvent(UICollectionEvent.DataProviderQueryComplete, _lastUri);
		}
	}

	private void ParseXml(Stream xmlStream, int requestGeneration, string requestUri, string requestBody, XmlDataProviderObject result, bool fPaged, DateTime tmStartTime, int tcStart)
	{
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Invalid comparison between Unknown and I4
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Expected O, but got Unknown
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Invalid comparison between Unknown and I4
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		int num = 0;
		int num2 = 0;
		bool flag = false;
		XmlDataProviderReader xmlDataProviderReader = null;
		try
		{
			num = Environment.TickCount - tcStart;
			SetWorkerStatus(requestGeneration, (DataProviderQueryStatus)2);
			StringBuilder stringBuilder = new StringBuilder(80);
			int[] array = new int[20];
			xmlDataProviderReader = new XmlDataProviderReader(xmlStream);
			List<XPathMatch> list = new List<XPathMatch>(2);
			bool flag2 = false;
			_ = TraceSwitches.DataProviderSwitch.TraceVerbose;
			XmlNodeType val = (XmlNodeType)0;
			while ((int)val != 0 || xmlDataProviderReader.Read())
			{
				val = (XmlNodeType)0;
				if (requestGeneration != _requestGeneration)
				{
					flag2 = true;
					break;
				}
				if ((int)xmlDataProviderReader.NodeType != 1 || xmlDataProviderReader.Depth >= array.Length)
				{
					continue;
				}
				int depth = xmlDataProviderReader.Depth;
				stringBuilder.Length = ((depth > 0) ? array[depth - 1] : 0);
				stringBuilder.Append('/');
				if (_ignoreNamespacePrefix)
				{
					stringBuilder.Append(xmlDataProviderReader.LocalName);
				}
				else
				{
					stringBuilder.Append(xmlDataProviderReader.Name);
				}
				array[depth] = stringBuilder.Length;
				string text = stringBuilder.ToString();
				if (fPaged)
				{
					IPageInfo pageInfo = ExtractNextPageInfo(requestUri, requestBody, text, xmlDataProviderReader);
					if (pageInfo != null)
					{
						result.NextPage = pageInfo;
					}
				}
				Hashtable hashtable = new Hashtable(3);
				if (xmlDataProviderReader.MoveToFirstAttribute())
				{
					do
					{
						hashtable[xmlDataProviderReader.Name] = xmlDataProviderReader.Value;
					}
					while (xmlDataProviderReader.MoveToNextAttribute());
				}
				result.ProcessXPath(text, hashtable, list);
				if (list.Count > 0)
				{
					flag = true;
					string xmlValue = null;
					if (xmlDataProviderReader.Read() && (int)(val = xmlDataProviderReader.NodeType) == 3)
					{
						val = (XmlNodeType)0;
						xmlValue = xmlDataProviderReader.Value;
					}
					ProcessXPathMatches(list, hashtable, xmlValue, xmlDataProviderReader);
				}
				foreach (string key in hashtable.Keys)
				{
					result.ProcessXPath(text + "@" + key, hashtable, list);
					if (list.Count > 0)
					{
						flag = true;
						ProcessXPathMatches(list, hashtable, null, null);
					}
				}
			}
			if (!flag2)
			{
				Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetResult), (object)new DeferredSetResultArgs(requestGeneration, result));
				SetWorkerStatus(requestGeneration, (DataProviderQueryStatus)3);
				if (TraceSwitches.DataProviderSwitch.TraceWarning)
				{
					num2 = Environment.TickCount - tcStart;
				}
			}
		}
		catch (Exception ex)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetResult), (object)new DeferredSetResultArgs(requestGeneration, result));
			SetWorkerStatus(requestGeneration, (DataProviderQueryStatus)4);
			throw ex;
		}
		finally
		{
			xmlDataProviderReader?.Close();
		}
		_ = TraceSwitches.DataProviderSwitch.TraceVerbose;
	}

	internal virtual IPageInfo ExtractNextPageInfo(string requestUri, string requestBody, string strElementPath, XmlDataProviderReader xmlReader)
	{
		if (strElementPath == "/feed/link")
		{
			bool flag = false;
			if (xmlReader.MoveToFirstAttribute())
			{
				do
				{
					if (xmlReader.Name == "rel")
					{
						flag = xmlReader.Value == "next";
					}
					else if (xmlReader.Name == "href" && flag && !string.IsNullOrEmpty(xmlReader.Value))
					{
						string nextPageUrl = ConstructLinkUrl(requestUri, xmlReader.Value);
						return new LinkPageInfo(nextPageUrl, requestBody);
					}
				}
				while (xmlReader.MoveToNextAttribute());
			}
		}
		return null;
	}

	private HttpWebRequest ConstructWebRequest(string requestUri, string requestBody)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		if (requestBody != null)
		{
			return WebRequestHelper.ConstructWebPostRequest(requestUri, requestBody, _passportTicketType, _cachePolicy, _keepAlive, _acceptGZipEncoding);
		}
		return WebRequestHelper.ConstructWebRequest(requestUri, _passportTicketType, _cachePolicy, _keepAlive, _acceptGZipEncoding);
	}

	private static string ConstructLinkUrl(string baseUrl, string relativeUrl)
	{
		string result = null;
		if (relativeUrl.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
		{
			result = relativeUrl;
		}
		else
		{
			int num = baseUrl.IndexOf("//");
			if (num > 0)
			{
				num = baseUrl.IndexOf('/', num + 2);
				if (num > 0)
				{
					result = baseUrl.Substring(0, num) + relativeUrl;
				}
			}
		}
		return result;
	}

	private static void ProcessXPathMatches(List<XPathMatch> xpathMatches, Hashtable attributes, string xmlValue, XmlDataProviderReader xmlReader)
	{
		foreach (XPathMatch xpathMatch in xpathMatches)
		{
			string text = xmlValue;
			if (xpathMatch.matchingAttributeName != null)
			{
				text = (string)attributes[xpathMatch.matchingAttributeName];
			}
			if (xpathMatch.encodedXml && text != null && xmlReader != null)
			{
				xmlReader.PushElement(text);
			}
			else if (text != null)
			{
				xpathMatch.instance.SetPropertyFromStringValue(xpathMatch.propertyMapping, text);
			}
		}
		xpathMatches.Clear();
	}

	protected void SetWorkerStatus(int requestGeneration, DataProviderQueryStatus eStatus)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		SetWorkerStatus(requestGeneration, eStatus, null);
	}

	protected void SetWorkerStatus(int requestGeneration, DataProviderQueryStatus eStatus, object errorCode)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetStatus), (object)new DeferredSetStatusArgs(requestGeneration, eStatus, errorCode));
	}

	private void DeferredSetStatus(object args)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Invalid comparison between Unknown and I4
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Invalid comparison between Unknown and I4
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Invalid comparison between Unknown and I4
		if (args is DeferredSetStatusArgs deferredSetStatusArgs && deferredSetStatusArgs.m_requestGeneration == _requestGeneration)
		{
			((DataProviderQuery)this).Status = deferredSetStatusArgs.m_eStatus;
			if ((int)((DataProviderQuery)this).Status == 4)
			{
				_errorCode = deferredSetStatusArgs.m_errorCode;
			}
			else
			{
				_errorCode = null;
			}
			if ((int)((DataProviderQuery)this).Status == 4)
			{
				_lastUri = null;
			}
			if (((DataProviderQuery)this).Result != null && (int)((DataProviderQuery)this).Status == 3)
			{
				((XmlDataProviderObject)((DataProviderQuery)this).Result).OnQueryComplete();
			}
		}
	}

	private void SetAutoRefreshTimer(DateTime expires)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Expected O, but got Unknown
		if (_refreshOnCacheExpire)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetAutoRefreshTimer), (object)expires);
		}
	}

	private void DeferredSetAutoRefreshTimer(object args)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		DateTime utcNow = DateTime.UtcNow;
		DateTime dateTime = (DateTime)args;
		if (dateTime == DateTime.MaxValue || dateTime <= utcNow)
		{
			int autoRefreshQueryFallback = ClientConfiguration.Service.AutoRefreshQueryFallback;
			dateTime = utcNow.AddMilliseconds(autoRefreshQueryFallback);
		}
		if (!(dateTime != DateTime.MaxValue))
		{
			return;
		}
		if (_autoRefreshTimer == null)
		{
			_autoRefreshTimer = new Timer();
			_autoRefreshTimer.Tick += delegate
			{
				_autoRefreshTimer.Stop();
				((DataProviderQuery)this).Refresh();
			};
		}
		Random random = new Random();
		int num = random.Next(1, 300000);
		if (dateTime > utcNow)
		{
			long num2 = dateTime.Subtract(utcNow).Ticks / 10000;
			num = ((num2 <= int.MaxValue - num) ? (num + (int)num2) : (num + 900000));
		}
		_autoRefreshTimer.Stop();
		_autoRefreshTimer.Interval = num;
		_autoRefreshTimer.Start();
	}

	private void DeferredSetResult(object argsObj)
	{
		if (argsObj is DeferredSetResultArgs deferredSetResultArgs && deferredSetResultArgs.m_requestGeneration == _requestGeneration)
		{
			deferredSetResultArgs.m_result.TransferToAppThread();
			if (((DataProviderQuery)this).Result != deferredSetResultArgs.m_result)
			{
				((DataProviderQuery)this).Result = deferredSetResultArgs.m_result;
			}
		}
	}
}
