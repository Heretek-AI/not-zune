using Microsoft.Zune.Service;

namespace ZuneUI;

public class PropertyEditCreditCard : MetadataEditMedia
{
	private CreditCard _creditCard;

	private static PropertyDescriptor[] s_dataProviderProperties;

	public static CountryFieldValidationPropertyDescriptor s_Street1 = new CountryFieldValidationPropertyDescriptor("Street1", CountryFieldValidatorType.Street1);

	public static CountryFieldValidationPropertyDescriptor s_Street2 = new CountryFieldValidationPropertyDescriptor("Street2", CountryFieldValidatorType.Street2);

	public static CountryFieldValidationPropertyDescriptor s_City = new CountryFieldValidationPropertyDescriptor("City", CountryFieldValidatorType.City);

	public static CountryFieldValidationPropertyDescriptor s_District = new CountryFieldValidationPropertyDescriptor("District", CountryFieldValidatorType.District);

	public static StateDescriptor s_State = new StateDescriptor("State");

	public static PostalCodeDescriptor s_PostalCode = new PostalCodeDescriptor("PostalCode");

	public static PhoneNumberDescriptors s_PhoneNumber = new PhoneNumberDescriptors("PhoneNumber", CountryFieldValidatorType.PhoneNumber);

	public static CreditCardNumberDescriptor s_PhoneExtension = new CreditCardNumberDescriptor("PhoneExtension", string.Empty, string.Empty, required: false, allowSeperators: false);

	public static CardTypePropertyDescriptor s_CardType = new CardTypePropertyDescriptor("CardType", string.Empty, Shell.LoadString(StringId.IDS_BILLING_EDIT_CC_CCTYPE_EMPTY), required: true);

	public static CountryFieldValidationPropertyDescriptor s_AccountHolderName = new CountryFieldValidationPropertyDescriptor("AccountHolderName", CountryFieldValidatorType.AccountHolderName);

	public static CreditCardNumberDescriptor s_AccountNumber = new CreditCardNumberDescriptor("AccountNumber", string.Empty, string.Empty, required: true, allowSeperators: true);

	public static CreditCardNumberDescriptor s_CcvNumber = new CreditCardNumberDescriptor("CcvNumber", string.Empty, string.Empty, required: true, allowSeperators: false);

	public static CreditCardExpirationDateDescriptor s_ExpirationDate = new CreditCardExpirationDateDescriptor("ExpirationDate", string.Empty, string.Empty, required: true);

	public CreditCard CreditCard => _creditCard;

	public static PropertyDescriptor Street1 => s_Street1;

	public static PropertyDescriptor Street2 => s_Street2;

	public static PropertyDescriptor City => s_City;

	public static PropertyDescriptor District => s_District;

	public static PropertyDescriptor State => s_State;

	public static PropertyDescriptor PostalCode => s_PostalCode;

	public static PropertyDescriptor PhoneNumber => s_PhoneNumber;

	public static PropertyDescriptor PhoneExtension => s_PhoneExtension;

	public static PropertyDescriptor CardType => s_CardType;

	public static PropertyDescriptor AccountHolderName => s_AccountHolderName;

	public static PropertyDescriptor AccountNumber => s_AccountNumber;

	public static PropertyDescriptor CcvNumber => s_CcvNumber;

	public static PropertyDescriptor ExpirationDate => s_ExpirationDate;

	public PropertyEditCreditCard(CreditCard creditCard)
	{
		_source = CreditCardPropertySource.Instance;
		Initialize(creditCard);
	}

	protected void Initialize(CreditCard creditCard)
	{
		_creditCard = creditCard;
		if (s_dataProviderProperties == null)
		{
			s_dataProviderProperties = new PropertyDescriptor[13]
			{
				s_Street1, s_Street2, s_City, s_District, s_State, s_PostalCode, s_PhoneNumber, s_PhoneExtension, s_CardType, s_AccountHolderName,
				s_AccountNumber, s_CcvNumber, s_ExpirationDate
			};
		}
		Initialize(new object[1] { creditCard }, s_dataProviderProperties);
	}
}
