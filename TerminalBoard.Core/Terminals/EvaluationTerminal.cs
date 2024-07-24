using Caliburn.Micro;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

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
        foreach (var wire in Connections)
        {
            if(wire.StartSocket.ParentTerminal.Id == this.Id) //Only notify outbound connections
                wire.Value = EvaluationFunction.Outputs[0];
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