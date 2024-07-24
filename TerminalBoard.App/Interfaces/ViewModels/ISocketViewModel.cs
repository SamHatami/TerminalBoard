﻿using Caliburn.Micro;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Interfaces.Terminals;


namespace TerminalBoard.App.Interfaces.ViewModels;

public interface ISocketViewModel
{
    bool IsConnected { get; }
    string Label { get; set; }
    double X { get; set; }
    double Y { get; set; }
    Guid Id { get; }

    List<IWireViewModel> Wires { get; set; }
    ITerminalViewModel ParentViewModel { get; }
    SocketTypeEnum Type { get; }
    
    ISocket Socket { get; }
    IEventAggregator Events { get; }

    void UpdatePosition();
}