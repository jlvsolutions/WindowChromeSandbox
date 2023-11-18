using CustomTitleBarWindowMVVM.ViewModels;
using System;
using System.Windows;
using System.Windows.Interop;

namespace CustomTitleBarWindowMVVM;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        DataContext = new WindowViewModel(this);
    }
}
