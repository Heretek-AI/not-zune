using Microsoft.Iris;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class LibraryVideoActionCommand : MarketplaceActionCommand
{
	public override bool CanFindInCollection => false;

	protected override string ZuneMediaIdPropertyName => "ZuneMediaId";

	protected override EContentType ContentType => (EContentType)3;

	public override void FindInCollection()
	{
	}

	public override void UpdateState()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		base.Progress = -1f;
		base.CollectionId = -1;
		base.HasPoints = false;
		base.Downloading = false;
		bool flag = false;
		bool flag2 = false;
		if (base.AllowDownload)
		{
			if (ZuneApplication.Service.IsDownloading(base.Id, ContentType, ref flag, ref flag2))
			{
				base.Downloading = true;
				if (flag)
				{
					((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PENDING);
					return;
				}
				float num = 0f;
				((ModelItem)this).Description = string.Empty;
				DownloadTask task = DownloadManager.Instance.GetTask(base.Id.ToString());
				if (task != null)
				{
					num = task.GetProgress();
					UpdateProgress(base.Id, num);
				}
			}
			else
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DOWNLOAD);
				base.Downloading = false;
			}
		}
		else
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_NOT_AVAILABLE);
			((Command)this).Available = false;
		}
	}
}
