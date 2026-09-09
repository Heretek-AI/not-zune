using Microsoft.Iris;

namespace ZuneUI;

public interface IWizardNavigation
{
	Command Back { get; }

	Command Next { get; }

	Command Finish { get; }

	Command Cancel { get; }
}
