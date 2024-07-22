using System.Numerics;
using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Functions.Math;

public class Multiplication<T> where T : INumber<T>, IFunction
{
    public string Name => "Multiply";

    public T Evaluate(T a, T b) //This is the output
    {
        return a * b;
    }
}