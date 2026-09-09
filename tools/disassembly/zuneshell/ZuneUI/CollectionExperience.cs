using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Configuration;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class CollectionExperience : Experience
{
	private ArrayListDataSet _nodes;

	private Node _music;

	private Node _videos;

	private Node _photos;

	private Node _podcasts;

	private Node _channels;

	private Node _applications;

	private Node _radio;

	private Node _downloads;

	private bool _isDevice;

	public override IList NodesList
	{
		get
		{
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			if (_nodes == null)
			{
				_nodes = new ArrayListDataSet((IModelItemOwner)(object)this);
				((ListDataSet)_nodes).Add((object)Music);
				((ListDataSet)_nodes).Add((object)Videos);
				((ListDataSet)_nodes).Add((object)Photos);
				((ListDataSet)_nodes).Add((object)Podcasts);
				if (FeatureEnablement.IsFeatureEnabled((Features)8))
				{
					((ListDataSet)_nodes).Add((object)Channels);
				}
				if (FeatureEnablement.IsFeatureEnabled((Features)9))
				{
					((ListDataSet)_nodes).Add((object)Radio);
				}
				UpdateApplicationPivot();
			}
			return (IList)_nodes;
		}
	}

	public Node Music
	{
		get
		{
			if (_music == null)
			{
				_music = new Node(this, StringId.IDS_MUSIC_PIVOT, _isDevice ? "Device\\Music" : "Collection\\Music\\Default", (SQMDataId)(_isDevice ? 111 : 105));
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
				_videos = new Node(this, StringId.IDS_VIDEO_PIVOT, _isDevice ? "Device\\Videos" : "Collection\\Videos\\Default", (SQMDataId)(_isDevice ? 112 : 106));
			}
			return _videos;
		}
	}

	public Node Photos
	{
		get
		{
			if (_photos == null)
			{
				_photos = new Node(this, StringId.IDS_PICTURES_PIVOT, _isDevice ? "Device\\Photos" : "Collection\\Photos", (SQMDataId)(_isDevice ? 113 : 107));
			}
			return _photos;
		}
	}

	public Node Podcasts
	{
		get
		{
			if (_podcasts == null)
			{
				_podcasts = new Node(this, StringId.IDS_PODCASTS_PIVOT, _isDevice ? "Device\\Podcasts" : "Collection\\Podcasts", (SQMDataId)(_isDevice ? 114 : 108));
			}
			return _podcasts;
		}
	}

	public Node Channels
	{
		get
		{
			if (_channels == null)
			{
				_channels = new Node(this, StringId.IDS_CHANNELS_PIVOT, _isDevice ? "Device\\Channels" : "Collection\\Channels", (SQMDataId)(_isDevice ? 116 : 109));
			}
			return _channels;
		}
	}

	public Node Applications
	{
		get
		{
			if (_applications == null)
			{
				_applications = new Node(this, StringId.IDS_APPS_PIVOT, _isDevice ? "Device\\Applications" : "Collection\\Applications", (SQMDataId)0);
			}
			return _applications;
		}
	}

	public Node Radio
	{
		get
		{
			if (_radio == null)
			{
				_radio = new Node(this, StringId.IDS_RADIO_PIVOT, _isDevice ? "Device\\Radio" : "Collection\\Radio", (SQMDataId)0);
			}
			return _radio;
		}
	}

	public Node Downloads
	{
		get
		{
			if (_downloads == null)
			{
				_downloads = new Node(this, StringId.IDS_DOWNLOADS_PIVOT, "Collection\\Downloads", (SQMDataId)124);
			}
			return _downloads;
		}
	}

	public override string DefaultUIPath => "Collection\\Default";

	public CollectionExperience(Frame frameOwner)
		: base(frameOwner, StringId.IDS_COLLECTION_PIVOT, (SQMDataId)99)
	{
	}

	public CollectionExperience(Frame frameOwner, bool isDevice)
		: base(frameOwner, StringId.IDS_DEVICE_PIVOT, (SQMDataId)100)
	{
		_isDevice = isDevice;
	}

	public void UpdateApplicationPivot()
	{
		if (FeatureEnablement.IsFeatureEnabled((Features)10) && ClientConfiguration.Shell.ShowApplicationPivot)
		{
			if (!NodesList.Contains(Applications))
			{
				NodesList.Add(Applications);
			}
		}
		else if (NodesList.Contains(Applications))
		{
			NodesList.Remove(Applications);
		}
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
}
