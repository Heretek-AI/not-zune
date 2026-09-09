using System.Collections;
using Microsoft.Iris;

namespace ZuneUI;

public class DefaultExperienceNavigationCommandHandler : ICommandHandler
{
	private Experience _experience;

	public Experience Experience
	{
		get
		{
			return _experience;
		}
		set
		{
			_experience = value;
		}
	}

	public void Execute(string command, IDictionary commandArgs)
	{
		Node node = (Node)_experience.Nodes.ChosenValue;
		((Command)node).Invoke();
	}
}
