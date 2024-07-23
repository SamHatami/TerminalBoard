using System.Numerics;
using TerminalBoard.App.Interfaces.Functions;
using TerminalBoard.App.Terminals;

namespace TerminalBoard.App.Functions.Math;

public class Multiplication : IEvaluationFunction
{
    public string Label { get; } = "Multiply";
    public List<IValue> Inputs { get; set; } = [];
    public List<IValue> Outputs { get; } = [];

    public Multiplication()
    {
        Initalize();
    }

    private void Initalize()
    {
       Outputs.Add(new FloatValue(0, "Out", Guid.NewGuid()));
    }


    public void SetInputValue(IValue value, Guid socketId)
    {
        
        var index = Inputs.FindIndex(i => i.Id == socketId);
        if(index == -1) return;

        Inputs[index] = value;
        Evaluate();
    }

    public void Evaluate() //This is the output
    {
        Outputs[0].ValueObject = (float)Inputs[0].ValueObject * (float)Inputs[1].ValueObject;
    }
}