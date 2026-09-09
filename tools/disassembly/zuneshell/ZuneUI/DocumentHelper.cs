using System.Text;

namespace ZuneUI;

public static class DocumentHelper
{
	public static string SeparateParagraphs(string content)
	{
		if (content != null)
		{
			content = content.Replace("\r\n", "\r\n\r\n");
		}
		return content;
	}

	public static string CleanseWhitespace(string str)
	{
		if (string.IsNullOrEmpty(str))
		{
			return str;
		}
		char[] separator = new char[4] { ' ', '\n', '\r', '\t' };
		StringBuilder stringBuilder = new StringBuilder();
		string[] array = str.Split(separator);
		bool flag = false;
		string[] array2 = array;
		foreach (string value in array2)
		{
			if (flag)
			{
				stringBuilder.Append(" ");
				flag = false;
			}
			if (!string.IsNullOrEmpty(value))
			{
				stringBuilder.Append(value);
				flag = true;
			}
		}
		return stringBuilder.ToString();
	}
}
