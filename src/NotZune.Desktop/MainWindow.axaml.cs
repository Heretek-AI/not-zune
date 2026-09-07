using System;
using Avalonia.Controls;
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
            Topmost = true;
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
