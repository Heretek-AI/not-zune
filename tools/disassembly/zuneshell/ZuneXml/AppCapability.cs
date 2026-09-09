using Microsoft.Iris;

namespace ZuneXml;

internal class AppCapability : XmlDataProviderObject
{
	private DisclosureEnum _disclosureEnum = DisclosureEnum.Invalid;

	public bool NeedDisclosure
	{
		get
		{
			if (DisclosureEnum != DisclosureEnum.Disclose)
			{
				return DisclosureEnum == DisclosureEnum.DiscloseAndPrompt;
			}
			return true;
		}
	}

	public bool NeedPrompt
	{
		get
		{
			if (DisclosureEnum != DisclosureEnum.Prompt)
			{
				return DisclosureEnum == DisclosureEnum.DiscloseAndPrompt;
			}
			return true;
		}
	}

	internal DisclosureEnum DisclosureEnum
	{
		get
		{
			return _disclosureEnum;
		}
		set
		{
			if (_disclosureEnum != value)
			{
				_disclosureEnum = value;
				((DataProviderObject)this).FirePropertyChanged("DisclosureEnum");
			}
		}
	}

	internal string Id => (string)base.GetProperty("Id");

	internal string Description => (string)base.GetProperty("Description");

	internal string DisclosureType => (string)base.GetProperty("DisclosureType");

	public override void SetProperty(string propertyName, object value)
	{
		string text;
		if ((text = propertyName) != null && text == "DisclosureType")
		{
			DisclosureEnum = SchemaHelper.ToDisclosureEnum((string)value);
		}
		base.SetProperty(propertyName, value);
	}

	internal static XmlDataProviderObject ConstructAppCapabilityObject(DataProviderQuery owner, object objectTypeCookie)
	{
		return new AppCapability(owner, objectTypeCookie);
	}

	internal AppCapability(DataProviderQuery owner, object resultTypeCookie)
		: base(owner, resultTypeCookie)
	{
	}

	public override object GetProperty(string propertyName)
	{
		return propertyName switch
		{
			"NeedDisclosure" => NeedDisclosure, 
			"NeedPrompt" => NeedPrompt, 
			_ => base.GetProperty(propertyName), 
		};
	}
}
