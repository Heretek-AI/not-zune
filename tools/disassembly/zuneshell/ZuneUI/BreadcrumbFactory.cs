using System.Collections;
using System.Collections.Generic;

namespace ZuneUI;

public class BreadcrumbFactory
{
	private IList<Breadcrumb> _breadcrumbs;

	public IList Breadcrumbs => (IList)_breadcrumbs;

	public void AddCrumb(Breadcrumb breadcrumb)
	{
		if (_breadcrumbs == null)
		{
			_breadcrumbs = new List<Breadcrumb>();
			breadcrumb.Active = true;
		}
		_breadcrumbs.Add(breadcrumb);
	}

	public void UpdateState(WizardPage currentPage, WizardPage destinationPage, bool movingNext)
	{
		for (int i = 0; i < _breadcrumbs.Count; i++)
		{
			Breadcrumb breadcrumb = _breadcrumbs[i];
			if (breadcrumb.Page == currentPage)
			{
				if (!movingNext)
				{
					_breadcrumbs[i].Active = false;
					if (i > 0)
					{
						_breadcrumbs[i - 1].Complete = false;
					}
				}
			}
			else
			{
				if (breadcrumb.Page != destinationPage)
				{
					continue;
				}
				_breadcrumbs[i].Active = true;
				if (movingNext)
				{
					if (i > 0)
					{
						_breadcrumbs[i - 1].Complete = true;
					}
				}
				else
				{
					_breadcrumbs[i].Complete = false;
				}
			}
		}
	}
}
