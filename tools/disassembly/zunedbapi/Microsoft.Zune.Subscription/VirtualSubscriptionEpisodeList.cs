using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Iris;

namespace Microsoft.Zune.Subscription;

[DefaultMember("Item")]
public class VirtualSubscriptionEpisodeList : VirtualList, IDisposable
{
	private string m_feedUrl;

	private DataProviderQuery m_owner;

	private object m_typeCookie;

	private List<SubscriptionDataProviderItem> m_items;

	private object m_lock;

	private string m_sortProperty;

	private bool m_sortAsc;

	internal virtual string FeedUrl => m_feedUrl;

	public VirtualSubscriptionEpisodeList(DataProviderQuery owner, object typeCookie, string feedUrl, string sort)
	{
		m_feedUrl = feedUrl;
		m_owner = owner;
		m_typeCookie = typeCookie;
		((VirtualList)this)._002Ector();
		try
		{
			m_items = new List<SubscriptionDataProviderItem>();
			m_lock = new object();
			SetSortProperty(sort);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)this).Dispose();
			throw;
		}
	}

	private void _007EVirtualSubscriptionEpisodeList()
	{
		Monitor.Enter(m_lock);
		try
		{
			List<SubscriptionDataProviderItem>.Enumerator enumerator = m_items.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.OnDispose();
			}
			m_items = null;
		}
		finally
		{
			Monitor.Exit(m_lock);
		}
	}

	internal unsafe void AddItem(IMSMediaSchemaPropertySet* pEpisodeItem)
	{
		Monitor.Enter(m_lock);
		try
		{
			if (m_items != null)
			{
				m_items.Add(new SubscriptionDataProviderItem(m_owner, m_typeCookie, m_feedUrl, pEpisodeItem));
			}
		}
		finally
		{
			Monitor.Exit(m_lock);
		}
	}

	internal unsafe void AsyncRetrieveEpisodeList(SubscriptionSeriesInfo seriesInfo)
	{
		UpdateQueryStatus((DataProviderQueryStatus)1);
		ISubscriptionViewer* ptr = null;
		VirtualSubscriptionEpisodeListProxy* ptr2 = null;
		int num = global::_003CModule_003E.ZuneLibraryExports_002ECreateNativeSubscriptionViewer((void**)(&ptr));
		if (num >= 0)
		{
			VirtualSubscriptionEpisodeListProxy* ptr3 = (VirtualSubscriptionEpisodeListProxy*)global::_003CModule_003E.@new(20u);
			VirtualSubscriptionEpisodeListProxy* ptr4;
			try
			{
				ptr4 = ((ptr3 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002ESubscription_002EVirtualSubscriptionEpisodeListProxy_002E_007Bctor_007D(ptr3, seriesInfo, this));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr3);
				throw;
			}
			ptr2 = ptr4;
			if (ptr4 == null)
			{
				num = -2147024882;
			}
			if (num >= 0)
			{
				fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(m_feedUrl)))
				{
					try
					{
						int num2 = *(int*)ptr + 12;
						((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ISubscriptionViewerCallback*, int>)(int)(*(uint*)num2))((nint)ptr, ptr5, (ISubscriptionViewerCallback*)ptr4);
					}
					catch
					{
						//try-fault
						ptr5 = null;
						throw;
					}
				}
			}
		}
		if (ptr != null)
		{
			ISubscriptionViewer* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			ptr = null;
		}
		if (ptr2 != null)
		{
			VirtualSubscriptionEpisodeListProxy* intPtr2 = ptr2;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr2 + 8)))((nint)intPtr2);
		}
	}

	internal void AsyncOperationCompleted(int hrErrorCode)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Expected O, but got Unknown
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		DataProviderQueryStatus val = (DataProviderQueryStatus)3;
		if (hrErrorCode < 0)
		{
			val = (DataProviderQueryStatus)4;
		}
		Sort(null);
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredUpdateQueryStatus), (object)val);
	}

	internal void Sort(string sortProperty)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		Monitor.Enter(m_lock);
		try
		{
			if (sortProperty != null)
			{
				SetSortProperty(sortProperty);
			}
			if (m_items != null)
			{
				m_items.Sort(CompareItems);
			}
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredResetList), (object)null);
		}
		finally
		{
			Monitor.Exit(m_lock);
		}
	}

	protected override object OnRequestItem(int index)
	{
		return m_items[index];
	}

	private void UpdateQueryStatus(DataProviderQueryStatus status)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		m_owner.Status = status;
	}

	private void DeferredUpdateQueryStatus(object param)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		DataProviderQueryStatus status = (DataProviderQueryStatus)param;
		UpdateQueryStatus(status);
	}

	private void DeferredResetList(object param)
	{
		((VirtualList)this).Clear();
		List<SubscriptionDataProviderItem> items = m_items;
		if (items != null)
		{
			((VirtualList)this).Count = items.Count;
		}
	}

	private void SetSortProperty(string sort)
	{
		if (!string.IsNullOrEmpty(sort) && sort.Length > 1)
		{
			m_sortProperty = sort.Substring(1);
			int sortAsc = ((sort[0] == '+') ? 1 : 0);
			m_sortAsc = (byte)sortAsc != 0;
		}
		else
		{
			m_sortAsc = false;
			m_sortProperty = "ReleaseDate";
		}
	}

	private int CompareItems(SubscriptionDataProviderItem x, SubscriptionDataProviderItem y)
	{
		int num = 0;
		object property = x.GetProperty(m_sortProperty);
		object property2 = y.GetProperty(m_sortProperty);
		if (property is IComparable comparable)
		{
			num = comparable.CompareTo(property2);
			if (!m_sortAsc)
			{
				num = -num;
			}
		}
		return num;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EVirtualSubscriptionEpisodeList();
				return;
			}
			finally
			{
				((ModelItem)this).Dispose();
			}
		}
		((ModelItem)this).Finalize();
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
