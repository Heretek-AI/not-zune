using System.Threading.Tasks;

namespace NotZune.Application.Interfaces;

public interface IFolderPickerService
{
    Task<string?> PickFolderAsync(string title = "Select Music Collection Folder");
}
