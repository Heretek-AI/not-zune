using Avalonia.Controls;
using Avalonia.Controls.Templates;
using NotZune.UI.ViewModels;
using NotZune.UI.Views;

namespace NotZune.UI;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        return data switch
        {
            QuickplayViewModel => new QuickplayView(),
            CollectionViewModel => new CollectionView(),
            NowPlayingViewModel => new NowPlayingView(),
            DeviceViewModel => new DeviceView(),
            SettingsViewModel => new SettingsView(),
            MixviewViewModel => new MixviewView(),
            MainShellViewModel => new MainShellView(),
            ZuneCardViewModel => new ZuneCardView(),
            PodcastsViewModel => new PodcastsView(),
            CDViewModel => new CDView(),
            _ => FindViewByConvention(data)
        };
    }

    private static Control FindViewByConvention(object data)
    {
        var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name) ?? data.GetType().Assembly.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = "View Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
