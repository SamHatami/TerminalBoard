using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using SlateBoard.App.Enum;

namespace SlateBoard.App.Events
{
    public class GridChangeEvent(bool snapToGrid, int gridSize, GridTypeEnum type)
    {
        public bool SnapToGrid { get; set; } = snapToGrid;
        public int GridSize { get; set; } = gridSize;

        public GridTypeEnum Type { get; set; } = type;
    }
}
