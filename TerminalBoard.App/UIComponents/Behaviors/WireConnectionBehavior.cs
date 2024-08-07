using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TerminalBoard.App.Events.UIEvents;
using TerminalBoard.App.Interfaces.ViewModels;
using TerminalBoard.App.UIComponents.Helpers;
using TerminalBoard.Core.Enum;

namespace TerminalBoard.App.UIComponents.Behaviors;

internal class WireConnectionBehavior : Behavior<UIElement>
{
    private IEventAggregator _events;

    private Canvas _mainCanvas;
    private Line _currentLine;

    private ISocketViewModel? _startSocketViewModel;
    private ISocketViewModel? _endSocketViewModel;
    private ITerminalViewModel? _terminal;

    protected override void OnAttached()
    {
        base.OnAttached();

        if (BehaviorHelper.EventsAggregator != null)
        {
            _events = BehaviorHelper.EventsAggregator;
            _events.SubscribeOnBackgroundThread(this);
        }

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
            X1 = _startSocketViewModel.X,
            Y1 = _startSocketViewModel.Y,
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

    private HitTestResultBehavior IsInputSocket(HitTestResult hit)
    {
        if (hit.VisualHit is FrameworkElement {DataContext: ISocketViewModel socket})
            _endSocketViewModel = socket;

        return HitTestResultBehavior.Continue;
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured)
        {
            VisualTreeHelper.HitTest(_mainCanvas, null, IsInputSocket,
                new GeometryHitTestParameters(new EllipseGeometry(e.GetPosition(_mainCanvas), 2, 2)));
            AssociatedObject.ReleaseMouseCapture();
        }

        //WireValidation is done in the BoardViewModel
        if (_endSocketViewModel == null)
        {
            _mainCanvas.Children.Remove(_currentLine);
            return;
        }

        if(_startSocketViewModel.Type == SocketTypeEnum.Output)
            _events.PublishOnBackgroundThreadAsync(new AddConnectionEvent(_startSocketViewModel, _endSocketViewModel));
        else
        {
            _events.PublishOnBackgroundThreadAsync(new AddConnectionEvent(_endSocketViewModel,_startSocketViewModel));
        }
        _mainCanvas.Children.Remove(_currentLine);
    }

    private void SetDataContextAndEvents()
    {
        if (AssociatedObject is FrameworkElement { DataContext: ISocketViewModel item })
        {
            _startSocketViewModel = item;
            _terminal = _startSocketViewModel.ParentViewModel;
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