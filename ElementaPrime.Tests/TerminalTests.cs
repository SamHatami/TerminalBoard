using Caliburn.Micro;
using FluentAssertions;
using ElementaPrime.Core.Functions;
using ElementaPrime.Core.Interfaces.Functions;

namespace ElementaPrime.Tests
{
    
    //TODO: Tests needs to be rewritten to use the new type of terminals
    
    // public class TerminalTests
    // {
    //     public TerminalTests()
    //     {
    //         TerminalHelper.EventsAggregator = new EventAggregator();
    //     }
    //
    //     [Fact]
    //     public void FloatTerminalTest()
    //     {
    //         ITerminal floatTerminal = new ValueTerminal<float>();
    //
    //         IOutputTerminal outputTerminal = new SimpleOutputTerminal();
    //
    //         WireConnection wire = new WireConnection(floatTerminal.OutputSockets[0], outputTerminal.InputSockets[0],
    //             new TypedValue<float>("", Guid.NewGuid()) { Value = 10f });
    //
    //         floatTerminal.Connections.Add(wire);
    //         outputTerminal.Connections.Add(wire);
    //
    //         floatTerminal.UpdateInput(floatTerminal.OutputSockets[0], new TypedValue<float>("", Guid.NewGuid()) { Value = 14f });
    //
    //         outputTerminal.Output.Value.Should().Be(14f);
    //     }
    //
    //     [Fact]
    //     public void MultiplicationTerminalTest1()
    //     {
    //         // Two Float-terminals into an evaluation terminal
    //         var multiplication = new Multiplication();
    //         IEvaluationTerminal multiplicationTerminal = new EvaluationTerminal(multiplication);
    //
    //         ITerminal floatTerminal1 = new ValueTerminal<float>();
    //
    //         WireConnection wire1 = new WireConnection(
    //             floatTerminal1.OutputSockets[0],
    //             multiplicationTerminal.InputSockets[0],
    //             new TypedValue<float>("", Guid.NewGuid()) { Value = 0f }
    //         );
    //
    //         floatTerminal1.Connections.Add(wire1);
    //         multiplicationTerminal.Connections.Add(wire1);
    //
    //         ITerminal floatTerminal2 = new ValueTerminal<float>();
    //
    //         WireConnection wire2 = new WireConnection(
    //             floatTerminal2.OutputSockets[0],
    //             multiplicationTerminal.InputSockets[1],
    //             new TypedValue<float>("", Guid.NewGuid()) { Value = 0f }
    //         );
    //
    //         floatTerminal2.Connections.Add(wire2);
    //         multiplicationTerminal.Connections.Add(wire2);
    //
    //         floatTerminal1.UpdateInput(floatTerminal1.OutputSockets[0], new TypedValue<float>("", Guid.NewGuid()) { Value = 10f });
    //         floatTerminal2.UpdateInput(floatTerminal2.OutputSockets[0], new TypedValue<float>("", Guid.NewGuid()) { Value = 5f });
    //
    //         multiplicationTerminal.EvaluationFunction.Outputs[0].Value.Should().Be(50f);
    //     }
    //
    //     [Fact]
    //     public void MultiplicationTerminalTest2()
    //     {
    //         // Two Float-terminals into one evaluation terminal
    //         IEvaluationFunction multiplication = new Multiplication();
    //         IEvaluationTerminal multiplicationTerminal = new EvaluationTerminal(multiplication);
    //
    //         ITerminal floatTerminal1 = new ValueTerminal<float>();
    //
    //         WireConnection wire1 = new WireConnection(
    //             floatTerminal1.OutputSockets[0],
    //             multiplicationTerminal.InputSockets[0],
    //             new TypedValue<float>("", Guid.NewGuid()) { Value = 0f }
    //         );
    //
    //         floatTerminal1.Connections.Add(wire1);
    //         multiplicationTerminal.Connections.Add(wire1);
    //
    //         ITerminal floatTerminal2 = new ValueTerminal<float>();
    //
    //         WireConnection wire2 = new WireConnection(
    //             floatTerminal2.OutputSockets[0],
    //             multiplicationTerminal.InputSockets[1],
    //             new TypedValue<float>("", Guid.NewGuid()) { Value = 0f }
    //         );
    //
    //         floatTerminal2.Connections.Add(wire2);
    //         multiplicationTerminal.Connections.Add(wire2);
    //
    //         floatTerminal1.UpdateInput(floatTerminal1.OutputSockets[0], new TypedValue<float>("", Guid.NewGuid()) { Value = 10f });
    //         floatTerminal2.UpdateInput(floatTerminal2.OutputSockets[0], new TypedValue<float>("", Guid.NewGuid()) { Value = 5f });
    //
    //         // One float and one evaluation into a second evaluation terminal
    //
    //         IEvaluationFunction multiplication2 = new Multiplication();
    //         IEvaluationTerminal multiplicationTerminal2 = new EvaluationTerminal(multiplication2);
    //
    //         WireConnection wire3 = new WireConnection(
    //             multiplicationTerminal.OutputSockets[0],
    //             multiplicationTerminal2.InputSockets[0],
    //             multiplicationTerminal.EvaluationFunction.Outputs[0]
    //         );
    //
    //         multiplicationTerminal.Connections.Add(wire3);
    //         multiplicationTerminal2.Connections.Add(wire3);
    //
    //         ITerminal floatTerminal3 = new ValueTerminal<float>();
    //
    //         WireConnection wire4 = new WireConnection(
    //             floatTerminal3.OutputSockets[0],
    //             multiplicationTerminal2.InputSockets[1],
    //             new TypedValue<float>("", Guid.NewGuid()) { Value = 0f }
    //         );
    //
    //         floatTerminal3.Connections.Add(wire4);
    //         multiplicationTerminal2.Connections.Add(wire4);
    //
    //         floatTerminal1.UpdateInput(floatTerminal1.OutputSockets[0], new TypedValue<float>("", Guid.NewGuid()) { Value = 10f });
    //         floatTerminal2.UpdateInput(floatTerminal2.OutputSockets[0], new TypedValue<float>("", Guid.NewGuid()) { Value = 5f });
    //         floatTerminal3.UpdateInput(floatTerminal3.OutputSockets[0], new TypedValue<float>("", Guid.NewGuid()) { Value = 3f });
    //
    //         multiplicationTerminal.EvaluationFunction.Outputs[0].Value.Should().Be(50f);
    //         multiplicationTerminal2.EvaluationFunction.Outputs[0].Value.Should().Be(150f);
    //     }
    // }
}