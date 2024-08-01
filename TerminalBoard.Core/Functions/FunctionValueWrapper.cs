using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Functions;

public class FunctionValueWrapper
{
    public List<IValue> Inputs { get; set; } = [];
    public List<IValue> Outputs { get; } = [];

    public void AddInput<T>(IValue<T> value)
    {
        Inputs.Add(value);
    }

    public void AddOutput<T>(IValue<T> value)
    {
        Outputs.Add(value);
    }
    
    public TypedValue<T> GetInput<T>(int index)
    {
        return (TypedValue<T>)Inputs[index];
    }

    public TypedValue<T> GetOutput<T>(int index)
    {
        return (TypedValue<T>)Outputs[index];
    }
}