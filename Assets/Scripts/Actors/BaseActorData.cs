using System;
using UnityEngine;

[Serializable]
public class BaseActorData
{
    public AnimationComponent AnimationComponent => _animationComponent;
    public Transform Transform => _transform;
    public float MoveDuration => _moveDuration;
    [SerializeField] private AnimationComponent _animationComponent;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _moveDuration;
}