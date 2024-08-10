using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TerminalBoard.App.Views;

public partial class BoardView : Window
{
    private const int GridSize = 20;

    public BoardView()
    {
        InitializeComponent();
    }
    private void MainCanvas_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
    {
        var canvas = sender as Canvas;
        if (canvas?.ContextMenu != null)
        {
            canvas.ContextMenu.DataContext = canvas.Tag;
            canvas.ContextMenu.IsOpen = true;
            e.Handled = true;
        }
    }

}