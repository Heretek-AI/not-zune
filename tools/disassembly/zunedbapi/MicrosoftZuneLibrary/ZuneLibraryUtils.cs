using System.Runtime.CompilerServices;

namespace MicrosoftZuneLibrary;

public class ZuneLibraryUtils
{
	public unsafe static int ConvertPropVariantToString(tagPROPVARIANT* pPropVariant, string* pRetString)
	{
		Unsafe.Write(pRetString, null);
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		int num;
		try
		{
			num = 0;
			tagPROPVARIANT* ptr;
			if (*(ushort*)pPropVariant == 8)
			{
				ptr = pPropVariant;
				goto IL_0030;
			}
			// IL initblk instruction
			Unsafe.InitBlock(ref cComPropVariant, 0, 16);
			num = global::_003CModule_003E.ZuneLibraryExports_002EZunePropVariantChangeType((tagPROPVARIANT*)(&cComPropVariant), pPropVariant, 1, 8);
			if (num >= 0)
			{
				ptr = (tagPROPVARIANT*)(&cComPropVariant);
				goto IL_0030;
			}
			goto end_IL_000a;
			IL_0030:
			Unsafe.Write(pRetString, new string((char*)(int)((uint*)ptr)[2]));
			end_IL_000a:;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		return num;
	}

	public unsafe static int ConvertPropVariantToInt(tagPROPVARIANT* pPropVariant)
	{
		int result = 0;
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		// IL initblk instruction
		Unsafe.InitBlock(ref cComPropVariant, 0, 16);
		try
		{
			tagPROPVARIANT* ptr;
			if (*(ushort*)pPropVariant == 3)
			{
				ptr = pPropVariant;
				goto IL_002b;
			}
			// IL initblk instruction
			Unsafe.InitBlock(ref cComPropVariant, 0, 16);
			if (global::_003CModule_003E.ZuneLibraryExports_002EZunePropVariantChangeType((tagPROPVARIANT*)(&cComPropVariant), pPropVariant, 1, 3) >= 0)
			{
				ptr = (tagPROPVARIANT*)(&cComPropVariant);
				goto IL_002b;
			}
			goto end_IL_0009;
			IL_002b:
			result = ((int*)ptr)[2];
			end_IL_0009:;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
			throw;
		}
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		return result;
	}
}
