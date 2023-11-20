using System.Windows;
using System.Windows.Input;

namespace CustomTitleBarWindowMVVM.ViewModels;

public class MainWindowViewModel : ChromeViewModelBase
{

    public ICommand OpenNewWindow { get; }

    // Ctor
    public MainWindowViewModel(Window window) : base(window)
    {
        OpenNewWindow = new RelayCommand(OnNewWindow);
    }

    private void OnNewWindow(object? obj)
    {
        NoMinMaxWindow newWin = new NoMinMaxWindow();
        newWin.DataContext = new NoMinMaxViewModel(newWin);

        newWin.Show();
    }
}
