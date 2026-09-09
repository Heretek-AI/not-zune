using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class CategoryPageNode : Node
{
	private IList _categories;

	private bool _allowBackNavigation = true;

	private bool _hideDeviceOnCancel = true;

	public IList Categories => _categories;

	public bool AllowBackNavigation => _allowBackNavigation;

	public bool HideDeviceOnCancel => _hideDeviceOnCancel;

	public CategoryPageNode(Experience owner, StringId id, IList categories, SQMDataId sqmDataID, bool allowBackNavigation, bool hideDeviceOnCancel)
		: base(owner, id, null, sqmDataID)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		_allowBackNavigation = allowBackNavigation;
		_hideDeviceOnCancel = hideDeviceOnCancel;
		bool flag = false;
		foreach (Category category3 in categories)
		{
			if (category3 == null)
			{
				flag = true;
			}
		}
		if (flag)
		{
			List<Category> list = new List<Category>(categories.Count);
			foreach (Category category4 in categories)
			{
				if (category4 != null)
				{
					list.Add(category4);
				}
			}
			categories = list.ToArray();
		}
		_categories = categories;
	}

	protected override void Execute(Shell shell)
	{
		Invoke((Category)_categories[0], null);
	}

	public void Invoke(Category category)
	{
		Invoke(category, null);
	}

	public void Invoke(Category category, IDictionary commandArgs)
	{
		ZuneShell defaultInstance = ZuneShell.DefaultInstance;
		if (defaultInstance == null)
		{
			throw new InvalidOperationException("No Shell instance has been registered.  Unable to perform navigation.");
		}
		CategoryPage categoryPage = new CategoryPage(this);
		categoryPage.CurrentCategory = category;
		if (commandArgs != null)
		{
			categoryPage.NavigationArguments = commandArgs;
		}
		defaultInstance.NavigateToPage(categoryPage);
	}
}
