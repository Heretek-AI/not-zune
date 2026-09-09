using NotZune.Application.Models;

namespace NotZune.Application.Interfaces;

public interface ISettingsStore
{
    AppSettings Load();

    void Save(AppSettings settings);
}
