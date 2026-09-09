using System.Globalization;

namespace ZuneUI;

public class LanguagePropertyDescriptor : PropertyDescriptor
{
	public LanguagePropertyDescriptor(string name, string multiValueString, string unknownString, bool required)
		: base(name, multiValueString, unknownString, 1000, required, CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
	{
		base.DefaultValue = CultureHelper.GetDefaultLanguage();
	}

	public override object ConvertFromString(string value)
	{
		return LanguageHelper.GetAbbreviation(value);
	}

	public override string ConvertToString(object value)
	{
		return LanguageHelper.GetDisplayName(value as string);
	}
}
