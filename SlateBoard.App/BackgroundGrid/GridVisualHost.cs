using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Caliburn.Micro;

namespace SlateBoard.App.BackgroundGrid
{
    public class GridVisualHost : FrameworkElement
    {
        private GridVisual _gridVisual;

        public static readonly DependencyProperty CanvasWidthProperty =
            DependencyProperty.Register("CanvasWidth", typeof(double), typeof(GridVisualHost), new PropertyMetadata(800.0));

        public static readonly DependencyProperty CanvasHeightProperty =
            DependencyProperty.Register("CanvasHeight", typeof(double), typeof(GridVisualHost), new PropertyMetadata(450.0));

        public static readonly DependencyProperty MouseLeftButtonDownHandlerProperty =
            DependencyProperty.Register("MouseLeftButtonDownHandler", typeof(Action<object, MouseButtonEventArgs>), typeof(GridVisualHost));

        public double CanvasWidth
        {
            get { return (double)GetValue(CanvasWidthProperty); }
            set { SetValue(CanvasWidthProperty, value); }
        }

        public double CanvasHeight
        {
            get { return (double)GetValue(CanvasHeightProperty); }
            set { SetValue(CanvasHeightProperty, value); }
        }
        public GridVisualHost(Canvas canvas)
        {
            _gridVisual = new GridVisual(canvas);
        }

        private void UpdateGrid()
        {
            _gridVisual.DrawGrid(CanvasHeight,CanvasWidth);
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            return _gridVisual;
        }

        private static void CanvasWidthPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridVisualHost element = (GridVisualHost)d;
            element.UpdateGrid();
        }

        private static void CanvasHeightPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridVisualHost element = (GridVisualHost)d;
            element.UpdateGrid();
        }
    }
}
