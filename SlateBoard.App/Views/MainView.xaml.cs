using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using SlateBoard.App.BackgroundGrid;

namespace SlateBoard.App.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        private const int GridSize = 20;

        public MainView()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawDottedGrid(MainCanvas);
        }

        private void DrawDottedGrid(Canvas canvas)
        {
            GridVisualHost gridVisual = new GridVisualHost(canvas);
            canvas.Children.Add(gridVisual);
            Canvas.SetLeft(gridVisual, 0);
            Canvas.SetTop(gridVisual, 0);
        }
    }
}
