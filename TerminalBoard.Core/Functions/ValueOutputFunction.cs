using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Functions;

internal class ValueOutputFunction : IValueFunction
{
    public string Label { get; } = "Float";
    public bool RequireValueInput { get; } = true;

    private IValue _output;

    public IValue Output
    {
        get => _output;
        set
        {
            _output = value;
        }
    }

    public void SetValue(IValue value)
    {
        if (value == null)
            return;

        _output = value;
    }

}