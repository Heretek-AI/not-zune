using Microsoft.Iris;

namespace MicrosoftZuneLibrary;

public class StaticLibraryDataProvider
{
	public static void Register()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		Application.RegisterDataProvider("StaticLibraryDataProvider", new DataProviderQueryFactory(StaticLibraryDataProviderQuery.CreateInstance));
	}

	private StaticLibraryDataProvider()
	{
	}
}
