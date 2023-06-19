using System;
using UnityEngine;

public class RollMove : IMove
{
    public bool IsRoll { get; private set; }
    public event Action onRollStart;
    public event Action onRollEnd;
    private readonly RollData _rollData;
    private readonly MonoBehaviour _monoBehaviour;
    private readonly Rigidbody _rigidbody;
    private readonly AnimationComponent _animationComponent;
    private Vector3 _startPosition;

    public RollMove(RollData rollData, Rigidbody rigidbody, AnimationComponent animationComponent,
        MonoBehaviour monoBehaviour)
    {
        _rollData = rollData;
        _rigidbody = rigidbody;
        _monoBehaviour = monoBehaviour;
        _animationComponent = animationComponent;
    }

    public void Move(Vector3 direction, float moveSpeed)
    {
    }

    public void Move(Vector2 direction)
    {
        if (IsRoll) return;
        IsRoll = true;
        onRollStart?.Invoke();
        _animationComponent.PlayAnimation(UnitAnimations.Roll);
        _startPosition = _rigidbody.transform.position;
        var moveDirection = Quaternion.AngleAxis(Helpers.Camera.transform.rotation.eulerAngles.y, Vector3.up) *
                            direction.ConvertToXZ().normalized;
        _rigidbody.transform.forward = moveDirection;
        _monoBehaviour.StartCoroutine(_startPosition.LerpValue(
            _startPosition + _rigidbody.transform.forward * _rollData.RollDistance, 0f, _rollData.RollDuration,
            SetPosition, null, () =>
            {
                IsRoll = false;
                onRollEnd?.Invoke();
            }));
    }

    public void StopMove()
    {
    }

    private void SetPosition(Vector3 position, float timeValue)
    {
        _rigidbody.transform.position = position; //.WithY(_startPosition.y + _rollData.YCurve.Evaluate(timeValue));
        // _rigidbody.transform.position = _giveDamage
        //     ? _rigidbody.transform.position.WithY(_startPosition.y + _rollData.YCurve.Evaluate(timeValue))
        //     : position.WithY(_startPosition.y + _rollData.YCurve.Evaluate(timeValue));
    }
}