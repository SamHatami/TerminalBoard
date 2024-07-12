using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace SlateBoard.App.Behaviours
{
    public class CanvasPanZoomBehavior : Behavior<UIElement>
    {
        private Canvas _mainCanvas;

        private Point _eventPosition;

        private MatrixTransform _matrixTransform;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject is Canvas maincanvas )
            {   
                _mainCanvas = maincanvas;
                _mainCanvas.MouseDown += OnCanvasMouseDown;
                _mainCanvas.MouseMove += OnCanvasMouseMove;
            }
 
            _matrixTransform = new MatrixTransform();
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
                _eventPosition = _matrixTransform.Inverse.Transform(mouseDownEvent.GetPosition(_mainCanvas));
            }
        }

        private void OnCanvasMouseMove(object sender, MouseEventArgs mouseMoveEvent)
        {
            if(mouseMoveEvent.MiddleButton == MouseButtonState.Pressed)
            {
                Point currentMousePosition = _matrixTransform.Inverse.Transform(mouseMoveEvent.GetPosition(_mainCanvas));

                Vector distanceVector = currentMousePosition - _eventPosition;

                TranslateTransform translation = new TranslateTransform(distanceVector.X, distanceVector.Y);
                _matrixTransform.Matrix = translation.Value * _matrixTransform.Matrix;

                foreach (UIElement element in _mainCanvas.Children)
                    element.RenderTransform = _matrixTransform;
            }

        }

    }
}
