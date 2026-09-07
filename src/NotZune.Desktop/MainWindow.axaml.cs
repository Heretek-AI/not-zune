using System;
using Avalonia.Controls;
using Avalonia.Input;
using NotZune.UI.ViewModels;

namespace NotZune.Desktop;

public partial class MainWindow : Window
{
    private double _preCompactWidth = 1240;
    private double _preCompactHeight = 780;

    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        base.OnDataContextChanged(e);
        if (DataContext is MainShellViewModel vm)
        {
            vm.CompactModeChanged += OnCompactModeChanged;
        }
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
        base.OnKeyDown(e);

        if (DataContext is not MainShellViewModel vm)
            return;

        var focused = FocusManager?.GetFocusedElement();
        bool isTextBoxFocused = focused is TextBox;

        // Ctrl shortcuts (work even if text box is focused)
        if (e.KeyModifiers.HasFlag(KeyModifiers.Control))
        {
            switch (e.Key)
            {
                case Key.P:
                    vm.PlayPauseCommand.Execute(null);
                    e.Handled = true;
                    return;
                case Key.F:
                    vm.NextCommand.Execute(null);
                    e.Handled = true;
                    return;
                case Key.B:
                    vm.PreviousCommand.Execute(null);
                    e.Handled = true;
                    return;
                case Key.H:
                    vm.ToggleShuffleCommand.Execute(null);
                    e.Handled = true;
                    return;
                case Key.T:
                    vm.ToggleRepeatCommand.Execute(null);
                    e.Handled = true;
                    return;
                case Key.M:
                    vm.ToggleCompactModeCommand.Execute(null);
                    e.Handled = true;
                    return;
                case Key.E:
                    vm.ActivePivot = NavigationPivot.Collection;
                    e.Handled = true;
                    return;
            }
        }

        // Non-Ctrl shortcuts (only if NOT typing in a TextBox)
        if (!isTextBoxFocused)
        {
            switch (e.Key)
            {
                case Key.Space:
                    vm.PlayPauseCommand.Execute(null);
                    e.Handled = true;
                    return;
                case Key.F7:
                    // Mute / Unmute
                    vm.Volume = vm.Volume > 0 ? 0 : 75;
                    e.Handled = true;
                    return;
                case Key.F8:
                    // Volume down
                    vm.Volume = Math.Max(0, vm.Volume - 5);
                    e.Handled = true;
                    return;
                case Key.F9:
                    // Volume up
                    vm.Volume = Math.Min(100, vm.Volume + 5);
                    e.Handled = true;
                    return;
                case Key.Escape:
                    if (vm.IsCompactMode)
                    {
                        vm.ToggleCompactModeCommand.Execute(null);
                        e.Handled = true;
                    }
                    else if (vm.IsNowPlayingActive)
                    {
                        vm.ActivePivot = NavigationPivot.Collection;
                        e.Handled = true;
                    }
                    else if (vm.HasHeaderSearchQuery)
                    {
                        vm.HeaderSearchQuery = string.Empty;
                        e.Handled = true;
                    }
                    return;
            }
        }
    }

    private void OnCompactModeChanged(object? sender, bool isCompact)
    {
        if (isCompact)
        {
            _preCompactWidth = Width;
            _preCompactHeight = Height;
            MinWidth = 360;
            MinHeight = 110;
            Width = 420;
            Height = 130;
            Topmost = (DataContext is MainShellViewModel vm) ? vm.SettingsVM.CompactModeAlwaysOnTop : true;
            CanResize = false;
        }
        else
        {
            MinWidth = 734;
            MinHeight = 500;
            Width = _preCompactWidth >= 734 ? _preCompactWidth : 1240;
            Height = _preCompactHeight >= 500 ? _preCompactHeight : 780;
            Topmost = false;
            CanResize = true;
        }
    }
}
