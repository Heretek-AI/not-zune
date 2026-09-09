using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using _003CCppImplementationDetails_003E;

namespace MicrosoftZuneLibrary;

public class ZuneQueryList : IDisposable
{
	protected unsafe IDatabaseQueryResults* m_pResults;

	protected unsafe ResultSetEventRelay* m_pRelay;

	private unsafe ushort* m_bstrQueryName = null;

	private int m_RefCount = 0;

	private bool m_disposed = false;

	public bool IsDisposed
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			return m_disposed;
		}
	}

	public unsafe bool IsEmpty
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			IDatabaseQueryResults* pResults = m_pResults;
			if (pResults != null)
			{
				return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pResults + 48)))((nint)pResults) == 1;
			}
			return true;
		}
	}

	public unsafe int Count
	{
		get
		{
			int result = 0;
			IDatabaseQueryResults* pResults = m_pResults;
			if (pResults != null && ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)pResults + 44)))((nint)pResults, (uint*)(&result)) >= 0)
			{
				return result;
			}
			return result;
		}
	}

	public unsafe ZuneQueryList(IDatabaseQueryResults* pResults, string queryName)
	{
		m_pResults = pResults;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(queryName)))
		{
			m_bstrQueryName = global::_003CModule_003E.SysAllocString(ptr);
			IDatabaseQueryResults* pResults2 = m_pResults;
			if (pResults2 != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pResults2 + 4)))((nint)pResults2);
				ResultSetEventRelay* ptr2 = (ResultSetEventRelay*)global::_003CModule_003E.@new(32u);
				ResultSetEventRelay* pRelay;
				try
				{
					pRelay = ((ptr2 == null) ? null : global::_003CModule_003E.MicrosoftZuneLibrary_002EResultSetEventRelay_002E_007Bctor_007D(ptr2));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.delete(ptr2);
					throw;
				}
				m_pRelay = pRelay;
			}
		}
	}

	private unsafe void _007EZuneQueryList()
	{
		m_disposed = true;
		IDatabaseQueryResults* pResults = m_pResults;
		if (pResults != null)
		{
			IDatabaseQueryResults* intPtr = pResults;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, void>)(int)(*(uint*)(*(int*)intPtr + 80)))((nint)intPtr);
			pResults = m_pResults;
			if (null != pResults)
			{
				IDatabaseQueryResults* intPtr2 = pResults;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
				m_pResults = null;
			}
		}
		ResultSetEventRelay* pRelay = m_pRelay;
		if (null != pRelay)
		{
			ResultSetEventRelay* ptr = pRelay;
			global::_003CModule_003E.MicrosoftZuneLibrary_002EResultSetEventRelay_002E_007Bdtor_007D(ptr);
			global::_003CModule_003E.delete(ptr);
			m_pRelay = null;
		}
		ushort* bstrQueryName = m_bstrQueryName;
		if (null != bstrQueryName)
		{
			global::_003CModule_003E.SysFreeString(bstrQueryName);
			m_bstrQueryName = null;
		}
	}

	private unsafe void _0021ZuneQueryList()
	{
		_ = string.Empty;
		ushort* bstrQueryName = m_bstrQueryName;
		if (bstrQueryName != null)
		{
			new string((char*)bstrQueryName);
		}
		ResultSetEventRelay* pRelay = m_pRelay;
		if (null != pRelay)
		{
			ResultSetEventRelay* ptr = pRelay;
			global::_003CModule_003E.MicrosoftZuneLibrary_002EResultSetEventRelay_002E_007Bdtor_007D(ptr);
			global::_003CModule_003E.delete(ptr);
			m_pRelay = null;
		}
		IDatabaseQueryResults* pResults = m_pResults;
		if (null != pResults)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pResults + 8)))((nint)pResults);
			m_pResults = null;
		}
		ushort* bstrQueryName2 = m_bstrQueryName;
		if (null != bstrQueryName2)
		{
			global::_003CModule_003E.SysFreeString(bstrQueryName2);
			m_bstrQueryName = null;
		}
	}

	public uint AddRef()
	{
		return (uint)Interlocked.Increment(ref m_RefCount);
	}

	public uint Release()
	{
		int num = Interlocked.Decrement(ref m_RefCount);
		if (0 == num)
		{
			((IDisposable)this).Dispose();
		}
		return (uint)num;
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool CheckItemIndex(uint index)
	{
		IDatabaseQueryResults* pResults = m_pResults;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)pResults + 12)))((nint)pResults, index) >= 0;
	}

	public unsafe void Advise(IQueryListEvents listener)
	{
		global::_003CModule_003E.MicrosoftZuneLibrary_002EResultSetEventRelay_002EAdvise(m_pRelay, listener, m_pResults);
	}

	public unsafe void Unadvise(IQueryListEvents listener)
	{
		global::_003CModule_003E.MicrosoftZuneLibrary_002EResultSetEventRelay_002EUnAdvise(m_pRelay, listener, m_pResults);
	}

	public unsafe void EndBulkEventsComplete([MarshalAs(UnmanagedType.U1)] bool fAbandon)
	{
		int num = (fAbandon ? 1 : 0);
		IDatabaseQueryResults* pResults = m_pResults;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, void>)(int)(*(uint*)(*(int*)pResults + 64)))((nint)pResults, num);
	}

	public object GetFieldValue(uint index, Type type, string AtomName, object defaultValue)
	{
		int num = AtomNameToAtom(AtomName);
		if (num == -1)
		{
			return defaultValue;
		}
		return GetFieldValue(index, type, (uint)num, defaultValue);
	}

	public object GetFieldValue(uint index, Type type, string AtomName)
	{
		return GetFieldValue(index, type, AtomName, null);
	}

	public unsafe object GetFieldValue(uint index, Type type, uint Atom, object defaultValue)
	{
		if (m_disposed)
		{
			if (Atom != 340 && Atom != 140 && global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[7] & 2) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[25] >= 5u)
			{
				global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[2], 10, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0x037fed32_002EWPP_ZuneDBList_cpp_Traceguids), Atom);
			}
			return defaultValue;
		}
		Unsafe.SkipInit(out CComPropVariant cComPropVariant);
		object result;
		if (m_pResults != null)
		{
			if ((object)type == typeof(string))
			{
				WMT_ATTR_DATATYPE wMT_ATTR_DATATYPE = *(WMT_ATTR_DATATYPE*)((ref *(_003F*)(Atom * 24)) + (ref Unsafe.As<_0024ArrayType_0024_0024_0024BY0A_0040_0024_0024CBU_SCHEMAMAPENTRY_0040CSchemaMap_0040_0040, _003F>(ref Unsafe.AddByteOffset(ref global::_003CModule_003E._003Fs_rgSchemaMapEntry_0040CSchemaMap_0040_00400QBU_SCHEMAMAPENTRY_00401_0040B, 8))));
				if (wMT_ATTR_DATATYPE == (WMT_ATTR_DATATYPE)1)
				{
					ushort* value = null;
					IDatabaseQueryResults* pResults = m_pResults;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, ushort**, int>)(int)(*(uint*)(*(int*)pResults + 16)))((nint)pResults, index, Atom, &value) >= 0)
					{
						return new string((char*)value);
					}
				}
			}
			// IL initblk instruction
			Unsafe.InitBlock(ref cComPropVariant, 0, 16);
			try
			{
				IDatabaseQueryResults* pResults2 = m_pResults;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pResults2 + 24)))((nint)pResults2, index, Atom, (tagPROPVARIANT*)(&cComPropVariant)) >= 0)
				{
					result = MarshalResult(type, (tagPROPVARIANT*)(&cComPropVariant), defaultValue);
					goto IL_00e5;
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
				throw;
			}
			global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		}
		return defaultValue;
		IL_00e5:
		global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		return result;
	}

	public object GetFieldValue(uint index, Type type, uint Atom)
	{
		return GetFieldValue(index, type, Atom, null);
	}

	public int SetFieldValue(uint index, string AtomName, object Value)
	{
		int num = AtomNameToAtom(AtomName);
		if (num == -1)
		{
			return -2147024809;
		}
		return SetFieldValue(index, (uint)num, Value);
	}

	public unsafe int SetFieldValue(uint index, uint Atom, object Value)
	{
		int num = -2147024809;
		if (m_pResults != null)
		{
			Unsafe.SkipInit(out CComPropVariant cComPropVariant);
			// IL initblk instruction
			Unsafe.InitBlock(ref cComPropVariant, 0, 16);
			try
			{
				if (Value == null)
				{
					goto IL_002d;
				}
				num = ConvertTypeToPropVariant(Value.GetType(), Value, (tagPROPVARIANT*)(&cComPropVariant));
				if (num >= 0)
				{
					goto IL_002d;
				}
				goto end_IL_0015;
				IL_002d:
				IDatabaseQueryResults* pResults = m_pResults;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, uint, tagPROPVARIANT*, int>)(int)(*(uint*)(*(int*)pResults + 32)))((nint)pResults, index, Atom, (tagPROPVARIANT*)(&cComPropVariant));
				end_IL_0015:;
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPropVariant*, void>)(&global::_003CModule_003E.CComPropVariant_002E_007Bdtor_007D), &cComPropVariant);
				throw;
			}
			global::_003CModule_003E.CComPropVariant_002EClear(&cComPropVariant);
		}
		return num;
	}

	public unsafe uint GetIndexForLibraryId(int indexHint, int LibraryId)
	{
		uint result = uint.MaxValue;
		fixed (uint* ptr = &Unsafe.AsRef<uint>(&result))
		{
			IDatabaseQueryResults* pResults = m_pResults;
			if (pResults != null)
			{
				int num = *(int*)pResults + 72;
				if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int, uint*, int>)(int)(*(uint*)num))((nint)m_pResults, (uint)indexHint, LibraryId, ptr) != 0)
				{
					result = uint.MaxValue;
				}
			}
			return result;
		}
	}

	public unsafe ArrayList GetUniqueIds()
	{
		ArrayList arrayList = null;
		IDatabaseQueryResults* pResults = m_pResults;
		Unsafe.SkipInit(out int num2);
		int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int*, int>)(int)(*(uint*)(*(int*)pResults + 76)))((nint)pResults, null, &num2);
		if (num >= 0)
		{
			int* ptr = (int*)global::_003CModule_003E.new_005B_005D(((uint)num2 > 1073741823u) ? uint.MaxValue : ((uint)(num2 << 2)));
			if (ptr == null)
			{
				num = -2147024882;
			}
			if (num >= 0)
			{
				pResults = m_pResults;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int*, int>)(int)(*(uint*)(*(int*)pResults + 76)))((nint)pResults, ptr, &num2);
				if (num >= 0)
				{
					arrayList = new ArrayList(num2);
					if (arrayList == null)
					{
						num = -2147024882;
					}
					if (num >= 0)
					{
						int num3 = 0;
						if (0 < num2)
						{
							do
							{
								arrayList.Add(*(int*)(num3 * 4 + (byte*)ptr));
								num3++;
							}
							while (num3 < num2);
						}
					}
				}
			}
			if (ptr != null)
			{
				global::_003CModule_003E.delete(ptr);
			}
		}
		return arrayList;
	}

	public unsafe int SearchForString(uint Atom, [MarshalAs(UnmanagedType.U1)] bool ascending, string SearchString)
	{
		int result = -1;
		if (m_pResults != null)
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(SearchString)))
			{
				try
				{
					int num = *(int*)m_pResults + 36;
					Unsafe.SkipInit(out uint num2);
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int, ushort*, uint*, int>)(int)(*(uint*)num))((nint)m_pResults, Atom, ascending ? 1 : 0, ptr, &num2) >= 0)
					{
						result = (int)num2;
					}
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		return result;
	}

	public unsafe int ClientBusy([MarshalAs(UnmanagedType.U1)] bool fBusy)
	{
		int result = 0;
		IDatabaseQueryResults* pResults = m_pResults;
		if (pResults != null)
		{
			result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pResults + 60)))((nint)pResults, fBusy ? 1 : 0);
		}
		return result;
	}

	public unsafe static object MarshalResult(Type type, tagPROPVARIANT* propVariant, object defaultValue)
	{
		if ((object)type == typeof(int))
		{
			switch (*(ushort*)propVariant)
			{
			case 3:
				return ((int*)propVariant)[2];
			case 11:
				return (((short*)propVariant)[4] == -1) ? 1 : 0;
			}
		}
		else if ((object)type == typeof(uint))
		{
			ushort num = *(ushort*)propVariant;
			if (num == 3 || num == 19)
			{
				return ((uint*)propVariant)[2];
			}
		}
		else if ((object)type == typeof(long))
		{
			switch (*(ushort*)propVariant)
			{
			case 20:
				return ((long*)propVariant)[1];
			case 3:
				return (long)((int*)propVariant)[2];
			case 19:
				return (long)(uint)((int*)propVariant)[2];
			}
		}
		else if ((object)type == typeof(ulong))
		{
			ushort num2 = *(ushort*)propVariant;
			if (num2 == 20 || num2 == 21)
			{
				return ((ulong*)propVariant)[1];
			}
		}
		else if ((object)type == typeof(string))
		{
			if (*(ushort*)propVariant == 72)
			{
				global::_003CModule_003E.ZuneLibraryExports_002EZunePropVariantChangeType(propVariant, propVariant, 0, 8);
			}
			if (*(ushort*)propVariant == 8)
			{
				return new string((char*)(int)((uint*)propVariant)[2]);
			}
		}
		else if ((object)type == typeof(DateTime))
		{
			if (*(ushort*)propVariant == 7)
			{
				return DateTime.FromOADate(((double*)propVariant)[1]);
			}
		}
		else if ((object)type == typeof(TimeSpan))
		{
			switch (*(ushort*)propVariant)
			{
			case 3:
				return TimeSpan.FromMilliseconds((double)((int*)propVariant)[2]);
			case 20:
			case 21:
				return TimeSpan.FromMilliseconds(((ulong*)propVariant)[1]);
			}
		}
		else if ((object)type == typeof(bool))
		{
			if (*(ushort*)propVariant == 11)
			{
				return (byte)((((short*)propVariant)[4] != 0) ? 1u : 0u) != 0;
			}
		}
		else if ((object)type == typeof(Guid))
		{
			if (*(ushort*)propVariant == 8)
			{
				ValueType valueType = default(Guid);
				(Guid)valueType = new Guid(new string((char*)(int)((uint*)propVariant)[2]));
				return valueType;
			}
		}
		else if (typeof(IList).IsAssignableFrom(type))
		{
			switch (*(ushort*)propVariant)
			{
			case 8200:
			{
				tagSAFEARRAY* ptr2 = (tagSAFEARRAY*)(int)((uint*)propVariant)[2];
				if (global::_003CModule_003E.SafeArrayGetDim(ptr2) != 1)
				{
					return defaultValue;
				}
				ArrayList arrayList2 = new ArrayList();
				if (arrayList2 != null)
				{
					int num4 = 0;
					int num5 = 0;
					Unsafe.SkipInit(out ushort** ptr3);
					if (global::_003CModule_003E.SafeArrayGetLBound(ptr2, 1u, &num4) < 0 || global::_003CModule_003E.SafeArrayGetUBound(ptr2, 1u, &num5) < 0 || global::_003CModule_003E.SafeArrayAccessData(ptr2, (void**)(&ptr3)) < 0)
					{
						return defaultValue;
					}
					uint num6 = (uint)(num5 - num4 + 1);
					uint num7 = 0u;
					if (0 < num6)
					{
						do
						{
							arrayList2.Add(new string((char*)(int)(*(uint*)((int)(num7 * 4) + (byte*)ptr3))));
							num7++;
						}
						while (num7 < num6);
					}
					global::_003CModule_003E.SafeArrayUnaccessData(ptr2);
				}
				return arrayList2;
			}
			case 4099:
			{
				ArrayList arrayList = new ArrayList();
				if (arrayList != null)
				{
					uint num3 = 0u;
					if (0u < (uint)((int*)propVariant)[2])
					{
						tagPROPVARIANT* ptr = (tagPROPVARIANT*)((byte*)propVariant + 12);
						do
						{
							arrayList.Add(*(int*)(int)(num3 * 4 + (uint)(*(int*)ptr)));
							num3++;
						}
						while (num3 < (uint)((int*)propVariant)[2]);
					}
				}
				return arrayList;
			}
			}
		}
		else if (type.IsEnum)
		{
			ushort num8 = *(ushort*)propVariant;
			if (num8 == 3 || num8 == 19)
			{
				return Enum.ToObject(type, ((int*)propVariant)[2]);
			}
		}
		return defaultValue;
	}

	public unsafe static int ConvertTypeToPropVariant(Type type, object value, tagPROPVARIANT* propVariant)
	{
		//The blocks IL_020b, IL_0219, IL_0264, IL_026c, IL_028c, IL_0293 are reachable both inside and outside the pinned region starting at IL_0238. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		//The blocks IL_0293 are reachable both inside and outside the pinned region starting at IL_002c. ILSpy has duplicated these blocks in order to place them both within and outside the `fixed` statement.
		int result = -2147467259;
		if ((object)type == null)
		{
			type = value.GetType();
		}
		if ((object)type == typeof(string))
		{
			*(short*)propVariant = 8;
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars((string)value)))
			{
				((int*)propVariant)[2] = (int)global::_003CModule_003E.SysAllocString(ptr);
				return 0;
			}
		}
		if ((object)type == typeof(int))
		{
			*(short*)propVariant = 3;
			((int*)propVariant)[2] = (int)value;
			result = 0;
		}
		else if ((object)type == typeof(uint))
		{
			*(short*)propVariant = 19;
			((int*)propVariant)[2] = (int)(uint)value;
			result = 0;
		}
		else if ((object)type == typeof(long))
		{
			*(short*)propVariant = 20;
			((long*)propVariant)[1] = (long)value;
			result = 0;
		}
		else if ((object)type == typeof(ulong))
		{
			*(short*)propVariant = 20;
			((long*)propVariant)[1] = (long)value;
			result = 0;
		}
		else if ((object)type == typeof(DateTime))
		{
			DateTime dateTime = (DateTime)value;
			if (dateTime != DateTime.MinValue)
			{
				if (dateTime.Year >= 0 && dateTime.Year < 100)
				{
					dateTime = ((dateTime.Year >= 30) ? dateTime.AddYears(1900) : dateTime.AddYears(2000));
				}
				*(short*)propVariant = 7;
				((double*)propVariant)[1] = dateTime.ToOADate();
			}
			result = 0;
		}
		else if ((object)type == typeof(TimeSpan))
		{
			*(short*)propVariant = 3;
			((int*)propVariant)[2] = (int)((TimeSpan)(TimeSpan)value).TotalMilliseconds;
			result = 0;
		}
		else if ((object)type == typeof(bool))
		{
			*(short*)propVariant = 11;
			int num = -1;
			if (!(bool)value)
			{
				num = ~num;
			}
			short num2 = (short)num;
			((short*)propVariant)[4] = num2;
			result = 0;
		}
		else if (typeof(IList).IsAssignableFrom(type))
		{
			IList list = (IList)value;
			if (list != null)
			{
				int count = list.Count;
				if (count == 0)
				{
					global::_003CModule_003E.PropVariantClear(propVariant);
				}
				else
				{
					Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY00UtagSAFEARRAYBOUND_0040_0040 _0024ArrayType_0024_0024_0024BY00UtagSAFEARRAYBOUND_0040_00402);
					Unsafe.As<_0024ArrayType_0024_0024_0024BY00UtagSAFEARRAYBOUND_0040_0040, int>(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY00UtagSAFEARRAYBOUND_0040_00402, 4)) = 0;
					*(int*)(&_0024ArrayType_0024_0024_0024BY00UtagSAFEARRAYBOUND_0040_00402) = count;
					tagSAFEARRAY* ptr2 = global::_003CModule_003E.SafeArrayCreate(8, 1u, (tagSAFEARRAYBOUND*)(&_0024ArrayType_0024_0024_0024BY00UtagSAFEARRAYBOUND_0040_00402));
					if (ptr2 == null)
					{
						return -2147024882;
					}
					Unsafe.SkipInit(out _0024ArrayType_0024_0024_0024BY01J _0024ArrayType_0024_0024_0024BY01J2);
					*(int*)(&_0024ArrayType_0024_0024_0024BY01J2) = 0;
					// IL initblk instruction
					Unsafe.InitBlock(ref Unsafe.AddByteOffset(ref _0024ArrayType_0024_0024_0024BY01J2, 4), 0, 4);
					int num3 = 0;
					if (0 < count)
					{
						object obj = list[num3];
						if (obj != null)
						{
							if ((object)obj.GetType() == typeof(string))
							{
								while (true)
								{
									fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars((string)obj)))
									{
										ushort* ptr4 = global::_003CModule_003E.SysAllocString(ptr3);
										if (ptr4 != null)
										{
											*(int*)(&_0024ArrayType_0024_0024_0024BY01J2) = num3;
											result = global::_003CModule_003E.SafeArrayPutElement(ptr2, (int*)(&_0024ArrayType_0024_0024_0024BY01J2), ptr4);
											if (result < 0)
											{
												goto IL_028c;
											}
											num3++;
											if (num3 < count)
											{
												obj = list[num3];
												if (obj != null)
												{
													if ((object)obj.GetType() != typeof(string))
													{
														result = -2147467259;
														goto IL_028c;
													}
													continue;
												}
												result = -2147467259;
												goto IL_028c;
											}
											goto IL_027a;
										}
										result = -2147024882;
										goto IL_027a;
										IL_0293:
										return result;
										IL_028c:
										global::_003CModule_003E.SafeArrayDestroy(ptr2);
										goto IL_0293;
										IL_027a:
										if (result < 0)
										{
											goto IL_028c;
										}
										*(short*)propVariant = 8200;
										((int*)propVariant)[2] = (int)ptr2;
										goto IL_0293;
									}
								}
							}
							result = -2147467259;
						}
						else
						{
							result = -2147467259;
						}
					}
					global::_003CModule_003E.SafeArrayDestroy(ptr2);
				}
			}
		}
		return result;
	}

	public unsafe static int AtomNameToAtom(string AtomName)
	{
		fixed (ushort* wszName = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(AtomName)))
		{
			return global::_003CModule_003E.CSchemaMap_002EGetIndex(wszName);
		}
	}

	public unsafe static string AtomToAtomName(int atom)
	{
		return new string((char*)(int)(*(uint*)((ref *(_003F*)(atom * 24)) + (ref Unsafe.As<_0024ArrayType_0024_0024_0024BY0A_0040_0024_0024CBU_SCHEMAMAPENTRY_0040CSchemaMap_0040_0040, _003F>(ref global::_003CModule_003E._003Fs_rgSchemaMapEntry_0040CSchemaMap_0040_00400QBU_SCHEMAMAPENTRY_00401_0040B)))));
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EZuneQueryList();
			return;
		}
		try
		{
			_0021ZuneQueryList();
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

	~ZuneQueryList()
	{
		Dispose(false);
	}
}
