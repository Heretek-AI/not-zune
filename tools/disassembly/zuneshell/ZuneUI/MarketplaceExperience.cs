using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class MarketplaceExperience : Experience
{
	private ArrayListDataSet _nodes;

	private Node _recommendations;

	private Node _music;

	private Node _videos;

	private Node _podcasts;

	private Node _apps;

	private Node _channels;

	private Node _cart;

	private Node _downloads;

	private int _cartItemsCount;

	private bool _cartItemsCountInitialized;

	public override IList NodesList
	{
		get
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			if (_nodes == null)
			{
				_nodes = new ArrayListDataSet((IModelItemOwner)(object)this);
				if (FeatureEnablement.IsFeatureEnabled((Features)3))
				{
					((ListDataSet)_nodes).Add((object)Recommendations);
				}
				if (FeatureEnablement.IsFeatureEnabled((Features)28))
				{
					((ListDataSet)_nodes).Add((object)Music);
				}
				if (FeatureEnablement.IsFeatureEnabled((Features)4))
				{
					((ListDataSet)_nodes).Add((object)Videos);
				}
				if (FeatureEnablement.IsFeatureEnabled((Features)7))
				{
					((ListDataSet)_nodes).Add((object)Podcasts);
				}
				if (FeatureEnablement.IsFeatureEnabled((Features)8))
				{
					((ListDataSet)_nodes).Add((object)Channels);
				}
				if (FeatureEnablement.IsFeatureEnabled((Features)10) || FeatureEnablement.IsFeatureEnabled((Features)11))
				{
					((ListDataSet)_nodes).Add((object)Apps);
				}
			}
			return (IList)_nodes;
		}
	}

	public Node Recommendations
	{
		get
		{
			if (_recommendations == null)
			{
				_recommendations = new Node(this, StringId.IDS_RECOMMENDATIONS_PIVOT, "Marketplace\\Recommendations\\Home", (SQMDataId)117);
			}
			return _recommendations;
		}
	}

	public Node Music
	{
		get
		{
			if (_music == null)
			{
				_music = new Node(this, StringId.IDS_MUSIC_PIVOT, "Marketplace\\Music\\Home", (SQMDataId)118);
			}
			return _music;
		}
	}

	public Node Videos
	{
		get
		{
			if (_videos == null)
			{
				_videos = new Node(this, StringId.IDS_VIDEO_PIVOT, "Marketplace\\Videos\\Home", (SQMDataId)119);
			}
			return _videos;
		}
	}

	public Node Podcasts
	{
		get
		{
			if (_podcasts == null)
			{
				_podcasts = new Node(this, StringId.IDS_PODCASTS_PIVOT, "Marketplace\\Podcasts\\Home", (SQMDataId)120);
			}
			return _podcasts;
		}
	}

	public Node Apps
	{
		get
		{
			if (_apps == null)
			{
				_apps = new Node(this, StringId.IDS_APPS_PIVOT, "Marketplace\\Apps\\Home", (SQMDataId)0);
			}
			return _apps;
		}
	}

	public Node Channels
	{
		get
		{
			if (_channels == null)
			{
				_channels = new Node(this, StringId.IDS_CHANNELS_PIVOT, "Marketplace\\Channels\\Home", (SQMDataId)122);
			}
			return _channels;
		}
	}

	public Node Cart
	{
		get
		{
			if (_cart == null)
			{
				_cart = new Node(this, StringId.IDS_CART_PIVOT, "Marketplace\\Cart", (SQMDataId)121);
			}
			return _cart;
		}
	}

	public Node Downloads
	{
		get
		{
			if (_downloads == null)
			{
				_downloads = new Node(this, StringId.IDS_DOWNLOADS_PIVOT, "Marketplace\\Downloads\\Home", (SQMDataId)124);
			}
			return _downloads;
		}
	}

	public int CartItemsCount
	{
		get
		{
			return _cartItemsCount;
		}
		set
		{
			if (_cartItemsCount != value)
			{
				_cartItemsCount = value;
				((ModelItem)this).FirePropertyChanged("CartItemsCount");
				Shell.MainFrame.Marketplace.UpdatePivots();
			}
		}
	}

	public bool CartItemsCountInitialized
	{
		get
		{
			return _cartItemsCountInitialized;
		}
		set
		{
			if (_cartItemsCountInitialized != value)
			{
				_cartItemsCountInitialized = value;
				((ModelItem)this).FirePropertyChanged("CartItemsCountInitialized");
			}
		}
	}

	public override string DefaultUIPath
	{
		get
		{
			if (FeatureEnablement.IsFeatureEnabled((Features)28))
			{
				return "Marketplace\\Music\\Home";
			}
			if (FeatureEnablement.IsFeatureEnabled((Features)4))
			{
				return "Marketplace\\Videos\\Home";
			}
			if (FeatureEnablement.IsFeatureEnabled((Features)11))
			{
				return "Marketplace\\Apps\\Home";
			}
			return "Marketplace\\Default";
		}
	}

	public MarketplaceExperience(Frame frameOwner)
		: base(frameOwner, StringId.IDS_MARKETPLACE_PIVOT, (SQMDataId)101)
	{
	}

	protected override void OnIsCurrentChanged()
	{
		base.OnIsCurrentChanged();
		if (base.IsCurrent)
		{
			CultureHelper.CheckMarketplaceCulture();
		}
	}

	public void UpdatePivots()
	{
		ShowCart(FeatureEnablement.IsFeatureEnabled((Features)28) && CartItemsCount > 0);
	}

	public void UpdateDownloadPivot(bool show)
	{
		int nodeIndex = GetNodeIndex(Downloads);
		bool flag = nodeIndex != -1;
		if (show != flag)
		{
			((Command)Downloads).Available = show;
			if (show)
			{
				NodesList.Add(Downloads);
			}
			else
			{
				NodesList.RemoveAt(nodeIndex);
			}
		}
	}

	private void ShowCart(bool show)
	{
		int nodeIndex = GetNodeIndex(Cart);
		bool flag = nodeIndex != -1;
		if (show == flag)
		{
			return;
		}
		((Command)Cart).Available = show;
		if (show)
		{
			int num = GetNodeIndex(Downloads);
			if (num == -1)
			{
				num = NodesList.Count;
			}
			NodesList.Insert(num, Cart);
		}
		else
		{
			NodesList.RemoveAt(nodeIndex);
		}
	}

	protected override void OnInvoked()
	{
		if (!base.IsCurrent)
		{
			Node node = (Node)base.Nodes.ChosenValue;
			if (node == _recommendations && !SignIn.Instance.SignedIn)
			{
				base.Nodes.ChosenIndex = 1;
			}
		}
		base.OnInvoked();
	}
}
