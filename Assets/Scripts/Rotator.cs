using System;
using UnityEngine;

[Serializable]
public class Rotator
{
    [SerializeField] private Transform[] _rotators;
    [SerializeField] private Vector3 _rotateDirection;
    [SerializeField] private float _rotateSpeed;

    public void Rotate()
    {
        foreach (var rotator in _rotators) rotator.Rotate(_rotateDirection, _rotateSpeed);
    }
}