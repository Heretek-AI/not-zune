using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ZuneUI;

namespace Microsoft.Zune.Service;

public class ServiceError : IDisposable
{
	private readonly CComPtrMgd_003CIServiceError_003E m_spServiceError;

	private IList<PropertyError> m_propertyErrors;

	public unsafe IList<PropertyError> PropertyErrors
	{
		get
		{
			if (m_propertyErrors == null)
			{
				IServiceError* p = m_spServiceError.p;
				if (p != null)
				{
					int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p);
					m_propertyErrors = new List<PropertyError>(num);
					int num2 = 0;
					if (0 < num)
					{
						do
						{
							int num3 = 0;
							ushort* ptr = null;
							IServiceError* p2 = m_spServiceError.p;
							if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ushort**, int*, int>)(int)(*(uint*)(*(int*)p2 + 20)))((nint)p2, num2, &ptr, &num3) >= 0)
							{
								PropertyError propertyError = new PropertyError();
								propertyError.Name = new string((char*)ptr);
								HRESULT hr = num3;
								propertyError.Hr = hr;
								m_propertyErrors.Add(propertyError);
							}
							global::_003CModule_003E.SysFreeString(ptr);
							num2++;
						}
						while (num2 < num);
					}
				}
			}
			return m_propertyErrors;
		}
	}

	public unsafe HRESULT RootError
	{
		get
		{
			HRESULT s_OK = HRESULT._S_OK;
			IServiceError* p = m_spServiceError.p;
			if (p != null)
			{
				IServiceError* ptr = p;
				s_OK.hr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)ptr + 12)))((nint)ptr);
			}
			return s_OK;
		}
	}

	internal unsafe ServiceError(IServiceError* pServiceError)
	{
		CComPtrMgd_003CIServiceError_003E spServiceError = new CComPtrMgd_003CIServiceError_003E();
		try
		{
			m_spServiceError = spServiceError;
			base._002Ector();
			m_spServiceError.op_Assign(pServiceError);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spServiceError).Dispose();
			throw;
		}
	}

	public void _007EServiceError()
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
				((IDisposable)m_spServiceError).Dispose();
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
