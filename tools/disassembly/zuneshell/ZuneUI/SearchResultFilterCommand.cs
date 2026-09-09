using System;
using Microsoft.Iris;

namespace ZuneUI;

public class SearchResultFilterCommand : Command
{
	private SearchResultFilterType _type = SearchResultFilterType.Undefined;

	private bool _hasResults;

	public SearchResultFilterType Type => _type;

	public bool HasResults
	{
		get
		{
			return _hasResults;
		}
		set
		{
			if (_hasResults != value)
			{
				_hasResults = value;
				((ModelItem)this).FirePropertyChanged("HasResults");
			}
		}
	}

	internal SearchResultFilterCommand(ModelItem owner, string description, SearchResultFilterType type)
		: base((IModelItemOwner)(object)owner, description, (EventHandler)null)
	{
		_type = type;
	}
}
