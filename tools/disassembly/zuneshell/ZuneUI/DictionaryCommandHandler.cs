using System;
using System.Collections;
using System.Collections.Generic;

namespace ZuneUI;

public class DictionaryCommandHandler : ICommandHandler
{
	private Dictionary<string, ICommandHandler> _handlers;

	private string _divider;

	public IDictionary Handlers => _handlers;

	public string Divider
	{
		get
		{
			return _divider;
		}
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				throw new ArgumentException("Must provide a non-empty divider.", "value");
			}
			_divider = value;
		}
	}

	public DictionaryCommandHandler()
	{
		_handlers = new Dictionary<string, ICommandHandler>();
		_divider = "\\";
	}

	public void Execute(string command, IDictionary commandArgs)
	{
		SplitCommand(command, out var prefix, out var suffix);
		ICommandHandler commandHandler = null;
		if (_handlers.ContainsKey(prefix))
		{
			commandHandler = _handlers[prefix];
		}
		if (commandHandler == null)
		{
			throw new ArgumentException("Unknown prefix: " + prefix, "prefix");
		}
		commandHandler.Execute(suffix, commandArgs);
	}

	private void SplitCommand(string command, out string prefix, out string suffix)
	{
		if (string.IsNullOrEmpty(command))
		{
			throw new ArgumentException("Must provide a non-empty command", "command");
		}
		int num = command.IndexOf(_divider);
		if (num < 0)
		{
			prefix = command;
			suffix = null;
		}
		else
		{
			prefix = command.Substring(0, num);
			suffix = command.Substring(num + _divider.Length);
		}
	}
}
