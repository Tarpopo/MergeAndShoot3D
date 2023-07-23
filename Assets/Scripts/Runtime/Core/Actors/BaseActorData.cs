using System;
using Interfaces;
using UnityEngine;

[Serializable]
public class BaseActorData
{
    public IDamageable Damageable => _damageable;
    public readonly Timer Timer = new Timer();
    public AnimationComponent AnimationComponent => _animationComponent;
    public Transform Transform => _transform;
    public float AttackDistance => _attackDistance;

    [SerializeField] private AnimationComponent _animationComponent;
    [SerializeField] private Transform _transform;
    [SerializeField] private float _attackDistance;
    private IDamageable _damageable;

    public virtual void SetParameters(MonoBehaviour monoBehaviour)
    {
        _damageable = monoBehaviour.GetComponent<IDamageable>();
    }
}