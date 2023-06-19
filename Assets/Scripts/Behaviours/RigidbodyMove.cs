using UnityEngine;

public class RigidbodyMove : IMove
{
    private readonly Rigidbody _rigidbody;
    public RigidbodyMove(Rigidbody rigidbody) => _rigidbody = rigidbody;

    public void Move(Vector3 direction, float moveSpeed) =>
        _rigidbody.MovePosition(_rigidbody.position + direction.normalized * moveSpeed);

    public void StopMove() => _rigidbody.velocity = Vector2.zero;
}