using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using _003CCppImplementationDetails_003E;

namespace MicrosoftZuneInterop;

public class QueryPropertyBag : IDisposable
{
	private unsafe IQueryPropertyBag* m_pPropertyBag;

	public unsafe QueryPropertyBag()
	{
		Unsafe.SkipInit(out IQueryPropertyBag* pPropertyBag);
		global::_003CModule_003E.ZuneLibraryExports_002ECreatePropertyBag(&pPropertyBag);
		m_pPropertyBag = pPropertyBag;
	}

	private unsafe void _0021QueryPropertyBag()
	{
		IQueryPropertyBag* pPropertyBag = m_pPropertyBag;
		if (null != pPropertyBag)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pPropertyBag + 8)))((nint)pPropertyBag);
			m_pPropertyBag = null;
		}
	}

	private void _007EQueryPropertyBag()
	{
		_0021QueryPropertyBag();
	}

	public unsafe void SetValue(string propertyName, object value)
	{
		EQueryPropertyBagProp eQueryPropertyBagProp = MapNameToProp(propertyName);
		if (eQueryPropertyBagProp == (EQueryPropertyBagProp)(-1) || value == null)
		{
			return;
		}
		int num = -2147024809;
		Type type = value.GetType();
		if ((object)type == typeof(int))
		{
			IQueryPropertyBag* pPropertyBag = m_pPropertyBag;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)pPropertyBag + 28)))((nint)pPropertyBag, eQueryPropertyBagProp, (int)value);
		}
		else if ((object)type == typeof(bool))
		{
			IQueryPropertyBag* pPropertyBag2 = m_pPropertyBag;
			num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int, int>)(int)(*(uint*)(*(int*)pPropertyBag2 + 28)))((nint)pPropertyBag2, eQueryPropertyBagProp, ((bool)value) ? 1 : 0);
		}
		else
		{
			if ((object)type != typeof(string))
			{
				goto IL_00bf;
			}
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars((string)value)))
			{
				try
				{
					int num2 = *(int*)m_pPropertyBag + 20;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, ushort*, int>)(int)(*(uint*)num2))((nint)m_pPropertyBag, eQueryPropertyBagProp, ptr);
				}
				catch
				{
					//try-fault
					ptr = null;
					throw;
				}
			}
		}
		if (num >= 0)
		{
			return;
		}
		goto IL_00bf;
		IL_00bf:
		throw new ApplicationException(global::_003CModule_003E.GetErrorDescription(num));
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool IsSet(string propertyName)
	{
		int num = 0;
		EQueryPropertyBagProp eQueryPropertyBagProp = MapNameToProp(propertyName);
		if (eQueryPropertyBagProp != (EQueryPropertyBagProp)(-1))
		{
			IQueryPropertyBag* pPropertyBag = m_pPropertyBag;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQueryPropertyBagProp, int*, int>)(int)(*(uint*)(*(int*)pPropertyBag + 52)))((nint)pPropertyBag, eQueryPropertyBagProp, &num);
		}
		return num == 1;
	}

	public unsafe EQueryPropertyBagProp MapNameToProp(string propertyName)
	{
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(propertyName)))
		{
			int num = 0;
			EQueryPropertyBagProp eQueryPropertyBagProp;
			while (true)
			{
				if (global::_003CModule_003E._wcsicmp(ptr, (ushort*)(int)(*(uint*)((ref *(_003F*)(num * 8)) + (ref Unsafe.As<_0024ArrayType_0024_0024_0024BY0CF_0040_0024_0024CBUPropIdMapEntry_0040MicrosoftZuneInterop_0040_0040, _003F>(ref global::_003CModule_003E.MicrosoftZuneInterop_002E_003FA0x30e7a1fd_002EkPropIdMap))))) != 0)
				{
					num++;
					if ((uint)num < 37u)
					{
						continue;
					}
				}
				else
				{
					eQueryPropertyBagProp = *(EQueryPropertyBagProp*)((ref *(_003F*)(num * 8)) + (ref Unsafe.As<_0024ArrayType_0024_0024_0024BY0CF_0040_0024_0024CBUPropIdMapEntry_0040MicrosoftZuneInterop_0040_0040, _003F>(ref Unsafe.AddByteOffset(ref global::_003CModule_003E.MicrosoftZuneInterop_002E_003FA0x30e7a1fd_002EkPropIdMap, 4))));
					if (eQueryPropertyBagProp != (EQueryPropertyBagProp)(-1))
					{
						break;
					}
				}
				throw new ArgumentException("Invalid property name: " + propertyName, "propertyName");
			}
			return eQueryPropertyBagProp;
		}
	}

	public unsafe IQueryPropertyBag* GetIQueryPropertyBag()
	{
		return m_pPropertyBag;
	}

	public unsafe IDList* PackIDList(IList multiIds)
	{
		int count = multiIds.Count;
		IDList* ptr = (IDList*)global::_003CModule_003E.@new(8u);
		IDList* ptr2;
		try
		{
			if (ptr != null)
			{
				((int*)ptr)[1] = 0;
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		if (ptr2 != null)
		{
			*(int*)ptr2 = count;
			uint num = (((uint)count > 1073741823u) ? uint.MaxValue : ((uint)(count << 2)));
			((int*)ptr2)[1] = (int)global::_003CModule_003E.new_005B_005D(num);
			int num2 = 0;
			if (0 < count)
			{
				do
				{
					*(int*)(num2 * 4 + ((int*)ptr2)[1]) = (int)multiIds[num2];
					num2++;
				}
				while (num2 < count);
			}
		}
		return ptr2;
	}

	public unsafe IMultiSortAttributes* PackMultiSortAttributes(string[] sortStrings, bool[] sortAscendings)
	{
		int num = sortStrings.Length;
		int* ptr = null;
		int* ptr2 = null;
		Unsafe.SkipInit(out IMultiSortAttributes* ptr3);
		if (global::_003CModule_003E.ZuneLibraryExports_002ECreateMultiSortAttributes(num, &ptr3) >= 0)
		{
			IMultiSortAttributes* intPtr = ptr3;
			ptr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*>)(int)(*(uint*)(*(int*)intPtr + 16)))((nint)intPtr);
			IMultiSortAttributes* intPtr2 = ptr3;
			ptr2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*>)(int)(*(uint*)(*(int*)intPtr2 + 20)))((nint)intPtr2);
		}
		else
		{
			num = 0;
		}
		int num2 = 0;
		if (0 < num)
		{
			int* ptr4 = ptr2;
			int* ptr5 = (int*)((byte*)ptr - (nuint)ptr2);
			do
			{
				EQuerySortType eQuerySortType = (sortAscendings[num2] ? EQuerySortType.eQuerySortOrderAscending : EQuerySortType.eQuerySortOrderDescending);
				fixed (ushort* wszName = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(sortStrings[num2])))
				{
					try
					{
						int num3 = global::_003CModule_003E.CSchemaMap_002EGetIndex(wszName);
						if (num3 == -1)
						{
							eQuerySortType = EQuerySortType.eQuerySortOrderNone;
						}
						*(int*)((byte*)ptr5 + (nuint)ptr4) = num3;
						*ptr4 = (int)eQuerySortType;
					}
					catch
					{
						//try-fault
						wszName = null;
						throw;
					}
				}
				num2++;
				ptr4++;
			}
			while (num2 < num);
		}
		return ptr3;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EQueryPropertyBag();
			return;
		}
		try
		{
			_0021QueryPropertyBag();
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

	~QueryPropertyBag()
	{
		Dispose(false);
	}
}
