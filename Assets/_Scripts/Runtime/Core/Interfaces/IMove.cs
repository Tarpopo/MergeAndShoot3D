using UnityEngine;

public interface IMove
{
    void Move(Vector3 direction, float moveSpeed);
    void StopMove();
}