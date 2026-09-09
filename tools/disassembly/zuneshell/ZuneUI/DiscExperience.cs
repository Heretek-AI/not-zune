using System.Collections;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class DiscExperience(Frame frameOwner) : Experience(frameOwner, StringId.IDS_DISC_PIVOT, (SQMDataId)103)
{
	private ArrayListDataSet _nodes = new ArrayListDataSet();

	private CDAlbumCommand _burnList;

	private CDAlbumCommand _noCD;

	public override IList NodesList => (IList)_nodes;

	public CDAlbumCommand BurnList
	{
		get
		{
			if (_burnList == null)
			{
				_burnList = new CDAlbumCommand(this, StringId.IDS_PLAYLIST_BURN_LIST);
			}
			return _burnList;
		}
	}

	public CDAlbumCommand NoCD
	{
		get
		{
			if (_noCD == null)
			{
				_noCD = new CDAlbumCommand(this, StringId.IDS_NO_CD);
			}
			return _noCD;
		}
	}

	public bool HasCD
	{
		get
		{
			if (_nodes != null)
			{
				return ((ListDataSet)_nodes)[0] != NoCD;
			}
			return true;
		}
	}

	protected override void OnIsCurrentChanged()
	{
		UpdateShowDisc();
	}

	public void UpdateShowDisc()
	{
		((MainFrame)base.Frame).ShowDisc(base.IsCurrent || CDAccess.Instance.HasLoadedMedia || !CDAccess.Instance.IsBurnListEmpty);
	}

	public void RecalculateAvailableNodes()
	{
		((ListDataSet)_nodes).Clear();
		foreach (CDAlbumCommand item in (ListDataSet)CDAccess.Instance.CDs)
		{
			if (item != null && item.TOC != null)
			{
				((ListDataSet)_nodes).Add((object)item);
			}
		}
		if (CDAccess.Instance.BurnListId >= 0)
		{
			((ListDataSet)_nodes).Add((object)BurnList);
		}
		if (((ListDataSet)_nodes).Count == 0)
		{
			((ListDataSet)_nodes).Add((object)NoCD);
		}
		UpdateShowDisc();
	}

	public void UpdateNodes(CDAccess cdAccess, char changedLetter, bool mediaArrived)
	{
		RecalculateAvailableNodes();
		CDAlbumCommand cDAlbumCommand = (CDAlbumCommand)NodesList[0];
		if (base.IsCurrent)
		{
			if (ZuneShell.DefaultInstance.CurrentPage is CDLand cDLand)
			{
				bool flag = false;
				if (cDLand.Album.CDDevice != null && cDLand.Album.CDDevice.DrivePath == changedLetter)
				{
					flag = true;
				}
				if (flag || (mediaArrived && NoCD.IsCurrent))
				{
					((Command)cDAlbumCommand).Invoke();
				}
			}
		}
		else
		{
			if (!mediaArrived)
			{
				return;
			}
			foreach (CDAlbumCommand nodes in NodesList)
			{
				if (nodes.CDDevice != null && nodes.CDDevice.DrivePath == changedLetter)
				{
					base.Nodes.ChosenValue = nodes;
					break;
				}
			}
		}
	}
}
