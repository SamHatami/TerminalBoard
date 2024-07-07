using System.Transactions;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using SlateBoard.App.Interface;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SlateBoard.App.Events;

namespace SlateBoard.App.Behaviours;

public class DragOnCanvasBehavior : Behavior<UIElement>, IHandle<GridChangeEvent>
{
    private Point _itemStartPosition;
    private Canvas _mainCanvas;
    
    private IMoveableItem _moveableItem;
    private IEventAggregator _events;
    
    private bool _gridSnapping = false;
    private int _gridSize = 0;

    protected override void OnAttached()
    {
        base.OnAttached();

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
        _itemStartPosition = new Point(_moveableItem.X-_moveableItem.Width, _moveableItem.Y-_moveableItem.Height);

        AssociatedObject.CaptureMouse();
        e.Handled = true;
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured)
        {
            var mouseCurrentPosition = e.GetPosition(_mainCanvas);

            var direction = mouseCurrentPosition - _itemStartPosition;

            var newLeft = _itemStartPosition.X + direction.X; //Consider width and height of contentcontrol
            var newTop = _itemStartPosition.Y + direction.Y;

            Canvas.SetLeft(AssociatedObject, direction.X);
            Canvas.SetTop(AssociatedObject, direction.Y);

            if (double.IsNaN(newLeft) || double.IsNaN(newTop))
                return;

            _moveableItem.X = _gridSnapping ? Math.Round(newLeft / _gridSize) * _gridSize: newLeft;
            _moveableItem.Y = _gridSnapping ? Math.Round(newTop/ _gridSize) * _gridSize: newTop;
        }
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (AssociatedObject.IsMouseCaptured) AssociatedObject.ReleaseMouseCapture();
    }

    private void SetDataContextAndEvents()
    {
        if (AssociatedObject is FrameworkElement { DataContext: IMoveableItem item })
        {
            _moveableItem = item;
            _events = _moveableItem.Events;
            _events.SubscribeOnBackgroundThread(this);
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

