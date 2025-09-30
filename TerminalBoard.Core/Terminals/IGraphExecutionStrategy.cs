namespace TerminalBoard.Core.Terminals
{
    public interface IGraphExecutionStrategy
    {
        void Execute();

        Task ExecuteAsync();
    }
}