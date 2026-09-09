using System.Collections;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class ListReleaseBehaviorManager
{
	private LibraryVirtualList _virtualList;

	private ReleaseBehavior _cachedBehavior;

	public void KeepItemsInMemory(IList list)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Invalid comparison between Unknown and I4
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (list != null && list.Count > 0)
		{
			LibraryVirtualList owner = ((LibraryDataProviderListItem)list[0]).GetOwner();
			if ((int)((VirtualList)owner).VisualReleaseBehavior == 1)
			{
				_cachedBehavior = ((VirtualList)owner).VisualReleaseBehavior;
				((VirtualList)owner).VisualReleaseBehavior = (ReleaseBehavior)0;
				_virtualList = owner;
			}
		}
	}

	public void RestoreDefaultBehavior()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (_virtualList != null)
		{
			((VirtualList)_virtualList).VisualReleaseBehavior = _cachedBehavior;
			_virtualList = null;
		}
	}
}
