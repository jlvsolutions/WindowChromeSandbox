using System;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Interop;

namespace CustomTitleBarWindowMVVM;

public class ChromeWindow : Window
{
    protected override void OnSourceInitialized(EventArgs e)
    {
        base.OnSourceInitialized(e);
        ((HwndSource)PresentationSource.FromVisual(this)).AddHook(User32Helpers.HookProc);
    }
}
