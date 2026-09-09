using System.Collections;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class ListPanel : LibraryPanel
{
	private IList _content;

	private object _selectedItem;

	public IList Content
	{
		get
		{
			return _content;
		}
		set
		{
			if (_content != value)
			{
				_content = value;
				((ModelItem)this).FirePropertyChanged("Content");
			}
		}
	}

	public object SelectedItem
	{
		get
		{
			return _selectedItem;
		}
		set
		{
			if (_selectedItem != value)
			{
				object selectedItem = _selectedItem;
				ModelItem val = (ModelItem)((selectedItem is ModelItem) ? selectedItem : null);
				if (val != null)
				{
					val.Selected = false;
				}
				val = (ModelItem)((value is ModelItem) ? value : null);
				if (val != null)
				{
					val.Selected = true;
				}
				_selectedItem = value;
				((ModelItem)this).FirePropertyChanged("SelectedItem");
			}
		}
	}

	public virtual IList SelectedLibraryIds
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public ListPanel()
		: this(null)
	{
	}

	public ListPanel(IModelItemOwner owner)
		: base(owner)
	{
	}

	public int GetIndexFromLibraryId(int libraryIdToFind)
	{
		int result = -1;
		int num = 0;
		if (Content != null)
		{
			foreach (object item in Content)
			{
				LibraryDataProviderItemBase val = (LibraryDataProviderItemBase)((item is LibraryDataProviderItemBase) ? item : null);
				if (val != null)
				{
					int num2 = (int)((DataProviderObject)val).GetProperty("LibraryId");
					if (num2 == libraryIdToFind)
					{
						result = num;
						break;
					}
				}
				num++;
			}
		}
		return result;
	}

	public IList ComputeSelectedIndicies()
	{
		ArrayList arrayList = null;
		if (SelectedLibraryIds != null && Content != null)
		{
			arrayList = new ArrayList(SelectedLibraryIds.Count);
			if (!IsContentDisposed())
			{
				ArrayList arrayList2 = new ArrayList(SelectedLibraryIds);
				int num = 0;
				foreach (object item in Content)
				{
					LibraryDataProviderItemBase val = (LibraryDataProviderItemBase)((item is LibraryDataProviderItemBase) ? item : null);
					if (val != null)
					{
						int num2 = (int)((DataProviderObject)val).GetProperty("LibraryId");
						if (arrayList2.Contains(num2))
						{
							arrayList.Add(num);
							arrayList2.Remove(num2);
							if (arrayList2.Count == 0)
							{
								break;
							}
						}
					}
					num++;
				}
			}
		}
		else
		{
			arrayList = new ArrayList();
		}
		return arrayList;
	}

	internal override void Release()
	{
		Content = null;
		base.Release();
	}

	private bool IsContentDisposed()
	{
		IList content = Content;
		LibraryVirtualList val = (LibraryVirtualList)((content is LibraryVirtualList) ? content : null);
		if (val != null)
		{
			ZuneQueryList queryList = val.QueryList;
			if (queryList != null)
			{
				return queryList.IsDisposed;
			}
		}
		return false;
	}
}
