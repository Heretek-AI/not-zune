using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace NotZune.UI.Views;

public partial class MainShellView : UserControl
{
    public MainShellView()
    {
        InitializeComponent();
    }

    private void OnTitleBarPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as Window;
        if (window == null)
            return;

        if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            if (e.ClickCount == 2 && window.CanResize)
            {
                window.WindowState = window.WindowState == WindowState.Maximized
                    ? WindowState.Normal
                    : WindowState.Maximized;
            }
            else
            {
                window.BeginMoveDrag(e);
            }
        }
    }

    private void OnMinimizeClicked(object? sender, RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as Window;
        if (window != null)
        {
            window.WindowState = WindowState.Minimized;
        }
    }

    private void OnMaximizeClicked(object? sender, RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as Window;
        if (window != null)
        {
            window.WindowState = window.WindowState == WindowState.Maximized 
                ? WindowState.Normal 
                : WindowState.Maximized;
        }
    }

    private void OnCloseClicked(object? sender, RoutedEventArgs e)
    {
        var window = TopLevel.GetTopLevel(this) as Window;
        window?.Close();
    }
}
