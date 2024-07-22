using System.Numerics;
using Caliburn.Micro;
using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Functions;

public class ValueOutputFunction<T> : PropertyChangedBase,IValueFunction<T> where T : INumber<T>
{
    public string Label { get; } = "Float";
    public bool RequireValueInput { get; } = true;

    private T? _output;

    public T? Output
    {
        get => _output;
        set
        {
            _output = value;
            NotifyOfPropertyChange(nameof(Output));
        }
    }

    public void SetValue(T? value)
    {
        if(value == null)
            return;

        _output = value;
    }
}