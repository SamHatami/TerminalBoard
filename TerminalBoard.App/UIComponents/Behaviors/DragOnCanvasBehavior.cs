using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TerminalBoard.App.Events;
using TerminalBoard.App.UIComponents.Helpers;
using ITerminalViewModel = TerminalBoard.App.Interfaces.ViewModels.ITerminalViewModel;

namespace TerminalBoard.App.UIComponents.Behaviors;

public class DragOnCanvasBehavior : Behavior<UIElement>, IHandle<GridChangeEvent>
{
    private Canvas _mainCanvas;

    private ITerminalViewModel _terminal;
    private IEventAggregator? _events;

    private bool _gridSnapping = false;
    private int _gridSize;
    private double _dx;
    private double _dy;

    protected override void OnAttached()
    {
        base.OnAttached();

        _events = BehaviorHelper.EventsAggregator;
        _events.SubscribeOnBackgroundThread(this);

        AssociatedObject.MouseLeftButtonDown += OnMouseLeftButtonDown;
        AssociatedObject.MouseMove += OnMouseMove;
        AssociatedObject.MouseLeftButtonUp += OnMouseLeftButtonUp;

        _mainCanvas = GetMainCanvas(AssociatedObject);

        SetDataContextAndEvents();
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.MouseLeftButtonDown -= OnMouseLeftButtonDown;
        AssociatedObject.MouseMove -= OnMouseMove;
        AssociatedObject.MouseLeftButtonUp -= OnMouseLeftButtonUp;
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (_terminal == null)
            return;

        var we = sender.GetType();
        var startPosition = e.GetPosition(_mainCanvas);

        _dx = _terminal.CanvasPositionX - startPosition.X;
        _dy = _terminal.CanvasPositionY - startPosition.Y;

        AssociatedObject.CaptureMouse();
        e.Handled = true;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured)
        {
            var mouseCurrentPosition = e.GetPosition(_mainCanvas);
            var newPosX = mouseCurrentPosition.X + _dx; //Consider width and height of contentcontrol
            var newPosY = mouseCurrentPosition.Y + _dy;

            if (double.IsNaN(newPosX) || double.IsNaN(newPosY))
                return;

            _terminal.CanvasPositionX =
                _gridSnapping
                    ? (int)Math.Round(newPosX / _gridSize) * _gridSize
                    : (int)newPosX; //TODO: Not the best to cast to int, fix later
            _terminal.CanvasPositionY = _gridSnapping ? (int)Math.Round(newPosY / _gridSize) * _gridSize : (int)newPosY;
        }

        e.Handled = true;
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured) AssociatedObject.ReleaseMouseCapture();
    }

    private void SetDataContextAndEvents()
    {
        if (AssociatedObject is FrameworkElement { DataContext: ITerminalViewModel item }) _terminal = item;
    }

    private Canvas GetMainCanvas(DependencyObject? element)
    {
        while (element != null)
        {
            if (element is Canvas { Name: "MainCanvas" } canvas) return canvas;
            element = VisualTreeHelper.GetParent(element);
        }

        return null;
    }

    public Task HandleAsync(GridChangeEvent message, CancellationToken cancellationToken)
    {
        _gridSize = message.GridSize;
        _gridSnapping = message.SnapToGrid;

        return Task.CompletedTask;
    }
}