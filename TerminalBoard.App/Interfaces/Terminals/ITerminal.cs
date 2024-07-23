﻿using Caliburn.Micro;
using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Interfaces.Terminals;

/// <summary>
/// Base interface for a Terminal which contains inputs and outputs.
/// </summary>
public interface ITerminal
{
    #region Properties

    string Label { get; }
    List<ISocket> InputSockets { get; }
    List<ISocket> OutputSockets { get; }
    List<IWire> Connections { get; set; }
    
    void UpdateInput(ISocket socket, IValue newValue);
    Guid Id { get; }

    #endregion Properties

}