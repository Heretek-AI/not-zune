using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;

namespace ZuneUI;

public abstract class MarketplaceActionCommand : ProgressCommand
{
	private bool m_allowPlay = true;

	private bool m_allowDownload = true;

	private bool m_hasPoints;

	private bool m_downloading;

	private bool m_downloadingHidden;

	private bool m_showHiddenProgress;

	private int m_collectionId = -1;

	private DataProviderObject m_model;

	private Guid m_id;

	private StringId m_addToCollectionStringId = StringId.IDS_ADD_TO_COLLECTION;

	public virtual bool CanFindInCollection => m_collectionId >= 0;

	public virtual bool CanFindInCollectionShortcut
	{
		get
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Invalid comparison between Unknown and I4
			if (m_collectionId >= 0)
			{
				return (int)ZuneApplication.Service.GetMediaStatus(Id, ContentType) == 7;
			}
			return false;
		}
	}

	public virtual bool CanFindInCollectionOwned
	{
		get
		{
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Invalid comparison between Unknown and I4
			if (m_collectionId >= 0)
			{
				return (int)ZuneApplication.Service.GetMediaStatus(Id, ContentType) == 4;
			}
			return false;
		}
	}

	public virtual bool CanFindInHiddenCollection => ZuneApplication.Service.InHiddenCollection(Id, ContentType);

	public virtual bool ShowHiddenProgress
	{
		get
		{
			return m_showHiddenProgress;
		}
		set
		{
			if (value != m_showHiddenProgress)
			{
				m_showHiddenProgress = value;
				((ModelItem)this).FirePropertyChanged("ShowHiddenProgress");
			}
		}
	}

	public bool AllowPlay
	{
		get
		{
			return m_allowPlay;
		}
		set
		{
			if (value != m_allowPlay)
			{
				m_allowPlay = value;
				((ModelItem)this).FirePropertyChanged("AllowPlay");
			}
		}
	}

	public bool AllowDownload
	{
		get
		{
			return m_allowDownload;
		}
		set
		{
			if (m_allowDownload != value)
			{
				m_allowDownload = value;
				((ModelItem)this).FirePropertyChanged("AllowDownload");
				UpdateState();
			}
		}
	}

	public bool CanFindInZuneDotNet
	{
		get
		{
			object property = Model.GetProperty("AlbumId");
			if (property is Guid)
			{
				return (Guid)property != Guid.Empty;
			}
			return false;
		}
	}

	public bool Downloading
	{
		get
		{
			return m_downloading;
		}
		protected set
		{
			if (value != m_downloading)
			{
				m_downloading = value;
				((ModelItem)this).FirePropertyChanged("Downloading");
			}
		}
	}

	public bool DownloadingHidden
	{
		get
		{
			return m_downloadingHidden;
		}
		protected set
		{
			if (value != m_downloadingHidden)
			{
				m_downloadingHidden = value;
				((ModelItem)this).FirePropertyChanged("DownloadingHidden");
			}
		}
	}

	public DataProviderObject Model
	{
		get
		{
			return m_model;
		}
		set
		{
			if (m_model != value)
			{
				m_model = value;
				if (value == null)
				{
					m_id = Guid.Empty;
				}
				else
				{
					m_id = (Guid)m_model.GetProperty(ZuneMediaIdPropertyName);
				}
				UpdateProgress(Guid.Empty, -1f);
				UpdateState();
				((ModelItem)this).FirePropertyChanged("Model");
			}
		}
	}

	protected virtual string ZuneMediaIdPropertyName => "Id";

	public Guid Id
	{
		get
		{
			return m_id;
		}
		set
		{
			if (m_id != value)
			{
				m_id = value;
				((ModelItem)this).FirePropertyChanged("Id");
			}
		}
	}

	public bool HasPoints
	{
		get
		{
			return m_hasPoints;
		}
		set
		{
			if (m_hasPoints != value)
			{
				m_hasPoints = value;
				((ModelItem)this).FirePropertyChanged("HasPoints");
			}
		}
	}

	protected abstract EContentType ContentType { get; }

	protected int CollectionId
	{
		get
		{
			return m_collectionId;
		}
		set
		{
			if (m_collectionId != value)
			{
				m_collectionId = value;
				((ModelItem)this).FirePropertyChanged("CollectionId");
				((ModelItem)this).FirePropertyChanged("CanFindInCollection");
				((ModelItem)this).FirePropertyChanged("CanFindInCollectionOwned");
				((ModelItem)this).FirePropertyChanged("CanFindInHiddenCollection");
			}
		}
	}

	public StringId AddToCollectionStringId
	{
		get
		{
			return m_addToCollectionStringId;
		}
		set
		{
			if (m_addToCollectionStringId != value)
			{
				m_addToCollectionStringId = value;
				((ModelItem)this).FirePropertyChanged("AddToCollectionStringId");
			}
		}
	}

	public MarketplaceActionCommand()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Expected O, but got Unknown
		((ModelItem)this).UniqueId = Guid.NewGuid();
		Download.Instance.DownloadEvent += new DownloadEventHandler(OnDownloadEvent);
		Download.Instance.DownloadProgressEvent += new DownloadEventProgressHandler(OnDownloadProgressEvent);
	}

	public abstract void FindInCollection();

	public void FindInZuneDotNet()
	{
		object property = Model.GetProperty("AlbumId");
		if (property is Guid && (Guid)property != Guid.Empty)
		{
			ZuneDotNet.ViewAlbum((Guid)property);
		}
	}

	public void FindInDownloads()
	{
		if (Downloading)
		{
			ZuneShell.DefaultInstance.Execute("Marketplace\\Downloads\\Home", null);
		}
	}

	public virtual void UpdateState()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		base.Progress = -1f;
		CollectionId = -1;
		HasPoints = false;
		Downloading = false;
		bool flag = false;
		bool flag2 = false;
		if (AllowDownload)
		{
			int collectionId = default(int);
			if (ZuneApplication.Service.InVisibleCollection(Id, ContentType, ref collectionId))
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_INCOLLECTION);
				((Command)this).Available = true;
				CollectionId = collectionId;
			}
			else if (ZuneApplication.Service.InHiddenCollection(Id, ContentType))
			{
				((ModelItem)this).Description = Shell.LoadString(AddToCollectionStringId);
				((Command)this).Available = true;
			}
			else if (ZuneApplication.Service.IsDownloading(Id, ContentType, ref flag, ref flag2) && (!flag2 || ShowHiddenProgress))
			{
				Downloading = true;
				((Command)this).Available = true;
				if (flag)
				{
					((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PENDING);
				}
				else
				{
					((ModelItem)this).Description = string.Empty;
					DownloadTask task = DownloadManager.Instance.GetTask(Id.ToString());
					if (task != null)
					{
						float num = 0f;
						num = task.GetProgress();
						OnDownloadProgressEvent(Id, num);
					}
				}
			}
			else
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_NOT_AVAILABLE);
				((Command)this).Available = false;
			}
		}
		else
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_NOT_AVAILABLE);
			((Command)this).Available = false;
		}
		DownloadingHidden = flag2;
	}

	private void OnDownloadProgressEvent(Guid trackId, float percent)
	{
		if (trackId == m_id && (!DownloadingHidden || ShowHiddenProgress))
		{
			UpdateProgress(trackId, percent);
			HasPoints = false;
			if (base.SecondsToProgressivePlayback == 0 && AllowPlay)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PLAY_SONG);
			}
			else
			{
				((ModelItem)this).Description = string.Format(Shell.LoadString(StringId.IDS_DOWNLOAD_PROGRESS), (int)percent);
			}
			Downloading = true;
			((Command)this).Available = true;
		}
	}

	private void OnDownloadEvent(Guid trackId, HRESULT hr)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (trackId == m_id)
		{
			if (hr == HRESULT._E_PENDING && (!DownloadingHidden || ShowHiddenProgress))
			{
				HasPoints = false;
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PENDING);
				Downloading = true;
				((Command)this).Available = true;
			}
			else
			{
				UpdateState();
			}
		}
	}

	protected override void OnDispose(bool fDisposing)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Expected O, but got Unknown
		((ModelItem)this).OnDispose(fDisposing);
		if (fDisposing)
		{
			Download.Instance.DownloadEvent -= new DownloadEventHandler(OnDownloadEvent);
			Download.Instance.DownloadProgressEvent -= new DownloadEventProgressHandler(OnDownloadProgressEvent);
		}
	}
}
