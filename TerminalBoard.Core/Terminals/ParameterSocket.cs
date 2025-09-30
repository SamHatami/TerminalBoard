using System.Reflection;
using TerminalBoard.Core.Enum;
using TerminalBoard.Core.Extensions;
using TerminalBoard.Core.Interfaces.Terminals;

namespace TerminalBoard.Core.Terminals;

public class ParameterSocket : ISocket
{
    public Type ParameterType { get; set; }
    public int ParameterPosition { get; set; }
    public bool IsOptional { get; }
    public SocketTypeEnum SocketType { get; }
    public bool IsConnected { get; set; }
    public ITerminal ParentTerminal { get; }
    public string Name { get; }
    public Guid Id { get; }

    public ParameterSocket(string parameterName, Type parameterType, bool isOptional,
        SocketTypeEnum socketType, ITerminal parentTerminal)
    {
        ParameterType = parameterType;
        IsOptional = isOptional;
        Name = parameterName;
        ParentTerminal = parentTerminal;
        SocketType = socketType;
    }

    public ParameterSocket(ParameterInfo? parameter, SocketTypeEnum socketType, ITerminal parentTerminal)
    {
        if (parameter == null)
            return;

        Name = parameter.ParameterType.GetAliasName() ?? "Unknown";
        ParameterPosition = parameter.Position;
        IsOptional = parameter.IsOptional;
        ParameterType = parameter.ParameterType;
        SocketType = socketType;
        ParentTerminal = parentTerminal;
    }

    public void SetToConnected()
    {
        IsConnected = true;
    }
}