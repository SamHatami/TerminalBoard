using TerminalBoard.Core.Interfaces.Graphs.Terminals;

namespace TerminalBoard.Core.Graphs.Management
{
    //Creates and manages terminals
    public class TerminalManager
    {
        List<ITerminal> _terminals = new();
        public TerminalManager()
        {
            Initalize();
        }

        private void Initalize()
        {

        }
    }
}