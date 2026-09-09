using System;
using System.Runtime.Serialization;

namespace Microsoft.Zune.Shell;

[Serializable]
internal class ZuneShellException : InvalidOperationException
{
	private string _context;

	public string Context => _context;

	public ZuneShellException(string message)
		: this(message, null)
	{
	}

	public ZuneShellException(string message, string context)
		: base(PrepareMessage(message, context))
	{
		_context = context;
	}

	protected ZuneShellException(SerializationInfo info, StreamingContext context)
		: base(info, context)
	{
	}

	private static string PrepareMessage(string message, string context)
	{
		return message;
	}
}
