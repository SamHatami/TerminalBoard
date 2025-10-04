namespace TerminalBoard.Core.Interfaces.Graphs.Execution
{
    public interface IGraphExecutionStrategy
    {
        void Execute();

        Task ExecuteAsync();
    }
}