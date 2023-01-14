using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class CellMerger : ManagerBase, IStart
{
    public event Action<WeaponType> OnMerge;
    public bool HavePlace => _cells.Any(cell => cell.Free);
    [SerializeField] private CellCombination[] _combinations;
    [SerializeField] private List<Cell> _cells;

    public Cell GetFreeCell() => _cells.First(item => item.Free);

    private void TryGetCellsAtPoint(Cell draggable)
    {
        var secondCell = _cells.FirstOrDefault(item => item.IsPointerEnter && item.Equals(draggable) == false);
        if (secondCell == null) return;
        if (_combinations.Any(combination => combination.TryMerge(draggable, secondCell, OnMerge))) return;
        var sprite = draggable.CurrentSprite;
        var weaponType = draggable.WeaponType;
        draggable.OccupiedCell(secondCell.CurrentSprite, secondCell.WeaponType);
        secondCell.OccupiedCell(sprite, weaponType);
    }

    public void OnStart()
    {
        foreach (var cell in _cells) cell.OnEndDragAction += TryGetCellsAtPoint;
    }
}

[Serializable]
public class CellCombination
{
    [SerializeField] private WeaponType _draggable;
    [SerializeField] private WeaponType _second;
    [SerializeField] private WeaponType _resultType;
    [SerializeField] private Sprite _resutlSprite;

    public bool TryMerge(Cell draggable, Cell secondCell, Action<WeaponType> onMerge)
    {
        if ((draggable.WeaponType.Equals(_draggable) && secondCell.WeaponType.Equals(_second)) == false) return false;
        secondCell.OccupiedCell(_resutlSprite, _resultType);
        draggable.FreeCell();
        onMerge?.Invoke(_resultType);
        return true;
    }
}