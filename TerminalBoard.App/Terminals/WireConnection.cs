using TerminalBoard.App.Interfaces.Functions;
using TerminalBoard.App.Interfaces.Terminals;

namespace TerminalBoard.App.Terminals;

internal class WireConnection : IWire
{
    public ISocket StartSocket { get; set; }
    public ISocket EndSocket { get; set; }

    private IValue _value;
    public IValue Value
    {
        get => _value;
        set
        {
            if (!EqualityComparer<IValue>.Default.Equals(_value, value))
            {
                _value = value;
                TransferData(_value);
            }
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