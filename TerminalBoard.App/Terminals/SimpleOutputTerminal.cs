using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Terminals;


namespace TerminalBoard.App.Interfaces.Terminals
{
    class SimpleOutputTerminal : IOutputTerminal
    {
        public string Label { get; } = "Output";
        public List<ISocket> InputSockets { get; } = [];
        public List<ISocket> OutputSockets { get; } = [];
        public List<ITerminal> Connectors { get; set; } = [];
        public bool ShowFinalOutputValue { get; } = true;
        public Guid Id { get; }

        public SimpleOutputTerminal()
        {
            Id = Guid.NewGuid();

            Initialize();
        }

        private void Initialize()
        {
            InputSockets.Add(new Socket(SocketTypeEnum.Input,"In"));
        }

   
    }
}
