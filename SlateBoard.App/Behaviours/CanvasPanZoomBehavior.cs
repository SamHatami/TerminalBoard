using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;
using SlateBoard.App.Events;
using SlateBoard.App.Interface;
using SlateBoard.App.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SlateBoard.App.Behaviours
{
    public class CanvasPanZoomBehavior : Behavior<UIElement>
    {
        private Canvas? _mainCanvas;
        private IEventAggregator? _events;
        private Point _eventPosition;

        private TransformGroup? _transformGroup;
        private ScaleTransform? _scaleTransform;
        private TranslateTransform? _translateTransform;
        private MainViewModel? _mainViewModel;

        private double maxScale = 1.5;
        private double minScale = 0.8;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject is Canvas maincanvas)
            {
                _mainCanvas = maincanvas;
                _mainCanvas.MouseDown += OnCanvasMouseDown;
                _mainCanvas.MouseMove += OnCanvasMouseMove;
                _mainCanvas.MouseUp += OnCanvasMouseUp;
                _mainCanvas.MouseWheel += OnCanvasMouseWheel;
                _mainCanvas.DataContextChanged += OnDataContextChange;
          
            }
            else
                return;

            _transformGroup = new TransformGroup();
            _scaleTransform = new ScaleTransform();
            _translateTransform = new TranslateTransform();
            _transformGroup.Children.Add(_scaleTransform);
            _transformGroup.Children.Add(_translateTransform);
        }

        private void OnDataContextChange(object sender, DependencyPropertyChangedEventArgs e)
        {
            _mainViewModel = _mainCanvas.DataContext as MainViewModel;
            if (_mainCanvas != null)
            {
                _events = _mainViewModel.Events;
                _events.SubscribeOnBackgroundThread(this);
            }
  
        }

        private void OnCanvasMouseWheel(object sender, MouseWheelEventArgs e)
        {

            //if (_mainCanvas.Children.Count > 0 && _mainCanvas.Children[0].RenderTransform != _transformGroup)
            //{
            //    foreach (UIElement element in _mainCanvas.Children)
            //        element.RenderTransform = _transformGroup;
            //}

            //Point mousePosition = _transformGroup.Inverse.Transform(e.GetPosition(_mainCanvas));
            //double zoomFactor = e.Delta > 0 ? 1.1 : 1 / 1.1;

            //Zoom(mousePosition, zoomFactor);

            e.Handled = true;
        }

        private void Zoom(Point mousePosition, double zoomFactor)
        {

            _scaleTransform.CenterX = mousePosition.X;
            _scaleTransform.CenterY = mousePosition.Y;

            var _newScaleX = _scaleTransform.ScaleX * zoomFactor;
            var _newScaleY = _scaleTransform.ScaleY * zoomFactor;

            _scaleTransform.ScaleX = Math.Clamp(_newScaleX, 0.8, 1.3);
            _scaleTransform.ScaleY = Math.Clamp(_newScaleY, 0.8, 1.3);
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
            if (mouseDownEvent.MiddleButton == MouseButtonState.Pressed)
                _eventPosition = _transformGroup.Inverse.Transform(mouseDownEvent.GetPosition(_mainCanvas));

            mouseDownEvent.Handled = true;
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs mouseMoveEvent)
        {
            if (mouseMoveEvent.MiddleButton == MouseButtonState.Pressed)
            {
                Point currentMousePosition = _transformGroup.Inverse.Transform(mouseMoveEvent.GetPosition(_mainCanvas));

                Vector distanceVector = currentMousePosition - _eventPosition;

                //_translateTransform.X += distanceVector.X;
                //_translateTransform.Y += distanceVector.Y;

                _events.PublishOnBackgroundThreadAsync(new CanvasZoomPanEvent(distanceVector.X, distanceVector.Y));

            }

            mouseMoveEvent.Handled = true;

        }

    }
}
