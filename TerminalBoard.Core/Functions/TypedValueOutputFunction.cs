using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Functions;

internal class TypedValueOutputFunction<T> : ITypedValueFunction<T>
{
    public string Label { get; }
    public bool RequireValueInput { get; } = true;

    private TypedValue<T> _output;

    public TypedValue<T> Output
    {
        get => _output;
        set
        {
            _output = value;
        }
    }

    public void SetValue(TypedValue<T> value)
    {
        if (value == null)
            return;

        _output = value;
    }

}