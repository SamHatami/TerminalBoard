using System.Numerics;
using TerminalBoard.App.Interfaces.Functions;
using Action = System.Action;

namespace TerminalBoard.App.Functions;

public class ValueOutputFunction : IValueFunction
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