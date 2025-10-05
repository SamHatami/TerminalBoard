using ElementaPrime.Core.Enum;

namespace ElementaPrime.TestApp.Events.UIEvents;

public class GridChangeEvent(bool snapToGrid, int gridSize, GridTypeEnum type)
{
    public bool SnapToGrid { get; set; } = snapToGrid;
    public int GridSize { get; set; } = gridSize;

    public GridTypeEnum Type { get; set; } = type;
}