using ElementaPrime.Core.Interfaces.Functions;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;
using ElementaPrime.Core.Interfaces.Graphs.Wires;

namespace ElementaPrime.Core.Graphs.Conduits;

//Wires hold the value of the terminals that are connected to it
public class ConduitConnection : IConduit
{
    public IDataPort Start { get; set; }
    public IDataPort End { get; set; }

    private IValue _value;
    public IValue Value
    {
        get => _value;
        set => _value = value;
    }
    public Guid Id { get; } = Guid.NewGuid();

    public ConduitConnection(IDataPort startDataPort, IDataPort endDataPort, IValue value) //Type check of sockets happens in WireConnectionValidator
    {
        _value = value;
        Start = startDataPort;
        End = endDataPort;
    }

    //This should probably not be here at all, no logic in this class, maybe extensions?
    public IValue? GetCorrespondingSocketValue(IDataPort dataPort)
    {
        return dataPort.Id == End.Id || dataPort.Id == Start.Id ? Value : null;
    }
}