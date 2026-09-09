using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Text;
using Microsoft.Iris;
using Microsoft.Zune.Util;

namespace ZuneUI;

public class Search : ModelItem
{
	private struct IndexRange
	{
		public int indexStart;

		public int indexEnd;
	}

	private string _keywords = "";

	private string[] _processedKeywords;

	private static Search _singletonInstance;

	private bool _ignoreAccented;

	private Dictionary<char, char> _accentTable;

	private Command _executeCommand;

	private Hashtable _keywordLinkTable;

	private Command _searchFocusHotkey;

	private Choice _filterList;

	private SearchResultFilterType _selectedFilter;

	private SearchResultContextType _usersContextType = SearchResultContextType.Undefined;

	private SearchHintHelper _hintHelper;

	private static List<SearchResultFilterCommand> _searchResultFilterCommandTypes;

	public static Search Instance
	{
		get
		{
			if (_singletonInstance == null)
			{
				_singletonInstance = new Search();
			}
			return _singletonInstance;
		}
	}

	internal static bool HasInstance => _singletonInstance != null;

	public Choice FilterList
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Expected O, but got Unknown
			if (_filterList == null)
			{
				_filterList = new Choice((IModelItemOwner)(object)this);
				_filterList.Options = (IList)new ArrayListDataSet();
				_filterList.ChosenChanged += FilterListChosenChanged;
				UpdateFiltersList();
			}
			_filterList.ChosenIndex = 0;
			return _filterList;
		}
	}

	public SearchResultFilterType SelectedFilterType
	{
		get
		{
			return _selectedFilter;
		}
		set
		{
			if (_selectedFilter != value)
			{
				_selectedFilter = value;
				((ModelItem)this).FirePropertyChanged("SelectedFilterType");
				SQMLog.Log((SQMDataId)79, 1);
				SQMLog.LogToStream((SQMDataId)80, (uint)_selectedFilter);
			}
		}
	}

	public SearchResultContextType UsersContextType
	{
		get
		{
			return _usersContextType;
		}
		set
		{
			if (_usersContextType != value)
			{
				_usersContextType = value;
				((ModelItem)this).FirePropertyChanged("UsersContextType");
			}
		}
	}

	public SearchHintHelper HintHelper
	{
		get
		{
			if (_hintHelper == null)
			{
				_hintHelper = new SearchHintHelper();
			}
			return _hintHelper;
		}
	}

	public Command SearchFocusHotkey
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Expected O, but got Unknown
			if (_searchFocusHotkey == null)
			{
				_searchFocusHotkey = new Command((IModelItemOwner)(object)this);
			}
			return _searchFocusHotkey;
		}
	}

	public string Keywords
	{
		get
		{
			return _keywords.Trim();
		}
		private set
		{
			if (_keywords != value && !string.IsNullOrEmpty(value))
			{
				_keywords = value;
				_processedKeywords = null;
				((ModelItem)this).FirePropertyChanged("Keywords");
			}
		}
	}

	public Command Executed => _executeCommand;

	public SearchKeywordLink KeywordLink => (SearchKeywordLink)KeywordLinkTable[Keywords.ToLower()];

	private Hashtable KeywordLinkTable
	{
		get
		{
			if (_keywordLinkTable == null)
			{
				_keywordLinkTable = new Hashtable();
				SearchKeywordLink[] array = new SearchKeywordLink[1]
				{
					new SearchKeywordLink(StringId.IDS_APPLICATIONS, StringId.IDS_SEARCH_MARKETPLACE_GAMES_LINK, StringId.IDS_SEARCH_MARKETPLACE_GAMES_KEYWORDS, "Marketplace\\Apps\\Home")
				};
				foreach (SearchKeywordLink searchKeywordLink in array)
				{
					for (int j = 0; j < searchKeywordLink.Keywords.Length; j++)
					{
						_keywordLinkTable[searchKeywordLink.Keywords[j].ToLower().Trim()] = searchKeywordLink;
					}
				}
			}
			return _keywordLinkTable;
		}
	}

	private Search()
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Expected O, but got Unknown
		_executeCommand = new Command();
		_ignoreAccented = string.Compare(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, "en", ignoreCase: true) == 0;
		_accentTable = new Dictionary<char, char>();
		_accentTable.Add('À', 'A');
		_accentTable.Add('Á', 'A');
		_accentTable.Add('Â', 'A');
		_accentTable.Add('Ã', 'A');
		_accentTable.Add('Ä', 'A');
		_accentTable.Add('Å', 'A');
		_accentTable.Add('à', 'a');
		_accentTable.Add('á', 'a');
		_accentTable.Add('â', 'a');
		_accentTable.Add('ã', 'a');
		_accentTable.Add('ä', 'a');
		_accentTable.Add('å', 'a');
		_accentTable.Add('È', 'E');
		_accentTable.Add('É', 'E');
		_accentTable.Add('Ê', 'E');
		_accentTable.Add('Ë', 'E');
		_accentTable.Add('è', 'e');
		_accentTable.Add('é', 'e');
		_accentTable.Add('ê', 'e');
		_accentTable.Add('ë', 'e');
		_accentTable.Add('Ì', 'I');
		_accentTable.Add('Í', 'I');
		_accentTable.Add('Î', 'I');
		_accentTable.Add('Ï', 'I');
		_accentTable.Add('ì', 'i');
		_accentTable.Add('í', 'i');
		_accentTable.Add('î', 'i');
		_accentTable.Add('ï', 'i');
		_accentTable.Add('Ò', 'O');
		_accentTable.Add('Ó', 'O');
		_accentTable.Add('Ô', 'O');
		_accentTable.Add('Õ', 'O');
		_accentTable.Add('Ö', 'O');
		_accentTable.Add('ò', 'o');
		_accentTable.Add('ó', 'o');
		_accentTable.Add('ô', 'o');
		_accentTable.Add('õ', 'o');
		_accentTable.Add('ö', 'o');
		_accentTable.Add('Ù', 'U');
		_accentTable.Add('Ú', 'U');
		_accentTable.Add('Û', 'U');
		_accentTable.Add('Ü', 'U');
		_accentTable.Add('ù', 'u');
		_accentTable.Add('ú', 'u');
		_accentTable.Add('û', 'u');
		_accentTable.Add('ü', 'u');
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing && _filterList != null)
		{
			_filterList.ChosenChanged -= FilterListChosenChanged;
		}
		((ModelItem)this).OnDispose(disposing);
	}

	private void FilterListChosenChanged(object sender, EventArgs args)
	{
		if (_filterList != null)
		{
			SearchResultFilterCommand searchResultFilterCommand = (SearchResultFilterCommand)_filterList.ChosenValue;
			if (searchResultFilterCommand != null)
			{
				SelectedFilterType = searchResultFilterCommand.Type;
			}
		}
	}

	public void UpdateFiltersList()
	{
		if (_filterList == null)
		{
			return;
		}
		if (_searchResultFilterCommandTypes == null)
		{
			_searchResultFilterCommandTypes = new List<SearchResultFilterCommand>();
			_searchResultFilterCommandTypes.Add(SearchResultFilter.All);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.Artists);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.Albums);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.Tracks);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.MusicVideos);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.TVShows);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.Movies);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.OtherVideo);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.Podcasts);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.Playlists);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.Channels);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.WindowsPhoneApps);
			_searchResultFilterCommandTypes.Add(SearchResultFilter.Profile);
		}
		_filterList.Options.Clear();
		for (int i = 0; i < _searchResultFilterCommandTypes.Count; i++)
		{
			SearchResultFilterCommand searchResultFilterCommand = _searchResultFilterCommandTypes[i];
			if (searchResultFilterCommand.HasResults)
			{
				_filterList.Options.Add(searchResultFilterCommand);
			}
		}
	}

	public void Navigate(string command, IDictionary commandArguments)
	{
		if (!string.IsNullOrEmpty(command))
		{
			if (command.Contains("Marketplace\\"))
			{
				SQMLog.Log((SQMDataId)82, 1);
			}
			else if (command.Contains("Collection\\"))
			{
				SQMLog.Log((SQMDataId)81, 1);
			}
			ZuneShell.DefaultInstance.Execute(command, commandArguments);
		}
	}

	public void RecordContextMenuType(SearchResultContextMenuType type)
	{
		SQMLog.LogToStream((SQMDataId)83, (uint)type);
	}

	public void Execute(string keywords)
	{
		Keywords = keywords;
		if (_executeCommand != null)
		{
			_executeCommand.Invoke();
		}
	}

	public bool IsValidKeyword(string keywords)
	{
		if (string.IsNullOrEmpty(keywords))
		{
			return false;
		}
		string text = keywords.Trim();
		if (string.IsNullOrEmpty(text) || (text.Length <= 1 && !IsValidCjkCharacter(text[0])))
		{
			return false;
		}
		return true;
	}

	private bool IsValidCjkCharacter(char c)
	{
		ushort num = c;
		if (num < 12352)
		{
			return false;
		}
		if ((num > 12351 && num < 12544) || (num > 13311 && num < 19904) || (num > 19967 && num < 40960) || (num > 44031 && num < 55216))
		{
			return true;
		}
		return false;
	}

	public string FormatResult(string keywordStyle, string item0, string item1)
	{
		List<string> list = new List<string>();
		AddResult(keywordStyle, list, item0);
		AddResult(keywordStyle, list, item1);
		return FormatResult(list);
	}

	public string FormatResult(string keywordStyle, string item0, string item1, string item2)
	{
		List<string> list = new List<string>();
		AddResult(keywordStyle, list, item0);
		AddResult(keywordStyle, list, item1);
		AddResult(keywordStyle, list, item2);
		return FormatResult(list);
	}

	private void AddResult(string keywordStyle, List<string> list, string item)
	{
		if (!string.IsNullOrEmpty(item))
		{
			if (keywordStyle != null)
			{
				list.Add(HighlightKeywords(item, keywordStyle));
			}
			else
			{
				list.Add(item);
			}
		}
	}

	private string FormatResult(List<string> list)
	{
		return list.Count switch
		{
			1 => list[0], 
			2 => string.Format(Shell.LoadString(StringId.IDS_FOUND_ITEM_BUTTON1), list[0], list[1]), 
			3 => string.Format(Shell.LoadString(StringId.IDS_FOUND_ITEM_BUTTON2), list[0], list[1], list[2]), 
			_ => "", 
		};
	}

	public string HighlightKeywords(string target, string styleName)
	{
		int num = 0;
		if (target == null)
		{
			return "";
		}
		StringBuilder stringBuilder = new StringBuilder(target.Length + styleName.Length * 4 + 10);
		List<IndexRange> list = FindKeywords(ProcessKeywords(), target);
		for (int i = 0; i < list.Count; i++)
		{
			IndexRange indexRange = list[i];
			string str;
			if (indexRange.indexStart > num)
			{
				str = target.Substring(num, indexRange.indexStart - num);
				stringBuilder.Append(SecurityElement.Escape(str));
			}
			str = target.Substring(indexRange.indexStart, indexRange.indexEnd - indexRange.indexStart);
			stringBuilder.Append(string.Format("<{0}>{1}</{0}>", styleName, SecurityElement.Escape(str)));
			num = indexRange.indexEnd;
		}
		if (num < target.Length)
		{
			string str = target.Substring(num);
			stringBuilder.Append(SecurityElement.Escape(str));
		}
		return stringBuilder.ToString();
	}

	private List<IndexRange> FindKeywords(string[] keywords, string text)
	{
		StringBuilder stringBuilder = new StringBuilder(text.Length);
		for (int i = 0; i < text.Length; i++)
		{
			stringBuilder.Append(MapAccentedChar(text[i]));
		}
		string text2 = stringBuilder.ToString();
		List<IndexRange> list = new List<IndexRange>();
		IndexRange keyRange = default(IndexRange);
		foreach (string text3 in keywords)
		{
			int startIndex = 0;
			while (true)
			{
				startIndex = text2.IndexOf(text3, startIndex, StringComparison.OrdinalIgnoreCase);
				if (startIndex < 0)
				{
					break;
				}
				char c = ((startIndex > 0) ? text2[startIndex - 1] : ' ');
				char c2 = ((startIndex + text3.Length < text2.Length) ? text2[startIndex + text3.Length] : ' ');
				if (!char.IsLetterOrDigit(c) && (text3.Length >= 2 || !char.IsLetterOrDigit(c2)))
				{
					keyRange.indexStart = startIndex;
					keyRange.indexEnd = startIndex + text3.Length;
					AddRange(keyRange, list);
				}
				startIndex += text3.Length;
			}
		}
		CollapseRanges(list);
		return list;
	}

	private void CollapseRanges(List<IndexRange> rangeList)
	{
		for (int i = 0; i < rangeList.Count; i++)
		{
			int num = i + 1;
			while (num < rangeList.Count)
			{
				IndexRange value = rangeList[i];
				IndexRange indexRange = rangeList[num];
				if (value.indexEnd <= indexRange.indexStart)
				{
					break;
				}
				if (value.indexEnd >= indexRange.indexEnd)
				{
					rangeList.RemoveAt(num);
					continue;
				}
				value.indexEnd = indexRange.indexEnd;
				rangeList[i] = value;
				rangeList.RemoveAt(num);
			}
		}
	}

	private void AddRange(IndexRange keyRange, List<IndexRange> rangeList)
	{
		int i;
		for (i = 0; i < rangeList.Count; i++)
		{
			IndexRange indexRange = rangeList[i];
			if (keyRange.indexStart <= indexRange.indexStart)
			{
				break;
			}
		}
		rangeList.Insert(i, keyRange);
	}

	private string[] ProcessKeywords()
	{
		if (_processedKeywords == null)
		{
			StringBuilder stringBuilder = new StringBuilder(_keywords.Length);
			bool flag = false;
			for (int i = 0; i < _keywords.Length; i++)
			{
				char c = _keywords[i];
				switch (c)
				{
				case '\'':
				{
					char c2 = ((i > 0) ? _keywords[i - 1] : ' ');
					char c3 = ((i + 1 < _keywords.Length) ? _keywords[i + 1] : ' ');
					if (char.IsLetterOrDigit(c2) && char.IsLetterOrDigit(c3))
					{
						stringBuilder.Append(c);
						break;
					}
					stringBuilder.Append(' ');
					flag = true;
					break;
				}
				case '\t':
				case '\n':
				case '\r':
				case '!':
				case '"':
				case '%':
				case '&':
				case '(':
				case ')':
				case '+':
				case '-':
				case ';':
				case '<':
				case '>':
				case '{':
				case '}':
					stringBuilder.Append(' ');
					flag = true;
					break;
				default:
					stringBuilder.Append(MapAccentedChar(c));
					break;
				}
			}
			_processedKeywords = stringBuilder.ToString().Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (flag)
			{
				Array.Resize(ref _processedKeywords, _processedKeywords.Length + 1);
				_processedKeywords[_processedKeywords.Length - 1] = _keywords;
			}
		}
		return _processedKeywords;
	}

	private char MapAccentedChar(char original)
	{
		char c = original;
		if (_ignoreAccented && _accentTable.TryGetValue(c, out var value))
		{
			c = value;
		}
		return c;
	}
}
