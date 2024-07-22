using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TerminalBoard.App.Events;
using TerminalBoard.App.UIComponents.Helpers;

namespace TerminalBoard.App.UIComponents.Behaviors;

public class CanvasPanZoomBehavior : Behavior<UIElement>
{
    private Canvas? _mainCanvas;
    private IEventAggregator? _events;
    private Point _mouseStartPosition;

    private TransformGroup? _transformGroup;
    private ScaleTransform? _scaleTransform;
    private TranslateTransform? _translateTransform;

    private readonly double _maxScale = 1.5;
    private readonly double _minScale = 0.8;

    protected override void OnAttached()
    {
        base.OnAttached();

        _events = BehaviorHelper.EventsAggregator;

        if (AssociatedObject is Canvas maincanvas)
        {
            _mainCanvas = maincanvas;
            _mainCanvas.MouseDown += OnCanvasMouseDown;
            _mainCanvas.MouseMove += OnCanvasMouseMove;
            _mainCanvas.MouseUp += OnCanvasMouseUp;
            _mainCanvas.MouseWheel += OnCanvasMouseWheel;
        }
        else
        {
            return;
        }

        _transformGroup = new TransformGroup();
        _scaleTransform = new ScaleTransform();
        _translateTransform = new TranslateTransform();
        _transformGroup.Children.Add(_scaleTransform);
        _transformGroup.Children.Add(_translateTransform);
    }

    private void OnCanvasMouseWheel(object sender, MouseWheelEventArgs e)
    {
        if(_mainCanvas == null ) return;

        if (_mainCanvas.Children.Count > 0 && _mainCanvas.Children[0].RenderTransform != _transformGroup)
            foreach (UIElement element in _mainCanvas.Children)
                element.RenderTransform = _transformGroup;

        var mousePosition = _transformGroup.Inverse.Transform(e.GetPosition(_mainCanvas));
        var zoomFactor = e.Delta > 0 ? 1.1 : 1 / 1.1;

        Zoom(mousePosition, zoomFactor);

        e.Handled = true;
    }

    private void Zoom(Point mousePosition, double zoomFactor)
    {
        _scaleTransform.CenterX = mousePosition.X;
        _scaleTransform.CenterY = mousePosition.Y;

        var newScaleX = _scaleTransform.ScaleX * zoomFactor;
        var newScaleY = _scaleTransform.ScaleY * zoomFactor;

        _scaleTransform.ScaleX = Math.Clamp(newScaleX, 0.8, 1.3);
        _scaleTransform.ScaleY = Math.Clamp(newScaleY, 0.8, 1.3);
    }

    private void OnCanvasMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (_mainCanvas.IsMouseCaptured) _mainCanvas.ReleaseMouseCapture();

        e.Handled = true;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();

        if (_mainCanvas != null)
        {
            _mainCanvas.MouseDown -= OnCanvasMouseDown;
            _mainCanvas.MouseMove -= OnCanvasMouseMove;
            _mainCanvas.MouseUp -= OnCanvasMouseUp;
            _mainCanvas.MouseWheel -= OnCanvasMouseWheel;
        }
    }

    private void OnCanvasMouseDown(object sender, MouseButtonEventArgs mouseDownEvent)
    {
        if (mouseDownEvent.MiddleButton == MouseButtonState.Pressed)
            _mouseStartPosition = _transformGroup.Inverse.Transform(mouseDownEvent.GetPosition(_mainCanvas));

        mouseDownEvent.Handled = true;
    }

    private void OnCanvasMouseMove(object sender, MouseEventArgs mouseMoveEvent)
    {
        if (mouseMoveEvent.MiddleButton == MouseButtonState.Pressed)
        {
            var currentMousePosition = _transformGroup.Inverse.Transform(mouseMoveEvent.GetPosition(_mainCanvas));

            var distanceVector = currentMousePosition - _mouseStartPosition;

            distanceVector /= _scaleTransform.ScaleX;
            _translateTransform.X += distanceVector.X;
            _translateTransform.Y += distanceVector.Y;

            _events.PublishOnBackgroundThreadAsync(new CanvasZoomPanEvent(distanceVector.X, distanceVector.Y));
            Trace.WriteLine("dx: " + distanceVector.X + " dy: " + distanceVector.Y.ToString(CultureInfo.CurrentCulture));
        }

        mouseMoveEvent.Handled = true;
    }
}