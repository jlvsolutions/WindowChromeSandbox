using CustomTitleBarWindowMVVM.Core;
using System.Windows.Input;
using System.Windows;

namespace CustomTitleBarWindowMVVM.ViewModels;

public abstract class ChromeViewModelBase : PropertySetter
{
    private readonly Window _window;

    /// <summary>
    /// The size of the resize border around the window
    /// </summary>
    public double ResizeBorder { get; set; } = 6D;

    /// <summary>
    /// The size of the resize border around the window, taking into account the outer margin
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

    private double _windowRadius = 15D;
    /// <summary>
    /// The radius of the window corners
    /// </summary>
    public double WindowRadius
    {
        get => _window.WindowState == WindowState.Maximized ? 0D : _windowRadius;
        set => Set(ref _windowRadius, value);
    }

    /// <summary>
    /// The radius of the window corners
    /// </summary>
    public CornerRadius WindowCornerRadius => new CornerRadius(WindowRadius);
    public CornerRadius WindowTitleCornerRadius => new CornerRadius(WindowRadius, WindowRadius, 0D, 0D);
    public CornerRadius WindowContentCornerRadius => new CornerRadius(0D, 0D, WindowRadius, WindowRadius);

    /// <summary>
    /// The height of the title bar / caption of the window
    /// </summary>
    public int TitleHeight { get; set; } = 28;
    public GridLength TitleHeightGridLength => new GridLength(TitleHeight + ResizeBorder);

    //
    // Commands
    public ICommand SystemMenuCommand { get; }
    public ICommand MinimizeCommand { get; }
    public ICommand MaximizeCommand { get; }
    public ICommand CloseCommand { get; }

    //
    // Ctor
    public ChromeViewModelBase(Window window)
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
            OnPropertyChanged(nameof(WindowRadius));
            OnPropertyChanged(nameof(WindowCornerRadius));
            OnPropertyChanged(nameof(WindowTitleCornerRadius));
            OnPropertyChanged(nameof(WindowContentCornerRadius));
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
}
