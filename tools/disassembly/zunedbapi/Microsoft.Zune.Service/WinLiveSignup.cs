using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ZuneUI;

namespace Microsoft.Zune.Service;

public class WinLiveSignup : IDisposable
{
	private readonly CComPtrMgd_003CIWinLiveSignup_003E m_spWinLiveSignup;

	public WinLiveSignup()
	{
		CComPtrMgd_003CIWinLiveSignup_003E spWinLiveSignup = new CComPtrMgd_003CIWinLiveSignup_003E();
		try
		{
			m_spWinLiveSignup = spWinLiveSignup;
			base._002Ector();
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spWinLiveSignup).Dispose();
			throw;
		}
	}

	public unsafe HRESULT GetInformation(string locale, EHipType hipType, out WinLiveInformation information, out ServiceError serviceError)
	{
		HRESULT result = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CIWinLiveInformation_003E cComPtrNtv_003CIWinLiveInformation_003E);
		*(int*)(&cComPtrNtv_003CIWinLiveInformation_003E) = 0;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
			*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
			try
			{
				if (result.hr >= 0)
				{
					fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(locale)))
					{
						try
						{
							IWinLiveSignup* p = m_spWinLiveSignup.p;
							int num = *(int*)p + 16;
							result.hr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, global::EHipType, IWinLiveInformation**, IServiceError**, int>)(int)(*(uint*)num))((nint)p, ptr, (global::EHipType)hipType, (IWinLiveInformation**)(&cComPtrNtv_003CIWinLiveInformation_003E), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
						}
						catch
						{
							//try-fault
							ptr = null;
							throw;
						}
					}
				}
				if (result.hr >= 0)
				{
					information = new WinLiveInformation((IWinLiveInformation*)(int)(*(uint*)(&cComPtrNtv_003CIWinLiveInformation_003E)));
				}
				if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
				{
					serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIServiceError_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIServiceError_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002ERelease(&cComPtrNtv_003CIServiceError_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIWinLiveInformation_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIWinLiveInformation_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIWinLiveInformation_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIWinLiveInformation_003E_002ERelease(&cComPtrNtv_003CIWinLiveInformation_003E);
		return result;
	}

	public unsafe HRESULT CheckAvailableSigninName(string signinName, [MarshalAs(UnmanagedType.U1)] bool needSuggestedNames, string firstName, string lastName, out WinLiveAvailableInformation information, out ServiceError serviceError)
	{
		HRESULT result = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CIWinLiveAvailableInformation_003E cComPtrNtv_003CIWinLiveAvailableInformation_003E);
		*(int*)(&cComPtrNtv_003CIWinLiveAvailableInformation_003E) = 0;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
			*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
			try
			{
				if (result.IsSuccess)
				{
					fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(signinName)))
					{
						try
						{
							fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(firstName)))
							{
								try
								{
									fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(lastName)))
									{
										try
										{
											IWinLiveSignup* p = m_spWinLiveSignup.p;
											int num = *(int*)p + 20;
											result.hr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, byte, ushort*, ushort*, IWinLiveAvailableInformation**, IServiceError**, int>)(int)(*(uint*)num))((nint)p, ptr, needSuggestedNames ? ((byte)1) : ((byte)0), ptr2, ptr3, (IWinLiveAvailableInformation**)(&cComPtrNtv_003CIWinLiveAvailableInformation_003E), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
										}
										catch
										{
											//try-fault
											ptr3 = null;
											throw;
										}
									}
								}
								catch
								{
									//try-fault
									ptr2 = null;
									throw;
								}
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
				if (result.hr >= 0)
				{
					information = new WinLiveAvailableInformation((IWinLiveAvailableInformation*)(int)(*(uint*)(&cComPtrNtv_003CIWinLiveAvailableInformation_003E)));
				}
				if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
				{
					serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIServiceError_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIServiceError_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002ERelease(&cComPtrNtv_003CIServiceError_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIWinLiveAvailableInformation_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIWinLiveAvailableInformation_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIWinLiveAvailableInformation_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIWinLiveAvailableInformation_003E_002ERelease(&cComPtrNtv_003CIWinLiveAvailableInformation_003E);
		return result;
	}

	public unsafe HRESULT CreateAccount(string signinName, string signinPassword, string secretQuestion, string secretAnswer, string countryCode, string hipChallenge, string hipSolution, DateTime birthday, int termsOfServiceVersion, int languagePreference, out ServiceError serviceError)
	{
		HRESULT result = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
		global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002E_007Bctor_007D(&cComPtrNtv_003CIServiceError_003E);
		try
		{
			if (result.IsSuccess)
			{
				_SYSTEMTIME sYSTEMTIME = global::_003CModule_003E.DateTimeToSystemTime(birthday);
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(signinName)))
				{
					try
					{
						fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(signinPassword)))
						{
							try
							{
								fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(secretQuestion)))
								{
									try
									{
										fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(secretAnswer)))
										{
											try
											{
												fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(countryCode)))
												{
													try
													{
														fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(hipChallenge)))
														{
															try
															{
																fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(hipSolution)))
																{
																	try
																	{
																		IWinLiveSignup* ptr8 = m_spWinLiveSignup.op_MemberSelection();
																		int num = *(int*)ptr8 + 24;
																		result.hr = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, _SYSTEMTIME*, int, int, IServiceError**, int>)(int)(*(uint*)num))((nint)ptr8, ptr, ptr2, ptr3, ptr4, ptr5, ptr6, ptr7, &sYSTEMTIME, termsOfServiceVersion, languagePreference, global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002E_0026(&cComPtrNtv_003CIServiceError_003E));
																	}
																	catch
																	{
																		//try-fault
																		ptr7 = null;
																		throw;
																	}
																}
															}
															catch
															{
																//try-fault
																ptr6 = null;
																throw;
															}
														}
													}
													catch
													{
														//try-fault
														ptr5 = null;
														throw;
													}
												}
											}
											catch
											{
												//try-fault
												ptr4 = null;
												throw;
											}
										}
									}
									catch
									{
										//try-fault
										ptr3 = null;
										throw;
									}
								}
							}
							catch
							{
								//try-fault
								ptr2 = null;
								throw;
							}
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
			if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
			{
				serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIServiceError_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIServiceError_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIServiceError_003E_002ERelease(&cComPtrNtv_003CIServiceError_003E);
		return result;
	}

	private unsafe HRESULT CreateComObject()
	{
		int num = 0;
		if (m_spWinLiveSignup.p == null)
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIService_003E cComPtrNtv_003CIService_003E);
			*(int*)(&cComPtrNtv_003CIService_003E) = 0;
			try
			{
				num = global::_003CModule_003E.GetSingleton((_GUID)global::_003CModule_003E._GUID_bb2d1edd_1bd5_4be1_8d38_36d4f0849911, (void**)(&cComPtrNtv_003CIService_003E));
				Unsafe.SkipInit(out CComPtrNtv_003CIUnknown_003E cComPtrNtv_003CIUnknown_003E);
				*(int*)(&cComPtrNtv_003CIUnknown_003E) = 0;
				try
				{
					if (num >= 0)
					{
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IUnknown**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 48)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E)), (IUnknown**)(&cComPtrNtv_003CIUnknown_003E));
					}
					Unsafe.SkipInit(out CComPtrNtv_003CIWinLiveSignup_003E cComPtrNtv_003CIWinLiveSignup_003E);
					*(int*)(&cComPtrNtv_003CIWinLiveSignup_003E) = 0;
					try
					{
						if (num >= 0)
						{
							num = global::_003CModule_003E.IUnknown_002EQueryInterface_003Cstruct_0020IWinLiveSignup_003E((IUnknown*)(int)(*(uint*)(&cComPtrNtv_003CIUnknown_003E)), (IWinLiveSignup**)(&cComPtrNtv_003CIWinLiveSignup_003E));
							if (num >= 0)
							{
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIWinLiveSignup_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIWinLiveSignup_003E)));
								if (num >= 0)
								{
									m_spWinLiveSignup.op_Assign((IWinLiveSignup*)(int)(*(uint*)(&cComPtrNtv_003CIWinLiveSignup_003E)));
								}
							}
						}
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIWinLiveSignup_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIWinLiveSignup_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIWinLiveSignup_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CIWinLiveSignup_003E_002ERelease(&cComPtrNtv_003CIWinLiveSignup_003E);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIUnknown_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIUnknown_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIUnknown_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIUnknown_003E_002ERelease(&cComPtrNtv_003CIUnknown_003E);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIService_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIService_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIService_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIService_003E_002ERelease(&cComPtrNtv_003CIService_003E);
		}
		return new HRESULT(num);
	}

	public void _007EWinLiveSignup()
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
				((IDisposable)m_spWinLiveSignup).Dispose();
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
