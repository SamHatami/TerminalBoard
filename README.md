Just because there is to much chaos in the garden of my mind: 
A venture in how a node editor could be built using WPF.  
## Dependencies / NuGet

WPF
- Caliburn.Micro (latest version)

Testing
- xUnit
- FluentAssertion

## Installation 
Clone and Restore NuGet packages if neede etc. Solution created in Visual Studio 2022.
## Project Structure

* *TerminalBoard.Core*  
	 Includes all base interfaces, classes and events that are independent of WPF
* *TerminalBoard.App* 
	 is the WPF project that includes all UI stuff. The Idea is to seperate general UI classes this to a WPF Class library when I get to a state of managable chaos.
* *TerminalBoard.Test* 
		Testing using xUnit and FluentAssertions. At the moment just a few minor tests to anchor my sanity to something.
## Terminology
Why not call it something else? Inspiration taken from the electrical world.

- *Terminal*  = Node
- *Socket* = Connection Point
- *Wire* = Connection.

## Key Components

BoardViewModel (Main View Model)
- Recieves a few services to create other view models.
- Hndles major events using the Caliburns EventAggregator. 
- Handles some UI events aswell.

### Base interfaces for viewmodels


- ITerminalViewModel -  Base view model of ITerminals
- ISocketViewModel  -  Base view model of ISockets
- IWireViewModel -  Base view model of IWires

## Model interfaces

ITerminal are the base model that keeps a collection of ISockets and IWires. 

Derived interfaces of ITerminal (listed below) includes a IFunction that evaluates, outputs or shows an results.  

ITerminals 
- IValueTerminal - Simple value creator (float or int)
- IEvaluationTerminal - Terminal that holds an evaulation function
- IOutputTerminal - Shows a single output value

IFunction
- IValueFunction single - IValue output
- IEvaluationFunction - multiple value input/outputs (ISockets). Has the ability to evaluate the inputs according to some type of implementation. (Yeh, so a function...)

ISocket - interface of generic type, since it can hold different type of values.
- SocketBase - abstract ISocket 
- Floatsocket 
- IntSocket
- VectorSocket
-  More types of sockets depending on which API that is implementing this (?). 
- At the moment these are not of generic type. Perhaps they should be or atleast that would be nice.

IWire 
-  Keeps track of
	- Input and output socket
	- Which IValue is being transfered
	- The ability to do the actual data transfer by calling the UpdateInput method in the parent terminal of the socket.

IValues are my short coming instead of using an generic type, more to come on this.

## I see your board
A picture of how the terminalboard looks like right now

![Simple Terminal Board](Pics/BaseUITerminalBoard.PNG?raw=true)
