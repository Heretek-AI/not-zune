using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Iris;

namespace MicrosoftZuneLibrary;

public class LibraryDataProviderListItem : LibraryDataProviderItemBase
{
	protected internal int m_LibraryId;

	protected LibraryVirtualList m_listOwner;

	protected int m_DontUseDirectly_Index;

	protected int m_QueryRN;

	internal unsafe LibraryDataProviderListItem(LibraryDataProviderQuery owner, LibraryVirtualList listOwner, object typeCookie, int QueryRN, int index)
		: base((DataProviderQuery)(object)owner, typeCookie)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_DD(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 11, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId, (uint)index);
		}
		m_DontUseDirectly_Index = index;
		m_QueryRN = QueryRN;
		m_listOwner = listOwner;
		if (QueryRN != -1)
		{
			m_LibraryId = (int)m_listOwner.QueryList.GetFieldValue((uint)index, typeof(int), 355u, -1);
		}
		SetSlowDataThumbnailExtraction(useSlowData: true);
	}

	public LibraryVirtualList GetOwner()
	{
		return m_listOwner;
	}

	protected internal unsafe int GetIndex()
	{
		int queryRN = m_QueryRN;
		if (queryRN != -1)
		{
			LibraryVirtualList listOwner = m_listOwner;
			if (queryRN != listOwner.GetQueryRN())
			{
				int indexForLibraryId = (int)listOwner.QueryList.GetIndexForLibraryId(m_DontUseDirectly_Index, m_LibraryId);
				if (indexForLibraryId != -1)
				{
					if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
					{
						global::_003CModule_003E.WPP_SF_DDD(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 50, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId, (uint)m_DontUseDirectly_Index, (uint)indexForLibraryId);
					}
					m_DontUseDirectly_Index = indexForLibraryId;
					m_QueryRN = m_listOwner.GetQueryRN();
				}
			}
		}
		return m_DontUseDirectly_Index;
	}

	protected override object GetFieldValue(Type type, uint Atom)
	{
		uint index = (uint)GetIndex();
		return m_listOwner.QueryList.GetFieldValue(index, type, Atom, null);
	}

	protected override object GetFieldValue(Type type, string AtomName, object defaultValue)
	{
		return m_listOwner.QueryList.GetFieldValue((uint)GetIndex(), type, AtomName, defaultValue);
	}

	protected override object GetFieldValue(Type type, uint Atom, object defaultValue)
	{
		return m_listOwner.QueryList.GetFieldValue((uint)GetIndex(), type, Atom, defaultValue);
	}

	protected override int SetFieldValue(string AtomName, object Value)
	{
		return m_listOwner.QueryList.SetFieldValue((uint)GetIndex(), AtomName, Value);
	}

	protected override int SetFieldValue(uint Atom, object Value)
	{
		return m_listOwner.QueryList.SetFieldValue((uint)GetIndex(), Atom, Value);
	}

	protected unsafe override void NotifySlowDataAcquireComplete()
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 20, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
		int index = GetIndex();
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_DD(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 21, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId, (uint)index);
		}
		((VirtualList)m_listOwner).NotifySlowDataAcquireComplete(index);
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_D(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 22, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids), m_uTraceId);
		}
	}

	[return: MarshalAs(UnmanagedType.U1)]
	protected override bool AntialiasImageEdges()
	{
		return m_listOwner.AntialiasImageEdges;
	}

	protected override string ThumbnailFallbackImageUrl()
	{
		return m_listOwner.QueryOwner.ThumbnailFallbackImageUrl;
	}

	protected unsafe override void UpdateThumbnail(object args)
	{
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 48, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids));
		}
		if (!((ModelItem)m_listOwner).IsDisposed)
		{
			base.UpdateThumbnail(args);
		}
		else
		{
			((IDisposable)(AsyncGetThumbnailState)args).Dispose();
		}
		if (global::_003CModule_003E.WPP_GLOBAL_Control != Unsafe.AsPointer(ref global::_003CModule_003E.WPP_GLOBAL_Control) && (((int*)global::_003CModule_003E.WPP_GLOBAL_Control)[23] & 0x40) != 0 && (uint)((byte*)global::_003CModule_003E.WPP_GLOBAL_Control)[89] >= 7u)
		{
			global::_003CModule_003E.WPP_SF_(((ulong*)global::_003CModule_003E.WPP_GLOBAL_Control)[10], 49, (_GUID*)Unsafe.AsPointer(ref global::_003CModule_003E._003FA0xb7c00b43_002EWPP_LibraryDataProvider_cpp_Traceguids));
		}
	}
}
