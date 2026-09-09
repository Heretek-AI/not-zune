using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class PlaylistsPanel : ListPanel
{
	private IList _selectedIds;

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

	protected new MusicLibraryPage LibraryPage => base.LibraryPage as MusicLibraryPage;

	private static string PanelTemplate => "res://ZuneShellResources!PlaylistsPanel.uix#PlaylistsPanel";

	internal PlaylistsPanel(MusicLibraryPage page)
		: base((IModelItemOwner)(object)page)
	{
		base.UI = PanelTemplate;
	}
}
