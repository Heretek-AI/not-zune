using System;
using System.Reflection;

namespace ZuneUI;

public static class Enum
{
	public static string GetDescription(System.Enum value)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Type type = value.GetType();
		FieldInfo field = type.GetField(value.ToString());
		object[] customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), inherit: false);
		if (customAttributes != null && customAttributes.Length > 0)
		{
			uint stringId = ((DescriptionAttribute)customAttributes[0]).StringId;
			return Shell.LoadString((StringId)stringId);
		}
		return value.ToString();
	}
}
