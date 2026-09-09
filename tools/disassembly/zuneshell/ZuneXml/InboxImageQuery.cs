using System;
using System.IO;
using System.Net;
using Microsoft.Iris;
using Microsoft.Zune.Messaging;
using Microsoft.Zune.Service;

namespace ZuneXml;

internal class InboxImageQuery : DataProviderQuery
{
	private class RequestArgs
	{
		public readonly string m_requestUri;

		public readonly InboxImageDataProviderObject m_result;

		public readonly int m_tcStart;

		public RequestArgs(string requestUri, InboxImageDataProviderObject result, int tcStart)
		{
			m_requestUri = requestUri;
			m_result = result;
			m_tcStart = tcStart;
		}
	}

	private class DeferredSetStatusArgs
	{
		public readonly DataProviderQueryStatus m_eStatus;

		public DeferredSetStatusArgs(DataProviderQueryStatus eStatus)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			m_eStatus = eStatus;
		}
	}

	private class DeferredSetResultArgs
	{
		public readonly InboxImageDataProviderObject m_result;

		public DeferredSetResultArgs(InboxImageDataProviderObject result)
		{
			m_result = result;
		}
	}

	public static readonly string PropertyName_Title = "Title";

	public static readonly string PropertyName_CollectionName = "CollectionName";

	public static readonly string PropertyName_URL = "URL";

	private string _title;

	private string _collectionName;

	private string _url;

	public static DataProviderQuery ConstructQuery(object queryTypeCookie)
	{
		return (DataProviderQuery)(object)new InboxImageQuery(queryTypeCookie);
	}

	public InboxImageQuery(object queryTypeCookie)
		: base(queryTypeCookie)
	{
		((DataProviderQuery)this).Result = new InboxImageDataProviderObject((DataProviderQuery)(object)this, ((DataProviderQuery)this).ResultTypeCookie);
	}

	public override void SetProperty(string propertyName, object value)
	{
		if (propertyName == PropertyName_Title)
		{
			_title = (string)value;
			UpdateLibraryState();
			return;
		}
		if (propertyName == PropertyName_CollectionName)
		{
			_collectionName = (string)value;
			UpdateLibraryState();
			return;
		}
		if (propertyName == PropertyName_URL)
		{
			_url = (string)value;
			((DataProviderQuery)this).BeginExecute();
			return;
		}
		throw new ApplicationException("unexpected property name");
	}

	public override object GetProperty(string propertyName)
	{
		if (propertyName == PropertyName_Title)
		{
			return _title;
		}
		if (propertyName == PropertyName_CollectionName)
		{
			return _collectionName;
		}
		if (propertyName == PropertyName_URL)
		{
			return _url;
		}
		return null;
	}

	protected override void BeginExecute()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		DisposeLocalFile();
		if (!string.IsNullOrEmpty(_url) && ((DataProviderQuery)this).Result is InboxImageDataProviderObject result)
		{
			SetWorkerStatus((DataProviderQueryStatus)1);
			RequestArgs requestArgs = new RequestArgs(_url, result, Environment.TickCount);
			HttpWebRequest val = WebRequestHelper.ConstructWebRequest(_url, (EPassportPolicyId)2, (HttpRequestCachePolicy)1, fKeepAlive: true, acceptGZipEncoding: false);
			val.GetResponseAsync(new AsyncRequestComplete(OnRequestComplete), (object)requestArgs);
		}
	}

	protected override void OnDispose()
	{
		DisposeLocalFile();
		((DataProviderQuery)this).OnDispose();
	}

	private void UpdateLibraryState()
	{
		DisposeLocalFile();
		InboxImageDataProviderObject inboxImageDataProviderObject = ((DataProviderQuery)this).Result as InboxImageDataProviderObject;
		string inboxPhotoUrl = MessagingService.Instance.GetInboxPhotoUrl(_title, _collectionName);
		inboxImageDataProviderObject.InLibrary = inboxPhotoUrl != null;
		inboxImageDataProviderObject.ImagePath = inboxPhotoUrl;
	}

	private void DisposeLocalFile()
	{
		if (((DataProviderQuery)this).Result is InboxImageDataProviderObject { InLibrary: false, ImagePath: var imagePath } && !string.IsNullOrEmpty(imagePath))
		{
			try
			{
				File.Delete(imagePath);
			}
			catch (Exception)
			{
			}
		}
	}

	private void OnRequestComplete(HttpWebResponse response, object requestArgs)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Expected O, but got Unknown
		RequestArgs requestArgs2 = (RequestArgs)requestArgs;
		_ = requestArgs2.m_requestUri;
		InboxImageDataProviderObject result = requestArgs2.m_result;
		int tcStart = requestArgs2.m_tcStart;
		Stream stream = null;
		FileStream fileStream = null;
		try
		{
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new HttpWebException(response);
			}
			stream = response.GetResponseStream();
			_ = Environment.TickCount;
			SetWorkerStatus((DataProviderQueryStatus)2);
			int num = (int)response.ContentLength;
			byte[] buffer = new byte[40960];
			string tempFileName = Path.GetTempFileName();
			fileStream = new FileStream(tempFileName, FileMode.Truncate, FileAccess.Write, FileShare.None, 40960, FileOptions.SequentialScan);
			int num2;
			while (num > 0 && (num2 = stream.Read(buffer, 0, 40960)) > 0)
			{
				fileStream.Write(buffer, 0, num2);
				num -= num2;
			}
			fileStream.Close();
			result.ImagePath = tempFileName;
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetResult), (object)new DeferredSetResultArgs(result));
			SetWorkerStatus((DataProviderQueryStatus)3);
			_ = Environment.TickCount;
		}
		catch (Exception)
		{
			SetWorkerStatus((DataProviderQueryStatus)4);
		}
		finally
		{
			fileStream?.Close();
			stream?.Close();
		}
	}

	private void SetWorkerStatus(DataProviderQueryStatus eStatus)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredSetStatus), (object)new DeferredSetStatusArgs(eStatus));
	}

	private void DeferredSetStatus(object args)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		if (args is DeferredSetStatusArgs deferredSetStatusArgs)
		{
			((DataProviderQuery)this).Status = deferredSetStatusArgs.m_eStatus;
		}
	}

	private void DeferredSetResult(object argsObj)
	{
		if (argsObj is DeferredSetResultArgs deferredSetResultArgs)
		{
			deferredSetResultArgs.m_result.TransferToAppThread();
			if (((DataProviderQuery)this).Result != deferredSetResultArgs.m_result)
			{
				((DataProviderQuery)this).Result = deferredSetResultArgs.m_result;
			}
		}
	}
}
