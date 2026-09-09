using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;

namespace Microsoft.Zune.Service;

public class AppOfferCollection : OfferCollection, IDisposable
{
	private IList m_items;

	private unsafe IAppCollection* m_pCollection;

	public IList Items => m_items;

	internal unsafe AppOfferCollection()
	{
		m_items = null;
		m_pCollection = null;
	}

	private void _007EAppOfferCollection()
	{
		_0021AppOfferCollection();
	}

	private unsafe void _0021AppOfferCollection()
	{
		IAppCollection* pCollection = m_pCollection;
		if (pCollection != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 8)))((nint)pCollection);
			m_pCollection = null;
		}
		m_items = null;
	}

	internal unsafe int Init(IAppCollection* pCollection)
	{
		int num = 0;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pCollection + 12)))((nint)pCollection);
		IList list = new ArrayList(num2);
		int num3 = 0;
		if (0 < num2)
		{
			Unsafe.SkipInit(out AppMetadata appMetadata);
			Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY01W4EMediaRights_0040_0040 _0024ArrayType_0024_0024_0024BY01W4EMediaRights_0040_00402);
			Unsafe.SkipInit(out CComPtrNtv_003CIPriceInfo_003E cComPtrNtv_003CIPriceInfo_003E);
			do
			{
				global::_003CModule_003E.AppMetadata_002E_007Bctor_007D(&appMetadata);
				try
				{
					if (num >= 0)
					{
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, AppMetadata*, IContextData**, int>)(int)(*(uint*)(*(int*)pCollection + 16)))((nint)pCollection, num3, &appMetadata, null);
					}
					DateTime result = default(DateTime);
					if (num >= 0)
					{
						if (Unsafe.As<AppMetadata, int>(ref Unsafe.AddByteOffset(ref appMetadata, 44)) == 0 || !DateTime.TryParse(new string((char*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 44))), out result))
						{
							result = DateTime.MinValue;
						}
						Guid id = global::_003CModule_003E.GUIDToGuid(Unsafe.As<AppMetadata, _GUID>(ref Unsafe.AddByteOffset(ref appMetadata, 4)));
						bool inCollection = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 76)) + 72)))((IntPtr)Unsafe.As<AppMetadata, int>(ref Unsafe.AddByteOffset(ref appMetadata, 76))) != 0) ? true : false);
						*(int*)(&_0024ArrayType_0024_0024_0024BY01W4EMediaRights_0040_00402) = 3;
						Unsafe.As<_0024ArrayType_0024_0024_0024BY01W4EMediaRights_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY01W4EMediaRights_0040_00402, 4)) = 11;
						int num4 = 0;
						do
						{
							*(int*)(&cComPtrNtv_003CIPriceInfo_003E) = 0;
							try
							{
								_GUID gUID_NULL = global::_003CModule_003E.GUID_NULL;
								_GUID gUID_NULL2 = global::_003CModule_003E.GUID_NULL;
								int num5 = *(int*)((ref *(_003F*)(num4 * 4)) + (ref *(_003F*)(&_0024ArrayType_0024_0024_0024BY01W4EMediaRights_0040_00402)));
								if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, _GUID*, _GUID*, IPriceInfo**, ushort**, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 76)) + 68)))((IntPtr)Unsafe.As<AppMetadata, int>(ref Unsafe.AddByteOffset(ref appMetadata, 76)), (EMediaRights)num5, (EMediaFormat)5, &gUID_NULL2, &gUID_NULL, (IPriceInfo**)(&cComPtrNtv_003CIPriceInfo_003E), null) >= 0)
								{
									bool isTrialPurchase = num5 == 11;
									bool previouslyPurchased = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, int, int, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 76)) + 84)))((IntPtr)Unsafe.As<AppMetadata, int>(ref Unsafe.AddByteOffset(ref appMetadata, 76)), (EMediaRights)num5, (EMediaFormat)5, 1, 1) != 0) ? true : false);
									list.Add(new AppOffer(id, new string((char*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 20))), new string((char*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 32))), new string((char*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 36))), new string((char*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 40))), new string((char*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 52))), new string((char*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 56))), new string((char*)(int)Unsafe.As<AppMetadata, uint>(ref Unsafe.AddByteOffset(ref appMetadata, 64))), new PriceInfo((IPriceInfo*)(int)(*(uint*)(&cComPtrNtv_003CIPriceInfo_003E))), result, previouslyPurchased, inCollection, isTrialPurchase));
								}
							}
							catch
							{
								//try-fault
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPriceInfo_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPriceInfo_003E);
								throw;
							}
							global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIPriceInfo_003E);
							num4++;
						}
						while ((uint)num4 < 2u);
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<AppMetadata*, void>)(&global::_003CModule_003E.AppMetadata_002E_007Bdtor_007D), &appMetadata);
					throw;
				}
				global::_003CModule_003E.AppMetadata_002E_007Bdtor_007D(&appMetadata);
				num3++;
			}
			while (num3 < num2);
			if (num < 0)
			{
				goto IL_01ec;
			}
		}
		m_items = list;
		m_pCollection = pCollection;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 4)))((nint)pCollection);
		goto IL_01ec;
		IL_01ec:
		return num;
	}

	internal unsafe IAppCollection* GetCollection()
	{
		IAppCollection* pCollection = m_pCollection;
		if (pCollection != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 4)))((nint)pCollection);
		}
		return m_pCollection;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021AppOfferCollection();
			return;
		}
		try
		{
			_0021AppOfferCollection();
		}
		finally
		{
			base.Finalize();
		}
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	~AppOfferCollection()
	{
		Dispose(false);
	}
}
