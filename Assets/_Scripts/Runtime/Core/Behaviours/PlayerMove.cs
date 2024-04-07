using UnityEngine;

namespace MyNamespace
{
    public class PlayerMove : IMove
    {
        private Rigidbody _rigidbody;
        private Transform _transform;
        private Rigidbody _planetRigidbody;
        private AnimationComponent _animationComponent;
        private RigidbodyMove _rigidbodyMove;
        private TargetRotator _targetRotator;
        private bool _active = true;

        public PlayerMove(Transform transform, Rigidbody planetRigidbody, Rigidbody rigidbody,
            AnimationComponent animationComponent, TargetRotator targetRotator)
        {
            _transform = transform;
            _planetRigidbody = planetRigidbody;
            _rigidbody = rigidbody;
            _animationComponent = animationComponent;
            _rigidbodyMove = new RigidbodyMove(rigidbody);
            _targetRotator = targetRotator;
        }

        public void Move(Vector3 direction, float moveSpeed, float rotationSpeed)
        {
            if (_active == false) return;
            var moveDirection = Quaternion.AngleAxis(Helpers.Camera.transform.rotation.eulerAngles.y, Vector3.up) *
                                direction;
            // var rotation = _planetRigidbody.rotation.eulerAngles -
            //                _transform.right * Mathf.Lerp(0, moveSpeed, direction.magnitude);
            // _planetRigidbody.MoveRotation(Quaternion.Euler(rotation));
            // _planet.eulerAngles += -_transform.right * Mathf.Lerp(0, moveSpeed, direction.magnitude);
            // _rigidbodyMove.Move(moveDirection, Mathf.Lerp(0, moveSpeed, direction.magnitude));

            Rotate(moveDirection, rotationSpeed);
        }
        
        public void Move(Vector3 direction, float moveSpeed)
        {
        }

        public void Rotate(Vector3 moveDirection, float rotationSpeed)
        {
            var rhs = moveDirection.WithY(0);
            var y = Vector3.Dot(_transform.forward, rhs);
            var x = Vector3.Dot(_transform.right, rhs);
            _animationComponent.PlayMoveAnimation(new Vector2(x, y));
            if (_targetRotator.Active) return;
            _transform.forward = Vector3.Lerp(_transform.transform.forward, moveDirection.normalized, rotationSpeed);
        }

        public void StopMove() => _rigidbody.velocity = Vector2.zero;
        public void Enable() => _active = true;
        public void Disable() => _active = false;
    }
}