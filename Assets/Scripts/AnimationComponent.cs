using System;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor.Animations;
#endif
using UnityEngine;

[Serializable]
public class AnimationComponent
{
#if UNITY_EDITOR
    [SerializeField] private AnimatorController _animatorController;
#endif
    [SerializeField] private Animator _animator;
    [SerializeReference] private Enum _animations;
    private Enum _currentState;
#if UNITY_EDITOR
    [Button]
    private void UpdateAnimator()
    {
        _animatorController.GenerateAnimatorControllerFromExist(_animations, true);
    }
#endif
    public void PlayAnimation(Enum animationType)
    {
        if (_currentState != null) _animator.SetBool(_currentState.ToString(), false);
        _currentState = animationType;
        _animator.SetBool(_currentState.ToString(), true);
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
    Death
}