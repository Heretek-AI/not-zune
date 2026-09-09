using Microsoft.Iris;
using Microsoft.Zune.Service;
using ZuneXml;

namespace ZuneUI;

public class MarketplaceVideoActionCommand : MarketplaceActionCommand
{
	private bool _enableForAlbumOnly;

	internal Video VideoModel => (Video)(object)base.Model;

	public bool EnableForAlbumOnly
	{
		get
		{
			return _enableForAlbumOnly;
		}
		set
		{
			if (_enableForAlbumOnly != value)
			{
				_enableForAlbumOnly = value;
				((ModelItem)this).FirePropertyChanged("EnableForAlbumOnly");
			}
		}
	}

	protected override EContentType ContentType => (EContentType)3;

	public override void FindInCollection()
	{
		if (CanFindInCollection)
		{
			VideoLibraryPage.FindInCollection(base.CollectionId);
		}
	}

	public override void UpdateState()
	{
		base.UpdateState();
		if (CanFindInCollection || base.Downloading)
		{
			return;
		}
		if (VideoModel.CanPurchaseAlbumOnly)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_ALBUM_ONLY);
			((Command)this).Available = _enableForAlbumOnly;
		}
		else if (VideoModel.CanRent)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_RENT_BUTTON);
			((Command)this).Available = true;
		}
		else if (VideoModel.CanPurchase)
		{
			int pointsPrice = VideoModel.PointsPrice;
			if (pointsPrice > 0)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_PURCHASE_BUTTON);
			}
			else
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FREE);
			}
			((Command)this).Available = true;
		}
		else
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_NOT_AVAILABLE);
			((Command)this).Available = false;
		}
	}
}
