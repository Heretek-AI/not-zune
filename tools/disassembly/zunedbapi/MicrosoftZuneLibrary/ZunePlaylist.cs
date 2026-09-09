using System;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class ZunePlaylist : IDisposable
{
	private unsafe IPlaylist* m_pPlaylist;

	private bool _disposed;

	internal unsafe IPlaylist* Playlist => m_pPlaylist;

	private void _007EZunePlaylist()
	{
		_0021ZunePlaylist();
	}

	private unsafe void _0021ZunePlaylist()
	{
		if (!_disposed)
		{
			IPlaylist* pPlaylist = m_pPlaylist;
			if (pPlaylist != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pPlaylist + 8)))((nint)pPlaylist);
				m_pPlaylist = null;
			}
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_007EZunePlaylist();
			return;
		}
		try
		{
			_0021ZunePlaylist();
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

	~ZunePlaylist()
	{
		Dispose(false);
	}
}
