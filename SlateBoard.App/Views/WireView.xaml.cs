using System.Windows.Controls;
using System.Windows.Input;

namespace SlateBoard.App.Views
{
    /// <summary>
    /// Interaction logic for WireView.xaml
    /// </summary>
    public partial class WireView : UserControl
    {
        public WireView()
        {
            InitializeComponent();

            WireGeometry.MouseLeftButtonDown += WireGeometryOnMouseLeftButtonDown; 

        }

        private void WireGeometryOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var something = e.LeftButton;
        }
    }
}
