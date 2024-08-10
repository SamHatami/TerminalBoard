using Caliburn.Micro;
using FluentAssertions;
using TerminalBoard.Core.Events.TerminalEvents;
using TerminalBoard.Core.Functions;
using TerminalBoard.Core.Interfaces.Functions;
using TerminalBoard.Core.Interfaces.Terminals;
using TerminalBoard.Core.Terminals;
using TerminalBoard.Core.Wires;

namespace TerminalBoard.Test
{
    public class TerminalTests
    {
        public TerminalTests()
        {
            TerminalHelper.EventsAggregator = new EventAggregator();
        }
        [Fact]
        public void FloatTerminalTest()
        {
            
           
            ITerminal floatTerminal = new FloatValueTerminal();

            IOutputTerminal outputTerminal = new SimpleOutputTerminal();

            WireConnection wire = new WireConnection(floatTerminal.OutputSockets[0], outputTerminal.InputSockets[0],
                new FloatValue(10f, "", Guid.NewGuid()));

            floatTerminal.Connections.Add(wire);
            outputTerminal.Connections.Add(wire);

            floatTerminal.UpdateInput(floatTerminal.OutputSockets[0], new FloatValue(14f, "", Guid.NewGuid()));

            outputTerminal.Output.ValueObject.Should().Be(14f);
        }

        [Fact]
        public void MultiplicationTerminalTest1()
        {
            //Two Float-terminals into a evaluationterminal
            IEvaluationFunction multiplication = new Multiplication();
            IEvaluationTerminal multiplicationTerminal = new EvaluationTerminal(multiplication);

            ITerminal floatTerminal1 = new FloatValueTerminal();

            WireConnection wire1 = new WireConnection(floatTerminal1.OutputSockets[0], multiplicationTerminal.InputSockets[0],
                new FloatValue(0f, "", Guid.NewGuid()));

            floatTerminal1.Connections.Add(wire1);
            multiplicationTerminal.Connections.Add(wire1);

            ITerminal floatTerminal2 = new FloatValueTerminal();

            WireConnection wire2 = new WireConnection(floatTerminal2.OutputSockets[0], multiplicationTerminal.InputSockets[1],
                new FloatValue(0f, "", Guid.NewGuid()));

            floatTerminal2.Connections.Add(wire2);
            multiplicationTerminal.Connections.Add(wire2);

            floatTerminal1.UpdateInput(floatTerminal1.OutputSockets[0], new FloatValue(10f, "", Guid.NewGuid()));
            floatTerminal2.UpdateInput(floatTerminal2.OutputSockets[0], new FloatValue(5f, "", Guid.NewGuid()));

            multiplicationTerminal.EvaluationFunction.Outputs[0].ValueObject.Should().Be(50f);

        }

        [Fact]
        public void MultiplicationTerminalTest2()
        {
            //two Float-terminals into one evaluationterminal
            IEvaluationFunction multiplication = new Multiplication();
            IEvaluationTerminal multiplicationTerminal = new EvaluationTerminal(multiplication);

            ITerminal floatTerminal1 = new FloatValueTerminal();

            WireConnection wire1 = new WireConnection(floatTerminal1.OutputSockets[0], multiplicationTerminal.InputSockets[0],
                new FloatValue(0f, "", Guid.NewGuid()));

            floatTerminal1.Connections.Add(wire1);
            multiplicationTerminal.Connections.Add(wire1);

            ITerminal floatTerminal2 = new FloatValueTerminal();

            WireConnection wire2 = new WireConnection(floatTerminal2.OutputSockets[0], multiplicationTerminal.InputSockets[1],
                new FloatValue(0f, "", Guid.NewGuid()));

            floatTerminal2.Connections.Add(wire2);
            multiplicationTerminal.Connections.Add(wire2);

            floatTerminal1.UpdateInput(floatTerminal1.OutputSockets[0], new FloatValue(10f, "", Guid.NewGuid()));
            floatTerminal2.UpdateInput(floatTerminal2.OutputSockets[0], new FloatValue(5f, "", Guid.NewGuid()));

            //one float and one evaluation into a second evaluationterminal

            IEvaluationFunction multiplication2 = new Multiplication();
            IEvaluationTerminal multiplicationTerminal2 = new EvaluationTerminal(multiplication2);

            WireConnection wire3 = new WireConnection(multiplicationTerminal.OutputSockets[0],
                multiplicationTerminal2.InputSockets[0], multiplicationTerminal.EvaluationFunction.Outputs[0]);

            multiplicationTerminal.Connections.Add(wire3);
            multiplicationTerminal2.Connections.Add(wire3);

            ITerminal floatTerminal3 = new FloatValueTerminal();

            WireConnection wire4 = new WireConnection(floatTerminal3.OutputSockets[0],
                multiplicationTerminal2.InputSockets[1], new FloatValue(0f, "", Guid.NewGuid()));

            floatTerminal3.Connections.Add(wire4);
            multiplicationTerminal2.Connections.Add(wire4);

            floatTerminal1.UpdateInput(floatTerminal1.OutputSockets[0], new FloatValue(10f, "", Guid.NewGuid()));
            floatTerminal2.UpdateInput(floatTerminal2.OutputSockets[0], new FloatValue(5f, "", Guid.NewGuid()));
            floatTerminal3.UpdateInput(floatTerminal3.OutputSockets[0], new FloatValue(3f,"", Guid.NewGuid()));

            multiplicationTerminal.EvaluationFunction.Outputs[0].ValueObject.Should().Be(50f);
            multiplicationTerminal2.EvaluationFunction.Outputs[0].ValueObject.Should().Be(150f);

        }

    }
}