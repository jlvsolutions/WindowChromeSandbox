using CustomTitleBarWindowMVVM.ViewModels;
using System;
using System.Windows;
using System.Windows.Interop;

namespace CustomTitleBarWindowMVVM;

/// <summary>
/// Interaction logic for NoMinMaxWindow.xaml
/// </summary>
public partial class NoMinMaxWindow : Window
{
    public NoMinMaxWindow()
    {
        InitializeComponent();
    }

    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        ((HwndSource)PresentationSource.FromVisual(this)).AddHook(User32Helpers.HookProc);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
