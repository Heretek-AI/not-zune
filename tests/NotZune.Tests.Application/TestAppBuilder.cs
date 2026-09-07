using Avalonia;
using Avalonia.Headless;
using NotZune.Tests.Application;

[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

namespace NotZune.Tests.Application;

public class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp() => AppBuilder.Configure<Avalonia.Application>()
        .UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
