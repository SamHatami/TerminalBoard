using TerminalBoard.Core.Interfaces.Functions;

namespace TerminalBoard.Core.Interfaces.Terminals
{
    /// <summary>
    /// Connection class to keep which sockets are connected and which value is beeing transfered
    /// </summary>
    public interface IWire
    {
        ISocket StartSocket { get; set; }
        ISocket EndSocket { get; set; }
        IValue Value { get; set;  }

        Guid Id { get; }
    }
}
