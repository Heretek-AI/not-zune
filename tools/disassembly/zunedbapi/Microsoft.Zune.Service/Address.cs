using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Zune.Service;

public class Address
{
	private string m_street1;

	private string m_street2;

	private string m_city;

	private string m_district;

	private string m_state;

	private string m_postalCode;

	public string PostalCode
	{
		get
		{
			return m_postalCode;
		}
		set
		{
			m_postalCode = value;
		}
	}

	public string State
	{
		get
		{
			return m_state;
		}
		set
		{
			m_state = value;
		}
	}

	public string District
	{
		get
		{
			return m_district;
		}
		set
		{
			m_district = value;
		}
	}

	public string City
	{
		get
		{
			return m_city;
		}
		set
		{
			m_city = value;
		}
	}

	public string Street2
	{
		get
		{
			return m_street2;
		}
		set
		{
			m_street2 = value;
		}
	}

	public string Street1
	{
		get
		{
			return m_street1;
		}
		set
		{
			m_street1 = value;
		}
	}

	internal unsafe Address(IAddress* pAddress)
	{
		if (pAddress == null)
		{
			return;
		}
		Unsafe.SkipInit(out WBSTRString wBSTRString);
		global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString);
		try
		{
			Unsafe.SkipInit(out WBSTRString wBSTRString2);
			global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString2);
			try
			{
				Unsafe.SkipInit(out WBSTRString wBSTRString3);
				global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString3);
				try
				{
					Unsafe.SkipInit(out WBSTRString wBSTRString4);
					global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString4);
					try
					{
						Unsafe.SkipInit(out WBSTRString wBSTRString5);
						global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString5);
						try
						{
							Unsafe.SkipInit(out WBSTRString wBSTRString6);
							global::_003CModule_003E.WBSTRString_002E_007Bctor_007D(&wBSTRString6);
							try
							{
								if (((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, ushort**, ushort**, ushort**, ushort**, ushort**, ushort**, int>)(int)(*(uint*)(*(int*)pAddress + 16)))((nint)pAddress, (ushort**)(&wBSTRString), (ushort**)(&wBSTRString2), (ushort**)(&wBSTRString3), (ushort**)(&wBSTRString4), (ushort**)(&wBSTRString5), (ushort**)(&wBSTRString6)) >= 0)
								{
									m_street1 = new string((char*)(int)(*(uint*)(&wBSTRString)));
									m_street2 = new string((char*)(int)(*(uint*)(&wBSTRString2)));
									m_city = new string((char*)(int)(*(uint*)(&wBSTRString3)));
									m_state = new string((char*)(int)(*(uint*)(&wBSTRString4)));
									m_district = new string((char*)(int)(*(uint*)(&wBSTRString5)));
									m_postalCode = new string((char*)(int)(*(uint*)(&wBSTRString6)));
								}
							}
							catch
							{
								//try-fault
								global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString6);
								throw;
							}
							global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString6));
						}
						catch
						{
							//try-fault
							global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString5);
							throw;
						}
						global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString5));
					}
					catch
					{
						//try-fault
						global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString4);
						throw;
					}
					global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString4));
				}
				catch
				{
					//try-fault
					global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString3);
					throw;
				}
				global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString3));
			}
			catch
			{
				//try-fault
				global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString2);
				throw;
			}
			global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString2));
		}
		catch
		{
			//try-fault
			global::_003CModule_003E.___CxxCallUnwindDtor((delegate*<void*, void>)(delegate*<WBSTRString*, void>)(&global::_003CModule_003E.WBSTRString_002E_007Bdtor_007D), &wBSTRString);
			throw;
		}
		global::_003CModule_003E.WString_002E_007Bdtor_007D((WString*)(&wBSTRString));
	}

	public Address(string street1, string street2, string city, string district, string state, string postalCode)
	{
		m_street1 = street1;
		m_street2 = street2;
		m_city = city;
		m_district = district;
		m_state = state;
		m_postalCode = postalCode;
	}

	public Address()
	{
	}
}
