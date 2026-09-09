namespace ZuneXml;

internal interface IPageInfo
{
	string GetPageUrl(int startIndex);

	string GetPagePostBody(int startIndex);
}
