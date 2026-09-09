using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace MicrosoftZuneLibrary;

public class TrackMetadata : INotifyPropertyChanged, IDisposable
{
	private unsafe ITrackInfo* m_pTrackInfo;

	private PropertyChangedEventHandler m_PropertyChanged;

	private bool m_fDisposed;

	internal unsafe ITrackInfo* TrackInfo => m_pTrackInfo;

	public unsafe int MediaId
	{
		get
		{
			int result = -1;
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pTrackInfo + 72)))((nint)pTrackInfo, &result);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::GetDiscNumber failed", num);
				}
			}
			return result;
		}
		set
		{
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null && MediaId != value)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pTrackInfo + 76)))((nint)pTrackInfo, value);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::SetMediaId failed", num);
				}
				FirePropertyChanged("MediaId");
			}
		}
	}

	public unsafe int DiscNumber
	{
		get
		{
			int result = -1;
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pTrackInfo + 60)))((nint)pTrackInfo, &result);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::GetDiscNumber failed", num);
				}
			}
			return result;
		}
		set
		{
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null && DiscNumber != value)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pTrackInfo + 64)))((nint)pTrackInfo, value);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::SetDiscNumber failed", num);
				}
				FirePropertyChanged("DiscNumber");
			}
		}
	}

	public unsafe int TrackNumber
	{
		get
		{
			int result = -1;
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)pTrackInfo + 52)))((nint)pTrackInfo, &result);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::GetTrackNumber failed", num);
				}
			}
			return result;
		}
		set
		{
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null && TrackNumber != value)
			{
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)pTrackInfo + 56)))((nint)pTrackInfo, value);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::SetTrackNumber failed", num);
				}
				FirePropertyChanged("TrackNumber");
			}
		}
	}

	public unsafe string Composer
	{
		get
		{
			string result = null;
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pTrackInfo + 12)))((nint)pTrackInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::GetComposer failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null && Composer.CompareTo(value) != 0)
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
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pTrackInfo + 16)))((nint)pTrackInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("ITrackInfo::SetComposer failed", num);
						}
						FirePropertyChanged("Composer");
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

	public unsafe string Conductor
	{
		get
		{
			string result = null;
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pTrackInfo + 20)))((nint)pTrackInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::GetConductor failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null && Conductor.CompareTo(value) != 0)
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
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pTrackInfo + 24)))((nint)pTrackInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("ITrackInfo::SetConductor failed", num);
						}
						FirePropertyChanged("Conductor");
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

	public unsafe string Genre
	{
		get
		{
			string result = null;
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pTrackInfo + 44)))((nint)pTrackInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::GetGenre failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null && Genre.CompareTo(value) != 0)
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
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pTrackInfo + 48)))((nint)pTrackInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("ITrackInfo::SetGenre failed", num);
						}
						FirePropertyChanged("Genre");
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

	public unsafe string TrackArtist
	{
		get
		{
			string result = null;
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pTrackInfo + 28)))((nint)pTrackInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::GetArtist failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null && TrackArtist.CompareTo(value) != 0)
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
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pTrackInfo + 32)))((nint)pTrackInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("ITrackInfo::SetArtist failed", num);
						}
						FirePropertyChanged("TrackArtist");
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

	public unsafe string TrackTitle
	{
		get
		{
			string result = null;
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null)
			{
				ushort* ptr = null;
				int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)pTrackInfo + 36)))((nint)pTrackInfo, &ptr);
				if (num < 0)
				{
					throw new COMException("ITrackInfo::GetTitle failed", num);
				}
				result = new string((char*)ptr);
				global::_003CModule_003E.SysFreeString(ptr);
			}
			return result;
		}
		set
		{
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null && TrackTitle.CompareTo(value) != 0)
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
						int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)(*(int*)pTrackInfo + 40)))((nint)pTrackInfo, ptr2);
						global::_003CModule_003E.SysFreeString(ptr2);
						if (num != 0)
						{
							throw new COMException("ITrackInfo::SetTitle failed", num);
						}
						FirePropertyChanged("TrackTitle");
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

	[SpecialName]
	public virtual event PropertyChangedEventHandler PropertyChanged
	{
		add
		{
			m_PropertyChanged = (PropertyChangedEventHandler)Delegate.Combine(m_PropertyChanged, value);
		}
		remove
		{
			m_PropertyChanged = (PropertyChangedEventHandler)Delegate.Remove(m_PropertyChanged, value);
		}
	}

	public unsafe TrackMetadata(ITrackInfo* pTrackInfo)
	{
		m_pTrackInfo = pTrackInfo;
		if (pTrackInfo != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pTrackInfo + 4)))((nint)pTrackInfo);
		}
	}

	private void _007ETrackMetadata()
	{
		_0021TrackMetadata();
	}

	private unsafe void _0021TrackMetadata()
	{
		if (!m_fDisposed)
		{
			ITrackInfo* pTrackInfo = m_pTrackInfo;
			if (pTrackInfo != null)
			{
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pTrackInfo + 8)))((nint)pTrackInfo);
				m_pTrackInfo = null;
			}
		}
	}

	private void FirePropertyChanged(string propName)
	{
		if (m_PropertyChanged != null)
		{
			m_PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021TrackMetadata();
			return;
		}
		try
		{
			_0021TrackMetadata();
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

	~TrackMetadata()
	{
		Dispose(false);
	}
}
