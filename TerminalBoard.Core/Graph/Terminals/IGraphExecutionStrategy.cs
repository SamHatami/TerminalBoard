namespace TerminalBoard.Core.Graph.Terminals
{
    public interface IGraphExecutionStrategy
    {
        void Execute();

        Task ExecuteAsync();
    }
}