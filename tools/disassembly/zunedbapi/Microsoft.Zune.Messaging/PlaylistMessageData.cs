using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Iris;

namespace Microsoft.Zune.Messaging;

public class PlaylistMessageData : IPropertySetMessageData
{
	private string m_title;

	private string m_author;

	private IList m_tracks;

	private unsafe IMSMediaSchemaPropertySet* m_pPlaylistPropSet;

	public unsafe PlaylistMessageData(string title, string author, IList tracks)
	{
		m_title = title;
		m_author = author;
		m_tracks = tracks;
		m_pPlaylistPropSet = null;
	}

	private void _007EPlaylistMessageData()
	{
		_0021PlaylistMessageData();
	}

	private unsafe void _0021PlaylistMessageData()
	{
		IMSMediaSchemaPropertySet* pPlaylistPropSet = m_pPlaylistPropSet;
		if (null != pPlaylistPropSet)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pPlaylistPropSet + 8)))((nint)pPlaylistPropSet);
			m_pPlaylistPropSet = null;
		}
	}

	public unsafe virtual int GetPropertySet(IMSMediaSchemaPropertySet** ppPropSet)
	{
		if (ppPropSet == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 141u);
			return -2147467261;
		}
		IMSMediaSchemaPropertyList* ptr = null;
		int num = global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertySetList((_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.ID_MS_MEDIA_SCHEMA_PLAYLIST), 3229617665u, &ptr);
		if (num >= 0)
		{
			num = AddTracksToPropList(ptr);
		}
		IMSMediaSchemaPropertySet* ptr2 = null;
		if (num >= 0)
		{
			num = global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertySet((_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E.ID_MS_MEDIA_SCHEMA_PLAYLIST), 3229617665u, &ptr2);
		}
		fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_title)))
		{
			if (num >= 0)
			{
				Unsafe.SkipInit(out CComPropVariant cComPropVariant);
				// IL initblk instruction
				Unsafe.InitBlock(ref cComPropVariant, 0, 16);
				try
				{
					*(short*)(&cComPropVariant) = 8;
					Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)) = (int)global::_003CModule_003E.SysAllocString(ptr3);
					tagPROPVARIANT tagPROPVARIANT2 = (tagPROPVARIANT)cComPropVariant;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, 16777217u, tagPROPVARIANT2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
					throw;
				}
				global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
			}
			fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_author)))
			{
				if (num >= 0)
				{
					Unsafe.SkipInit(out CComPropVariant cComPropVariant2);
					// IL initblk instruction
					Unsafe.InitBlock(ref cComPropVariant2, 0, 16);
					try
					{
						*(short*)(&cComPropVariant2) = 8;
						Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant2, 8)) = (int)global::_003CModule_003E.SysAllocString(ptr4);
						tagPROPVARIANT tagPROPVARIANT3 = (tagPROPVARIANT)cComPropVariant2;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, 16777219u, tagPROPVARIANT3);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant2);
						throw;
					}
					global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant2);
				}
				Unsafe.SkipInit(out CComPropVariant cComPropVariant3);
				// IL initblk instruction
				Unsafe.InitBlock(ref cComPropVariant3, 0, 16);
				try
				{
					if (num < 0)
					{
						goto IL_013c;
					}
					*(short*)(&cComPropVariant3) = 13;
					Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant3, 8)) = (int)ptr;
					tagPROPVARIANT tagPROPVARIANT4 = (tagPROPVARIANT)cComPropVariant3;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)ptr2 + 28)))((nint)ptr2, 3229617665u, tagPROPVARIANT4);
					if (num < 0)
					{
						goto IL_013c;
					}
					m_pPlaylistPropSet = ptr2;
					*(int*)ppPropSet = (int)ptr2;
					goto end_IL_0107;
					IL_013c:
					if (null != ptr)
					{
						IMSMediaSchemaPropertyList* intPtr = ptr;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
						ptr = null;
					}
					if (null != ptr2)
					{
						IMSMediaSchemaPropertySet* intPtr2 = ptr2;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
						ptr2 = null;
					}
					end_IL_0107:;
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant3);
					throw;
				}
				global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant3);
				return num;
			}
		}
	}

	private unsafe int AddTracksToPropList(IMSMediaSchemaPropertyList* pPropList)
	{
		//The blocks IL_0024, IL_0035, IL_004d, IL_0063, IL_0075, IL_0091, IL_0098, IL_0186, IL_0191 are reachable both inside and outside the pinned region starting at IL_009f. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		if (pPropList == null)
		{
			global::_003CModule_003E._ZuneShipAssert(1001u, 45u);
			return -2147467261;
		}
		int num = 0;
		if (m_tracks != null)
		{
			int num2 = 0;
			Unsafe.SkipInit(out CComPropVariant cComPropVariant);
			while (num2 < m_tracks.Count)
			{
				object? obj = m_tracks[num2];
				DataProviderObject val = (DataProviderObject)((obj is DataProviderObject) ? obj : null);
				if (val != null)
				{
					_GUID gUID = global::_003CModule_003E.GUID_NULL;
					object property = val.GetProperty("ZuneMediaId");
					if (property != null)
					{
						gUID = global::_003CModule_003E.GuidToGUID((Guid)property);
					}
					string text = val.GetProperty("Title") as string;
					object s = ((!(text == null)) ? text : "");
					while (true)
					{
						fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars((string)s)))
						{
							string text2 = val.GetProperty("ArtistName") as string;
							fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars((!(text2 == null)) ? text2 : "")))
							{
								string text3 = val.GetProperty("AlbumName") as string;
								fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars((!(text3 == null)) ? text3 : "")))
								{
									IMSMediaSchemaPropertySet* ptr4 = null;
									num = global::_003CModule_003E.ZuneLibraryExports_002ECreateTrackPropSet(gUID, global::_003CModule_003E.GUID_NULL, 0, ptr, 0, ptr3, ptr2, (ushort*)Unsafe.AsPointer(ref global::_003CModule_003E._003F_003F_C_0040_11LOCGONAA_0040_003F_0024AA_003F_0024AA_0040), &ptr4);
									if (num >= 0)
									{
										// IL initblk instruction
										Unsafe.InitBlock(ref cComPropVariant, 0, 16);
										try
										{
											*(short*)(&cComPropVariant) = 8;
											Unsafe.As<CComPropVariant, int>(ref Unsafe.AddByteOffset(ref cComPropVariant, 8)) = (int)global::_003CModule_003E.SysAllocString(ptr2);
											tagPROPVARIANT tagPROPVARIANT2 = (tagPROPVARIANT)cComPropVariant;
											num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, tagPROPVARIANT, int>)(int)(*(uint*)(*(int*)ptr4 + 28)))((nint)ptr4, 16785409u, tagPROPVARIANT2);
										}
										catch
										{
											//try-fault
											global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
											throw;
										}
										global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
										if (num >= 0)
										{
											num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, IMSMediaSchemaPropertySet*, IMSMediaSchemaPropertySet**, int>)(int)(*(uint*)(*(int*)pPropList + 36)))((nint)pPropList, 0u, ptr4, null);
										}
									}
									if (null != ptr4)
									{
										IMSMediaSchemaPropertySet* intPtr = ptr4;
										((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
										ptr4 = null;
									}
									do
									{
										num2++;
										if (num >= 0 && num2 < m_tracks.Count)
										{
											obj = m_tracks[num2];
											val = (DataProviderObject)((obj is DataProviderObject) ? obj : null);
											continue;
										}
										return num;
									}
									while (val == null);
									gUID = global::_003CModule_003E.GUID_NULL;
									property = val.GetProperty("ZuneMediaId");
									if (property != null)
									{
										gUID = global::_003CModule_003E.GuidToGUID((Guid)property);
									}
									text = val.GetProperty("Title") as string;
									s = ((!(text == null)) ? text : "");
								}
							}
						}
					}
				}
				num2++;
				if (num < 0)
				{
					break;
				}
			}
		}
		return num;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EPlaylistMessageData();
			return;
		}
		try
		{
			_0021PlaylistMessageData();
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

	~PlaylistMessageData()
	{
		Dispose(false);
	}
}
