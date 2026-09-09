using System;
using System.Collections;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Iris;

namespace MicrosoftZuneLibrary;

[DefaultMember("Item")]
public class VirtualDatabaseList : VirtualList, IQueryListEvents, IDisposable
{
	protected int m_QueryRN;

	protected ZuneQueryList m_pQueryList;

	protected bool m_fInBulkEvents;

	protected bool m_fDisposed;

	protected ArrayList m_lNotifyData;

	protected int m_blockChangesFlags;

	protected bool m_fBlockChanges;

	protected bool m_fEndBulkArrivedDuringBlock;

	protected bool m_fItemsAdded;

	internal VirtualDatabaseList(ZuneQueryList pQueryList, [MarshalAs(UnmanagedType.U1)] bool enableSlowDataRequests)
		: base(enableSlowDataRequests)
	{
		try
		{
			m_pQueryList = pQueryList;
			m_lNotifyData = new ArrayList();
			m_fDisposed = false;
			m_fItemsAdded = false;
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)this).Dispose();
			throw;
		}
	}

	private void _007EVirtualDatabaseList()
	{
		m_fDisposed = true;
	}

	public virtual void ListInsert(int index)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		if (m_fInBulkEvents)
		{
			ListNotifyData listNotifyData = new ListNotifyData();
			listNotifyData.type = 0;
			listNotifyData.pos = index;
			m_lNotifyData.Add(listNotifyData);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredInsert), (object)index);
		}
	}

	public virtual void ListRemoveAt(int index)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		if (m_fInBulkEvents)
		{
			ListNotifyData listNotifyData = new ListNotifyData();
			listNotifyData.type = 1;
			listNotifyData.pos = index;
			m_lNotifyData.Add(listNotifyData);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredRemoveAt), (object)index);
		}
	}

	public virtual void ListModified(int index)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Expected O, but got Unknown
		if (m_fInBulkEvents)
		{
			ListNotifyData listNotifyData = new ListNotifyData();
			listNotifyData.type = 2;
			listNotifyData.pos = index;
			m_lNotifyData.Add(listNotifyData);
		}
		else
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(DeferredModified), (object)index);
		}
	}

	public virtual void ListBeginBulkEvents()
	{
		m_fInBulkEvents = true;
	}

	public virtual void ListEndBulkEvents()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Expected O, but got Unknown
		m_fInBulkEvents = false;
		m_fItemsAdded = false;
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredEndBulkEvents), (object)null);
	}

	public virtual void ListNotifyCount(uint count)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		Application.DeferredInvoke(new DeferredInvokeHandler(DeferredCountChange), (object)(int)count);
	}

	public void SetBlockChangesFlag(int flag, [MarshalAs(UnmanagedType.U1)] bool block)
	{
		if (block)
		{
			m_blockChangesFlags |= flag;
		}
		else
		{
			m_blockChangesFlags &= ~flag;
		}
		byte blockChanges = (byte)((m_blockChangesFlags != 0) ? 1 : 0);
		BlockListChanges(blockChanges != 0);
	}

	public int GetQueryRN()
	{
		return m_QueryRN;
	}

	protected virtual void InvalidateItem(int index)
	{
		((VirtualList)this).Modified(index);
	}

	private unsafe void DeferredEndBulkEvents(object P_0)
	{
		if (m_fDisposed)
		{
			m_lNotifyData.Clear();
			return;
		}
		if (m_fBlockChanges)
		{
			m_fEndBulkArrivedDuringBlock = true;
			return;
		}
		if (m_fItemsAdded)
		{
			m_lNotifyData.Clear();
			m_pQueryList.EndBulkEventsComplete(fAbandon: true);
			m_fItemsAdded = false;
			return;
		}
		bool flag = false;
		global::_003CModule_003E.PERFTRACE_COLLECTIONEVENT((_COLLECTION_EVENT)14, null);
		foreach (ListNotifyData lNotifyDatum in m_lNotifyData)
		{
			if (!m_fDisposed)
			{
				switch (lNotifyDatum.type)
				{
				case 2:
					DeferredModified(lNotifyDatum.pos);
					break;
				case 1:
					DeferredRemoveAt(lNotifyDatum.pos);
					flag = true;
					break;
				case 0:
					DeferredInsert(lNotifyDatum.pos);
					flag = true;
					break;
				}
				continue;
			}
			break;
		}
		global::_003CModule_003E.PERFTRACE_COLLECTIONEVENT((_COLLECTION_EVENT)15, null);
		m_lNotifyData.Clear();
		m_pQueryList.EndBulkEventsComplete(fAbandon: false);
		if (flag)
		{
			m_QueryRN++;
		}
	}

	private void DeferredInsert(object args)
	{
		if (!m_fDisposed && ((VirtualList)this).Count > (int)args)
		{
			((VirtualList)this).Insert((int)args);
		}
	}

	private void DeferredRemoveAt(object args)
	{
		if (!m_fDisposed && ((VirtualList)this).Count > 0 && ((VirtualList)this).Count > (int)args)
		{
			((VirtualList)this).RemoveAt((int)args);
		}
	}

	private void DeferredModified(object args)
	{
		if (!m_fDisposed && ((VirtualList)this).Count > (int)args && ((VirtualList)this).IsItemAvailable((int)args))
		{
			InvalidateItem((int)args);
		}
	}

	private void DeferredCountChange(object args)
	{
		int num = (int)args;
		if (!m_fDisposed)
		{
			if (num > ((VirtualList)this).Count)
			{
				((VirtualList)this).AddRange(num - ((VirtualList)this).Count);
			}
			else
			{
				((VirtualList)this).Count = num;
			}
		}
	}

	private void BlockListChanges([MarshalAs(UnmanagedType.U1)] bool blockChanges)
	{
		if (m_fBlockChanges != blockChanges)
		{
			m_fBlockChanges = blockChanges;
			if (!blockChanges && m_fEndBulkArrivedDuringBlock)
			{
				DeferredEndBulkEvents(null);
				m_fEndBulkArrivedDuringBlock = false;
			}
			m_pQueryList.ClientBusy(blockChanges);
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_007EVirtualDatabaseList();
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
