using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Service;

public class TrackOfferCollection : OfferCollection, IDisposable
{
	private IList m_items;

	private unsafe IMusicTrackCollection* m_pCollection;

	public IList Items => m_items;

	internal unsafe TrackOfferCollection()
	{
		m_items = null;
		m_pCollection = null;
	}

	private void _007ETrackOfferCollection()
	{
		_0021TrackOfferCollection();
	}

	private unsafe void _0021TrackOfferCollection()
	{
		IMusicTrackCollection* pCollection = m_pCollection;
		if (pCollection != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 8)))((nint)pCollection);
			m_pCollection = null;
		}
		m_items = null;
	}

	internal unsafe int Init(IMusicTrackCollection* pCollection, IDictionary mapIdToContext)
	{
		int num = 0;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pCollection + 12)))((nint)pCollection);
		IList list = new ArrayList(num2);
		int num3 = 0;
		if (0 < num2)
		{
			Unsafe.SkipInit(out MusicTrackMetadata musicTrackMetadata);
			Unsafe.SkipInit(out CComPtrNtv_003CIContextData_003E cComPtrNtv_003CIContextData_003E);
			Unsafe.SkipInit(out CComPtrNtv_003CIPriceInfo_003E cComPtrNtv_003CIPriceInfo_003E);
			do
			{
				global::_003CModule_003E.MusicTrackMetadata_002E_007Bctor_007D(&musicTrackMetadata);
				try
				{
					*(int*)(&cComPtrNtv_003CIContextData_003E) = 0;
					try
					{
						if (num >= 0)
						{
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, MusicTrackMetadata*, IContextData**, int>)(int)(*(uint*)(*(int*)pCollection + 16)))((nint)pCollection, num3, &musicTrackMetadata, (IContextData**)(&cComPtrNtv_003CIContextData_003E));
						}
						global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bctor_007D(&cComPtrNtv_003CIPriceInfo_003E);
						try
						{
							if (num >= 0)
							{
								EMediaRights eMediaRights = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)) + 104)))((IntPtr)Unsafe.As<MusicTrackMetadata, int>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112))) != 0) ? ((EMediaRights)5) : ((EMediaRights)3));
								int num4 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)) + 72)))((IntPtr)Unsafe.As<MusicTrackMetadata, int>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)));
								int num5 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)) + 88)))((IntPtr)Unsafe.As<MusicTrackMetadata, int>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)), 1, 1);
								int num6 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)) + 36)))((IntPtr)Unsafe.As<MusicTrackMetadata, int>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)), eMediaRights, (EMediaFormat)0);
								if (eMediaRights != (EMediaRights)5)
								{
									EMediaFormat eMediaFormat = ((num6 == 0) ? ((EMediaFormat)1) : ((EMediaFormat)0));
									if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)) + 36)))((IntPtr)Unsafe.As<MusicTrackMetadata, int>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)), eMediaRights, eMediaFormat) != 0)
									{
										EMediaFormat eMediaFormat2 = ((num6 == 0) ? ((EMediaFormat)1) : ((EMediaFormat)0));
										num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, IPriceInfo**, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)) + 52)))((IntPtr)Unsafe.As<MusicTrackMetadata, int>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)), eMediaRights, eMediaFormat2, global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_0026(&cComPtrNtv_003CIPriceInfo_003E));
									}
								}
								if (num >= 0)
								{
									Guid id = global::_003CModule_003E.GUIDToGuid(Unsafe.As<MusicTrackMetadata, _GUID>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 4)));
									string recommendationContext = GetRecommendationContext(id, mapIdToContext, global::_003CModule_003E.CComPtrNtv_003CIContextData_003E_002E_002EPAUIContextData_0040_0040(&cComPtrNtv_003CIContextData_003E));
									bool subscriptionFree = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112)) + 104)))((IntPtr)Unsafe.As<MusicTrackMetadata, int>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 112))) != 0) ? true : false);
									bool inCollection = ((num4 != 0) ? true : false);
									bool previouslyPurchased = ((num5 != 0) ? true : false);
									bool isMP = ((num6 != 0) ? true : false);
									list.Add(new TrackOffer(id, Unsafe.As<MusicTrackMetadata, int>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 68)), new string((char*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 76))), new string((char*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 80))), new string((char*)(int)Unsafe.As<MusicTrackMetadata, uint>(ref Unsafe.AddByteOffset(ref musicTrackMetadata, 84))), recommendationContext, new PriceInfo((IPriceInfo*)(int)(*(uint*)(&cComPtrNtv_003CIPriceInfo_003E))), isMP, previouslyPurchased, inCollection, subscriptionFree));
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
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<MusicTrackMetadata*, void>)(&global::_003CModule_003E.MusicTrackMetadata_002E_007Bdtor_007D), &musicTrackMetadata);
					throw;
				}
				global::_003CModule_003E.MusicTrackMetadata_002E_007Bdtor_007D(&musicTrackMetadata);
				num3++;
			}
			while (num3 < num2);
			if (num < 0)
			{
				goto IL_023c;
			}
		}
		m_items = list;
		m_pCollection = pCollection;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 4)))((nint)pCollection);
		goto IL_023c;
		IL_023c:
		return num;
	}

	internal unsafe IMusicTrackCollection* GetCollection()
	{
		IMusicTrackCollection* pCollection = m_pCollection;
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
			_0021TrackOfferCollection();
			return;
		}
		try
		{
			_0021TrackOfferCollection();
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

	~TrackOfferCollection()
	{
		Dispose(false);
	}
}
