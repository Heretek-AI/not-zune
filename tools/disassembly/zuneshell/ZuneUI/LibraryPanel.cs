using Microsoft.Iris;

namespace ZuneUI;

public class LibraryPanel : PropertySet
{
	private string _ui;

	private LibraryPage _libraryPage;

	public string UI
	{
		get
		{
			return _ui;
		}
		set
		{
			if (_ui != value)
			{
				_ui = value;
				((ModelItem)this).FirePropertyChanged("UI");
			}
		}
	}

	public virtual MediaType MediaType => LibraryPage.MediaType;

	public virtual SyncCategory SyncCategory => UIDeviceList.MapMediaTypeToSyncCategory(MediaType);

	public LibraryPage LibraryPage => _libraryPage;

	public LibraryPanel()
		: this(null)
	{
	}

	public LibraryPanel(IModelItemOwner owner)
		: base(owner)
	{
		_libraryPage = owner as LibraryPage;
	}

	internal virtual void Release()
	{
	}
}
