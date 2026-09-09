using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class PlaylistContentsPanel : ListPanel
{
	private IList _selectedIds;

	private IList _selectedPlaylistIds;

	public override MediaType MediaType => MediaType.PlaylistContentItem;

	public override IList SelectedLibraryIds
	{
		get
		{
			return _selectedIds;
		}
		set
		{
			if (_selectedIds != value)
			{
				_selectedIds = value;
				((ModelItem)this).FirePropertyChanged("SelectedLibraryIds");
			}
		}
	}

	public IList SelectedPlaylistIds
	{
		get
		{
			return _selectedPlaylistIds;
		}
		set
		{
			if (_selectedPlaylistIds != value)
			{
				PlaylistManager.Instance.UnfreezeAutoPlaylist(SelectedPlaylistId);
				_selectedPlaylistIds = value;
				((ModelItem)this).FirePropertyChanged("SelectedPlaylistIds");
				((ModelItem)this).FirePropertyChanged("SelectedPlaylistId");
				PlaylistManager.Instance.FreezeAutoPlaylist(SelectedPlaylistId);
			}
		}
	}

	public int SelectedPlaylistId
	{
		get
		{
			if (_selectedPlaylistIds != null)
			{
				return (int)_selectedPlaylistIds[0];
			}
			return -1;
		}
	}

	private static string PanelTemplate => "res://ZuneShellResources!PlaylistContentsPanel.uix#PlaylistContentsPanel";

	internal PlaylistContentsPanel(LibraryPage page)
		: base((IModelItemOwner)(object)page)
	{
		base.UI = PanelTemplate;
	}

	internal override void Release()
	{
		PlaylistManager.Instance.UnfreezeAutoPlaylist(SelectedPlaylistId);
		base.Release();
	}
}
