using System.Text;

namespace ZuneUI;

public class PrepaidCodePropertyDescriptor : PropertyDescriptor
{
	private static int s_subLength = 5;

	private static int s_subCount = 5;

	private static int s_length = s_subLength * s_subCount;

	private static int s_lengthWithDelimiters = s_length + s_subCount - 1;

	private static string s_delimiter = "-";

	public PrepaidCodePropertyDescriptor(string name, string multiValueString, string unknownString, bool required)
		: base(name, multiValueString, unknownString, s_lengthWithDelimiters, required)
	{
	}

	public override bool IsValidInternal(string value)
	{
		bool result = false;
		if (value != null)
		{
			string text = value.Replace(s_delimiter, "");
			result = text.Length == s_length;
		}
		return result;
	}

	public override object ConvertFromString(string value)
	{
		string result = string.Empty;
		if (value != null)
		{
			result = AddDelimiters(value);
		}
		return result;
	}

	public override string ConvertToString(object value)
	{
		return AddDelimiters(value as string);
	}

	public static string AddDelimiters(string value)
	{
		StringBuilder stringBuilder = new StringBuilder(s_length);
		if (value != null)
		{
			int num = 0;
			for (int i = 0; i < value.Length; i++)
			{
				if (char.IsLetterOrDigit(value[i]))
				{
					if (num % s_subLength == 0 && num != 0)
					{
						stringBuilder.Append(s_delimiter);
					}
					stringBuilder.Append(value[i]);
					num++;
				}
			}
		}
		return stringBuilder.ToString().ToUpper();
	}
}
