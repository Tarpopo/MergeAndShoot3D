using System;
using UnityEngine;

[Serializable]
public class CharacterData : BaseActorData
{
    public float MoveDuration => _moveDuration;
    public float RotateDuration => _rotateDuration;
    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;
    public float WeightSetSpeed => _weightSetSpeed;

    [SerializeField] private float _moveDuration;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _weightSetSpeed;
}