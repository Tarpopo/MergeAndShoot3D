using System;
using UnityEngine;

[Serializable]
public class CharacterData : BaseActorData
{
    public ICanon Canon => canonSetter.CurrentCanon;
    public EnemySpawner EnemySpawner => _enemySpawner;
    public AttackDurationSetter AttackDurationSetter => _attackDurationSetter;
    public ShootBoosterSlider ShootBoosterSlider => _shootBoosterSlider;
    public float AttackDuration => _attackDurationSetter.CurrentDuration;
    public float MoveDuration => _moveDuration;
    public float RotateDuration => _rotateDuration;
    public float MoveSpeed => _moveSpeed;
    public float RotateSpeed => _rotateSpeed;
    public float WeightSetSpeed => _weightSetSpeed;

    [SerializeField] private CanonSetter canonSetter;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private AttackDurationSetter _attackDurationSetter;
    [SerializeField] private ShootBoosterSlider _shootBoosterSlider;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _weightSetSpeed;
}