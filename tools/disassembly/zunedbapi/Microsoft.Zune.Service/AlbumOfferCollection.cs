using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Service;

public class AlbumOfferCollection : OfferCollection, IDisposable
{
	private IList m_items;

	private unsafe IMusicAlbumCollection* m_pCollection;

	public IList Items => m_items;

	internal unsafe AlbumOfferCollection()
	{
		m_items = null;
		m_pCollection = null;
	}

	private void _007EAlbumOfferCollection()
	{
		_0021AlbumOfferCollection();
	}

	private unsafe void _0021AlbumOfferCollection()
	{
		IMusicAlbumCollection* pCollection = m_pCollection;
		if (pCollection != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 8)))((nint)pCollection);
			m_pCollection = null;
		}
		m_items = null;
	}

	internal unsafe int Init(IMusicAlbumCollection* pCollection, IDictionary mapIdToContext)
	{
		int num = 0;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pCollection + 12)))((nint)pCollection);
		IList list = new ArrayList(num2);
		int num3 = 0;
		if (0 < num2)
		{
			Unsafe.SkipInit(out MusicAlbumMetadata musicAlbumMetadata);
			Unsafe.SkipInit(out CComPtrNtv_003CIContextData_003E cComPtrNtv_003CIContextData_003E);
			Unsafe.SkipInit(out CComPtrNtv_003CIPriceInfo_003E cComPtrNtv_003CIPriceInfo_003E);
			do
			{
				global::_003CModule_003E.MusicAlbumMetadata_002E_007Bctor_007D(&musicAlbumMetadata);
				try
				{
					*(int*)(&cComPtrNtv_003CIContextData_003E) = 0;
					try
					{
						if (num >= 0)
						{
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, MusicAlbumMetadata*, IContextData**, int>)(int)(*(uint*)(*(int*)pCollection + 16)))((nint)pCollection, num3, &musicAlbumMetadata, (IContextData**)(&cComPtrNtv_003CIContextData_003E));
						}
						global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bctor_007D(&cComPtrNtv_003CIPriceInfo_003E);
						try
						{
							int releaseYear = 0;
							if (num >= 0)
							{
								int num4 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)) + 72)))((IntPtr)Unsafe.As<MusicAlbumMetadata, int>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)));
								int num5 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)) + 88)))((IntPtr)Unsafe.As<MusicAlbumMetadata, int>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)), 1, 1);
								int num6 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)) + 36)))((IntPtr)Unsafe.As<MusicAlbumMetadata, int>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)), (EMediaRights)4, (EMediaFormat)0);
								if (Unsafe.As<MusicAlbumMetadata, int>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 48)) != 0)
								{
									releaseYear = global::_003CModule_003E._wtoi((ushort*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 48)));
								}
								EMediaFormat eMediaFormat = ((num6 == 0) ? ((EMediaFormat)1) : ((EMediaFormat)0));
								if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)) + 36)))((IntPtr)Unsafe.As<MusicAlbumMetadata, int>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)), (EMediaRights)4, eMediaFormat) != 0)
								{
									EMediaFormat eMediaFormat2 = ((num6 == 0) ? ((EMediaFormat)1) : ((EMediaFormat)0));
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, IPriceInfo**, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)) + 52)))((IntPtr)Unsafe.As<MusicAlbumMetadata, int>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 64)), (EMediaRights)4, eMediaFormat2, global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_0026(&cComPtrNtv_003CIPriceInfo_003E));
								}
								if (num >= 0)
								{
									Guid id = global::_003CModule_003E.GUIDToGuid(Unsafe.As<MusicAlbumMetadata, _GUID>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 4)));
									string recommendationContext = GetRecommendationContext(id, mapIdToContext, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
									bool inCollection = ((num4 != 0) ? true : false);
									bool previouslyPurchased = ((num5 != 0) ? true : false);
									bool premium = ((Unsafe.As<MusicAlbumMetadata, int>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 60)) != 0) ? true : false);
									bool isMP = ((num6 != 0) ? true : false);
									list.Add(new AlbumOffer(id, new string((char*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 36))), new string((char*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 40))), new string((char*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 44))), releaseYear, new string((char*)(int)Unsafe.As<MusicAlbumMetadata, uint>(ref Unsafe.AddByteOffset(ref musicAlbumMetadata, 52))), new PriceInfo((IPriceInfo*)(int)(*(uint*)(&cComPtrNtv_003CIPriceInfo_003E))), isMP, premium, previouslyPurchased, inCollection, recommendationContext));
								}
							}
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPriceInfo_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPriceInfo_003E);
							throw;
						}
						global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIPriceInfo_003E);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIContextData_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIContextData_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIContextData_003E);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<MusicAlbumMetadata*, void>)(&global::_003CModule_003E.MusicAlbumMetadata_002E_007Bdtor_007D), &musicAlbumMetadata);
					throw;
				}
				global::_003CModule_003E.MusicAlbumMetadata_002E_007Bdtor_007D(&musicAlbumMetadata);
				num3++;
			}
			while (num3 < num2);
			if (num < 0)
			{
				goto IL_0220;
			}
		}
		m_items = list;
		m_pCollection = pCollection;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 4)))((nint)pCollection);
		goto IL_0220;
		IL_0220:
		return num;
	}

	internal unsafe IMusicAlbumCollection* GetCollection()
	{
		IMusicAlbumCollection* pCollection = m_pCollection;
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
			_0021AlbumOfferCollection();
			return;
		}
		try
		{
			_0021AlbumOfferCollection();
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

	~AlbumOfferCollection()
	{
		Dispose(false);
	}
}
