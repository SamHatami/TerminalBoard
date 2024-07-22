using System.Numerics;
using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Functions.Math;

public class Multiplication<T> : IEvaluationFunction<T> where T : INumber<T>
{
    public string Label { get; } = "Multiply";
    public List<Input<T>> Inputs { get; } = [];
    public List<Output<T>> Outputs { get; } = [];

    public void SetInputValues(Input<T>[] inputs)
    {
        Inputs[0].Value = inputs[0].Value;
        Inputs[1].Value = inputs[1].Value;
    }

    public void Evaluate() //This is the output
    {
        Outputs[0].Value = Inputs[0].Value * Inputs[1].Value;
    }
}