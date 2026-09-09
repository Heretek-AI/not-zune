using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace Microsoft.Zune.Service;

public class BillingOfferCollection : IDisposable
{
	private ArrayList m_items;

	private unsafe IBillingOfferCollection* m_pCollection;

	public IList Items => m_items;

	internal unsafe BillingOfferCollection()
	{
		m_items = null;
		m_pCollection = null;
	}

	private void _007EBillingOfferCollection()
	{
		_0021BillingOfferCollection();
	}

	private unsafe void _0021BillingOfferCollection()
	{
		IBillingOfferCollection* pCollection = m_pCollection;
		if (pCollection != null)
		{
			((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 8)))((nint)pCollection);
			m_pCollection = null;
		}
		m_items = null;
	}

	internal unsafe int Init(IBillingOfferCollection* pCollection)
	{
		int num = 0;
		int num2 = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int>)(int)(*(uint*)(*(int*)pCollection + 12)))((nint)pCollection);
		ArrayList arrayList = new ArrayList();
		int num3 = 0;
		if (0 < num2)
		{
			do
			{
				ulong id = 0uL;
				ushort* ptr = null;
				ushort* ptr2 = null;
				uint points = 0u;
				float price = 0f;
				int num4 = 1;
				int num5 = 0;
				global::EBillingOfferType eBillingOfferType = (global::EBillingOfferType)0;
				if (num >= 0)
				{
					num = ((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, int, ulong*, global::EBillingOfferType*, ushort**, ushort**, uint*, float*, int*, int*, int>)(int)(*(uint*)(*(int*)pCollection + 16)))((nint)pCollection, num3, &id, &eBillingOfferType, &ptr, &ptr2, &points, &price, &num4, &num5);
					if (num >= 0)
					{
						bool trial = ((num5 != 0) ? true : false);
						bool taxes = ((num4 != 0) ? true : false);
						arrayList.Add(new BillingOffer(id, (EBillingOfferType)eBillingOfferType, new string((char*)ptr), new string((char*)ptr2), points, price, taxes, trial));
					}
				}
				global::_003CModule_003E.SysFreeString(ptr);
				global::_003CModule_003E.SysFreeString(ptr2);
				num3++;
			}
			while (num3 < num2);
			if (num < 0)
			{
				goto IL_00d4;
			}
		}
		m_items = arrayList;
		m_pCollection = pCollection;
		((delegate* unmanaged[Stdcall, Stdcall]<IntPtr, uint>)(int)(*(uint*)(*(int*)pCollection + 4)))((nint)pCollection);
		goto IL_00d4;
		IL_00d4:
		return num;
	}

	protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool P_0)
	{
		if (P_0)
		{
			_0021BillingOfferCollection();
			return;
		}
		try
		{
			_0021BillingOfferCollection();
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

	~BillingOfferCollection()
	{
		Dispose(false);
	}
}
