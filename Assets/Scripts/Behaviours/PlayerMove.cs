using UnityEngine;

public class PlayerMove : IMove
{
    private readonly Rigidbody _rigidbody;
    private readonly Transform _transform;
    private readonly AnimationComponent _animationComponent;
    private readonly RigidbodyMove _rigidbodyMove;
    private bool _active = true;

    public PlayerMove(Transform transform, Rigidbody rigidbody, AnimationComponent animationComponent)
    {
        _transform = transform;
        _rigidbody = rigidbody;
        _animationComponent = animationComponent;
        _rigidbodyMove = new RigidbodyMove(rigidbody);
    }

    public void Move(Vector3 direction, float moveSpeed, float rotationSpeed)
    {
        if (_active == false) return;
        var moveDirection = Quaternion.AngleAxis(Helpers.Camera.transform.rotation.eulerAngles.y, Vector3.up) *
                            direction;
        _rigidbodyMove.Move(moveDirection, Mathf.Lerp(0, moveSpeed, direction.magnitude));
        Rotate(moveDirection, rotationSpeed);
    }

    public void Move(Vector3 direction, float moveSpeed)
    {
    }

    private void Rotate(Vector3 moveDirection, float rotationSpeed)
    {
        var rhs = moveDirection.WithY(0);
        var y = Vector3.Dot(_transform.forward, rhs);
        var x = Vector3.Dot(_transform.right, rhs);
        _animationComponent.PlayMoveAnimation(new Vector2(x, y));
        _transform.forward = Vector3.Lerp(_rigidbody.transform.forward, moveDirection.normalized,
            Time.deltaTime * rotationSpeed);
    }

    public void StopMove() => _rigidbody.velocity = Vector2.zero;
    public void Enable() => _active = true;
    public void Disable() => _active = false;
}