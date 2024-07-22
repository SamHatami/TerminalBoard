using System.Numerics;

namespace TerminalBoard.App.Functions;

public class NumericValue<TValue> where TValue : INumber<TValue>
{
    public string Label { get; set; }
    public TValue Value { get; set; }

    public NumericValue(TValue value, string label)
    {
        Value = value;
        Label = label;
    }
}