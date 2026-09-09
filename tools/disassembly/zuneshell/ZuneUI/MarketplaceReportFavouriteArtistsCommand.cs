using System;
using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class MarketplaceReportFavouriteArtistsCommand : Command
{
	private HRESULT m_lastError;

	private Guid m_userId;

	private IList m_artists;

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

	public Guid UserId
	{
		get
		{
			return m_userId;
		}
		set
		{
			if (m_userId != value)
			{
				m_userId = value;
				((ModelItem)this).FirePropertyChanged("UserId");
			}
		}
	}

	public IList Artists
	{
		get
		{
			return m_artists;
		}
		set
		{
			if (m_artists != value)
			{
				m_artists = value;
				((ModelItem)this).FirePropertyChanged("Artists");
			}
		}
	}

	public event EventHandler Completed;

	public MarketplaceReportFavouriteArtistsCommand()
	{
	}

	public MarketplaceReportFavouriteArtistsCommand(IModelItem owner)
		: base((IModelItemOwner)(object)owner)
	{
	}

	protected override void OnInvoked()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		Service.Instance.ReportFavouriteArtists(m_userId, m_artists, new AsyncCompleteHandler(OnCompleteHandler));
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
			ErrorDialogInfo.Show(((HRESULT)(ref lastError2)).Int, Shell.LoadString(StringId.IDS_ArtistChooserUploadFailedDialogTitle));
		}
	}
}
