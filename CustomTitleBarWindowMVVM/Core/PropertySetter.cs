using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomTitleBarWindowMVVM.Core;

public abstract class PropertySetter : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Sets the field to the given value if not equal and fires the <see cref="PropertyChanged"/> event.
    /// </summary>
    /// <typeparam name="T">Type of the field and value</typeparam>
    /// <param name="field">The field to conditionally set</param>
    /// <param name="newValue">The new value</param>
    /// <param name="propertyName">The name of the property the field backs</param>
    /// <returns>True if the value was set, false otherwise.</returns>
    protected virtual bool Set<T>(ref T? field, T? newValue = default, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field!, newValue!))
        {
            return false;
        }

        field = newValue;

        OnPropertyChanged(propertyName);

        return true;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
