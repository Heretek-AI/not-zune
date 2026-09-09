using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class AccountCountry : CountryBaseDetails
{
	private IDictionary<string, string> m_states;

	private string m_localizedStates;

	private static Hashtable m_countryFieldValidatorLookup;

	public IList States
	{
		get
		{
			LoadStateData();
			ArrayList arrayList = null;
			if (m_states != null)
			{
				arrayList = new ArrayList(m_states.Values.Count);
				arrayList.AddRange((ICollection)m_states.Values);
				arrayList.Sort(StringComparer.CurrentCultureIgnoreCase);
			}
			return arrayList;
		}
	}

	public IList StatesAbbreviations
	{
		get
		{
			LoadStateData();
			string[] array = null;
			if (m_states != null)
			{
				array = new string[m_states.Keys.Count];
				m_states.Keys.CopyTo(array, 0);
			}
			return array;
		}
	}

	public static Hashtable CountryFieldValidatorLookup
	{
		get
		{
			if (m_countryFieldValidatorLookup == null)
			{
				m_countryFieldValidatorLookup = new Hashtable();
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.FirstName, "firstName");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.LastName, "lastName");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.AccountHolderName, "accountHolderName");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.Street1, "street1");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.Street2, "street2");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.City, "city");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.State, "state");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.PostalCode, "postalCode");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.District, "district");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.Country, "country");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.PhoneType, "phoneType");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.PhonePrefix, "phonePrefix");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.PhoneNumber, "phoneNumber");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.PhoneCountryCode, "phoneCountryCode");
				m_countryFieldValidatorLookup.Add(CountryFieldValidatorType.PhoneExtension, "phoneExtension");
			}
			return m_countryFieldValidatorLookup;
		}
	}

	private AccountCountry(string abbreviation, string[] languageAbbreviations, int teenagerAge, int adultAge, bool showAccountSettings, bool usageCollection, CountryFieldValidator[] validators, string localizedStates)
		: base(abbreviation, languageAbbreviations, teenagerAge, adultAge, showAccountSettings, usageCollection, validators)
	{
		m_localizedStates = localizedStates;
	}

	public CountryFieldValidator GetValidator(CountryFieldValidatorType type)
	{
		CountryFieldValidator result = null;
		if (((CountryBaseDetails)this).Validators != null && CountryFieldValidatorLookup.ContainsKey(type))
		{
			for (int i = 0; i < ((CountryBaseDetails)this).Validators.Length; i++)
			{
				CountryFieldValidator val = ((CountryBaseDetails)this).Validators[i];
				if (val.Name == (string)CountryFieldValidatorLookup[type])
				{
					result = val;
					break;
				}
			}
		}
		return result;
	}

	public static AccountCountry Create(CountryBaseDetails details)
	{
		AccountCountry result = null;
		if (details != null)
		{
			string[] languageAbbreviations = details.LanguageAbbreviations;
			if (languageAbbreviations != null)
			{
				Array.Sort(languageAbbreviations, LanguageNameComparer.Instance);
			}
			string localizedStates = null;
			if (details.Abbreviation.Equals("US", StringComparison.InvariantCultureIgnoreCase))
			{
				localizedStates = Shell.LoadString(StringId.IDS_BILLING_USA_STATES);
			}
			else if (details.Abbreviation.Equals("CA", StringComparison.InvariantCultureIgnoreCase))
			{
				localizedStates = Shell.LoadString(StringId.IDS_BILLING_CA_PROVINCES);
			}
			result = new AccountCountry(details.Abbreviation, details.LanguageAbbreviations, details.TeenagerAge, details.AdultAge, details.ShowNewsletterOptions, details.UsageCollection, details.Validators, localizedStates);
		}
		return result;
	}

	private void LoadStateData()
	{
		if (m_states != null || m_localizedStates == null)
		{
			return;
		}
		string[] array = m_localizedStates.Split(new char[1] { ';' }, StringSplitOptions.RemoveEmptyEntries);
		int num = array.Length / 2;
		m_states = new Dictionary<string, string>(num);
		int num2 = 0;
		int num3 = 1;
		for (int i = 0; i < num; i++)
		{
			if (!m_states.ContainsKey(array[num2]))
			{
				m_states.Add(array[num2], array[num3]);
			}
			num2 += 2;
			num3 += 2;
		}
	}

	public string GetStateAbbreviation(string stateName)
	{
		LoadStateData();
		string result = null;
		if (m_states != null && stateName != null)
		{
			foreach (KeyValuePair<string, string> state in m_states)
			{
				if (state.Value.Equals(stateName, StringComparison.InvariantCultureIgnoreCase))
				{
					result = state.Key;
					break;
				}
			}
		}
		return result;
	}

	public string GetState(string stateAbbreviation)
	{
		LoadStateData();
		string result = null;
		if (m_states != null && stateAbbreviation != null && m_states.ContainsKey(stateAbbreviation))
		{
			result = m_states[stateAbbreviation];
		}
		return result;
	}
}
