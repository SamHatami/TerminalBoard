using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Interfaces.Terminals
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
