using System;
using System.Text;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using ZuneUI;

namespace ZuneXml;

internal class WMISServiceDataProviderQuery : XmlDataProviderQuery
{
	private string m_strPostBody;

	private bool _endpointsInitialized;

	internal static DataProviderQuery ConstructWmisQuery(object queryTypeCookie)
	{
		return (DataProviderQuery)(object)new WMISServiceDataProviderQuery(queryTypeCookie);
	}

	public WMISServiceDataProviderQuery(object queryTypeCookie)
		: base(queryTypeCookie)
	{
		_keepAlive = false;
	}

	protected override void BeginExecute()
	{
		if (_endpointsInitialized)
		{
			base.BeginExecute();
		}
		else
		{
			ThreadPool.QueueUserWorkItem(BackgroundInitializeWMISEndpointCollection);
		}
	}

	private void BackgroundInitializeWMISEndpointCollection(object unused)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		HRESULT val = HRESULT.op_Implicit(ZuneApplication.Service.InitializeWMISEndpointCollection());
		if (((HRESULT)(ref val)).IsSuccess)
		{
			_endpointsInitialized = true;
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredInvokeBeginExecute), (object)null);
		}
		else
		{
			_requestGeneration++;
			SetWorkerStatus(_requestGeneration, (DataProviderQueryStatus)1);
			SetWorkerStatus(_requestGeneration, (DataProviderQueryStatus)4, val);
		}
	}

	private void DeferredInvokeBeginExecute(object unused)
	{
		base.BeginExecute();
	}

	protected override string GetResourceUri()
	{
		return BuildServiceUri(0, 0);
	}

	protected override string GetPostBody()
	{
		return m_strPostBody;
	}

	private string BuildServiceUri(int chunkStart, int chunkSize)
	{
		m_strPostBody = null;
		string text = (string)((DataProviderQuery)this).GetProperty("SearchString");
		string text2 = (string)((DataProviderQuery)this).GetProperty("artistId");
		string text3 = (string)((DataProviderQuery)this).GetProperty("albumId");
		string text4 = null;
		string text5;
		string paramName;
		if (!string.IsNullOrEmpty(text))
		{
			text5 = WMISEndpointIds.WMISEID_Search;
			paramName = "SearchString";
			text4 = text;
		}
		else if (!string.IsNullOrEmpty(text2))
		{
			text5 = WMISEndpointIds.WMISEID_GetResultsForArtist;
			paramName = "artistId";
			text4 = text2;
		}
		else
		{
			if (string.IsNullOrEmpty(text3))
			{
				return null;
			}
			text5 = WMISEndpointIds.WMISEID_GetAlbumDetailsByAlbumId;
			paramName = "albumId";
			text4 = text3;
			Guid.NewGuid();
			m_strPostBody = string.Empty;
		}
		string wMISEndPointUri = ZuneApplication.Service.GetWMISEndPointUri(text5);
		if (string.IsNullOrEmpty(wMISEndPointUri))
		{
			return null;
		}
		UriBuilder uriBuilder = new UriBuilder(wMISEndPointUri);
		StringBuilder stringBuilder = new StringBuilder();
		UrlHelper.AppendParam(first: true, stringBuilder, paramName, text4);
		string wMISPartner = ClientConfiguration.Service.WMISPartner;
		if (!string.IsNullOrEmpty(wMISPartner))
		{
			UrlHelper.AppendParam(first: false, stringBuilder, "Partner", wMISPartner);
		}
		string[] array = new string[5] { "locale", "maxNumberOfResults", "resultTypeString", "countOnly", "volume" };
		string[] array2 = array;
		foreach (string text6 in array2)
		{
			text4 = null;
			object property = ((DataProviderQuery)this).GetProperty(text6);
			if (property != null)
			{
				text4 = property.ToString();
			}
			if (!string.IsNullOrEmpty(text4))
			{
				UrlHelper.AppendParam(first: false, stringBuilder, text6, text4);
			}
		}
		string text7 = stringBuilder.ToString();
		if (text7.Length > 0 && text7[0] == '?')
		{
			text7 = text7.Remove(0, 1);
		}
		uriBuilder.Query = text7;
		return uriBuilder.Uri.AbsoluteUri;
	}
}
