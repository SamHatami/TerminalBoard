namespace TerminalBoard.Core.Interfaces.Functions
{
    public interface IValue
    {   
        object Value{ get; set; } //Generics?
        string Name { get; }
        Guid Id { get; }
    }

    public interface IValue<T> : IValue
    {
        new T Value { get; set; }
    }
}