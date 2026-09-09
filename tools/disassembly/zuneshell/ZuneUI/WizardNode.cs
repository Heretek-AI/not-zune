using Microsoft.Zune.Util;

namespace ZuneUI;

public class WizardNode : Node
{
	private Wizard _wizard;

	public WizardNode(Experience owner, Wizard wizard, SQMDataId sqmDataID)
		: base(owner, null, sqmDataID)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		_wizard = wizard;
	}

	protected override void Execute(Shell shell)
	{
		WizardZunePage page = new WizardZunePage(this, _wizard);
		shell.NavigateToPage(page);
	}
}
