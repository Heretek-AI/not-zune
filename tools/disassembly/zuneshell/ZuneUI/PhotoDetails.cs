using Microsoft.Iris;
using MicrosoftZuneLibrary;

namespace ZuneUI;

public class PhotoDetails
{
	private static int[] ColumnIndexes = new int[8] { 344, 177, 317, 181, 175, 255, 254, 68 };

	private static string[] DataProperties = new string[8] { "Title", "MediaType", "ImagePath", "FolderName", "FileName", "Width", "Height", "Copyright" };

	public static void Populate(object dataContainer, int libraryId)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		DataProviderObject val = (DataProviderObject)dataContainer;
		object[] array = new object[8]
		{
			string.Empty,
			0,
			string.Empty,
			string.Empty,
			string.Empty,
			0,
			0,
			string.Empty
		};
		ZuneLibrary.GetFieldValues(libraryId, (EListType)3, ColumnIndexes.Length, ColumnIndexes, array, PlaylistManager.Instance.QueryContext);
		for (int i = 0; i < ColumnIndexes.Length; i++)
		{
			if (ColumnIndexes[i] == 177)
			{
				array[i] = MediaDescriptions.Map((MediaType)array[i]);
			}
			val.SetProperty(DataProperties[i], array[i]);
		}
	}
}
