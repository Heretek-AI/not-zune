using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class ApplicationDetails
{
	private static int[] ColumnIndices = new int[8] { 344, 181, 317, 175, 176, 68, 24, 376 };

	private static string[] DataProperties = new string[8] { "Title", "FolderName", "FilePath", "FileName", "FileSize", "Copyright", "Author", "Version" };

	private static object[] DefaultFieldValues = new object[8]
	{
		string.Empty,
		string.Empty,
		string.Empty,
		string.Empty,
		0L,
		string.Empty,
		string.Empty,
		string.Empty
	};

	public static void Populate(object dataContainer, int libraryId)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		DataProviderObject val = (DataProviderObject)dataContainer;
		object[] array = (object[])DefaultFieldValues.Clone();
		bool[] array2 = new bool[array.Length];
		ZuneLibrary.GetFieldValues(libraryId, (EListType)20, ColumnIndices.Length, ColumnIndices, array, array2, PlaylistManager.Instance.QueryContext);
		for (int i = 0; i < ColumnIndices.Length; i++)
		{
			val.SetProperty(DataProperties[i], array[i]);
		}
	}
}
