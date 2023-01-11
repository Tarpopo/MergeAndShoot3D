using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CellMerger : ManagerBase
{
    public bool HavePlace => _cells.Any(cell => cell.IsOccupied == false);
    [SerializeField] private List<Cell> _cells;

    public Cell GetFreeCell() => _cells.First(item => item.IsOccupied == false);
}