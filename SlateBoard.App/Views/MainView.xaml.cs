using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Caliburn.Micro;
using SlateBoard.App.BackgroundGrid;
using SlateBoard.App.Events;
using SlateBoard.App.Extensions;
using SlateBoard.App.Interface;

namespace SlateBoard.App.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window, IHandle<UpdateWireVisualsEvent>
    {
        private const int GridSize = 20;

        public MainView()
        {
            InitializeComponent();
            //Loaded += MainWindow_Loaded;
        }

        //private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    DrawDottedGrid(MainCanvas);
        //}

        //private void DrawDottedGrid(Canvas canvas)
        //{
        //    GridVisualHost gridVisual = new GridVisualHost(canvas);
        //    canvas.Children.Add(gridVisual);
        //    Canvas.SetLeft(gridVisual, 0);
        //    Canvas.SetTop(gridVisual, 0);
        //}
        public Task HandleAsync(UpdateWireVisualsEvent message, CancellationToken cancellationToken)
        {
            Guid lineId = message.Wire.Id;
            IWire wire = message.Wire;

            foreach (UIElement child in MainCanvas.Children)
            {
                if (child is Line line && LineExtension.GetWireId(child) == lineId)
                {
                    line.X1 = wire.Start.X;
                    line.Y1 = wire.Start.Y;
                    line.X2 = wire.End.X;
                    line.Y2 = wire.End.Y;
                }
                
            } 

            return Task.CompletedTask;
        }
    }


}
