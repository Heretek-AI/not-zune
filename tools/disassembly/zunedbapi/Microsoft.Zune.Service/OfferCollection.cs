using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace Microsoft.Zune.Service;

public abstract class OfferCollection
{
	protected unsafe string GetRecommendationContext(Guid id, IDictionary mapIdToContext, IContextData* pContextData)
	{
		string result = null;
		if (mapIdToContext != null && mapIdToContext.Contains(id))
		{
			result = (string)mapIdToContext[id];
		}
		else if (pContextData != null)
		{
			Unsafe.SkipInit(out WBSTRString wBSTRString);
			global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
			try
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pContextData + 12)))((nint)pContextData, (ushort**)(&wBSTRString));
				bool flag = *(int*)(&wBSTRString) != 0 && ((*(ushort*)(int)(*(uint*)(&wBSTRString)) != 0) ? true : false);
				if (flag)
				{
					result = new string((char*)(int)(*(uint*)(&wBSTRString)));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
				throw;
			}
			global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
		}
		return result;
	}

	public OfferCollection()
	{
	}
}
