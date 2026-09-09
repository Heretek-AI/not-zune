using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Zune.Playlist;
using ZuneUI;

namespace Microsoft.Zune.QuickMix;

public class QuickMixSession : IDisposable
{
	private readonly CComPtrMgd_003CIQuickMixSession_003E m_spSession;

	public unsafe QuickMixSession(IQuickMixSession* pSession)
	{
		CComPtrMgd_003CIQuickMixSession_003E spSession = new CComPtrMgd_003CIQuickMixSession_003E();
		try
		{
			m_spSession = spSession;
			base._002Ector();
			m_spSession.op_Assign(pSession);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spSession).Dispose();
			throw;
		}
	}

	private void _007EQuickMixSession()
	{
		_0021QuickMixSession();
	}

	private unsafe void _0021QuickMixSession()
	{
		IQuickMixSession* p = m_spSession.p;
		if (p != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 40)))((nint)p);
			m_spSession.op_Assign(null);
		}
	}

	public unsafe EQuickMixType GetQuickMixType()
	{
		IQuickMixSession* p = m_spSession.p;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQuickMixType>)(int)(*(uint*)(*(int*)p + 12)))((nint)p);
	}

	public unsafe HRESULT SetQuickMixType(EQuickMixType eQuickMixType)
	{
		IQuickMixSession* p = m_spSession.p;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQuickMixType, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, eQuickMixType);
	}

	[return: MarshalAs(UnmanagedType.U1)]
	public unsafe bool GetQuickMixTypeAvailable(EQuickMixType eQuickMixType)
	{
		IQuickMixSession* p = m_spSession.p;
		return ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EQuickMixType, byte>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, eQuickMixType) != 0;
	}

	public unsafe HRESULT GetSimilarMedia(uint maxBatchTracks, TimeSpan maxBatchTimeout, SimilarMediaBatchHandler similarBatchHandler, BatchEndHandler batchEndHandler)
	{
		if (null == batchEndHandler)
		{
			return -2147467261;
		}
		QuickMixCallbackProxy* ptr = (QuickMixCallbackProxy*)global::_003CModule_003E.@new(24u);
		QuickMixCallbackProxy* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002E_007Bctor_007D(ptr, similarBatchHandler, batchEndHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		int num = ((ptr2 == null) ? (-2147024882) : 0);
		int num2 = num;
		if (num >= 0)
		{
			IQuickMixSession* p = m_spSession.p;
			int num3 = *(int*)p + 24;
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EPlaylistLimitType, uint, uint, IQuickMixSessionCallback*, int>)(int)(*(uint*)num3))((nint)p, (EPlaylistLimitType)2, maxBatchTracks, (uint)maxBatchTimeout.TotalMilliseconds, (IQuickMixSessionCallback*)ptr2);
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
		return num2;
	}

	public unsafe HRESULT GetSimilarMedia(TimeSpan maxBatchDuration, TimeSpan maxBatchTimeout, SimilarMediaBatchHandler similarBatchHandler, BatchEndHandler batchEndHandler)
	{
		if (null == batchEndHandler)
		{
			return -2147467261;
		}
		QuickMixCallbackProxy* ptr = (QuickMixCallbackProxy*)global::_003CModule_003E.@new(24u);
		QuickMixCallbackProxy* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002E_007Bctor_007D(ptr, similarBatchHandler, batchEndHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		int num = ((ptr2 == null) ? (-2147024882) : 0);
		int num2 = num;
		if (num >= 0)
		{
			IQuickMixSession* p = m_spSession.p;
			int num3 = *(int*)p + 24;
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EPlaylistLimitType, uint, uint, IQuickMixSessionCallback*, int>)(int)(*(uint*)num3))((nint)p, (EPlaylistLimitType)1, (uint)maxBatchDuration.TotalSeconds, (uint)maxBatchTimeout.TotalMilliseconds, (IQuickMixSessionCallback*)ptr2);
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
		return num2;
	}

	public unsafe HRESULT Refresh(TimeSpan maxBatchTimeout, [MarshalAs(UnmanagedType.U1)] bool reloadSettings, SimilarMediaBatchHandler similarBatchHandler, BatchEndHandler batchEndHandler)
	{
		if (null == batchEndHandler)
		{
			return -2147467261;
		}
		QuickMixCallbackProxy* ptr = (QuickMixCallbackProxy*)global::_003CModule_003E.@new(24u);
		QuickMixCallbackProxy* ptr2;
		try
		{
			ptr2 = ((ptr == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EQuickMix_002EQuickMixCallbackProxy_002E_007Bctor_007D(ptr, similarBatchHandler, batchEndHandler));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.delete(ptr);
			throw;
		}
		int num = ((ptr2 == null) ? (-2147024882) : 0);
		int num2 = num;
		if (num >= 0)
		{
			IQuickMixSession* p = m_spSession.p;
			int num3 = *(int*)p + 28;
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, byte, IQuickMixSessionCallback*, int>)(int)(*(uint*)num3))((nint)p, (uint)maxBatchTimeout.TotalMilliseconds, reloadSettings ? ((byte)1) : ((byte)0), (IQuickMixSessionCallback*)ptr2);
		}
		if (ptr2 != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)ptr2 + 8)))((nint)ptr2);
		}
		return num2;
	}

	public unsafe HRESULT GetPlaylistTitle(out string playlistTitle)
	{
		Unsafe.SkipInit(out WBSTRString wBSTRString);
		global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
		HRESULT result;
		try
		{
			IQuickMixSession* p = m_spSession.p;
			int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 32)))((nint)p, (ushort**)(&wBSTRString));
			if (num >= 0)
			{
				playlistTitle = new string((char*)(int)(*(uint*)(&wBSTRString)));
			}
			result = num;
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
			throw;
		}
		global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
		return result;
	}

	public unsafe HRESULT SaveAsPlaylist(string playlistTitle, CreatePlaylistOption createOption, out int playlistId)
	{
		int num = -1;
		fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(playlistTitle)))
		{
			int num2;
			EPlaylistCreateConflictAction ePlaylistCreateConflictAction;
			if (createOption != CreatePlaylistOption.None)
			{
				if (createOption != CreatePlaylistOption.RenameOnConflict)
				{
					if (createOption != CreatePlaylistOption.OverwriteOnConflict)
					{
						num2 = -2147418113;
						goto IL_0050;
					}
					ePlaylistCreateConflictAction = (EPlaylistCreateConflictAction)1;
				}
				else
				{
					ePlaylistCreateConflictAction = (EPlaylistCreateConflictAction)2;
				}
			}
			else
			{
				ePlaylistCreateConflictAction = (EPlaylistCreateConflictAction)0;
			}
			IQuickMixSession* p = m_spSession.p;
			int num3 = *(int*)p + 36;
			num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, EPlaylistCreateConflictAction, int*, int>)(int)(*(uint*)num3))((nint)p, ptr, ePlaylistCreateConflictAction, &num);
			if (num2 >= 0)
			{
				playlistId = num;
			}
			goto IL_0050;
			IL_0050:
			return num2;
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				_0021QuickMixSession();
				return;
			}
			finally
			{
				((IDisposable)m_spSession).Dispose();
			}
		}
		try
		{
			_0021QuickMixSession();
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

	~QuickMixSession()
	{
		Dispose(false);
	}
}
