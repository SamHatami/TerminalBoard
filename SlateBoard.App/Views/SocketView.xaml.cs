using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Linq;
using SlateBoard.App.Enum;
using SlateBoard.App.ViewModels;

namespace SlateBoard.App.Views;

/// <summary>
/// Interaction logic for NodeView.xaml
/// </summary>
public partial class SocketView : UserControl
{
    private SocketViewModel _socketViewModel;
    public SocketView()
    {
        InitializeComponent();

        Loaded += SocketView_Loaded;

    }

    private void SocketView_Loaded(object sender, RoutedEventArgs e)
    {
        _socketViewModel = DataContext as SocketViewModel;
        var parent = GetParentView(this);
        var canvas = GetMainCanvas(this);

        if (_socketViewModel != null)
        {
            Point parentRelativeToCanvas = parent.TranslatePoint(default, canvas);
            Point relativeLocationToCanvas = Socket.TranslatePoint(default, canvas);

            relativeLocationToCanvas.X += (Socket.Height / 2);
            relativeLocationToCanvas.Y += (Socket.Height / 2);


            Vector v = Point.Subtract(relativeLocationToCanvas, parentRelativeToCanvas);

            _socketViewModel.X = relativeLocationToCanvas.X;
            _socketViewModel.Y = relativeLocationToCanvas.Y;
            _socketViewModel.SetRelativeDistances(v);
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

    private TerminalView GetParentView(DependencyObject? socketView)
    {
        while (socketView != null)
        {
            if (socketView is TerminalView  terminal) return  terminal;
            socketView = VisualTreeHelper.GetParent(socketView);
        }

        return null;
    }



}