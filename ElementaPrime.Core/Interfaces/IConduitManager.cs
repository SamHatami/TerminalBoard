using ElementaPrime.Core.Events.TerminalEvents;
using ElementaPrime.Core.Interfaces.Functions;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Interfaces
{
    public interface IConduitManager
    {
        public void ConnectSockets(IDataPort inputDataPort, IDataPort outputDataPort);
        public void TerminalRemoved(IOperator @operator);
        public Task HandleAsync(TerminalRemovedEvent message, CancellationToken cancellationToken);
        public IConduit CreateConnection(IDataPort input, IDataPort output, IValue value);
    }
}