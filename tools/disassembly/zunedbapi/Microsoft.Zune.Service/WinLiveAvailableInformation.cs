using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Service;

public class WinLiveAvailableInformation : IDisposable
{
	private readonly CComPtrMgd_003CIWinLiveAvailableInformation_003E m_spWinLiveAvailableInformation;

	private string m_signinName;

	private IList m_suggestedNames;

	public unsafe IList SuggestedNames
	{
		get
		{
			if (m_suggestedNames == null)
			{
				IWinLiveAvailableInformation* p = m_spWinLiveAvailableInformation.p;
				if (p != null)
				{
					int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 24)))((nint)p);
					m_suggestedNames = new ArrayList(num);
					int num2 = 0;
					if (0 < num)
					{
						do
						{
							ushort* ptr = null;
							IWinLiveAvailableInformation* p2 = m_spWinLiveAvailableInformation.p;
							if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ushort**, int>)(int)(*(uint*)(*(int*)p2 + 28)))((nint)p2, num2, &ptr) >= 0)
							{
								m_suggestedNames.Add(new string((char*)ptr));
							}
							global::_003CModule_003E.SysFreeString(ptr);
							num2++;
						}
						while (num2 < num);
					}
				}
			}
			return m_suggestedNames;
		}
	}

	public unsafe bool Available
	{
		[return: MarshalAs(UnmanagedType.U1)]
		get
		{
			bool result = false;
			IWinLiveAvailableInformation* p = m_spWinLiveAvailableInformation.p;
			if (p != null)
			{
				result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte>)(int)(*(uint*)(*(int*)p + 20)))((nint)p) != 0;
			}
			return result;
		}
	}

	public unsafe string SigninName
	{
		get
		{
			if (m_signinName == null)
			{
				CComPtrMgd_003CIWinLiveAvailableInformation_003E spWinLiveAvailableInformation = m_spWinLiveAvailableInformation;
				if (spWinLiveAvailableInformation.p != null)
				{
					ushort* ptr = null;
					IWinLiveAvailableInformation* p = spWinLiveAvailableInformation.p;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, &ptr) >= 0)
					{
						m_signinName = new string((char*)ptr);
					}
					global::_003CModule_003E.SysFreeString(ptr);
				}
			}
			return m_signinName;
		}
	}

	internal unsafe WinLiveAvailableInformation(IWinLiveAvailableInformation* pWinLiveAvailableInformation)
	{
		CComPtrMgd_003CIWinLiveAvailableInformation_003E spWinLiveAvailableInformation = new CComPtrMgd_003CIWinLiveAvailableInformation_003E();
		try
		{
			m_spWinLiveAvailableInformation = spWinLiveAvailableInformation;
			base._002Ector();
			m_spWinLiveAvailableInformation.op_Assign(pWinLiveAvailableInformation);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spWinLiveAvailableInformation).Dispose();
			throw;
		}
	}

	public void _007EWinLiveAvailableInformation()
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
				((IDisposable)m_spWinLiveAvailableInformation).Dispose();
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
