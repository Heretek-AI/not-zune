using System;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class CartItem : ModelItem
{
	private string _messagingId;

	private string _type;

	private string _detailsLink;

	private Guid _mediaId;

	private EContentType _mediaType;

	private string _displayType;

	private DataProviderObject _marketplaceItem;

	private string _title;

	private string _sortTitle;

	private string _artistName;

	private string _albumTitle;

	private bool _availableInMarketplace;

	private Command _executeCommand;

	private object _extraData;

	public string MessagingId
	{
		get
		{
			return _messagingId;
		}
		set
		{
			if (_messagingId != value)
			{
				_messagingId = value;
				((ModelItem)this).FirePropertyChanged("MessagingId");
			}
		}
	}

	public string Type
	{
		get
		{
			return _type;
		}
		set
		{
			if (_type != value)
			{
				_type = value;
				((ModelItem)this).FirePropertyChanged("Type");
			}
		}
	}

	public string DetailsLink
	{
		get
		{
			return _detailsLink;
		}
		set
		{
			if (_detailsLink != value)
			{
				_detailsLink = value;
				((ModelItem)this).FirePropertyChanged("DetailsLink");
			}
		}
	}

	public Guid MediaId
	{
		get
		{
			return _mediaId;
		}
		set
		{
			if (_mediaId != value)
			{
				_mediaId = value;
				((ModelItem)this).FirePropertyChanged("MediaId");
			}
		}
	}

	public EContentType MediaType
	{
		get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _mediaType;
		}
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			if (_mediaType != value)
			{
				_mediaType = value;
				((ModelItem)this).FirePropertyChanged("MediaType");
			}
		}
	}

	public string DisplayType
	{
		get
		{
			return _displayType;
		}
		set
		{
			if (_displayType != value)
			{
				_displayType = value;
				((ModelItem)this).FirePropertyChanged("DisplayType");
			}
		}
	}

	public DataProviderObject MarketplaceItem
	{
		get
		{
			return _marketplaceItem;
		}
		set
		{
			if (_marketplaceItem != value)
			{
				_marketplaceItem = value;
				((ModelItem)this).FirePropertyChanged("MarketplaceItem");
			}
		}
	}

	public string Title
	{
		get
		{
			return _title;
		}
		set
		{
			if (_title != value)
			{
				_title = value;
				((ModelItem)this).FirePropertyChanged("Title");
			}
		}
	}

	public string SortTitle
	{
		get
		{
			return _sortTitle;
		}
		set
		{
			if (_sortTitle != value)
			{
				_sortTitle = value;
				((ModelItem)this).FirePropertyChanged("SortTitle");
			}
		}
	}

	public string ArtistName
	{
		get
		{
			return _artistName;
		}
		set
		{
			if (_artistName != value)
			{
				_artistName = value;
				((ModelItem)this).FirePropertyChanged("ArtistName");
			}
		}
	}

	public string AlbumTitle
	{
		get
		{
			return _albumTitle;
		}
		set
		{
			if (_albumTitle != value)
			{
				_albumTitle = value;
				((ModelItem)this).FirePropertyChanged("AlbumTitle");
			}
		}
	}

	public bool AvailableInMarketplace
	{
		get
		{
			return _availableInMarketplace;
		}
		set
		{
			if (_availableInMarketplace != value)
			{
				_availableInMarketplace = value;
				((ModelItem)this).FirePropertyChanged("AvailableInMarketplace");
			}
		}
	}

	public Command ExecuteCommand
	{
		get
		{
			return _executeCommand;
		}
		set
		{
			if (_executeCommand != value)
			{
				_executeCommand = value;
				((ModelItem)this).FirePropertyChanged("ExecuteCommand");
			}
		}
	}

	public object ExtraData
	{
		get
		{
			return _extraData;
		}
		set
		{
			if (_extraData != value)
			{
				_extraData = value;
				((ModelItem)this).FirePropertyChanged("ExtraData");
			}
		}
	}

	public CartItem()
	{
		_mediaId = Guid.Empty;
		_albumTitle = string.Empty;
		_title = string.Empty;
		_artistName = string.Empty;
	}
}
