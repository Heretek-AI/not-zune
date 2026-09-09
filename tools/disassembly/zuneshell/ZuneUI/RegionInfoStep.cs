using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Iris;
using Microsoft.Zune.Service;

namespace ZuneUI;

public abstract class RegionInfoStep : AccountManagementStep
{
	protected class RegionServiceData
	{
		public IList SupportedCountries;

		public IList SupportedLanguages;

		public IList SupportedStates;

		public string SelectedCountry;

		public string SelectedLanguage;
	}

	private static IList _supportedCountries;

	private IList _supportedLanguages;

	private IList _supportedStates;

	private bool _loadStates;

	private bool _loadLanguages;

	private bool _initializeOnce;

	protected bool LoadStates
	{
		get
		{
			return _loadStates;
		}
		set
		{
			if (_loadStates != value)
			{
				_loadStates = value;
				((ModelItem)this).FirePropertyChanged("LoadStates");
			}
		}
	}

	protected bool LoadLanguages
	{
		get
		{
			return _loadLanguages;
		}
		set
		{
			if (_loadLanguages != value)
			{
				_loadLanguages = value;
				((ModelItem)this).FirePropertyChanged("LoadLanguages");
			}
		}
	}

	public string SelectedLocale
	{
		get
		{
			string result = string.Empty;
			string selectedCountry = SelectedCountry;
			string selectedLanguage = SelectedLanguage;
			if (!string.IsNullOrEmpty(selectedCountry) && !string.IsNullOrEmpty(selectedLanguage))
			{
				result = $"{selectedLanguage}-{selectedCountry}";
			}
			return result;
		}
		set
		{
			if (value != null)
			{
				GetLanguageAndCountry(value, out var language, out var country);
				SelectedLanguage = language;
				SelectedCountry = country;
			}
		}
	}

	public string SelectedCountry
	{
		get
		{
			if (CountryDescriptor != null)
			{
				return GetUncommittedValue(CountryDescriptor) as string;
			}
			return null;
		}
		set
		{
			if (CountryDescriptor != null)
			{
				string text = GetCommittedValue(CountryDescriptor) as string;
				if (text != value)
				{
					SetCommittedValue(CountryDescriptor, value);
				}
			}
		}
	}

	public string SelectedLanguage
	{
		get
		{
			if (LanguageDescriptor != null)
			{
				return GetUncommittedValue(LanguageDescriptor) as string;
			}
			return null;
		}
		set
		{
			if (LanguageDescriptor != null)
			{
				SetCommittedValue(LanguageDescriptor, value);
			}
		}
	}

	public string SelectedState
	{
		get
		{
			if (StateDescriptor != null)
			{
				return GetUncommittedValue(StateDescriptor) as string;
			}
			return null;
		}
		set
		{
			bool flag = SupportedStates == null || SupportedStates.Count == 0 || SupportedStates.Contains(value);
			if (StateDescriptor != null && flag)
			{
				SetCommittedValue(StateDescriptor, value);
			}
		}
	}

	protected virtual PropertyDescriptor CountryDescriptor => null;

	protected virtual PropertyDescriptor LanguageDescriptor => null;

	protected virtual PropertyDescriptor StateDescriptor => null;

	public IList SupportedCountries
	{
		get
		{
			return _supportedCountries;
		}
		protected set
		{
			if (_supportedCountries != value)
			{
				_supportedCountries = value;
				string text = SelectBestCountry(_supportedCountries, SelectedCountry);
				if (text != null)
				{
					SelectedCountry = text;
				}
				((ModelItem)this).FirePropertyChanged("SupportedCountries");
			}
		}
	}

	public IList SupportedLanguages
	{
		get
		{
			return _supportedLanguages;
		}
		protected set
		{
			if (_supportedLanguages != value)
			{
				_supportedLanguages = value;
				string text = SelectBestLanguage(_supportedLanguages, SelectedLanguage);
				if (text != null)
				{
					SelectedLanguage = text;
				}
				((ModelItem)this).FirePropertyChanged("SupportedLanguages");
			}
		}
	}

	public IList SupportedStates
	{
		get
		{
			return _supportedStates;
		}
		protected set
		{
			if (_supportedStates != value)
			{
				_supportedStates = value;
				string text = null;
				if (_supportedStates != null && _supportedStates.Count > 0)
				{
					text = ((!_supportedStates.Contains(SelectedState)) ? (_supportedStates[0] as string) : SelectedState);
				}
				if (text != null)
				{
					SelectedState = text;
				}
				((ModelItem)this).FirePropertyChanged("SupportedStates");
			}
		}
	}

	public RegionInfoStep(Wizard owner, AccountManagementWizardState state, bool parentAccount)
		: base(owner, state, parentAccount)
	{
		_supportedCountries = null;
		_supportedLanguages = null;
		_supportedStates = null;
	}

