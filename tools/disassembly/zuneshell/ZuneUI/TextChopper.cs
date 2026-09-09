using System;
using System.Collections.Generic;
using System.Text;

namespace ZuneUI;

public class TextChopper
{
	public class TextChopperException : Exception
	{
		public TextChopperException(string message)
			: base(message)
		{
		}
	}

	private static Random _random = new Random();

	public static string Chop(string text, int minChunkSize, int maxChunkSize)
	{
		try
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			if (minChunkSize < 1)
			{
				minChunkSize = 1;
			}
			List<string> list = new List<string>();
			list.Add("<C>");
			StringBuilder stringBuilder = new StringBuilder(text.Length * 8);
			int num = 0;
			int num2 = 0;
			bool flag = false;
			while (num < text.Length)
			{
				if (text[num] == '<')
				{
					num2 = 0;
					if (flag)
					{
						flag = false;
						for (int num3 = list.Count; num3 > 0; num3--)
						{
							string text2 = list[num3 - 1];
							text2 = text2.Insert(1, "/");
							stringBuilder.Append(text2);
						}
					}
					int num4 = text.IndexOf('>', num);
					if (num4 == -1)
					{
						throw new TextChopperException("Missing closing > in tag for string : " + text);
					}
					string text3 = text.Substring(num, num4 - num + 1);
					if (text3[1] == '/')
					{
						list.RemoveAt(list.Count - 1);
					}
					else
					{
						list.Add(text3);
					}
					num += text3.Length;
					continue;
				}
				if (num2 == 0)
				{
					num2 = ((maxChunkSize <= minChunkSize) ? minChunkSize : _random.Next(minChunkSize, maxChunkSize));
					num2 = Math.Min(num2, text.Length - num);
					foreach (string item in list)
					{
						stringBuilder.Append(item);
					}
					flag = true;
				}
				string value;
				int num7;
				if (text[num] == '&')
				{
					int num5 = text.IndexOf(';', num);
					if (num5 == -1)
					{
						throw new TextChopperException("Missing closing ; in escaped text for string : " + text);
					}
					int num6 = num5 - num + 1;
					value = text.Substring(num, num6);
					num7 = num6;
				}
				else
				{
					value = text[num].ToString();
					num7 = 1;
				}
				stringBuilder.Append(value);
				num += num7;
				num2--;
				if (num2 == 0 || num == text.Length)
				{
					for (int num8 = list.Count; num8 > 0; num8--)
					{
						string text4 = list[num8 - 1];
						text4 = text4.Insert(1, "/");
						stringBuilder.Append(text4);
					}
					flag = false;
				}
			}
			return stringBuilder.ToString();
		}
		catch (Exception)
		{
			return text;
		}
	}
}
