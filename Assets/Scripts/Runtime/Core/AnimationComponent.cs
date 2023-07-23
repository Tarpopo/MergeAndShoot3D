using System;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

[Serializable]
public class AnimationComponent
{
    public Animator Animator => _animator;
#if UNITY_EDITOR
    [SerializeField] private AnimatorController _animatorController;
#endif
    [SerializeField] private Animator _animator;
    [SerializeReference] private Enum _animations;
    private Enum _currentState;
    private static readonly int X = Animator.StringToHash("X");
    private static readonly int Y = Animator.StringToHash("Y");
#if UNITY_EDITOR
    [Button]
    private void UpdateAnimator()
    {
        _animatorController.GenerateAnimatorControllerFromExist(_animations, true);
    }
#endif
    public void PlayAnimation(Enum animationType)
    {
        Debug.Log(animationType);
        if (_currentState != null) _animator.SetBool(_currentState.ToString(), false);
        _currentState = animationType;
        _animator.SetBool(_currentState.ToString(), true);
    }

    public void PlayMoveAnimation(Vector2 direction)
    {
        PlayAnimation(UnitAnimations.Run);
        _animator.SetFloat(X, direction.x);
        _animator.SetFloat(Y, direction.y);
    }

    public void PlayMoveAnimation(float y)
    {
        _animator.SetFloat(Y, y);
    }
}

public enum UnitAnimations
{
    Idle,
    Walk,
    Run,
    FirstAttack,
    SecondAttack,
    TakeDamage,
    Death,
    Roll
}