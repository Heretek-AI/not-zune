using System;
using Microsoft.Zune.Service;
using Microsoft.Zune.Shell;

namespace ZuneUI;

public static class ArtistHelper
{
	public static int InCollection(Guid serviceMediaId)
	{
		int result = -1;
		bool flag = false;
		if (!ZuneApplication.Service.InCompleteCollection(serviceMediaId, (EContentType)4, ref result, ref flag))
		{
			return -1;
		}
		return result;
	}
}
