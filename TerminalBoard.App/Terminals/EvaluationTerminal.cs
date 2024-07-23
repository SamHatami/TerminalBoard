using Caliburn.Micro;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Functions;
using TerminalBoard.App.Interfaces.Functions;
using TerminalBoard.App.Interfaces.Terminals;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TerminalBoard.App.Terminals;

public class EvaluationTerminal : IEvaluationTerminal
{
    public EvaluationTerminal(IEvaluationFunction evaluationFunction)
    {
        EvaluationFunction = evaluationFunction;
        Connections = [];
        Id = Guid.NewGuid();
        Label = EvaluationFunction.Label;
        InitializeSockets();

    }

    public string Label { get; }
    public List<ISocket> InputSockets { get; private set; } = [];
    public List<ISocket> OutputSockets { get; } = [];

    private List<IWire> _connections; 
    public List<IWire> Connections
    {
        get => _connections;
        set
        {
            _connections = value;
            Connect();
        }
    } 

    public Guid Id { get; }
    public IEventAggregator Events { get; }

    public IEvaluationFunction EvaluationFunction { get; }

    private void InitializeSockets()
    {

        InputSockets.Add(new Socket(SocketTypeEnum.Input, "", this)); //TODO: cast?
        InputSockets.Add(new Socket(SocketTypeEnum.Input, "", this)); //TODO: cast?

        List<IValue> temp = new List<IValue>();
        foreach (var socket in InputSockets)
        {
            temp.Add(new FloatValue(1.0f,"", socket.Id));
        }
  
        SetFunctionInputs(temp);
            

        foreach (var output in EvaluationFunction.Outputs)
            OutputSockets.Add(new Socket(SocketTypeEnum.Output, output.Name, this)); //TODO: cast?
    }


    public void NotifyConnectors()
    {
        foreach (var connectionWire in Connections)
        {
            connectionWire.Value = EvaluationFunction.Outputs[0];
        }
    }

    public void SetFunctionInputs(List<IValue> inputs)
    {
        EvaluationFunction.Inputs = inputs;
    }

    public void Connect()
    {
        // check what the connection id Is and set the input socket 
   
    }

    public void UpdateInput(ISocket socket, IValue newValue)
    {
   
        EvaluationFunction.SetInputValue(newValue, socket.Id);
        NotifyConnectors();
    }

}