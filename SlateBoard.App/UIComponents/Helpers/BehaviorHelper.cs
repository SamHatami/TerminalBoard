using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Caliburn.Micro;

namespace SlateBoard.App.UIComponents.Helpers
{
    public class BehaviorHelper()
    {
        public static IEventAggregator? EventsAggregator { get; set; }


        private static Canvas GetMainCanvas(DependencyObject? element)
        {
            while (element != null)
            {
                if (element is Canvas { Name: "MainCanvas" } canvas) return canvas;
                element = VisualTreeHelper.GetParent(element);
            }

            return null;
        }

        private static T FindParent<T>(DependencyObject child) where T : class
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null) return null;

            if (parentObject is T parent)
                return parent;

            return FindParent<T>(parentObject);
        }

        //private static ITerminal GetTerminalViewModel(UIElement AssociatedObject, ISocket socket)
        //{
        //    ITerminal terminal = null;

        //    if (AssociatedObject is FrameworkElement { DataContext: ISocket = item })
        //    {
        //        terminal = socket.ParentTerminal;

        //    }

        //    return termin
        //}
    }
}