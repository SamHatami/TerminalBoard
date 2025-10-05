namespace ElementaPrime.Core.Interfaces.Graphs.Terminals;

public interface IValueOperator<T> : IOperator
{
    T Value { get; set; }

}