namespace TerminalBoard.App.Interfaces;

/// <summary>
/// Interface for a dragable item.
/// </summary>
public interface ITerminal
{
    #region Properties

    string Label { get; }
    List<ISocket> InputSockets { get; }
    List<ISocket> OutputSockets { get; }
    List<ITerminal> Connectors { get; set; }

    bool RequireInputValue { get; }
    Guid Id { get; }

    #endregion Properties

    //TODO: Future wire connections
}