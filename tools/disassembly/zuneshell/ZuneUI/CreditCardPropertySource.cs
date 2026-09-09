using System;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class CreditCardPropertySource : PropertySource
{
	private static PropertySource _instance;

	public static PropertySource Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new CreditCardPropertySource();
			}
			return _instance;
		}
	}

	protected CreditCardPropertySource()
	{
	}

	public override object Get(object media, PropertyDescriptor property)
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		CreditCard val = (CreditCard)((media is CreditCard) ? media : null);
		string descriptorName = property.DescriptorName;
		if (val == null)
		{
			return null;
		}
		if (descriptorName == PropertyEditCreditCard.s_Street1.DescriptorName)
		{
			return val.Address.Street1;
		}
		if (descriptorName == PropertyEditCreditCard.s_Street2.DescriptorName)
		{
			return val.Address.Street2;
		}
		if (descriptorName == PropertyEditCreditCard.s_City.DescriptorName)
		{
			return val.Address.City;
		}
		if (descriptorName == PropertyEditCreditCard.s_District.DescriptorName)
		{
			return val.Address.District;
		}
		if (descriptorName == PropertyEditCreditCard.s_State.DescriptorName)
		{
			return val.Address.State;
		}
		if (descriptorName == PropertyEditCreditCard.s_PostalCode.DescriptorName)
		{
			return val.Address.PostalCode;
		}
		if (descriptorName == PropertyEditCreditCard.s_CardType.DescriptorName)
		{
			return val.CreditCardType;
		}
		if (descriptorName == PropertyEditCreditCard.s_AccountHolderName.DescriptorName)
		{
			return val.AccountHolderName;
		}
		if (descriptorName == PropertyEditCreditCard.s_AccountNumber.DescriptorName)
		{
			return val.AccountNumber;
		}
		if (descriptorName == PropertyEditCreditCard.s_CcvNumber.DescriptorName)
		{
			return val.CCVNumber;
		}
		if (descriptorName == PropertyEditCreditCard.s_ExpirationDate.DescriptorName)
		{
			return val.ExpirationDate;
		}
		if (descriptorName == PropertyEditCreditCard.s_PhoneExtension.DescriptorName)
		{
			return val.PhoneExtension;
		}
		if (descriptorName == PropertyEditCreditCard.s_PhoneNumber.DescriptorName)
		{
			return PropertyEditCreditCard.s_PhoneNumber.Combine(val.PhonePrefix, val.PhoneNumber);
		}
		return null;
	}

	public override void Set(object media, PropertyDescriptor property, object value)
	{
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		CreditCard val = (CreditCard)((media is CreditCard) ? media : null);
		string descriptorName = property.DescriptorName;
		if (val != null)
		{
			if (descriptorName == PropertyEditCreditCard.s_Street1.DescriptorName)
			{
				val.Address.Street1 = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_Street2.DescriptorName)
			{
				val.Address.Street2 = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_City.DescriptorName)
			{
				val.Address.City = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_District.DescriptorName)
			{
				val.Address.District = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_State.DescriptorName)
			{
				val.Address.State = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_PostalCode.DescriptorName)
			{
				val.Address.PostalCode = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_CardType.DescriptorName)
			{
				val.CreditCardType = (CreditCardType)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_AccountHolderName.DescriptorName)
			{
				val.AccountHolderName = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_AccountNumber.DescriptorName)
			{
				val.AccountNumber = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_CcvNumber.DescriptorName)
			{
				val.CCVNumber = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_ExpirationDate.DescriptorName)
			{
				val.ExpirationDate = (DateTime)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_PhoneExtension.DescriptorName)
			{
				val.PhoneExtension = (string)value;
			}
			else if (descriptorName == PropertyEditCreditCard.s_PhoneNumber.DescriptorName)
			{
				PropertyEditCreditCard.s_PhoneNumber.Split(value as string, out var areaCode, out var number);
				val.PhonePrefix = areaCode;
				val.PhoneNumber = number;
			}
		}
	}
}
