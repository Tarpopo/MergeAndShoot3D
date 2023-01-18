using System;
using UnityEngine;

[Serializable]
public class Rotator
{
    [SerializeField] private Transform[] _rotators;
    [SerializeField] private Vector3 _rotateDirection;
    [SerializeField] private float _rotateSpeed;

    public void Rotate(int direction)
    {
        foreach (var rotator in _rotators) rotator.Rotate(_rotateDirection, _rotateSpeed * Math.Sign(direction));
    }
}