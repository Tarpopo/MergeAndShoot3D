using AmazingAssets.CurvedWorld;
using UnityEngine;

public class CurvedMover : IMove
{
    // private readonly Rigidbody _rigidbody;
    private readonly CurvedWorldController _curvedWorldController;
    private readonly Transform _transform;
    private readonly Transform _parent;

    public CurvedMover(Transform transform, Transform parent, CurvedWorldController curvedWorldController)
    {
        // _rigidbody = rigidbody;
        _curvedWorldController = curvedWorldController;
        _parent = parent;
        _transform = transform;
    }

    public void Move(Vector3 direction, float moveSpeed)
    {
        var position = _transform.position + direction.normalized * moveSpeed;
        _transform.position = _curvedWorldController.TransformPosition(_parent.position + position);
        // _transform.rotation =
        //     _curvedWorldController.TransformRotation(_parent.position + position, _parent.forward,
        //         _parent.right);
        // _rigidbody.MovePosition(_rigidbody.position + direction.normalized * moveSpeed);
    }

    public void StopMove()
    {
        // _rigidbody.velocity = Vector3.zero;
    }
}