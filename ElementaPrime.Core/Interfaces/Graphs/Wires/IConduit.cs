using ElementaPrime.Core.Interfaces.Functions;
using ElementaPrime.Core.Interfaces.Graphs.Sockets;

namespace ElementaPrime.Core.Interfaces.Graphs.Wires
{
    /// <summary>
    /// Connection class to keep which sockets are connected and which value is beeing transfered
    /// </summary>
    public interface IConduit
    {
        IDataPort Start { get; set; }
        IDataPort End { get; set; }
        IValue Value { get; set;  }

        Guid Id { get; }
    }
}
