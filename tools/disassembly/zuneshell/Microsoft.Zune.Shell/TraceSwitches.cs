namespace Microsoft.Zune.Shell;

internal static class TraceSwitches
{
	private static ZuneTraceSwitch collectionSwitch;

	private static ZuneTraceSwitch shellSwitch;

	private static ZuneTraceSwitch dataProviderSwitch;

	public static ZuneTraceSwitch CollectionSwitch
	{
		get
		{
			if (collectionSwitch == null)
			{
				collectionSwitch = new ZuneTraceSwitch("Collection", "Collection page traces");
			}
			return collectionSwitch;
		}
	}

	public static ZuneTraceSwitch ShellSwitch
	{
		get
		{
			if (shellSwitch == null)
			{
				shellSwitch = new ZuneTraceSwitch("Shell", "Shell traces");
			}
			return shellSwitch;
		}
	}

	public static ZuneTraceSwitch DataProviderSwitch
	{
		get
		{
			if (dataProviderSwitch == null)
			{
				dataProviderSwitch = new ZuneTraceSwitch("DataProvider", "Data provider traces");
			}
			return dataProviderSwitch;
		}
	}
}
