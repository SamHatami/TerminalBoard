using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TerminalBoard.App.Events.UIEvents;
using TerminalBoard.App.UIComponents.Helpers;
using TerminalBoard.App.ViewModels;
using ITerminalViewModel = TerminalBoard.App.Interfaces.ViewModels.ITerminalViewModel;

namespace TerminalBoard.App.UIComponents.Behaviors;

public class DragOnCanvasBehavior : Behavior<UIElement>, IHandle<GridChangeEvent>
{
    private Canvas _mainCanvas;

    private ITerminalViewModel _terminalViewModel;
    private BoardViewModel _boardViewModel;
    private IEventAggregator? _events;

    private ITerminalViewModel[] _selectedTerminalViewModels = [];
    private bool _gridSnapping;
    private int _gridSize;
    private Point[] _relativePositions;
    private Point[] _newPosition;
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
        if (_terminalViewModel == null)
            return;
        AssociatedObject.Focus();
        var we = sender.GetType();
        var startPosition = e.GetPosition(_mainCanvas);
        _selectedTerminalViewModels = _boardViewModel.TerminalViewModels.Where(t => t.Selected).ToArray();
        _relativePositions = new Point[_selectedTerminalViewModels.Length];
        for (var i = 0; i < _selectedTerminalViewModels.Length; i++)
        {
            _relativePositions[i].X = _selectedTerminalViewModels[i].CanvasPositionX - startPosition.X;
            _relativePositions[i].Y = _selectedTerminalViewModels[i].CanvasPositionY - startPosition.Y;
        }

        AssociatedObject.CaptureMouse();
        e.Handled = true;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured)
        {
            var mouseCurrentPosition = e.GetPosition(_mainCanvas);

            MoveSelected(mouseCurrentPosition);
        }

        e.Handled = true;
    }

    private void MoveSelected(Point mousePos)
    {
        for (var i = 0; i < _selectedTerminalViewModels.Length; i++)
        {
            var newX = mousePos.X + _relativePositions[i].X;
            var newY = mousePos.Y + _relativePositions[i].Y;

            if (double.IsNaN(newX) || double.IsNaN(newY))
                return;

            _selectedTerminalViewModels[i].CanvasPositionX =
                _gridSnapping
                    ? (int)System.Math.Round(newX / _gridSize) * _gridSize
                    : (int)newX; //TODO: Not the best to cast to int, fix later

            _selectedTerminalViewModels[i].CanvasPositionY =
                _gridSnapping ? (int)System.Math.Round(newY / _gridSize) * _gridSize : (int)newY;
        }
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured) AssociatedObject.ReleaseMouseCapture();
    }

    private void SetDataContextAndEvents()
    {
        if (AssociatedObject is FrameworkElement { DataContext: ITerminalViewModel item })
        {
            _terminalViewModel = item;
            _boardViewModel = _mainCanvas.DataContext as BoardViewModel;
        }
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