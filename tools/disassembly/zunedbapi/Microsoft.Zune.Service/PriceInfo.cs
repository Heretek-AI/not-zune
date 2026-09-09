using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Service;

public class PriceInfo
{
	private int m_pointsPrice;

	private double m_currencyPrice;

	private bool m_hasPoints;

	private bool m_hasCurrency;

	private string m_displayPrice;

	private string m_currencyCode;

	public string CurrencyCode => m_currencyCode;

	public string DisplayPrice => m_displayPrice;

	public bool HasCurrency
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_hasCurrency;
		}
	}

	public bool HasPoints
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_hasPoints;
		}
	}

	public double CurrencyPrice => m_currencyPrice;

	public int PointsPrice => m_pointsPrice;

	internal unsafe PriceInfo(IPriceInfo* pPriceInfo)
	{
		Init(pPriceInfo);
	}

	internal unsafe PriceInfo()
	{
		Init(null);
	}

	public unsafe PriceInfo(int pointsPrice)
	{
		Init(null);
		m_pointsPrice = pointsPrice;
	}

	public void MakeFree()
	{
		m_pointsPrice = 0;
		m_currencyPrice = 0.0;
		m_displayPrice = null;
	}

	public static PriceInfo FreeWithPoints()
	{
		return new PriceInfo();
	}

	internal unsafe void Init(IPriceInfo* pPriceInfo)
	{
		m_pointsPrice = 0;
		m_currencyPrice = 0.0;
		m_hasPoints = true;
		m_hasCurrency = false;
		if (pPriceInfo == null)
		{
			return;
		}
		m_pointsPrice = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pPriceInfo + 12)))((nint)pPriceInfo);
		m_currencyPrice = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, double>)(int)(*(uint*)(*(int*)pPriceInfo + 16)))((nint)pPriceInfo);
		bool hasPoints = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pPriceInfo + 20)))((nint)pPriceInfo) != 0) ? true : false);
		m_hasPoints = hasPoints;
		bool hasCurrency = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pPriceInfo + 24)))((nint)pPriceInfo) != 0) ? true : false);
		m_hasCurrency = hasCurrency;
		Unsafe.SkipInit(out WBSTRString wBSTRString);
		global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
		try
		{
			Unsafe.SkipInit(out WBSTRString wBSTRString2);
			global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString2);
			try
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pPriceInfo + 32)))((nint)pPriceInfo, (ushort**)(&wBSTRString));
				if (num >= 0)
				{
					m_displayPrice = new string((char*)(int)(*(uint*)(&wBSTRString)));
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pPriceInfo + 36)))((nint)pPriceInfo, (ushort**)(&wBSTRString2));
					if (num >= 0)
					{
						m_currencyCode = new string((char*)(int)(*(uint*)(&wBSTRString2)));
					}
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString2);
				throw;
			}
			global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString2));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
			throw;
		}
		global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
	}
}
