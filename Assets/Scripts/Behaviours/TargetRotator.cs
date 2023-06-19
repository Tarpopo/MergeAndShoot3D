using System;
using UnityEngine;

[Serializable]
public class TargetRotator
{
    public Vector3 TargetDirection => _target.position - _rotator.position;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _rotator;

    public void UpdateRotation()
    {
        _rotator.forward = (_target.position - _rotator.position).WithY(0).normalized;
    }
}