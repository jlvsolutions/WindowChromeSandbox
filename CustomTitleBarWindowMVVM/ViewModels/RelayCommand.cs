using System;
using System.Windows.Input;

namespace CustomTitleBarWindowMVVM.ViewModels;

public class RelayCommand : ICommand
{
    // Fields
    private readonly Action<object?> _executeAction;
    private readonly Predicate<object?> _canExecuteAction;


    // Constructor
    public RelayCommand(Action<object?> executeAction, Predicate<object?> canExecuteAction = null!)
    {
        _executeAction = executeAction;
        _canExecuteAction = canExecuteAction;
    }

    //Events
    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    //Methods
    public bool CanExecute(object? parameter)
    {
        return _canExecuteAction == null ? true : _canExecuteAction(parameter);
    }

    public void Execute(object? parameter)
    {
        _executeAction(parameter);
    }
}
