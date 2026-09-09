using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class MarketplaceReportAConcernCommand : Command
{
	private HRESULT m_lastError;

	private EConcernType m_concernType;

	private EContentType m_contentType;

	private Guid m_mediaId;

	private string m_message;

	public HRESULT LastError
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_lastError;
		}
		private set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			if (m_lastError != value)
			{
				m_lastError = value;
				((ModelItem)this).FirePropertyChanged("LastError");
			}
		}
	}

	public EConcernType ConcernType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_concernType;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (m_concernType != value)
			{
				m_concernType = value;
				((ModelItem)this).FirePropertyChanged("ConcernType");
			}
		}
	}

	public EContentType ContentType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return m_contentType;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (m_contentType != value)
			{
				m_contentType = value;
				((ModelItem)this).FirePropertyChanged("ContentType");
			}
		}
	}

	public Guid MediaId
	{
		get
		{
			return m_mediaId;
		}
		set
		{
			if (m_mediaId != value)
			{
				m_mediaId = value;
				((ModelItem)this).FirePropertyChanged("MediaId");
			}
		}
	}

	public string Message
	{
		get
		{
			return m_message;
		}
		set
		{
			if (m_message != value)
			{
				m_message = value;
				((ModelItem)this).FirePropertyChanged("Message");
			}
		}
	}

	public event EventHandler Completed;

	public MarketplaceReportAConcernCommand()
	{
	}

	public MarketplaceReportAConcernCommand(IModelItem owner)
		: base((IModelItemOwner)(object)owner)
	{
	}

	protected override void OnInvoked()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		Service.Instance.ReportAConcern(m_concernType, m_contentType, m_mediaId, m_message, new AsyncCompleteHandler(OnCompleteHandler));
	}

	private void OnCompleteHandler(HRESULT hr)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(OnCompleteHandler), (object)hr);
	}

	private void OnCompleteHandler(object arg)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		LastError = (HRESULT)arg;
		if (this.Completed != null)
		{
			this.Completed(this, null);
		}
		((ModelItem)this).FirePropertyChanged("Completed");
		HRESULT lastError = LastError;
		if (((HRESULT)(ref lastError)).IsError)
		{
			HRESULT lastError2 = LastError;
			ErrorDialogInfo.Show(((HRESULT)(ref lastError2)).Int, Shell.LoadString(StringId.IDS_PodcastConcernTitle));
		}
	}
}
