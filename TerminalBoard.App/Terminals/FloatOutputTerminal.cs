using TerminalBoard.App.Enum;
using TerminalBoard.App.Interfaces;
using TerminalBoard.App.Interfaces.Functions;

namespace TerminalBoard.App.Terminals;

public class FloatOutputTerminal: IValueTerminal<float>
{
    public string Label { get; }
    public List<ISocket> InputSockets { get; } = [];
    public List<ISocket> OutputSockets { get; } = [];
    public List<ITerminal> Connectors { get; set; }
    public bool RequireInputValue { get; } = true;
    public Guid Id { get; }
    public IValueFunction<float> Function { get; }
    
    public FloatOutputTerminal()
    {
        Function = new Functions.ValueOutputFunction<float>();
        Label = Function.Label;
        Initialize();
        
    }

    private void Initialize()
    {
        OutputSockets.Add(new Socket(SocketTypeEnum.Output, ""));
        Function.SetValue(1.0f);
    }
}