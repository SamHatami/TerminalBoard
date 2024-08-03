using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Math.Operators;

public class Addition : IEvaluationFunction
{
    public string Label { get; } = "Divide";
    public List<IValue> Inputs => _functionValues.Inputs;
    public List<IValue> Outputs => _functionValues.Outputs;

    private readonly FunctionValueWrapper _functionValues = new();

    public Addition()
    {
        Initalize();
    }

    private void Initalize()
    {
        Inputs.Add(new TypedValue<float>("Float", Guid.NewGuid()) { Value = 1.0f });
        Inputs.Add(new TypedValue<float>("Float", Guid.NewGuid()) { Value = 1.0f });
        Outputs.Add(new TypedValue<float>("Out", Guid.NewGuid()) { Value = 0 });
    }

    public void SetInputValue(IValue newValue, IValue actualValue)
    {
        var index = Inputs.IndexOf(actualValue);
        if (index == -1 || newValue.Value.GetType() != actualValue.Value.GetType())
            return; //This should be covered from the UI as Socket connection should not happen if the types do not match

        _functionValues.Inputs[index].Value = newValue.Value;

        Evaluate();
    }

    public void Evaluate()
    {
        if (_functionValues.GetInput<float>(1).Value == 0f) Outputs[0].Value = 0f;

        var product = _functionValues.GetInput<float>(0).Value / _functionValues.GetInput<float>(1).Value;
        Outputs[0].Value = product;
    }
}