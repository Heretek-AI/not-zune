using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;

namespace Microsoft.Zune.Service;

public class VideoOfferCollection : OfferCollection, IDisposable
{
	private IList m_items;

	private unsafe IVideoCollection* m_pCollection;

	public IList Items => m_items;

	internal unsafe VideoOfferCollection()
	{
		m_items = null;
		m_pCollection = null;
	}

	private void _007EVideoOfferCollection()
	{
		_0021VideoOfferCollection();
	}

	private unsafe void _0021VideoOfferCollection()
	{
		IVideoCollection* pCollection = m_pCollection;
		if (pCollection != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 8)))((nint)pCollection);
			m_pCollection = null;
		}
		m_items = null;
	}

	internal unsafe int Init(IVideoCollection* pCollection)
	{
		//IL_0437->IL043c: Incompatible stack types: I vs Ref
		int num = 0;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pCollection + 12)))((nint)pCollection);
		IList list = new ArrayList(num2);
		int num3 = 0;
		if (0 < num2)
		{
			Unsafe.SkipInit(out VideoMetadata videoMetadata);
			Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040 _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402);
			Unsafe.SkipInit(out CComPtrNtv_003CIPriceInfo_003E cComPtrNtv_003CIPriceInfo_003E);
			do
			{
				int num4 = 0;
				int num5 = 0;
				int num6 = 0;
				int num7 = 0;
				global::_003CModule_003E.VideoMetadata_002E_007Bctor_007D(&videoMetadata);
				try
				{
					if (num >= 0)
					{
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, VideoMetadata*, IContextData**, int>)(int)(*(uint*)(*(int*)pCollection + 16)))((nint)pCollection, num3, &videoMetadata, null);
						if (num >= 0)
						{
							Guid id = global::_003CModule_003E.GUIDToGuid(Unsafe.As<VideoMetadata, _GUID>(ref Unsafe.AddByteOffset(ref videoMetadata, 4)));
							Guid albumId = global::_003CModule_003E.GUIDToGuid(Unsafe.As<VideoMetadata, _GUID>(ref Unsafe.AddByteOffset(ref videoMetadata, 56)));
							string title = new string((char*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 24)));
							string seriesTitle = new string((char*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 84)));
							string artist = new string((char*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 32)));
							string genre = new string((char*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 72)));
							string previewImageUrl = new string((char*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 108)));
							string productionCompany = new string((char*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 80)));
							bool flag = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)) + 72)))((IntPtr)Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 124))) != 0) ? true : false);
							int releaseYear = 0;
							if (Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 100)) != 0)
							{
								releaseYear = global::_003CModule_003E._wtoi((ushort*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 100)));
							}
							*(int*)(&_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402) = 3;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 4)) = 3;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 8)) = 4;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 12)) = 3;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 16)) = 2;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 20)) = 4;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 24)) = 3;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 28)) = 4;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 32)) = -1;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 36)) = 7;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 40)) = 2;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 44)) = -1;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 48)) = 7;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 52)) = 3;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 56)) = -1;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 60)) = 8;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 64)) = 2;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 68)) = -1;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 72)) = 8;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 76)) = 3;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 80)) = -1;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 84)) = 9;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 88)) = 3;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 92)) = -1;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 96)) = 9;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 100)) = 2;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 104)) = -1;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 108)) = 12;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 112)) = 2;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 116)) = 4;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 120)) = 12;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 124)) = 3;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 128)) = 4;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 132)) = 13;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 136)) = 2;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 140)) = -1;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 144)) = 13;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 148)) = 3;
							Unsafe.As<_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 152)) = -1;
							int num8 = 0;
							VideoOfferParams* ptr = (VideoOfferParams*)(&_0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402);
							int num9 = (int)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 4));
							int num10 = (int)Unsafe.AsPointer(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY0N_0040UVideoOfferParams_0040Service_0040Zune_0040Microsoft_0040_00402, 8));
							while (num6 == 0)
							{
								int num11 = *(int*)ptr;
								if ((num11 != 8 && num11 != 13) || *(int*)num9 != 3 || *(int*)num10 != -1 || num7 == 0)
								{
									if (num11 == 3)
									{
										if (*(int*)num9 == 4 && *(int*)num10 == -1 && num4 != 0)
										{
											goto IL_059c;
										}
									}
									else if (num11 != 12)
									{
										goto IL_0292;
									}
									if (*(int*)num9 != 3 || *(int*)num10 != 4 || num5 == 0)
									{
										goto IL_0292;
									}
								}
								goto IL_059c;
								IL_03f1:
								bool flag2;
								bool flag3;
								bool flag4;
								int num12;
								bool flag6;
								ushort* ptr2;
								bool flag7;
								bool isRental;
								bool isMusicVideo;
								bool flag8;
								bool flag9;
								try
								{
									if (!flag2 && !flag3)
									{
										EMediaRights eMediaRights = (flag4 ? ((EMediaRights)13) : ((EMediaRights)12));
										bool flag5 = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, int, int, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)) + 84)))((IntPtr)Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)), eMediaRights, (EMediaFormat)num12, 1, 1) != 0) ? true : false);
										flag6 = flag5;
									}
									string expirationDate = new string((char*)Unsafe.AsPointer(ref ptr2 == null ? ref Unsafe.As<_0024ArrayType_0024_0024_0024BY00_0024_0024CBG, _003F>(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040) : ref *(_003F*)ptr2));
									VideoOffer value = new VideoOffer(id, title, seriesTitle, Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 88)), Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 92)), artist, albumId, genre, releaseYear, previewImageUrl, productionCompany, new PriceInfo(global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_002EPAUIPriceInfo_0040_0040(&cComPtrNtv_003CIPriceInfo_003E)), flag7, isRental, flag4, isMusicVideo, flag3, flag6, flag, expirationDate);
									list.Add(value);
									num4 = 1;
									if (flag8 && flag9 && flag6 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, EMediaFormat, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)) + 32)))((IntPtr)Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)), (EMediaRights)num11, (EMediaFormat)3, (EMediaFormat)4) != 0)
									{
										num6 = (flag ? 1 : 0);
										num5 = 1;
									}
									else
									{
										if (flag7 && flag4 && flag6 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)) + 36)))((IntPtr)Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)), (EMediaRights)num11, (EMediaFormat)2) != 0)
										{
											num7 = 1;
										}
										if (num5 == 0 && flag7 && flag9 && flag6 && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, EMediaFormat, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)) + 32)))((IntPtr)Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)), (EMediaRights)num11, (EMediaFormat)2, (EMediaFormat)3) != 0)
										{
											VideoOffer value2 = new VideoOffer(id, title, seriesTitle, Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 88)), Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 92)), artist, albumId, genre, releaseYear, previewImageUrl, productionCompany, PriceInfo.FreeWithPoints(), isHD: false, isRental: false, isStream: false, isMusicVideo: false, flag3, previouslyPurchased: true, flag, expirationDate);
											list.Add(value2);
											num5 = 1;
										}
									}
								}
								catch
								{
									//try-fault
									global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPriceInfo_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPriceInfo_003E);
									throw;
								}
								goto IL_057e;
								IL_03e4:
								global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIPriceInfo_003E);
								goto IL_059c;
								IL_059c:
								num8++;
								num10 += 12;
								num9 += 12;
								ptr = (VideoOfferParams*)((byte*)ptr + 12);
								if ((uint)num8 >= 13u)
								{
									break;
								}
								continue;
								IL_0292:
								num12 = *(int*)num9;
								flag7 = num12 == 2;
								flag8 = num12 == 3;
								ESeasonPurchaseFlags eSeasonPurchaseFlags = ((!flag7) ? ((ESeasonPurchaseFlags)1) : ((ESeasonPurchaseFlags)2));
								flag3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ESeasonPurchaseFlags, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)) + 112)))((IntPtr)Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)), eSeasonPurchaseFlags) == 1;
								global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bctor_007D(&cComPtrNtv_003CIPriceInfo_003E);
								try
								{
									ptr2 = null;
									_GUID gUID_NULL = global::_003CModule_003E.GUID_NULL;
									_GUID gUID_NULL2 = global::_003CModule_003E.GUID_NULL;
									_GUID gUID_NULL3 = global::_003CModule_003E.GUID_NULL;
									int num13 = *(int*)num10;
									if (((num13 == -1) ? ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, _GUID*, _GUID*, IPriceInfo**, ushort**, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)) + 68)))((IntPtr)Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)), (EMediaRights)num11, (EMediaFormat)num12, &gUID_NULL2, &gUID_NULL, global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_0026(&cComPtrNtv_003CIPriceInfo_003E), &ptr2) : ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, EMediaFormat, _GUID*, _GUID*, _GUID*, IPriceInfo**, ushort**, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)) + 64)))((IntPtr)Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)), (EMediaRights)num11, (EMediaFormat)num12, (EMediaFormat)num13, &gUID_NULL2, &gUID_NULL3, &gUID_NULL, global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_0026(&cComPtrNtv_003CIPriceInfo_003E), &ptr2)) >= 0)
									{
										flag9 = ((num11 == 3 || num11 == 12) ? true : false);
										isRental = ((num11 == 7 || num11 == 9) ? true : false);
										flag4 = ((num11 == 8 || num11 == 13 || num11 == 9) ? true : false);
										int num14 = ((num11 == 12 || num11 == 13) ? 1 : 0);
										bool flag10 = (byte)num14 != 0;
										flag2 = ((((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EMediaRights, EMediaFormat, int, int, int>)(int)(*(uint*)(*(int*)(int)Unsafe.As<VideoMetadata, uint>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)) + 84)))((IntPtr)Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 124)), (EMediaRights)num11, (EMediaFormat)num12, 1, 1) != 0) ? true : false);
										flag6 = flag2;
										isMusicVideo = Unsafe.As<VideoMetadata, int>(ref Unsafe.AddByteOffset(ref videoMetadata, 20)) == 0;
										if (flag3 == flag10)
										{
											goto IL_03f1;
										}
										goto IL_03e4;
									}
								}
								catch
								{
									//try-fault
									global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPriceInfo_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPriceInfo_003E);
									throw;
								}
								goto IL_057e;
								IL_057e:
								try
								{
									global::_003CModule_003E.SysFreeString(ptr2);
								}
								catch
								{
									//try-fault
									global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPriceInfo_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPriceInfo_003E);
									throw;
								}
								global::_003CModule_003E.CComPtrNtv_003CIPriceInfo_003E_002E_007Bdtor_007D(&cComPtrNtv_003CIPriceInfo_003E);
								goto IL_059c;
							}
						}
					}
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<VideoMetadata*, void>)(&global::_003CModule_003E.VideoMetadata_002E_007Bdtor_007D), &videoMetadata);
					throw;
				}
				global::_003CModule_003E.VideoMetadata_002E_007Bdtor_007D(&videoMetadata);
				num3++;
			}
			while (num3 < num2);
			if (num < 0)
			{
				goto IL_0602;
			}
		}
		m_items = list;
		m_pCollection = pCollection;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 4)))((nint)pCollection);
		goto IL_0602;
		IL_0602:
		return num;
	}

	internal unsafe IVideoCollection* GetCollection()
	{
		IVideoCollection* pCollection = m_pCollection;
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
			_0021VideoOfferCollection();
			return;
		}
		try
		{
			_0021VideoOfferCollection();
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

	~VideoOfferCollection()
	{
		Dispose(false);
	}
}
