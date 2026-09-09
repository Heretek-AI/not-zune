using Microsoft.Iris;

namespace ZuneUI;

public class DataProviderObjectPropertySource : PropertySource
{
	private static PropertySource _instance;

	public static PropertySource Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new DataProviderObjectPropertySource();
			}
			return _instance;
		}
	}

	protected DataProviderObjectPropertySource()
	{
	}

	public override object Get(object media, PropertyDescriptor property)
	{
		DataProviderObject val = (DataProviderObject)((media is DataProviderObject) ? media : null);
		if (val == null)
		{
			return null;
		}
		return val.GetProperty(property.DescriptorName);
	}

	public override void Set(object media, PropertyDescriptor property, object value)
	{
		DataProviderObject val = (DataProviderObject)((media is DataProviderObject) ? media : null);
		if (val != null)
		{
			val.SetProperty(property.DescriptorName, value);
		}
	}
}
