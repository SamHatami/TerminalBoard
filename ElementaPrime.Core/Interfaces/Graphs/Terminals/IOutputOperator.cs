namespace ElementaPrime.Core.Interfaces.Graphs.Terminals;

public interface IOutputOperator : IOperator
{
    bool ShowFinalOutputValue { get; }
}