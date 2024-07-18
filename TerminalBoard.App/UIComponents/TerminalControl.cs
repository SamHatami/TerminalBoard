using System.Windows;
using System.Windows.Controls;

namespace TerminalBoard.App.UIComponents
{
    public class TerminalControl : UserControl
    {
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(
            nameof(IsSelected), typeof(bool), typeof(TerminalControl),
            new PropertyMetadata(false));

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }
    }
}