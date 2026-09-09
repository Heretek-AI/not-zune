using System;

namespace MicrosoftZuneLibrary;

public class ZuneLibraryGenreList : ZuneQueryList
{
	public unsafe ZuneLibraryGenreList(IDatabaseQueryResults* pResults)
		: base(pResults, "ZuneLibraryGenreList")
	{
	}

	public string GetGenre(int index)
	{
		Type typeFromHandle = typeof(string);
		return (string)GetFieldValue((uint)index, typeFromHandle, 399u, null);
	}
}
