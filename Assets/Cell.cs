using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool IsOccupied;
    public void OccupiedCell(GameObject cellItem)
    {
        IsOccupied = true;
    }
}