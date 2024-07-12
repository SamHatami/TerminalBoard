using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using Caliburn.Micro;

namespace SlateBoard.App.Behaviours
{
    public class CanvasPanZoomBehavior : Behavior<UIElement>
    {
        private Canvas _mainCanvas;

        private Point _eventPosition;

        private TransformGroup _transformGroup;
        private ScaleTransform _scaleTransform;
        private TranslateTransform _translateTransform;

        private double maxScale = 1.5;
        private double minScale = 0.8;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject is Canvas maincanvas )
            {   
                _mainCanvas = maincanvas;
                _mainCanvas.MouseDown += OnCanvasMouseDown;
                _mainCanvas.MouseMove += OnCanvasMouseMove;
                _mainCanvas.MouseUp += OnCanvasMouseUp;
                _mainCanvas.MouseWheel += OnCanvasMouseWheel;
            }
            else
                return;
            
            
            _transformGroup = new TransformGroup();
            _scaleTransform = new ScaleTransform();
            _translateTransform = new TranslateTransform();
            _transformGroup.Children.Add(_scaleTransform);
            _transformGroup.Children.Add( _translateTransform);
        }

        private void OnCanvasMouseWheel(object sender, MouseWheelEventArgs e)
        {
            foreach (UIElement element in _mainCanvas.Children)
                element.RenderTransform = _transformGroup;

            Point mousePosition = _transformGroup.Inverse.Transform(e.GetPosition(_mainCanvas));
            double zoomFactor = e.Delta > 0 ? 1.1 : 1 / 1.1;

            _scaleTransform.CenterX = mousePosition.X;
            _scaleTransform.CenterY = mousePosition.Y;

            if (_scaleTransform.ScaleX > 1.5)
                zoomFactor = 1/1.1;
            if (_scaleTransform.ScaleX < 0.8)
                zoomFactor = 1.1;

            _scaleTransform.ScaleX *= zoomFactor;
            _scaleTransform.ScaleY *= zoomFactor;

            e.Handled = true;
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
            }
        }


        private void OnCanvasMouseDown(object sender, MouseButtonEventArgs mouseDownEvent)
        {
            if(mouseDownEvent.MiddleButton == MouseButtonState.Pressed)
            {
                _eventPosition = _transformGroup.Inverse.Transform(mouseDownEvent.GetPosition(_mainCanvas));

                foreach (UIElement element in _mainCanvas.Children)
                    element.RenderTransform = _transformGroup;
            }

            mouseDownEvent.Handled = true;
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs mouseMoveEvent)
        {
            if(mouseMoveEvent.MiddleButton == MouseButtonState.Pressed)
            {
                Point currentMousePosition = _transformGroup.Inverse.Transform(mouseMoveEvent.GetPosition(_mainCanvas));

                Vector distanceVector = currentMousePosition - _eventPosition;

                _translateTransform.X += distanceVector.X;
                _translateTransform.Y += distanceVector.Y;

            }

            mouseMoveEvent.Handled = true;

        }

    }
}