	protected override void OnDispose(bool disposing)
	{
		if (disposing)
		{
			if (CountryDescriptor != null)
			{
				base.WizardPropertyEditor.GetProperty(CountryDescriptor).PropertyChanged -= OnCountryPropertyChanged;
			}
			if (LanguageDescriptor != null)
			{
				base.WizardPropertyEditor.GetProperty(LanguageDescriptor).PropertyChanged -= OnLanguagePropertyChanged;
			}
		}
		((ModelItem)this).OnDispose(disposing);
	}

	public IList GetCountryDisplayNames()
	{
		List<string> list = null;
		if (_supportedCountries != null)
		{
			list = new List<string>(_supportedCountries.Count);
			foreach (string supportedCountry in _supportedCountries)
			{
				list.Add(CountryHelper.GetDisplayName(supportedCountry));
			}
		}
		return list;
	}

	public IList GetLanguageDisplayNames()
	{
		List<string> list = null;
		if (_supportedLanguages != null)
		{
			list = new List<string>(_supportedLanguages.Count);
			foreach (string supportedLanguage in _supportedLanguages)
			{
				list.Add(LanguageHelper.GetDisplayName(supportedLanguage));
			}
		}
		return list;
	}

	public IList GetStateDisplayNames()
	{
		IList result = null;
		AccountCountry country = AccountCountryList.Instance.GetCountry(SelectedCountry);
		if (country != null)
		{
			result = country.States;
		}
		return result;
	}

	internal static void GetLanguageAndCountry(string locale, out string language, out string country)
	{
		language = null;
		country = null;
		if (string.IsNullOrEmpty(locale))
		{
			return;
		}
		try
		{
			CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(locale);
			while (!cultureInfo.IsNeutralCulture && cultureInfo.LCID != 1044)
			{
				cultureInfo = cultureInfo.Parent;
			}
			language = cultureInfo.Name;
			string name = locale;
			string[] array = locale.Split(new char[1] { '-' });
			if (array.Length >= 2)
			{
				name = array[^1];
			}
			RegionInfo regionInfo = new RegionInfo(name);
			country = regionInfo.TwoLetterISORegionName;
		}
		catch (ArgumentException)
		{
		}
	}

	protected override void OnActivate()
	{
		IntializeOnce();
		RegionServiceData regionServiceData = new RegionServiceData();
		regionServiceData.SelectedCountry = SelectedCountry;
		regionServiceData.SelectedLanguage = SelectedLanguage;
		if (!_loadStates)
		{
			regionServiceData.SupportedStates = new ArrayList(0);
		}
		else
		{
			regionServiceData.SupportedStates = SupportedStates;
		}
		if (!_loadLanguages)
		{
			regionServiceData.SupportedLanguages = new ArrayList(0);
		}
		else
		{
			regionServiceData.SupportedLanguages = SupportedLanguages;
		}
		if (!string.IsNullOrEmpty(regionServiceData.SelectedCountry))
		{
			regionServiceData.SupportedCountries = SupportedCountries;
		}
		base.ServiceActivationRequestsDone = base.ServiceActivationRequestsDone && regionServiceData.SupportedStates != null && regionServiceData.SupportedLanguages != null && regionServiceData.SupportedCountries != null;
		if (!base.ServiceActivationRequestsDone)
		{
			StartActivationRequests(regionServiceData);
		}
	}

	private void IntializeOnce()
	{
		if (_initializeOnce)
		{
			return;
		}
		_initializeOnce = true;
		if (SignIn.Instance.SignedIn)
		{
			SelectedCountry = SignIn.Instance.CountryCode;
		}
		if (base.WizardPropertyEditor != null)
		{
			if (CountryDescriptor != null)
			{
				base.WizardPropertyEditor.GetProperty(CountryDescriptor).PropertyChanged += OnCountryPropertyChanged;
			}
			if (LanguageDescriptor != null)
			{
				base.WizardPropertyEditor.GetProperty(LanguageDescriptor).PropertyChanged += OnLanguagePropertyChanged;
			}
		}
		OnCountryChangedInternal(refreshLanguagesStates: false);
		OnLanguageChangedInternal();
	}

