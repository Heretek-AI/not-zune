using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ZuneUI;

namespace Microsoft.Zune.Service;

public class AccountManagement : IDisposable
{
	private readonly CComPtrMgd_003CIAccountManagement_003E m_spAccountManagement;

	public AccountManagement()
	{
		CComPtrMgd_003CIAccountManagement_003E spAccountManagement = new CComPtrMgd_003CIAccountManagement_003E();
		try
		{
			m_spAccountManagement = spAccountManagement;
			base._002Ector();
			return;
		}
		catch
		{
			//try-fault
			((IDisposable)m_spAccountManagement).Dispose();
			throw;
		}
	}

	public unsafe HRESULT CreateAccount(PassportIdentity passportIdentity, string zuneTag, string locale, DateTime birthday, string firstName, string lastName, string email, Address address, AccountSettings accountSettings, PassportIdentity parentPassportIdentity, CreditCard parentCreditCard, out ServiceError serviceError)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CINewsletterSettings_003E cComPtrNtv_003CINewsletterSettings_003E);
		global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002E_007Bctor_007D(&cComPtrNtv_003CINewsletterSettings_003E);
		HRESULT result;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIPrivacySettings_003E cComPtrNtv_003CIPrivacySettings_003E);
			global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002E_007Bctor_007D(&cComPtrNtv_003CIPrivacySettings_003E);
			try
			{
				if (num >= 0 && accountSettings != null)
				{
					num = CreateAccountSettings(accountSettings, global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002E_0026(&cComPtrNtv_003CINewsletterSettings_003E), global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002E_0026(&cComPtrNtv_003CIPrivacySettings_003E));
				}
				Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E);
				global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bctor_007D(&cComPtrNtv_003CIPassportIdentity_003E);
				try
				{
					if (num >= 0 && passportIdentity != null)
					{
						num = passportIdentity.GetComPointer(global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_0026(&cComPtrNtv_003CIPassportIdentity_003E));
					}
					Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E2);
					global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bctor_007D(&cComPtrNtv_003CIPassportIdentity_003E2);
					try
					{
						if (num >= 0 && parentPassportIdentity != null)
						{
							num = parentPassportIdentity.GetComPointer((IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E2));
						}
						Unsafe.SkipInit(out CComPtrNtv_003CICreditCard_003E cComPtrNtv_003CICreditCard_003E);
						*(int*)(&cComPtrNtv_003CICreditCard_003E) = 0;
						try
						{
							if (num >= 0 && parentCreditCard != null)
							{
								num = CreateCreditCard(parentCreditCard, (ICreditCard**)(&cComPtrNtv_003CICreditCard_003E));
							}
							Unsafe.SkipInit(out CComPtrNtv_003CIAddress_003E cComPtrNtv_003CIAddress_003E);
							*(int*)(&cComPtrNtv_003CIAddress_003E) = 0;
							try
							{
								if (num >= 0 && address != null)
								{
									num = CreateAddress(address, (IAddress**)(&cComPtrNtv_003CIAddress_003E));
								}
								Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
								*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
								try
								{
									if (num >= 0)
									{
										_SYSTEMTIME sYSTEMTIME = global::_003CModule_003E.DateTimeToSystemTime(birthday);
										fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(firstName)))
										{
											try
											{
												fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(lastName)))
												{
													try
													{
														fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(zuneTag)))
														{
															try
															{
																fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(email)))
																{
																	try
																	{
																		fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(locale)))
																		{
																			try
																			{
																				IAccountManagement* p = m_spAccountManagement.p;
																				int num2 = *(int*)p + 36;
																				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IPassportIdentity*, ushort*, ushort*, _SYSTEMTIME*, ushort*, ushort*, ushort*, IAddress*, INewsletterSettings*, IPrivacySettings*, IPassportIdentity*, ICreditCard*, IServiceError**, int>)(int)(*(uint*)num2))((nint)p, (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E)), ptr3, ptr5, &sYSTEMTIME, ptr, ptr2, ptr4, (IAddress*)(int)(*(uint*)(&cComPtrNtv_003CIAddress_003E)), (INewsletterSettings*)(int)(*(uint*)(&cComPtrNtv_003CINewsletterSettings_003E)), (IPrivacySettings*)(int)(*(uint*)(&cComPtrNtv_003CIPrivacySettings_003E)), (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E2)), (ICreditCard*)(int)(*(uint*)(&cComPtrNtv_003CICreditCard_003E)), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
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
									result = new HRESULT(num);
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
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAddress_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAddress_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAddress_003E);
								throw;
							}
							global::_003CModule_003E.CComPtrNtv_003CIAddress_003E_002ERelease(&cComPtrNtv_003CIAddress_003E);
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CICreditCard_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CICreditCard_003E_002E_007Bdtor_007D), &cComPtrNtv_003CICreditCard_003E);
							throw;
						}
						global::_003CModule_003E.CComPtrNtv_003CICreditCard_003E_002ERelease(&cComPtrNtv_003CICreditCard_003E);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E2);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPrivacySettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPrivacySettings_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002ERelease(&cComPtrNtv_003CIPrivacySettings_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CINewsletterSettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CINewsletterSettings_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002ERelease(&cComPtrNtv_003CINewsletterSettings_003E);
		return result;
	}

	public unsafe HRESULT ReserveZuneTag(string zuneTag, string countryCode, out IList suggestedNames, out ServiceError serviceError)
	{
		int num = CreateComObject();
		suggestedNames = null;
		Unsafe.SkipInit(out CComPtrNtv_003CIAvailableZuneTagInformation_003E cComPtrNtv_003CIAvailableZuneTagInformation_003E);
		*(int*)(&cComPtrNtv_003CIAvailableZuneTagInformation_003E) = 0;
		HRESULT result;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
			*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
			try
			{
				if (num >= 0)
				{
					fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(zuneTag)))
					{
						try
						{
							fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(countryCode)))
							{
								try
								{
									IAccountManagement* p = m_spAccountManagement.p;
									int num2 = *(int*)p + 40;
									num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, IAvailableZuneTagInformation**, IServiceError**, int>)(int)(*(uint*)num2))((nint)p, ptr, ptr2, (IAvailableZuneTagInformation**)(&cComPtrNtv_003CIAvailableZuneTagInformation_003E), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
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
				if (num == -1056857549)
				{
					int num3 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIAvailableZuneTagInformation_003E)) + 24)))((IntPtr)(*(int*)(&cComPtrNtv_003CIAvailableZuneTagInformation_003E)));
					suggestedNames = new ArrayList(num3);
					int num4 = 0;
					if (0 < num3)
					{
						do
						{
							ushort* ptr3 = null;
							num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ushort**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIAvailableZuneTagInformation_003E)) + 28)))((IntPtr)(*(int*)(&cComPtrNtv_003CIAvailableZuneTagInformation_003E)), num4, &ptr3);
							if (num >= 0)
							{
								suggestedNames.Add(new string((char*)ptr3));
							}
							global::_003CModule_003E.SysFreeString(ptr3);
							if (num < 0)
							{
								break;
							}
							num4++;
						}
						while (num4 < num3);
					}
				}
				if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
				{
					serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
				}
				result = num;
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
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAvailableZuneTagInformation_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAvailableZuneTagInformation_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAvailableZuneTagInformation_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIAvailableZuneTagInformation_003E_002ERelease(&cComPtrNtv_003CIAvailableZuneTagInformation_003E);
		return result;
	}

	public unsafe HRESULT ValidateCreditCard(PassportIdentity parentPassportIdentity, CreditCard creditCard, out ServiceError serviceError)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CICreditCard_003E cComPtrNtv_003CICreditCard_003E);
		*(int*)(&cComPtrNtv_003CICreditCard_003E) = 0;
		HRESULT result;
		try
		{
			if (num >= 0 && creditCard != null)
			{
				num = CreateCreditCard(creditCard, (ICreditCard**)(&cComPtrNtv_003CICreditCard_003E));
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E);
			*(int*)(&cComPtrNtv_003CIPassportIdentity_003E) = 0;
			try
			{
				if (num >= 0 && parentPassportIdentity != null)
				{
					num = parentPassportIdentity.GetComPointer((IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E));
				}
				Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
				*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
				try
				{
					if (num >= 0)
					{
						IAccountManagement* p = m_spAccountManagement.p;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IPassportIdentity*, ICreditCard*, IServiceError**, int>)(int)(*(uint*)(*(int*)p + 44)))((nint)p, (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E)), (ICreditCard*)(int)(*(uint*)(&cComPtrNtv_003CICreditCard_003E)), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
					}
					if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
					{
						serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
					}
					result = new HRESULT(num);
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
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CICreditCard_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CICreditCard_003E_002E_007Bdtor_007D), &cComPtrNtv_003CICreditCard_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CICreditCard_003E_002ERelease(&cComPtrNtv_003CICreditCard_003E);
		return result;
	}

	public unsafe HRESULT GetAccount(PassportIdentity passportIdentity, GetAccountCompleteCallback onSuccess, AccountManagementErrorCallback onError)
	{
		int num = CreateComObject();
		GetAccountCallbackWrapper* ptr = null;
		if (num >= 0)
		{
			GetAccountCallbackWrapper* ptr2 = (GetAccountCallbackWrapper*)global::_003CModule_003E.@new(16u);
			GetAccountCallbackWrapper* ptr3;
			try
			{
				ptr3 = ((ptr2 == null) ? null : global::_003CModule_003E.Microsoft_002EZune_002EService_002EGetAccountCallbackWrapper_002E_007Bctor_007D(ptr2, onSuccess, onError));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.delete(ptr2);
				throw;
			}
			ptr = ptr3;
			if (ptr3 == null)
			{
				num = -2147024882;
			}
		}
		Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E);
		*(int*)(&cComPtrNtv_003CIPassportIdentity_003E) = 0;
		HRESULT result;
		try
		{
			if (num >= 0)
			{
				if (passportIdentity != null)
				{
					num = passportIdentity.GetComPointer((IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E));
				}
				if (num >= 0)
				{
					IAccountManagement* p = m_spAccountManagement.p;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IPassportIdentity*, IGetAccountCallback*, int>)(int)(*(uint*)(*(int*)p + 48)))((nint)p, (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E)), (IGetAccountCallback*)ptr);
				}
			}
			if (ptr != null)
			{
				GetAccountCallbackWrapper* intPtr = ptr;
				((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)intPtr + 8)))((nint)intPtr);
			}
			result = new HRESULT(num);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E);
		return result;
	}

	public unsafe HRESULT GetAccount(PassportIdentity passportIdentity, out AccountUser accountUser, out ServiceError serviceError)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E);
		*(int*)(&cComPtrNtv_003CIPassportIdentity_003E) = 0;
		HRESULT result;
		try
		{
			if (num >= 0 && passportIdentity != null)
			{
				num = passportIdentity.GetComPointer((IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E));
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIAccountUser_003E cComPtrNtv_003CIAccountUser_003E);
			*(int*)(&cComPtrNtv_003CIAccountUser_003E) = 0;
			try
			{
				Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
				*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
				try
				{
					if (num >= 0)
					{
						IAccountManagement* p = m_spAccountManagement.p;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IPassportIdentity*, IAccountUser**, IServiceError**, int>)(int)(*(uint*)(*(int*)p + 52)))((nint)p, (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E)), (IAccountUser**)(&cComPtrNtv_003CIAccountUser_003E), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
					}
					if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
					{
						serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
					}
					if (*(int*)(&cComPtrNtv_003CIAccountUser_003E) != 0)
					{
						accountUser = new AccountUser((IAccountUser*)(int)(*(uint*)(&cComPtrNtv_003CIAccountUser_003E)));
					}
					result = new HRESULT(num);
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
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAccountUser_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAccountUser_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAccountUser_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIAccountUser_003E_002ERelease(&cComPtrNtv_003CIAccountUser_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E);
		return result;
	}

	public unsafe HRESULT SetAccount(PassportIdentity passportIdentity, AccountUser accountUser, out ServiceError serviceError)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E);
		*(int*)(&cComPtrNtv_003CIPassportIdentity_003E) = 0;
		HRESULT result;
		try
		{
			if (num >= 0 && passportIdentity != null)
			{
				num = passportIdentity.GetComPointer((IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E));
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIAccountUser_003E cComPtrNtv_003CIAccountUser_003E);
			*(int*)(&cComPtrNtv_003CIAccountUser_003E) = 0;
			try
			{
				if (num >= 0 && accountUser != null)
				{
					num = CreateAccountUser(accountUser, (IAccountUser**)(&cComPtrNtv_003CIAccountUser_003E));
				}
				Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
				*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
				try
				{
					if (num >= 0)
					{
						IAccountManagement* p = m_spAccountManagement.p;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IPassportIdentity*, IAccountUser*, IServiceError**, int>)(int)(*(uint*)(*(int*)p + 56)))((nint)p, (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E)), (IAccountUser*)(int)(*(uint*)(&cComPtrNtv_003CIAccountUser_003E)), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
					}
					if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
					{
						serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
					}
					result = new HRESULT(num);
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
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAccountUser_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAccountUser_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAccountUser_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIAccountUser_003E_002ERelease(&cComPtrNtv_003CIAccountUser_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E);
		return result;
	}

	public unsafe HRESULT SetNewsLetterSettings(AccountSettings accountSettings, out ServiceError serviceError)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CINewsletterSettings_003E cComPtrNtv_003CINewsletterSettings_003E);
		*(int*)(&cComPtrNtv_003CINewsletterSettings_003E) = 0;
		HRESULT result;
		try
		{
			if (num >= 0 && accountSettings != null)
			{
				num = CreateAccountSettings(accountSettings, (INewsletterSettings**)(&cComPtrNtv_003CINewsletterSettings_003E), null);
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
			*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
			try
			{
				if (num >= 0)
				{
					IAccountManagement* p = m_spAccountManagement.p;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, INewsletterSettings*, IServiceError**, int>)(int)(*(uint*)(*(int*)p + 60)))((nint)p, (INewsletterSettings*)(int)(*(uint*)(&cComPtrNtv_003CINewsletterSettings_003E)), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
				}
				if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
				{
					serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
				}
				result = new HRESULT(num);
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
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CINewsletterSettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CINewsletterSettings_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002ERelease(&cComPtrNtv_003CINewsletterSettings_003E);
		return result;
	}

	public unsafe HRESULT SetPrivacySettings(AccountSettings accountSettings, PassportIdentity parentPassportIdentity, out ServiceError serviceError)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CIPrivacySettings_003E cComPtrNtv_003CIPrivacySettings_003E);
		*(int*)(&cComPtrNtv_003CIPrivacySettings_003E) = 0;
		HRESULT result;
		try
		{
			if (num >= 0 && accountSettings != null)
			{
				num = CreateAccountSettings(accountSettings, null, (IPrivacySettings**)(&cComPtrNtv_003CIPrivacySettings_003E));
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E);
			*(int*)(&cComPtrNtv_003CIPassportIdentity_003E) = 0;
			try
			{
				if (num >= 0 && parentPassportIdentity != null)
				{
					num = parentPassportIdentity.GetComPointer((IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E));
				}
				Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
				*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
				try
				{
					if (num >= 0)
					{
						IAccountManagement* p = m_spAccountManagement.p;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IPrivacySettings*, IPassportIdentity*, IServiceError**, int>)(int)(*(uint*)(*(int*)p + 64)))((nint)p, (IPrivacySettings*)(int)(*(uint*)(&cComPtrNtv_003CIPrivacySettings_003E)), (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E)), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
					}
					if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
					{
						serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
					}
					result = new HRESULT(num);
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
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPrivacySettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPrivacySettings_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002ERelease(&cComPtrNtv_003CIPrivacySettings_003E);
		return result;
	}

	public unsafe HRESULT UpgradeAccount(PassportIdentity passportIdentity, AccountSettings accountSettings, PassportIdentity parentPassportIdentity, out ServiceError serviceError)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CINewsletterSettings_003E cComPtrNtv_003CINewsletterSettings_003E);
		*(int*)(&cComPtrNtv_003CINewsletterSettings_003E) = 0;
		HRESULT result;
		try
		{
			Unsafe.SkipInit(out CComPtrNtv_003CIPrivacySettings_003E cComPtrNtv_003CIPrivacySettings_003E);
			*(int*)(&cComPtrNtv_003CIPrivacySettings_003E) = 0;
			try
			{
				if (num >= 0 && accountSettings != null)
				{
					num = CreateAccountSettings(accountSettings, (INewsletterSettings**)(&cComPtrNtv_003CINewsletterSettings_003E), (IPrivacySettings**)(&cComPtrNtv_003CIPrivacySettings_003E));
				}
				Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E);
				*(int*)(&cComPtrNtv_003CIPassportIdentity_003E) = 0;
				try
				{
					if (num >= 0 && passportIdentity != null)
					{
						num = passportIdentity.GetComPointer((IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E));
					}
					Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E2);
					*(int*)(&cComPtrNtv_003CIPassportIdentity_003E2) = 0;
					try
					{
						if (num >= 0 && parentPassportIdentity != null)
						{
							num = parentPassportIdentity.GetComPointer((IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E2));
						}
						Unsafe.SkipInit(out CComPtrNtv_003CIServiceError_003E cComPtrNtv_003CIServiceError_003E);
						*(int*)(&cComPtrNtv_003CIServiceError_003E) = 0;
						try
						{
							if (num >= 0)
							{
								IAccountManagement* p = m_spAccountManagement.p;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IPassportIdentity*, INewsletterSettings*, IPrivacySettings*, IPassportIdentity*, IServiceError**, int>)(int)(*(uint*)(*(int*)p + 68)))((nint)p, (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E)), (INewsletterSettings*)(int)(*(uint*)(&cComPtrNtv_003CINewsletterSettings_003E)), (IPrivacySettings*)(int)(*(uint*)(&cComPtrNtv_003CIPrivacySettings_003E)), (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E2)), (IServiceError**)(&cComPtrNtv_003CIServiceError_003E));
							}
							if (*(int*)(&cComPtrNtv_003CIServiceError_003E) != 0)
							{
								serviceError = new ServiceError((IServiceError*)(int)(*(uint*)(&cComPtrNtv_003CIServiceError_003E)));
							}
							result = new HRESULT(num);
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
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E2);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E2);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPrivacySettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPrivacySettings_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002ERelease(&cComPtrNtv_003CIPrivacySettings_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CINewsletterSettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CINewsletterSettings_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002ERelease(&cComPtrNtv_003CINewsletterSettings_003E);
		return result;
	}

	public unsafe HRESULT GetTermsOfService(string languageCode, string countryCode, out string termsOfService)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out WBSTRString wBSTRString);
		global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
		HRESULT result;
		try
		{
			if (num >= 0)
			{
				fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(countryCode)))
				{
					try
					{
						fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(languageCode)))
						{
							try
							{
								IAccountManagement* p = m_spAccountManagement.p;
								int num2 = *(int*)p + 72;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort**, int>)(int)(*(uint*)num2))((nint)p, ptr2, ptr, (ushort**)(&wBSTRString));
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
				if (num >= 0)
				{
					termsOfService = new string((char*)(int)(*(uint*)(&wBSTRString)));
				}
			}
			result = new HRESULT(num);
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

	public unsafe HRESULT GetSubscriptionDetails(string offerId, out ArrayList bulletStrings)
	{
		int num = CreateComObject();
		tagSAFEARRAY* ptr = null;
		if (num >= 0)
		{
			fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(offerId)))
			{
				try
				{
					IAccountManagement* p = m_spAccountManagement.p;
					int num2 = *(int*)p + 76;
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, tagSAFEARRAY**, int>)(int)(*(uint*)num2))((nint)p, ptr2, &ptr);
				}
				catch
				{
					//try-fault
					ptr2 = null;
					throw;
				}
			}
		}
		int num3 = 0;
		if (num >= 0)
		{
			num = global::_003CModule_003E.SafeArrayGetUBound(ptr, 1u, &num3);
		}
		int num4 = 0;
		if (num >= 0)
		{
			num = global::_003CModule_003E.SafeArrayGetLBound(ptr, 1u, &num4);
			if (num >= 0)
			{
				bulletStrings = new ArrayList(num3 - num4 + 1);
				int num5 = num4;
				if (num4 <= num3)
				{
					do
					{
						ushort* value = null;
						num = global::_003CModule_003E.SafeArrayGetElement(ptr, &num5, &value);
						if (num < 0)
						{
							break;
						}
						bulletStrings.Add(new string((char*)value));
						num5++;
					}
					while (num5 <= num3);
				}
			}
		}
		if (ptr != null)
		{
			global::_003CModule_003E.SafeArrayDestroy(ptr);
		}
		return new HRESULT(num);
	}

	private unsafe int CreateAddress(Address address, IAddress** ppAddress)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CIAddress_003E cComPtrNtv_003CIAddress_003E);
		*(int*)(&cComPtrNtv_003CIAddress_003E) = 0;
		try
		{
			if (num >= 0)
			{
				IAccountManagement* p = m_spAccountManagement.p;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IAddress**, int>)(int)(*(uint*)(*(int*)p + 16)))((nint)p, (IAddress**)(&cComPtrNtv_003CIAddress_003E));
				if (num >= 0)
				{
					fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(address.Street1)))
					{
						try
						{
							fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(address.Street2)))
							{
								try
								{
									fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(address.City)))
									{
										try
										{
											fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(address.State)))
											{
												try
												{
													fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(address.District)))
													{
														try
														{
															fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(address.PostalCode)))
															{
																try
																{
																	int num2 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIAddress_003E)) + 12;
																	num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, int>)(int)(*(uint*)num2))((IntPtr)(*(int*)(&cComPtrNtv_003CIAddress_003E)), ptr, ptr2, ptr3, ptr4, ptr5, ptr6);
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
					if (num >= 0 && ppAddress != null)
					{
						IAddress* ptr7 = (IAddress*)(int)(*(uint*)(&cComPtrNtv_003CIAddress_003E));
						*(int*)(&cComPtrNtv_003CIAddress_003E) = 0;
						*(int*)ppAddress = (int)ptr7;
					}
				}
			}
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAddress_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAddress_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAddress_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIAddress_003E_002ERelease(&cComPtrNtv_003CIAddress_003E);
		return num;
	}

	private unsafe int CreateCreditCard(CreditCard creditCard, ICreditCard** ppCreditCard)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CICreditCard_003E cComPtrNtv_003CICreditCard_003E);
		*(int*)(&cComPtrNtv_003CICreditCard_003E) = 0;
		try
		{
			if (num >= 0)
			{
				IAccountManagement* p = m_spAccountManagement.p;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ICreditCard**, int>)(int)(*(uint*)(*(int*)p + 20)))((nint)p, (ICreditCard**)(&cComPtrNtv_003CICreditCard_003E));
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIAddress_003E cComPtrNtv_003CIAddress_003E);
			*(int*)(&cComPtrNtv_003CIAddress_003E) = 0;
			try
			{
				if (num >= 0)
				{
					num = CreateAddress(creditCard.Address, (IAddress**)(&cComPtrNtv_003CIAddress_003E));
					if (num >= 0)
					{
						_SYSTEMTIME sYSTEMTIME = global::_003CModule_003E.DateTimeToSystemTime(creditCard.ExpirationDate);
						fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.AccountHolderName)))
						{
							try
							{
								fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.AccountNumber)))
								{
									try
									{
										fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.CCVNumber)))
										{
											try
											{
												fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Locale)))
												{
													try
													{
														fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.PhoneNumber)))
														{
															try
															{
																fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.Email)))
																{
																	try
																	{
																		fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.ContactFirstName)))
																		{
																			try
																			{
																				fixed (ushort* ptr8 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(creditCard.ContactLastName)))
																				{
																					try
																					{
																						int num2 = *(int*)(&cComPtrNtv_003CICreditCard_003E);
																						int num3 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CICreditCard_003E)) + 12;
																						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IAddress*, ECreditCardType, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, _SYSTEMTIME*, int>)(int)(*(uint*)num3))((IntPtr)num2, (IAddress*)(int)(*(uint*)(&cComPtrNtv_003CIAddress_003E)), (ECreditCardType)creditCard.CreditCardType, ptr, ptr2, ptr3, ptr4, ptr5, ptr6, ptr7, ptr8, &sYSTEMTIME);
																					}
																					catch
																					{
																						//try-fault
																						ptr8 = null;
																						throw;
																					}
																				}
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
						if (num >= 0)
						{
							int num4 = *(int*)(&cComPtrNtv_003CICreditCard_003E);
							int num5 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CICreditCard_003E)) + 20;
							((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, byte, int>)(int)(*(uint*)num5))((IntPtr)num4, creditCard.ParentCreditCard ? ((byte)1) : ((byte)0));
							if (ppCreditCard != null)
							{
								ICreditCard* ptr9 = (ICreditCard*)(int)(*(uint*)(&cComPtrNtv_003CICreditCard_003E));
								*(int*)(&cComPtrNtv_003CICreditCard_003E) = 0;
								*(int*)ppCreditCard = (int)ptr9;
							}
						}
					}
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAddress_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAddress_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAddress_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIAddress_003E_002ERelease(&cComPtrNtv_003CIAddress_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CICreditCard_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CICreditCard_003E_002E_007Bdtor_007D), &cComPtrNtv_003CICreditCard_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CICreditCard_003E_002ERelease(&cComPtrNtv_003CICreditCard_003E);
		return num;
	}

	private unsafe int CreateAccountSettings(AccountSettings accountSettings, INewsletterSettings** ppNewsletterSettings, IPrivacySettings** ppPrivacySettings)
	{
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CINewsletterSettings_003E cComPtrNtv_003CINewsletterSettings_003E);
		*(int*)(&cComPtrNtv_003CINewsletterSettings_003E) = 0;
		try
		{
			if (num >= 0)
			{
				IAccountManagement* p = m_spAccountManagement.p;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, INewsletterSettings**, int>)(int)(*(uint*)(*(int*)p + 24)))((nint)p, (INewsletterSettings**)(&cComPtrNtv_003CINewsletterSettings_003E));
				if (num >= 0)
				{
					Unsafe.SkipInit(out NewsletterOptions newsletterOptions);
					*(EmailFormat*)(&newsletterOptions) = accountSettings.EmailFormat;
					Unsafe.As<NewsletterOptions, int>(ref Unsafe.AddByteOffset(ref newsletterOptions, 4)) = (accountSettings.AllowZuneEmails ? 1 : 0);
					Unsafe.As<NewsletterOptions, int>(ref Unsafe.AddByteOffset(ref newsletterOptions, 8)) = (accountSettings.AllowPartnerEmails ? 1 : 0);
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, NewsletterOptions, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CINewsletterSettings_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CINewsletterSettings_003E)), newsletterOptions);
				}
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIPrivacySettings_003E cComPtrNtv_003CIPrivacySettings_003E);
			*(int*)(&cComPtrNtv_003CIPrivacySettings_003E) = 0;
			try
			{
				if (num >= 0)
				{
					if (accountSettings.PrivacySettings.Count > 0)
					{
						IAccountManagement* p2 = m_spAccountManagement.p;
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IPrivacySettings**, int>)(int)(*(uint*)(*(int*)p2 + 28)))((nint)p2, (IPrivacySettings**)(&cComPtrNtv_003CIPrivacySettings_003E));
					}
					if (num >= 0)
					{
						if (*(int*)(&cComPtrNtv_003CIPrivacySettings_003E) != 0)
						{
							foreach (KeyValuePair<PrivacySettingId, PrivacySettingValue> privacySetting in accountSettings.PrivacySettings)
							{
								EPrivacySettingId key = (EPrivacySettingId)privacySetting.Key;
								EPrivacySettingValue value = (EPrivacySettingValue)privacySetting.Value;
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, EPrivacySettingId, EPrivacySettingValue, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIPrivacySettings_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIPrivacySettings_003E)), key, value);
								if (num >= 0)
								{
									continue;
								}
								break;
							}
						}
						if (num >= 0)
						{
							if (ppNewsletterSettings != null)
							{
								INewsletterSettings* ptr = (INewsletterSettings*)(int)(*(uint*)(&cComPtrNtv_003CINewsletterSettings_003E));
								*(int*)(&cComPtrNtv_003CINewsletterSettings_003E) = 0;
								*(int*)ppNewsletterSettings = (int)ptr;
							}
							if (ppPrivacySettings != null)
							{
								if (*(int*)(&cComPtrNtv_003CIPrivacySettings_003E) != 0)
								{
									IPrivacySettings* ptr2 = (IPrivacySettings*)(int)(*(uint*)(&cComPtrNtv_003CIPrivacySettings_003E));
									*(int*)(&cComPtrNtv_003CIPrivacySettings_003E) = 0;
									*(int*)ppPrivacySettings = (int)ptr2;
								}
								else
								{
									*(int*)ppPrivacySettings = 0;
								}
							}
						}
					}
				}
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPrivacySettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPrivacySettings_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002ERelease(&cComPtrNtv_003CIPrivacySettings_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CINewsletterSettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CINewsletterSettings_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002ERelease(&cComPtrNtv_003CINewsletterSettings_003E);
		return num;
	}

	private unsafe int CreateAccountUser(AccountUser accountUser, IAccountUser** ppAccountUser)
	{
		//IL_013f->IL0141: Incompatible stack types: I4 vs Ref
		int num = CreateComObject();
		Unsafe.SkipInit(out CComPtrNtv_003CIAccountUser_003E cComPtrNtv_003CIAccountUser_003E);
		*(int*)(&cComPtrNtv_003CIAccountUser_003E) = 0;
		try
		{
			if (num >= 0)
			{
				IAccountManagement* p = m_spAccountManagement.p;
				num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IAccountUser**, int>)(int)(*(uint*)(*(int*)p + 32)))((nint)p, (IAccountUser**)(&cComPtrNtv_003CIAccountUser_003E));
			}
			Unsafe.SkipInit(out CComPtrNtv_003CIAddress_003E cComPtrNtv_003CIAddress_003E);
			*(int*)(&cComPtrNtv_003CIAddress_003E) = 0;
			try
			{
				if (num >= 0 && accountUser.Address != null)
				{
					num = CreateAddress(accountUser.Address, (IAddress**)(&cComPtrNtv_003CIAddress_003E));
				}
				Unsafe.SkipInit(out CComPtrNtv_003CINewsletterSettings_003E cComPtrNtv_003CINewsletterSettings_003E);
				*(int*)(&cComPtrNtv_003CINewsletterSettings_003E) = 0;
				try
				{
					Unsafe.SkipInit(out CComPtrNtv_003CIPrivacySettings_003E cComPtrNtv_003CIPrivacySettings_003E);
					*(int*)(&cComPtrNtv_003CIPrivacySettings_003E) = 0;
					try
					{
						if (num >= 0 && accountUser.AccountSettings != null)
						{
							num = CreateAccountSettings(accountUser.AccountSettings, (INewsletterSettings**)(&cComPtrNtv_003CINewsletterSettings_003E), (IPrivacySettings**)(&cComPtrNtv_003CIPrivacySettings_003E));
						}
						Unsafe.SkipInit(out CComPtrNtv_003CICreditCard_003E cComPtrNtv_003CICreditCard_003E);
						*(int*)(&cComPtrNtv_003CICreditCard_003E) = 0;
						try
						{
							if (num >= 0 && accountUser.ParentCreditCard != null)
							{
								num = CreateCreditCard(accountUser.ParentCreditCard, (ICreditCard**)(&cComPtrNtv_003CICreditCard_003E));
							}
							Unsafe.SkipInit(out CComPtrNtv_003CIPassportIdentity_003E cComPtrNtv_003CIPassportIdentity_003E);
							*(int*)(&cComPtrNtv_003CIPassportIdentity_003E) = 0;
							try
							{
								if (num >= 0)
								{
									if (accountUser.ParentPassportIdentity != null)
									{
										num = accountUser.ParentPassportIdentity.GetComPointer((IPassportIdentity**)(&cComPtrNtv_003CIPassportIdentity_003E));
									}
									if (num >= 0)
									{
										bool flag = accountUser.Birthday != DateTime.MinValue;
										Unsafe.SkipInit(out _SYSTEMTIME sYSTEMTIME);
										if (flag)
										{
											sYSTEMTIME = global::_003CModule_003E.DateTimeToSystemTime(accountUser.Birthday);
										}
										fixed (ushort* ptr = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(accountUser.ZuneTag)))
										{
											try
											{
												fixed (ushort* ptr2 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(accountUser.Locale)))
												{
													try
													{
														fixed (ushort* ptr3 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(accountUser.FirstName)))
														{
															try
															{
																fixed (ushort* ptr4 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(accountUser.LastName)))
																{
																	try
																	{
																		fixed (ushort* ptr5 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(accountUser.Email)))
																		{
																			try
																			{
																				fixed (ushort* ptr6 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(accountUser.PhoneNumber)))
																				{
																					try
																					{
																						fixed (ushort* ptr7 = &Unsafe.As<char, ushort>(ref global::_003CModule_003E.PtrToStringChars(accountUser.MobilePhoneNumber)))
																						{
																							try
																							{
																								_SYSTEMTIME* ptr8 = (_SYSTEMTIME*)Unsafe.AsPointer(ref flag ? ref *(_003F*)(&sYSTEMTIME) : ref *(_003F*)null);
																								int num2 = *(int*)(int)(*(uint*)(&cComPtrNtv_003CIAccountUser_003E)) + 12;
																								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort*, ushort*, _SYSTEMTIME*, ushort*, ushort*, ushort*, ushort*, ushort*, IAddress*, INewsletterSettings*, IPrivacySettings*, IPassportIdentity*, ICreditCard*, int>)(int)(*(uint*)num2))((IntPtr)(*(int*)(&cComPtrNtv_003CIAccountUser_003E)), ptr, ptr2, ptr8, ptr3, ptr4, ptr5, ptr6, ptr7, (IAddress*)(int)(*(uint*)(&cComPtrNtv_003CIAddress_003E)), (INewsletterSettings*)(int)(*(uint*)(&cComPtrNtv_003CINewsletterSettings_003E)), (IPrivacySettings*)(int)(*(uint*)(&cComPtrNtv_003CIPrivacySettings_003E)), (IPassportIdentity*)(int)(*(uint*)(&cComPtrNtv_003CIPassportIdentity_003E)), (ICreditCard*)(int)(*(uint*)(&cComPtrNtv_003CICreditCard_003E)));
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
										if (num >= 0 && ppAccountUser != null)
										{
											IAccountUser* ptr9 = (IAccountUser*)(int)(*(uint*)(&cComPtrNtv_003CIAccountUser_003E));
											*(int*)(&cComPtrNtv_003CIAccountUser_003E) = 0;
											*(int*)ppAccountUser = (int)ptr9;
										}
									}
								}
							}
							catch
							{
								//try-fault
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPassportIdentity_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPassportIdentity_003E);
								throw;
							}
							global::_003CModule_003E.CComPtrNtv_003CIPassportIdentity_003E_002ERelease(&cComPtrNtv_003CIPassportIdentity_003E);
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CICreditCard_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CICreditCard_003E_002E_007Bdtor_007D), &cComPtrNtv_003CICreditCard_003E);
							throw;
						}
						global::_003CModule_003E.CComPtrNtv_003CICreditCard_003E_002ERelease(&cComPtrNtv_003CICreditCard_003E);
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIPrivacySettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIPrivacySettings_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CIPrivacySettings_003E_002ERelease(&cComPtrNtv_003CIPrivacySettings_003E);
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CINewsletterSettings_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002E_007Bdtor_007D), &cComPtrNtv_003CINewsletterSettings_003E);
					throw;
				}
				global::_003CModule_003E.CComPtrNtv_003CINewsletterSettings_003E_002ERelease(&cComPtrNtv_003CINewsletterSettings_003E);
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAddress_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAddress_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAddress_003E);
				throw;
			}
			global::_003CModule_003E.CComPtrNtv_003CIAddress_003E_002ERelease(&cComPtrNtv_003CIAddress_003E);
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAccountUser_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAccountUser_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAccountUser_003E);
			throw;
		}
		global::_003CModule_003E.CComPtrNtv_003CIAccountUser_003E_002ERelease(&cComPtrNtv_003CIAccountUser_003E);
		return num;
	}

	private unsafe int CreateComObject()
	{
		int num = 0;
		if (m_spAccountManagement.p == null)
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
						num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IUnknown**, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)) + 52)))((IntPtr)(*(int*)(&cComPtrNtv_003CIService_003E)), (IUnknown**)(&cComPtrNtv_003CIUnknown_003E));
					}
					Unsafe.SkipInit(out CComPtrNtv_003CIAccountManagement_003E cComPtrNtv_003CIAccountManagement_003E);
					*(int*)(&cComPtrNtv_003CIAccountManagement_003E) = 0;
					try
					{
						if (num >= 0)
						{
							num = global::_003CModule_003E.CComPtrNtv_003CIUnknown_003E_002EQueryInterface_003Cstruct_0020IAccountManagement_003E(&cComPtrNtv_003CIUnknown_003E, (IAccountManagement**)(&cComPtrNtv_003CIAccountManagement_003E));
							if (num >= 0)
							{
								num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, IService*, int>)(int)(*(uint*)(*(int*)(int)(*(uint*)(&cComPtrNtv_003CIAccountManagement_003E)) + 12)))((IntPtr)(*(int*)(&cComPtrNtv_003CIAccountManagement_003E)), (IService*)(int)(*(uint*)(&cComPtrNtv_003CIService_003E)));
								if (num >= 0)
								{
									m_spAccountManagement.op_Assign((IAccountManagement*)(int)(*(uint*)(&cComPtrNtv_003CIAccountManagement_003E)));
								}
							}
						}
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<CComPtrNtv_003CIAccountManagement_003E*, void>)(&global::_003CModule_003E.CComPtrNtv_003CIAccountManagement_003E_002E_007Bdtor_007D), &cComPtrNtv_003CIAccountManagement_003E);
						throw;
					}
					global::_003CModule_003E.CComPtrNtv_003CIAccountManagement_003E_002ERelease(&cComPtrNtv_003CIAccountManagement_003E);
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
		return num;
	}

	public void _007EAccountManagement()
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
				((IDisposable)m_spAccountManagement).Dispose();
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
