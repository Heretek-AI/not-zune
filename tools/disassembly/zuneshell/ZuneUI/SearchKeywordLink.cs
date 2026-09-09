using Microsoft.Iris;

namespace ZuneUI;

public class SearchKeywordLink : ShellCommand
{
	private string _header;

	private string[] _keywords;

	private StringId _idsKeywords;

	public string Header => _header;

	public string[] Keywords
	{
		get
		{
			if (_keywords == null)
			{
				_keywords = Shell.LoadString(_idsKeywords).Split(new char[1] { ',' });
			}
			return _keywords;
		}
	}

	public SearchKeywordLink(StringId idsHeader, StringId idsDescription, StringId idsKeywords, string command)
	{
		_header = Shell.LoadString(idsHeader);
		((ModelItem)this).Description = Shell.LoadString(idsDescription);
		base.Command = command;
		_idsKeywords = idsKeywords;
	}
}
