﻿namespace TerminalBoard.App.Interfaces.Functions
{
    public interface IValue
    {   
        object ValueObject { get; set; }
        string Name { get; }
        Guid Id { get; }
    }
}