using System;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Util;

public class RadioPlaylist : IDisposable
{
	private unsafe IRadioPlaylist* m_pRadioPlaylist;

	public unsafe RadioPlaylist(IRadioPlaylist* pRadioPlaylist)
	{
		m_pRadioPlaylist = pRadioPlaylist;
		base._002Ector();
		IRadioPlaylist* pRadioPlaylist2 = m_pRadioPlaylist;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pRadioPlaylist2 + 4)))((nint)pRadioPlaylist2);
	}

	private void _007ERadioPlaylist()
	{
		_0021RadioPlaylist();
	}

	private unsafe void _0021RadioPlaylist()
	{
		IRadioPlaylist* pRadioPlaylist = m_pRadioPlaylist;
		if (null != pRadioPlaylist)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pRadioPlaylist + 8)))((nint)pRadioPlaylist);
			m_pRadioPlaylist = null;
		}
	}

	public unsafe string GetNextUri()
	{
		object result = null;
		ushort* ptr = null;
		IRadioPlaylist* pRadioPlaylist = m_pRadioPlaylist;
		if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pRadioPlaylist + 12)))((nint)pRadioPlaylist, &ptr) >= 0)
		{
			result = new string((char*)ptr);
		}
		global::_003CModule_003E.SysFreeString(ptr);
		return (string)result;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021RadioPlaylist();
			return;
		}
		try
		{
			_0021RadioPlaylist();
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

	~RadioPlaylist()
	{
		Dispose(false);
	}
}
