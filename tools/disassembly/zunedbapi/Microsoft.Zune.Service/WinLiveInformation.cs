using System;
using System.Collections;
using System.Runtime.InteropServices;
using MicrosoftZuneLibrary;

namespace Microsoft.Zune.Service;

public class WinLiveInformation : IDisposable
{
	private readonly CComPtrMgd_003CIWinLiveInformation_003E m_spWinLiveInformation;

	private string m_termsOfServiceUrl;

	private string m_privacyUrl;

	private string m_hipChallenge;

	private SafeBitmapWithData m_hipImage;

	private IList m_domains;

	public unsafe IList Domains
	{
		get
		{
			if (m_domains == null)
			{
				IWinLiveInformation* p = m_spWinLiveInformation.p;
				if (p != null)
				{
					int num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 36)))((nint)p);
					m_domains = new ArrayList(num);
					int num2 = 0;
					if (0 < num)
					{
						do
						{
							ushort* ptr = null;
							IWinLiveInformation* p2 = m_spWinLiveInformation.p;
							if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ushort**, int>)(int)(*(uint*)(*(int*)p2 + 40)))((nint)p2, num2, &ptr) >= 0)
							{
								m_domains.Add(new string((char*)ptr));
							}
							global::_003CModule_003E.SysFreeString(ptr);
							num2++;
						}
						while (num2 < num);
					}
				}
			}
			return m_domains;
		}
	}

	public unsafe int HipLength
	{
		get
		{
			int result = 0;
			IWinLiveInformation* p = m_spWinLiveInformation.p;
			if (p != null)
			{
				result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 32)))((nint)p);
			}
			return result;
		}
	}

	public unsafe SafeBitmapWithData HipImage
	{
		get
		{
			if (m_hipImage == null)
			{
				CComPtrMgd_003CIWinLiveInformation_003E spWinLiveInformation = m_spWinLiveInformation;
				if (spWinLiveInformation.p != null)
				{
					int num = 0;
					IWinLiveInformation* p = spWinLiveInformation.p;
					HBITMAP__* ptr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, HBITMAP__*>)(int)(*(uint*)(*(int*)p + 28)))((nint)p);
					if (ptr == null)
					{
						num = -2147467259;
					}
					HBITMAP__* ptr2 = null;
					void* pData = null;
					if (num >= 0 && global::_003CModule_003E.ZuneLibraryExports_002ECopyThumbnailBitmapData(ptr, &ptr2, &pData) >= 0)
					{
						try
						{
							m_hipImage = new SafeBitmapWithData(pData, ptr2);
						}
						catch (ApplicationException)
						{
							m_hipImage = null;
							if (ptr2 != null)
							{
								global::_003CModule_003E.DeleteObject(ptr2);
							}
						}
					}
				}
			}
			return m_hipImage;
		}
	}

	public unsafe string HipChallenge
	{
		get
		{
			if (m_hipChallenge == null)
			{
				CComPtrMgd_003CIWinLiveInformation_003E spWinLiveInformation = m_spWinLiveInformation;
				if (spWinLiveInformation.p != null)
				{
					ushort* ptr = null;
					IWinLiveInformation* p = spWinLiveInformation.p;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 24)))((nint)p, &ptr) >= 0)
					{
						m_hipChallenge = new string((char*)ptr);
					}
					global::_003CModule_003E.SysFreeString(ptr);
				}
			}
			return m_hipChallenge;
		}
	}

	public unsafe string PrivacyUrl
	{
		get
		{
			if (m_privacyUrl == null)
			{
				CComPtrMgd_003CIWinLiveInformation_003E spWinLiveInformation = m_spWinLiveInformation;
				if (spWinLiveInformation.p != null)
				{
					ushort* ptr = null;
					IWinLiveInformation* p = spWinLiveInformation.p;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, &ptr) >= 0)
					{
						m_privacyUrl = new string((char*)ptr);
					}
					global::_003CModule_003E.SysFreeString(ptr);
				}
			}
			return m_privacyUrl;
		}
	}

	public unsafe int TermsOfServiceVersion
	{
		get
		{
			int result = 0;
			IWinLiveInformation* p = m_spWinLiveInformation.p;
			if (p != null)
			{
				result = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p);
			}
			return result;
		}
	}

	public unsafe string TermsOfServiceUrl
	{
		get
		{
			if (m_termsOfServiceUrl == null)
			{
				CComPtrMgd_003CIWinLiveInformation_003E spWinLiveInformation = m_spWinLiveInformation;
				if (spWinLiveInformation.p != null)
				{
					ushort* ptr = null;
					IWinLiveInformation* p = spWinLiveInformation.p;
					if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, int>)(int)(*(uint*)(*(int*)p + 12)))((nint)p, &ptr) >= 0)
					{
						m_termsOfServiceUrl = new string((char*)ptr);
					}
					global::_003CModule_003E.SysFreeString(ptr);
				}
			}
			return m_termsOfServiceUrl;
		}
	}

	internal unsafe WinLiveInformation(IWinLiveInformation* pWinLiveInformation)
	{
		CComPtrMgd_003CIWinLiveInformation_003E spWinLiveInformation = new CComPtrMgd_003CIWinLiveInformation_003E();
		try
		{
			m_spWinLiveInformation = spWinLiveInformation;
			base._002Ector();
			m_spWinLiveInformation.op_Assign(pWinLiveInformation);
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spWinLiveInformation).Dispose();
			throw;
		}
	}

	public void _007EWinLiveInformation()
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
				((IDisposable)m_spWinLiveInformation).Dispose();
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
