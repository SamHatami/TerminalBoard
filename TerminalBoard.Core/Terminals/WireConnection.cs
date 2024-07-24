using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class WireConnection : IWire
{
    public ISocket StartSocket { get; set; }
    public ISocket EndSocket { get; set; }

    private IValue _value;
    public IValue Value
    {
        get => _value;
        set
        {
       
                _value = value;
                TransferData(_value);
        }
    } 
    public Guid Id { get; } = Guid.NewGuid();

    public WireConnection(ISocket startSocket, ISocket endSocket, IValue value)
    {
        _value = value;
        StartSocket = startSocket;
        EndSocket = endSocket;
    }

    private void TransferData(IValue value)
    {
        EndSocket.ParentTerminal.UpdateInput(EndSocket,value);
    }
}