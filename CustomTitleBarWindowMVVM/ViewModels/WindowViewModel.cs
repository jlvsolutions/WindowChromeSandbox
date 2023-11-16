using Accessibility;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace CustomTitleBarWindowMVVM.ViewModels;

public class WindowViewModel : ViewModelBase
{
    private readonly Window _window;

    /// <summary>
    /// The size of the resize border around the window
    /// </summary>
    public double ResizeBorder { get; set; } = 6D;
    /// <summary>
    /// The size of teh resize border around the window, taking into account the outer margin
    /// </summary>
    public Thickness ResizeBorderThickness => new Thickness(ResizeBorder + OuterMarginSize);

    private int _outerMarginSize = 10;
    /// <summary>
    /// The margin around the window to allow for a drop shadow
    /// </summary>
    public int OuterMarginSize
    {
        get => _window.WindowState == WindowState.Maximized ? 0 : _outerMarginSize;
        set => Set(ref _outerMarginSize, value);
    }

    /// <summary>
    /// The margin around the window to allow for a drop shadow
    /// </summary>
    public Thickness OuterMarginSizeThickness => new Thickness(OuterMarginSize);


    private int _windowRadius = 6;
    /// <summary>
    /// The radius of the window corners
    /// </summary>
    public int WindowRadius
    {
        get => _window.WindowState == WindowState.Maximized ? 0 : _windowRadius;
        set => Set(ref _windowRadius, value);
    }

    /// <summary>
    /// The radius of the window corners
    /// </summary>
    public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);

    /// <summary>
    /// The height of the title bar / caption of the window
    /// </summary>
    public int TitleHeight { get; set; } = 42;  
    public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);
    
    
    // TODO:  ViewModel conversions for CornerRadius, Thickness, and GridLength may not be needed as far as binding goes
    
    public ICommand SystemMenuCommand { get; }
    public ICommand MinimizeCommand { get; }
    public ICommand MaximizeCommand { get; }
    public ICommand CloseCommand { get; }
    

    //
    // Ctor
    public WindowViewModel(Window window)
    {
        _window = window; // TODO:  uumm, this makes the view model aware of the view instance....

        SystemMenuCommand = new RelayCommand((obj) => SystemCommands.ShowSystemMenu(_window, GetMouseScreenPosition()));
        MinimizeCommand = new RelayCommand((obj) => _window.WindowState = WindowState.Minimized);
        MaximizeCommand = new RelayCommand((obj) => _window.WindowState ^= WindowState.Maximized);
        CloseCommand = new RelayCommand((obj) => _window.Close());

        // Notify for all properties that are affected by a resize
        _window.StateChanged += (sender, e) =>
        {
            OnPropertyChanged(nameof(ResizeBorderThickness));
            OnPropertyChanged(nameof(OuterMarginSize));
            OnPropertyChanged(nameof(OuterMarginSizeThickness));
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
        };

    }

    /// <summary>
    /// Gets the current mouse position on the screen
    /// </summary>
    /// <returns></returns>
    private Point GetMouseScreenPosition()
    {
        // Position of the mouse relative to the window
        Point windowPosition = Mouse.GetPosition(_window);
        
        // Add the window position so its a "ToScreen"
        return new Point(windowPosition.X + _window.Left, windowPosition.Y + _window.Top);
    }


    #region user32 dll import for mouse position
    [DllImport("User32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool GetCursorPos(ref Win32Point pt);

    [StructLayout(LayoutKind.Sequential)]
    internal struct Win32Point
    {
        public Int32 X;
        public Int32 Y;
    };

    private static Point GetMousePosition()
    {
        Win32Point pt = new Win32Point();
        GetCursorPos(ref pt);
        return new Point(pt.X, pt.Y);
    }
    #endregion 
}
