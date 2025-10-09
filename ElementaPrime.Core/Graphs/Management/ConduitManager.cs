using Caliburn.Micro;
using ElementaPrime.Core.Events.TerminalEvents;
using ElementaPrime.Core.Functions;
using ElementaPrime.Core.Graphs.Conduits;
using ElementaPrime.Core.Interfaces;
using ElementaPrime.Core.Interfaces.Functions;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Terminals;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Graphs.Management;

internal class ConduitManager(): IHandle<TerminalRemovedEvent>, IConduitManager
{
    public void ConnectSockets(IDataPort inputDataPort, IDataPort outputDataPort)
    {
      
        ConduitConnection newConnection = new ConduitConnection(inputDataPort,
            outputDataPort, new TypedValue<object>("", Guid.NewGuid())); 

        inputDataPort.ParentOperator.Connections.Add(newConnection);
        outputDataPort.ParentOperator.Connections.Add(newConnection);

    }

    public void TerminalRemoved(IOperator @operator)
    {
        foreach (var connection in @operator.Connections)
        {
            connection.End.ParentOperator.Connections.Remove(connection);
        }
    }

    public Task HandleAsync(TerminalRemovedEvent message, CancellationToken cancellationToken)
    {
        TerminalRemoved(message.Operator);

        return Task.CompletedTask;
    }

    public IConduit CreateConnection(IDataPort input, IDataPort output, IValue value)
    {
        return new ConduitConnection(input, output, value);
    }
}