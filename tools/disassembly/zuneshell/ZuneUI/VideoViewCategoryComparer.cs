using System.Collections;

namespace ZuneUI;

public class VideoViewCategoryComparer : IComparer
{
	public int Compare(object x, object y)
	{
		VideoViewCategory videoViewCategory = x as VideoViewCategory;
		VideoViewCategory videoViewCategory2 = y as VideoViewCategory;
		if (videoViewCategory != null && videoViewCategory2 != null)
		{
			string view = videoViewCategory.View;
			string view2 = videoViewCategory2.View;
			if (view != null && view2 != null)
			{
				return view.CompareTo(view2);
			}
		}
		return 1;
	}
}
