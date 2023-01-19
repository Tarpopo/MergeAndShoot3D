using System;
using FSM;
using Interfaces;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    public bool IsAlive => _health.IsDeath == false;
    public event Action<IDamageable> OnDie;
    public Vector3 TargetPoint => _targetPoint.position;
    [SerializeReference] private Health _health;
    [SerializeField] private Transform _targetPoint;
    [SerializeField] private Quaternion _startRotation;
    [SerializeField] private EnemyData _enemyData;
    private StateMachine<EnemyData> _stateMachine;

    public void TakeDamage(int damage)
    {
        SetTakeDamageState();
        _health.ReduceHealth(damage, () =>
        {
            Toolbox.Get<Shop>().AddMoney(Random.Range(50, 100));
            OnDie?.Invoke(this);
        });
    }

    public void ApplyDamage() => _enemyData.PlayerDamageable.TakeDamage(_enemyData.Damage);

    public void TrySetMoveState()
    {
        if (_enemyData.PlayerDamageable.IsAlive == false) return;
        _stateMachine.ChangeState<EnemyMove>();
    }

    private void SetTakeDamageState() => _stateMachine.ChangeState<EnemyTakeDamage>();
    public void SetIdleState() => _stateMachine.ChangeState<EnemyIdle>();

    private void SetDeathState(IDamageable enemy)
    {
        _enemyData.EnemyCollider.enabled = false;
        _stateMachine.ChangeState<EnemyDeath>();
    }

    private void Start()
    {
        _enemyData.SetParameters(this);
        OnDie += SetDeathState;
        _stateMachine = new StateMachine<EnemyData>();
        _stateMachine.AddState(new EnemyIdle(_enemyData, _stateMachine));
        _stateMachine.AddState(new EnemyAttack(_enemyData, _stateMachine));
        _stateMachine.AddState(new EnemyMove(_enemyData, _stateMachine));
        _stateMachine.AddState(new EnemyDeath(_enemyData, _stateMachine));
        _stateMachine.AddState(new EnemyTakeDamage(_enemyData, _stateMachine));
        _stateMachine.Initialize<EnemyMove>();
    }

    private void Update() => _stateMachine.CurrentState.LogicUpdate();

    private void FixedUpdate() => _stateMachine.CurrentState.PhysicsUpdate();

    private void OnEnable()
    {
        _enemyData.EnemyCollider.enabled = true;
        _health.ResetHealth();
        transform.rotation = _startRotation;
        _stateMachine?.ChangeState<EnemyMove>();
    }
}