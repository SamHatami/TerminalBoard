namespace TerminalBoard.App.Interfaces;

/// <summary>
/// Interface for a Terminal which contains inputs and outputs.
/// </summary>
public interface ITerminal
{
    #region Properties

    string Label { get; }
    List<ISocket> InputSockets { get; }
    List<ISocket> OutputSockets { get; }
    List<ITerminal> Connectors { get; set; }
    Guid Id { get; }

    #endregion Properties

    //TODO: Future wire connections
}