using Microsoft.Zune.ErrorMapperApi;

namespace ZuneUI;

public struct MappedError
{
	private string _text;

	private string _url;

	public string Text => _text;

	public string URL => _url;

	public MappedError(int hr)
	{
		ErrorMapperResult mappedErrorDescriptionAndUrl = ErrorMapperApi.GetMappedErrorDescriptionAndUrl(hr);
		_text = mappedErrorDescriptionAndUrl.Description;
		_url = mappedErrorDescriptionAndUrl.WebHelpUrl;
	}
}
