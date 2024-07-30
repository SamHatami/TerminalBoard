namespace TerminalBoard.Core.Interfaces.Functions
{
    public interface IValue
    {   
        object ValueObject { get; set; } //Generics?
        string Name { get; }
        Guid Id { get; }
    }
}