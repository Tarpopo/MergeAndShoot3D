using UnityEngine;

public class RigidbodyMove : IMove
{
    private readonly Rigidbody _rigidbody;
    private Transform _transform;

    public RigidbodyMove(Rigidbody rigidbody, Transform transform)
    {
        _rigidbody = rigidbody;
        _transform = transform;
    }

    public void Move(Vector3 direction, float moveSpeed)
    {
        _transform.forward = Quaternion.AngleAxis(Helpers.Camera.transform.rotation.eulerAngles.y, Vector3.up) *
                             direction.normalized;
        _rigidbody.MovePosition(_transform.position + _transform.forward * moveSpeed);
    }

    public void StopMove() => _rigidbody.velocity = Vector2.zero;
}