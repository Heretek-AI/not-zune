using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class AutoCompleteHelper : ModelItem
{
	private IList _options;

	private IList _filteredOptions;

	private string _entry;

	private int _cursorPosition;

	private bool _useEntrySeparator = true;

	private bool _applyfilter = true;

	protected static char[] s_entrySeparators = new char[2] { ';', ',' };

	private static char[] s_entryTrimmers = new char[3] { ';', ',', ' ' };

	public IList Options
	{
		get
		{
			return _options;
		}
		set
		{
			if (_options != value)
			{
				_options = value;
				((ModelItem)this).FirePropertyChanged("Options");
			}
		}
	}

	public string Entry
	{
		get
		{
			return _entry;
		}
		set
		{
			if (_entry != value)
			{
				if (_entry != null && value != null && value.StartsWith(_entry, StringComparison.CurrentCultureIgnoreCase))
				{
					_cursorPosition = value.Length;
				}
				else
				{
					_cursorPosition = -1;
				}
				_entry = value;
				if (Applyfilter)
				{
					Filter();
				}
				else
				{
					FilteredOptions = _options;
				}
				((ModelItem)this).FirePropertyChanged("Entry");
			}
		}
	}

	public IList FilteredOptions
	{
		get
		{
			return _filteredOptions;
		}
		set
		{
			if (_filteredOptions != value)
			{
				_filteredOptions = value;
				((ModelItem)this).FirePropertyChanged("FilteredOptions");
			}
		}
	}

	public bool UseEntrySeparator
	{
		get
		{
			return _useEntrySeparator;
		}
		set
		{
			if (_useEntrySeparator != value)
			{
				_useEntrySeparator = value;
				((ModelItem)this).FirePropertyChanged("UseEntrySeparator");
			}
		}
	}

	public bool Applyfilter
	{
		get
		{
			return _applyfilter;
		}
		set
		{
			if (_applyfilter != value)
			{
				_applyfilter = value;
				((ModelItem)this).FirePropertyChanged("Applyfilter");
			}
		}
	}

	public AutoCompleteHelper()
	{
		_entry = string.Empty;
		_cursorPosition = 0;
	}

	public string InsertAtCursor(string insert)
	{
		insert = insert ?? string.Empty;
		string filterString = GetFilterString();
		if (_cursorPosition > 0 && filterString.Length > 0)
		{
			insert = _entry.Substring(0, _cursorPosition - filterString.Length) + insert;
			if (_cursorPosition < _entry.Length)
			{
				insert += _entry.Substring(_cursorPosition - filterString.Length);
			}
			else if (_useEntrySeparator)
			{
				insert = $"{insert}{s_entrySeparators[0]} ";
			}
			Entry = insert;
		}
		else
		{
			InsertAtEnd(insert);
		}
		return Entry;
	}

	public string InsertAtEnd(string insert)
	{
		if (Entry == null || !_useEntrySeparator)
		{
			Entry = insert;
		}
		else if (!string.IsNullOrEmpty(insert))
		{
			string text = Entry.TrimEnd(s_entryTrimmers);
			if (text.Length > 0)
			{
				insert = $"{s_entrySeparators[0]} {insert}{s_entrySeparators[0]} ";
			}
			else if (_useEntrySeparator)
			{
				insert = $"{insert}{s_entrySeparators[0]} ";
			}
			Entry = text + insert;
		}
		return Entry;
	}

	private void Filter()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		List<string> list = new List<string>();
		string filterString = GetFilterString();
		if (_options != null && !string.IsNullOrEmpty(filterString))
		{
			int i;
			if (_options is ISearchableList)
			{
				i = ((ISearchableList)_options).SearchForString(filterString);
			}
			else if (_options is List<string>)
			{
				i = ((List<string>)_options).BinarySearch(filterString, StringComparer.CurrentCultureIgnoreCase);
				if (i < 0)
				{
					i = ~i;
				}
			}
			else
			{
				i = -1;
			}
			if (i >= 0)
			{
				for (; i < _options.Count; i++)
				{
					string text = (string)_options[i];
					if (ZuneLibrary.CompareWithoutArticles(filterString, text) != 0)
					{
						break;
					}
					list.Add(text);
				}
			}
		}
		FilteredOptions = list;
	}

	private string GetFilterString()
	{
		string text = string.Empty;
		if (_cursorPosition > 0 && _entry != null)
		{
			int i;
			for (i = _entry.LastIndexOfAny(s_entrySeparators, _cursorPosition - 1) + 1; i < _entry.Length && char.IsWhiteSpace(_entry, i); i++)
			{
			}
			int num = _entry.IndexOfAny(s_entrySeparators, i);
			num = ((num <= 0) ? (_entry.Length - i) : (num - i));
			text = _entry.Substring(i, num);
		}
		return text.Normalize();
	}
}
