using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Graph
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