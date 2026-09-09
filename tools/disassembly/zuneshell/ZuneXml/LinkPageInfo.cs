namespace ZuneXml;

internal class LinkPageInfo : IPageInfo
{
	private string _nextPageUrl;

	private string _requestBody;

	public LinkPageInfo(string nextPageUrl, string requestBody)
	{
		_nextPageUrl = nextPageUrl;
		_requestBody = requestBody;
	}

	public string GetPageUrl(int startIndex)
	{
		return _nextPageUrl;
	}

	public string GetPagePostBody(int startIndex)
	{
		return _requestBody;
	}
}
