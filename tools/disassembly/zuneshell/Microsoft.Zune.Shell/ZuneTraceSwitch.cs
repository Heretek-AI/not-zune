using System;
using System.Diagnostics;

namespace Microsoft.Zune.Shell;

internal class ZuneTraceSwitch : TraceSwitch
{
	public ZuneTraceSwitch(string displayName, string description)
		: base(displayName, description)
	{
	}

	protected override void OnValueChanged()
	{
		try
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(TraceLevel), base.Value, ignoreCase: true);
		}
		catch (ArgumentException)
		{
			base.SwitchSetting = 0;
		}
		catch (FormatException)
		{
			base.SwitchSetting = 0;
		}
		catch (OverflowException)
		{
			base.SwitchSetting = 0;
		}
	}
}
