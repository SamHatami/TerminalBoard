using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using TerminalBoard.App.Enum;
using TerminalBoard.App.Interface.ViewModel;
using TerminalBoard.App.UIComponents.Helpers;
using TerminalBoard.App.ViewModels;

namespace TerminalBoard.App.UIComponents.Behaviors;

internal class WireConnectionBehavior : Behavior<UIElement>
{
    private IEventAggregator _events;

    private Canvas _mainCanvas;
    private Line _currentLine;

    private ISocket? _startSocket;
    private ISocket? _endSocket;
    private ITerminal? _terminal;
    
    protected override void OnAttached()
    {
        base.OnAttached();

        _events = BehaviorHelper.EventsAggregator;
        _events.SubscribeOnBackgroundThread(this);

        AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
        AssociatedObject.PreviewMouseMove += OnMouseMove;
        AssociatedObject.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
     

        _mainCanvas = GetMainCanvas(AssociatedObject);
        SetDataContextAndEvents();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
        AssociatedObject.PreviewMouseMove -= OnMouseMove;
        AssociatedObject.PreviewMouseLeftButtonUp -= OnMouseLeftButtonUp;

    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (_terminal == null)
            SetDataContextAndEvents();

        var mouseCurrentPosition = e.GetPosition(_mainCanvas);

        _currentLine = new Line //TODO: Change to similar wire as final
        {
            Stroke = Brushes.Black,
            StrokeThickness = 2,
            X1 = _startSocket.X,
            Y1 = _startSocket.Y,
            X2 = mouseCurrentPosition.X,
            Y2 = mouseCurrentPosition.Y
        };

        _mainCanvas.Children.Add(_currentLine); //Temporary add and wait for user

        AssociatedObject.CaptureMouse();
        e.Handled = true;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured)
        {
            _currentLine.X2 = e.GetPosition(_mainCanvas).X;
            _currentLine.Y2 = e.GetPosition(_mainCanvas).Y;
        }
    }

    private HitTestResultBehavior CheckIfOverSocket(HitTestResult hit)
    {
        if (hit.VisualHit is FrameworkElement element)

            //TODO: Move to WireValidation?
            //add endsock if socket is an input and does not belong to the same terminal
            if (element.DataContext is ISocket { Type: SocketTypeEnum.Input } socket &&
                socket.ParentTerminal != _startSocket.ParentTerminal)
                _endSocket = socket;

        return HitTestResultBehavior.Continue;
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured)
        {
            VisualTreeHelper.HitTest(_mainCanvas, null, CheckIfOverSocket,
                new GeometryHitTestParameters(new EllipseGeometry(e.GetPosition(_mainCanvas), 1, 1)));
            AssociatedObject.ReleaseMouseCapture();
        }



        //TODO: Create WireValidation, that will do all the checks and sends out events instead of this below
        if (_endSocket == null)
        {
            _mainCanvas.Children.Remove(_currentLine);
            return;
        }

        IWire wire = new WireViewModel(_startSocket, _endSocket, _startSocket.Events); //TODO meh
        _startSocket.Wires.Add(wire); //TODO use event after validation
        _endSocket.Wires.Add(wire); //TODO use event after validation
        var mainViewModel = _mainCanvas.DataContext as MainViewModel; //TODO do this with event instead of direct connection
        mainViewModel.Wires.Add(wire); //replace with a WireModel
        _mainCanvas.Children.Remove(_currentLine);
    }

    private void SetDataContextAndEvents()
    {
        if (AssociatedObject is FrameworkElement { DataContext: ISocket item })
        {
            _startSocket = item;
            _terminal = _startSocket.ParentTerminal;

        }
    }

    private static Canvas GetMainCanvas(DependencyObject? element)
    {
        while (element != null)
        {
            if (element is Canvas { Name: "MainCanvas" } canvas) return canvas;
            element = VisualTreeHelper.GetParent(element);
        }

        return null;
    }

}