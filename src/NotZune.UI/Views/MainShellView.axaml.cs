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

    public void FocusHeaderSearch()
    {
        if (HeaderSearchBox.IsVisible)
        {
            HeaderSearchBox.Focus();
            HeaderSearchBox.SelectAll();
        }
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

    private void OnPivotStripPointerWheelChanged(object? sender, PointerWheelEventArgs e)
    {
        if (sender is not ScrollViewer strip || e.Delta.Y == 0)
            return;

        var nextX = Math.Max(0, strip.Offset.X - e.Delta.Y * 48);
        strip.Offset = new Avalonia.Vector(nextX, 0);
        e.Handled = true;
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
