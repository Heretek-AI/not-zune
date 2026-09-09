using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;
using ZuneXml;

namespace ZuneUI;

public class MarketplaceAppActionCommand : MarketplaceActionCommand
{
	private string _serviceVersionString;

	private bool _allowPurchaseFull = true;

	private bool _allowPurchaseTrial = true;

	internal AppData AppData => (AppData)(object)base.Model;

	public bool AllowPurchaseFull
	{
		get
		{
			return _allowPurchaseFull;
		}
		set
		{
			if (_allowPurchaseFull != value)
			{
				_allowPurchaseFull = value;
				((ModelItem)this).FirePropertyChanged("AllowPurchaseFull");
				((ModelItem)this).FirePropertyChanged("CanPurchaseFull");
				UpdateState();
			}
		}
	}

	public bool AllowPurchaseTrial
	{
		get
		{
			return _allowPurchaseTrial;
		}
		set
		{
			if (_allowPurchaseTrial != value)
			{
				_allowPurchaseTrial = value;
				((ModelItem)this).FirePropertyChanged("AllowPurchaseTrial");
				((ModelItem)this).FirePropertyChanged("CanPurchaseTrial");
				UpdateState();
			}
		}
	}

	private string ServiceVersionString
	{
		get
		{
			if (_serviceVersionString == null)
			{
				_serviceVersionString = ((base.Model != null) ? ((string)base.Model.GetProperty("Version")) : null);
			}
			return _serviceVersionString;
		}
	}

	public bool NeedsUpdate
	{
		get
		{
			bool result = false;
			if (CanFindInCollection)
			{
				return ApplicationLibraryPage.DoesApplicationNeedUpdate(base.CollectionId, ServiceVersionString);
			}
			return result;
		}
	}

	public bool CanDownload
	{
		get
		{
			if (AppData != null && AppData.CanDownload)
			{
				return base.AllowDownload;
			}
			return false;
		}
	}

	public bool CanPurchaseFull
	{
		get
		{
			if (AppData != null && AppData.CanPurchaseFull)
			{
				return AllowPurchaseFull;
			}
			return false;
		}
	}

	public bool CanPurchaseTrial
	{
		get
		{
			if (AppData != null && AppData.CanPurchaseTrial)
			{
				return AllowPurchaseTrial;
			}
			return false;
		}
	}

	protected override EContentType ContentType => (EContentType)7;

	public override void FindInCollection()
	{
		if (CanFindInCollection)
		{
			ApplicationLibraryPage.FindInCollection(base.CollectionId);
		}
	}

	protected override void OnPropertyChanged(string property)
	{
		((ModelItem)this).OnPropertyChanged(property);
		if ("Model" == property)
		{
			base.Id = ((base.Model != null) ? ((Guid)base.Model.GetProperty("Id")) : Guid.Empty);
			_serviceVersionString = null;
			((ModelItem)this).FirePropertyChanged("CanDownload");
			((ModelItem)this).FirePropertyChanged("CanPurchaseFull");
			((ModelItem)this).FirePropertyChanged("CanPurchaseTrial");
		}
		else if ("AllowDownload" == property)
		{
			((ModelItem)this).FirePropertyChanged("CanDownload");
		}
	}

	public override void UpdateState()
	{
		base.UpdateState();
		if (base.Downloading)
		{
			return;
		}
		if (NeedsUpdate)
		{
			((ModelItem)this).Description = Shell.LoadString(StringId.IDS_UPDATE);
			((Command)this).Available = true;
		}
		else
		{
			if (CanFindInCollection)
			{
				return;
			}
			if (CanDownload)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_DOWNLOAD);
				((Command)this).Available = true;
			}
			else if (CanPurchaseFull)
			{
				if (AppData.Price == 0.0)
				{
					((ModelItem)this).Description = Shell.LoadString(StringId.IDS_FREE);
				}
				else
				{
					((ModelItem)this).Description = string.Format(Shell.LoadString(StringId.IDS_BUY_CURRENCY), AppData.DisplayPriceFull);
				}
				((Command)this).Available = true;
			}
			else if (CanPurchaseTrial)
			{
				((ModelItem)this).Description = Shell.LoadString(StringId.IDS_TRY);
				((Command)this).Available = true;
			}
		}
	}
}
