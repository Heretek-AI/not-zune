namespace Microsoft.Zune.ErrorMapperApi;

public class ErrorMapperApi
{
	public unsafe static ErrorMapperResult GetMappedErrorDescriptionAndUrl(int hrOrig, eErrorCondition eCondition)
	{
		ErrorMapperResult errorMapperResult = new ErrorMapperResult();
		ushort* ptr = null;
		ushort* ptr2 = null;
		int hr = 0;
		if (global::_003CModule_003E.ZuneLibraryExports_002EGetMappedErrorDescriptionAndUrl(hrOrig, eCondition, &hr, &ptr, &ptr2) < 0)
		{
			errorMapperResult.Hr = hrOrig;
			errorMapperResult.Description = "";
			errorMapperResult.Description = "";
		}
		else
		{
			errorMapperResult.Hr = hr;
			errorMapperResult.Description = new string((char*)ptr);
			errorMapperResult.WebHelpUrl = new string((char*)ptr2);
		}
		global::_003CModule_003E.SysFreeString(ptr);
		global::_003CModule_003E.SysFreeString(ptr2);
		return errorMapperResult;
	}

	public static ErrorMapperResult GetMappedErrorDescriptionAndUrl(int hrOrig)
	{
		return GetMappedErrorDescriptionAndUrl(hrOrig, eErrorCondition.eEC_None);
	}
}
