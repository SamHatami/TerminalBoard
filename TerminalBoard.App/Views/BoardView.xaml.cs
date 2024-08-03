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
        MainCanvas.ContextMenu.IsOpen = true;
    }

}