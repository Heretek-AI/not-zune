using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public class AccountCountryList : NotifyPropertyChangedImpl
{
	private static AccountCountryList s_instance;

	private IDictionary<string, AccountCountry> _countries;

	public static AccountCountryList Instance
	{
		get
		{
			if (s_instance == null)
			{
				s_instance = new AccountCountryList();
			}
			return s_instance;
		}
	}

	public IList CountryAbbreviations
	{
		get
		{
			string[] array = null;
			if (_countries != null)
			{
				array = new string[_countries.Keys.Count];
				_countries.Keys.CopyTo(array, 0);
			}
			return array;
		}
	}

	public bool Loaded => _countries != null;

	private AccountCountryList()
	{
	}

	public AccountCountry GetCountry(string abbreviation)
	{
		AccountCountry result = null;
		if (_countries != null && abbreviation != null && _countries.ContainsKey(abbreviation))
		{
			result = _countries[abbreviation];
		}
		return result;
	}

	public void LoadCountryData()
	{
		ThreadPool.QueueUserWorkItem(LoadDataOnWorkerThread);
	}

	private void LoadDataOnWorkerThread(object state)
	{
		LoadDataOnWorkerThread();
	}

	internal bool LoadDataOnWorkerThread()
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Expected O, but got Unknown
		bool flag = true;
		if (_countries == null)
		{
			CountryBaseDetails[] countryDetails = Service.Instance.GetCountryDetails();
			SortedDictionary<string, AccountCountry> sortedDictionary = null;
			if (countryDetails != null && countryDetails.Length > 0)
			{
				sortedDictionary = new SortedDictionary<string, AccountCountry>(CountryNameComparer.Instance);
				CountryBaseDetails[] array = countryDetails;
				foreach (CountryBaseDetails val in array)
				{
					sortedDictionary[val.Abbreviation] = AccountCountry.Create(val);
				}
			}
			if (sortedDictionary == null || sortedDictionary.Count == 0)
			{
				flag = false;
			}
			else
			{
				_countries = sortedDictionary;
			}
		}
		if (flag)
		{
			Application.DeferredInvoke(new DeferredInvokeHandler(NotifyLoaded), (object)null);
		}
		return flag;
	}

	private void NotifyLoaded(object args)
	{
		FirePropertyChanged("CountryAbbreviations");
		FirePropertyChanged("Loaded");
	}
}
