using System.Collections;

namespace ZuneUI;

public interface ICommandHandler
{
	void Execute(string command, IDictionary commandArgs);
}
