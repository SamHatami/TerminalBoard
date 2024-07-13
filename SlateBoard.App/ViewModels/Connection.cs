﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlateBoard.App.Interface.ViewModel;

namespace SlateBoard.App.ViewModels
{
    public class Connection
    {
        public ISocket InputSocket { get; set; }
        public ISocket OutputSocket { get; set; }

        public ITerminal InputTerminal { get; set; }
        public ITerminal OutputTerminal { get; set; }

    }
}
