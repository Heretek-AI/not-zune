using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Util;

public class ThumbBarButton : IDisposable
{
	private readonly CComPtrMgd_003CIThumbBarButton_003E m_spButton;

	public unsafe bool ShowBackground
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			int num = 0;
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)p + 52)))((nint)p, &num);
			return num == 1;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			int num = (value ? 1 : 0);
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)p + 56)))((nint)p, num);
		}
	}

	public unsafe bool IsHidden
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			int num = 0;
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)p + 44)))((nint)p, &num);
			return num == 1;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			int num = (value ? 1 : 0);
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)p + 48)))((nint)p, num);
		}
	}

	public unsafe bool IsEnabled
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			int num = 0;
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int*, int>)(int)(*(uint*)(*(int*)p + 36)))((nint)p, &num);
			return num == 1;
		}
		[param: MarshalAs(UnmanagedType.U1)]
		set
		{
			int num = (value ? 1 : 0);
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, int>)(int)(*(uint*)(*(int*)p + 40)))((nint)p, num);
		}
	}

	public unsafe ThumbBarButtonIcons Icon
	{
		get
		{
			EThumbBarButtonIcons result = (EThumbBarButtonIcons)10;
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EThumbBarButtonIcons*, int>)(int)(*(uint*)(*(int*)p + 28)))((nint)p, &result);
			return (ThumbBarButtonIcons)result;
		}
		set
		{
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EThumbBarButtonIcons, int>)(int)(*(uint*)(*(int*)p + 32)))((nint)p, (EThumbBarButtonIcons)value);
		}
	}

	public unsafe string Tooltip
	{
		get
		{
			ushort* ptr = null;
			object result = null;
			IThumbBarButton* p = m_spButton.p;
			if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, &ptr) >= 0)
			{
				result = new string((char*)ptr);
			}
			global::_003CModule_003E.SysFreeString(ptr);
			return (string)result;
		}
		set
		{
			fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(value)))
			{
				IThumbBarButton* p = m_spButton.p;
				int num = *(int*)p + 24;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, int>)(int)(*(uint*)num))((nint)p, ptr);
			}
		}
	}

	public unsafe uint UniqueID
	{
		get
		{
			uint result = 0u;
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint*, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &result);
			return result;
		}
		set
		{
			IThumbBarButton* p = m_spButton.p;
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, value);
		}
	}

	internal unsafe ThumbBarButton(IThumbBarButton* pButton)
	{
		CComPtrMgd_003CIThumbBarButton_003E spButton = new CComPtrMgd_003CIThumbBarButton_003E();
		try
		{
			m_spButton = spButton;
			base._002Ector();
			m_spButton.op_Assign(pButton);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spButton).Dispose();
			throw;
		}
	}

	public void _007EThumbBarButton()
	{
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			try
			{
				return;
			}
			finally
			{
				((IDisposable)m_spButton).Dispose();
			}
		}
		base.Finalize();
	}

	public virtual sealed void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
}
