using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class AlbumMetadata : IDisposable
{
	private unsafe IAlbumInfo* m_pAlbumInfo;

	private bool m_fDisposed;

	internal unsafe IAlbumInfo* AlbumInfo => m_pAlbumInfo;

	public unsafe bool ExactMatch
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				Unsafe.SkipInit(out int num2);
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 88)))((nint)pAlbumInfo, &num2);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::IsExactMatch failed", num);
				}
				bool flag = ((num2 != 0) ? true : false);
				result = flag;
			}
			return result;
		}
	}

	public unsafe uint TrackCount
	{
		get
		{
			uint result = 0u;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 64)))((nint)pAlbumInfo, &result);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::GetTrackCount failed", num);
				}
			}
			return result;
		}
	}

	public unsafe uint WMISTrackCount
	{
		get
		{
			uint result = 0u;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 84)))((nint)pAlbumInfo, &result);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::GetWMISTrackCount failed", num);
				}
			}
			return result;
		}
	}

	public unsafe int MediaId
	{
		get
		{
			int result = -1;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 80)))((nint)pAlbumInfo, &result);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::GetMediaId failed", num);
				}
			}
			return result;
		}
	}

	public unsafe string CoverUrl
	{
		get
		{
			string result = null;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 72)))((nint)pAlbumInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::GetCoverUrl failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value)))
				{
					try
					{
						ushort* ptr2 = global::_003CModule_003E.SysAllocString(ptr);
						if (ptr2 == null)
						{
							throw new COMException("SysAllocString failed", -2147024882);
						}
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 76)))((nint)pAlbumInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("IAlbumInfo::SetCoverUrl failed", num);
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
			try
			{
				return;
			}
			catch
			{
				//try-fault
				throw;
			}
		}
	}

	public unsafe int ReleaseYear
	{
		get
		{
			int result = -1;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 56)))((nint)pAlbumInfo, &result);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::GetReleaseYear failed", num);
				}
			}
			return result;
		}
		set
		{
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				switch (value)
				{
				case 0:
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
				case 17:
				case 18:
				case 19:
				case 20:
				case 21:
				case 22:
				case 23:
				case 24:
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:
					value += 2000;
					break;
				case 30:
				case 31:
				case 32:
				case 33:
				case 34:
				case 35:
				case 36:
				case 37:
				case 38:
				case 39:
				case 40:
				case 41:
				case 42:
				case 43:
				case 44:
				case 45:
				case 46:
				case 47:
				case 48:
				case 49:
				case 50:
				case 51:
				case 52:
				case 53:
				case 54:
				case 55:
				case 56:
				case 57:
				case 58:
				case 59:
				case 60:
				case 61:
				case 62:
				case 63:
				case 64:
				case 65:
				case 66:
				case 67:
				case 68:
				case 69:
				case 70:
				case 71:
				case 72:
				case 73:
				case 74:
				case 75:
				case 76:
				case 77:
				case 78:
				case 79:
				case 80:
				case 81:
				case 82:
				case 83:
				case 84:
				case 85:
				case 86:
				case 87:
				case 88:
				case 89:
				case 90:
				case 91:
				case 92:
				case 93:
				case 94:
				case 95:
				case 96:
				case 97:
				case 98:
				case 99:
					value += 1900;
					break;
				}
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 60)))((nint)pAlbumInfo, value);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::SetReleaseYear failed", num);
				}
			}
		}
	}

	public unsafe string AlbumArtistYomi
	{
		get
		{
			string result = null;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 40)))((nint)pAlbumInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::GetAlbumArtistYomi failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value)))
				{
					try
					{
						ushort* ptr2 = global::_003CModule_003E.SysAllocString(ptr);
						if (ptr2 == null)
						{
							throw new COMException("SysAllocString failed", -2147024882);
						}
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 44)))((nint)pAlbumInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("IAlbumInfo::SetAlbumArtistYomi failed", num);
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
			try
			{
				return;
			}
			catch
			{
				//try-fault
				throw;
			}
		}
	}

	public unsafe string AlbumArtist
	{
		get
		{
			string result = null;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 32)))((nint)pAlbumInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::GetAlbumArtist failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value)))
				{
					try
					{
						ushort* ptr2 = global::_003CModule_003E.SysAllocString(ptr);
						if (ptr2 == null)
						{
							throw new COMException("SysAllocString failed", -2147024882);
						}
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 36)))((nint)pAlbumInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("IAlbumInfo::SetAlbumArtist failed", num);
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
			try
			{
				return;
			}
			catch
			{
				//try-fault
				throw;
			}
		}
	}

	public unsafe string AlbumTitleYomi
	{
		get
		{
			string result = null;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 24)))((nint)pAlbumInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::GetTitleYomi failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value)))
				{
					try
					{
						ushort* ptr2 = global::_003CModule_003E.SysAllocString(ptr);
						if (ptr2 == null)
						{
							throw new COMException("SysAllocString failed", -2147024882);
						}
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 28)))((nint)pAlbumInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("IAlbumInfo::SetTitleYomi failed", num);
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
			try
			{
				return;
			}
			catch
			{
				//try-fault
				throw;
			}
		}
	}

	public unsafe string AlbumTitle
	{
		get
		{
			string result = null;
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 16)))((nint)pAlbumInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("IAlbumInfo::GetTitle failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value)))
				{
					try
					{
						ushort* ptr2 = global::_003CModule_003E.SysAllocString(ptr);
						if (ptr2 == null)
						{
							throw new COMException("SysAllocString failed", -2147024882);
						}
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 20)))((nint)pAlbumInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("IAlbumInfo::SetTitle failed", num);
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
			try
			{
				return;
			}
			catch
			{
				//try-fault
				throw;
			}
		}
	}

	public unsafe AlbumMetadata(IAlbumInfo* pAlbumInfo)
	{
		m_pAlbumInfo = pAlbumInfo;
		if (pAlbumInfo != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pAlbumInfo + 4)))((nint)pAlbumInfo);
		}
	}

	private void _007EAlbumMetadata()
	{
		_0021AlbumMetadata();
	}

	private unsafe void _0021AlbumMetadata()
	{
		if (!m_fDisposed)
		{
			IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
			if (pAlbumInfo != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pAlbumInfo + 8)))((nint)pAlbumInfo);
				m_pAlbumInfo = null;
			}
		}
	}

	public unsafe TrackMetadata GetTrack(uint index)
	{
		TrackMetadata result = null;
		IAlbumInfo* pAlbumInfo = m_pAlbumInfo;
		if (pAlbumInfo != null)
		{
			ITrackInfo* ptr = null;
			int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, ITrackInfo**, int>)(int)(*(uint*)(*(int*)pAlbumInfo + 68)))((nint)pAlbumInfo, index, &ptr);
			if (num < 0)
			{
				throw new COMException("IAlbumInfo::GetTrack failed", num);
			}
			result = new TrackMetadata(ptr);
			ITrackInfo* intPtr = ptr;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
		}
		return result;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021AlbumMetadata();
			return;
		}
		try
		{
			_0021AlbumMetadata();
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

	~AlbumMetadata()
	{
		Dispose(false);
	}
}
