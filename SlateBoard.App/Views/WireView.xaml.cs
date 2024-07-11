﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SlateBoard.App.ViewModels
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
