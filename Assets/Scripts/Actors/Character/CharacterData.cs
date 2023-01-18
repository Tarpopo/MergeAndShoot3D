using System;
using UnityEngine;

[Serializable]
public class CharacterData : BaseActorData
{
    public ICanon Canon => canonSetter.CurrentCanon;
    public EnemySpawner EnemySpawner => _enemySpawner;
    public float MoveDuration => _moveDuration;
    public float RotateDuration => _rotateDuration;
    public float IdleTime => _idleTime;
    [SerializeField] private CanonSetter canonSetter;
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _rotateDuration;
    [SerializeField] private float _idleTime;
}