	private void OnCountryPropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "Value")
		{
			OnCountryChangedInternal(refreshLanguagesStates: true);
		}
	}

	private void OnLanguagePropertyChanged(object sender, PropertyChangedEventArgs args)
	{
		if (args.PropertyName == "Value")
		{
			OnLanguageChangedInternal();
		}
	}

	private void OnCountryChangedInternal(bool refreshLanguagesStates)
	{
		((ModelItem)this).FirePropertyChanged("SelectedLocale");
		if (refreshLanguagesStates)
		{
			RefreshLanguagesStates();
		}
		if (base.WizardPropertyEditor != null && StateDescriptor != null)
		{
			base.WizardPropertyEditor.SetPropertyState(StateDescriptor, SelectedCountry);
		}
		OnCountryChanged();
	}

	private void OnLanguageChangedInternal()
	{
		((ModelItem)this).FirePropertyChanged("SelectedLocale");
		OnLanguageChanged();
	}

	protected virtual void OnCountryChanged()
	{
	}

	protected virtual void OnLanguageChanged()
	{
	}

	protected void RefreshLanguagesStates()
	{
		if (base.WizardPropertyEditor != null && StateDescriptor != null)
		{
			base.WizardPropertyEditor.SetPropertyState(StateDescriptor, SelectedCountry);
		}
		if (base.ServiceActivationRequestsDone)
		{
			SupportedLanguages = null;
			SupportedStates = null;
			Activate();
		}
	}

	protected override void OnStartActivationRequests(object state)
	{
		RegionServiceData regionServiceData = (RegionServiceData)state;
		if (regionServiceData.SupportedCountries == null)
		{
			regionServiceData.SupportedCountries = ObtainSupportedCountries();
			if (regionServiceData.SupportedCountries == null || regionServiceData.SupportedCountries.Count == 0)
			{
				regionServiceData.SelectedCountry = null;
			}
			else
			{
				regionServiceData.SelectedCountry = SelectBestCountry(regionServiceData.SupportedCountries, regionServiceData.SelectedCountry);
			}
		}
		if (regionServiceData.SupportedLanguages == null)
		{
			regionServiceData.SupportedLanguages = ObtainSupportedLanguages(regionServiceData.SelectedCountry);
			regionServiceData.SelectedLanguage = SelectBestLanguage(regionServiceData.SupportedLanguages, regionServiceData.SelectedLanguage);
		}
		if (regionServiceData.SupportedStates == null)
		{
			regionServiceData.SupportedStates = ObtainSupportedStates(regionServiceData.SelectedCountry);
		}
		EndActivationRequests(regionServiceData);
	}

	protected override void OnEndActivationRequests(object args)
	{
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		RegionServiceData regionServiceData = (RegionServiceData)args;
		if (regionServiceData.SupportedCountries != null)
		{
			SupportedCountries = regionServiceData.SupportedCountries;
		}
		if (regionServiceData.SupportedLanguages != null)
		{
			SupportedLanguages = regionServiceData.SupportedLanguages;
		}
		if (regionServiceData.SupportedStates != null)
		{
			SupportedStates = regionServiceData.SupportedStates;
		}
		if (regionServiceData.SupportedCountries == null || (regionServiceData.SupportedLanguages == null && LoadLanguages))
		{
			SetError(HRESULT._E_FAIL, null);
			NavigateToErrorHandler();
		}
	}

	private IList ObtainSupportedCountries()
	{
		if (AccountCountryList.Instance.LoadDataOnWorkerThread())
		{
			return AccountCountryList.Instance.CountryAbbreviations;
		}
		return null;
	}

	private IList ObtainSupportedLanguages(string country)
	{
		IList list = null;
		AccountCountry country2 = AccountCountryList.Instance.GetCountry(country);
		if (country2 != null && ((CountryBaseDetails)country2).LanguageAbbreviations != null)
		{
			return ((CountryBaseDetails)country2).LanguageAbbreviations;
		}
		return new ArrayList(0);
	}

	private IList ObtainSupportedStates(string country)
	{
		IList list = null;
		AccountCountry country2 = AccountCountryList.Instance.GetCountry(country);
		if (country2 != null && country2.StatesAbbreviations != null)
		{
			return country2.StatesAbbreviations;
		}
		return new List<string>(0);
	}

	private string SelectBestCountry(IList supportedCountries, string currentCountry)
	{
		string text = null;
		if (supportedCountries != null && supportedCountries.Count > 0)
		{
			text = currentCountry;
			if (string.IsNullOrEmpty(text))
			{
				text = CultureHelper.GetDefaultCountry();
			}
			if (!supportedCountries.Contains(text))
			{
				text = supportedCountries[0] as string;
			}
		}
		return text;
	}

	private string SelectBestLanguage(IList supportedLanguage, string currentLanguage)
	{
		string text = null;
		if (supportedLanguage != null && supportedLanguage.Count > 0)
		{
			text = currentLanguage;
			if (string.IsNullOrEmpty(text))
			{
				text = CultureHelper.GetDefaultLanguage();
			}
			if (!supportedLanguage.Contains(text))
			{
				text = supportedLanguage[0] as string;
			}
		}
		return text;
	}
}
