using System;
using Interfaces;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class EnemyData : BaseActorData
{
    public Collider EnemyCollider => _enemyCollider;
    public IDamageable PlayerDamageable { get; private set; }
    public Vector3 PlayerPosition => _playerTransform.position;
    public int Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    [SerializeField] private int _damage;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Collider _enemyCollider;
    private Transform _playerTransform;

    public override void SetParameters(MonoBehaviour monoBehaviour)
    {
        base.SetParameters(monoBehaviour);
        var character = Object.FindObjectOfType<Character>();
        _playerTransform = character.transform;
        PlayerDamageable = character;
    }
}