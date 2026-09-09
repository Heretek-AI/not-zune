using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Zune.Shell;

internal struct CommandLineArgument
{
	public string Name;

	public string Value;

	public CommandLineArgument(string name, string value)
	{
		if (name == null)
		{
			throw new ArgumentNullException("name");
		}
		Name = name.ToLower(CultureInfo.InvariantCulture);
		Value = value;
	}

	public static CommandLineArgument[] ParseArgs(string[] arArgs, string stDefaultName)
	{
		List<CommandLineArgument> list = new List<CommandLineArgument>();
		foreach (string text in arArgs)
		{
			if (string.IsNullOrEmpty(text))
			{
				throw new ArgumentException("arArgs");
			}
			string value = null;
			string text2;
			if (text[0] == '-' || text[0] == '/')
			{
				text2 = text.Substring(1, text.Length - 1);
			}
			else
			{
				if (stDefaultName == null)
				{
					continue;
				}
				text2 = stDefaultName + ":" + text;
			}
			int num = text2.IndexOf(':');
			if (num != -1)
			{
				value = text2.Substring(num + 1).Trim(new char[1] { '"' });
				text2 = text2.Substring(0, num);
			}
			CommandLineArgument item = new CommandLineArgument(text2, value);
			list.Add(item);
		}
		return list.ToArray();
	}
}
