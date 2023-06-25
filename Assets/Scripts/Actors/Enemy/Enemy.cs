using System;
using FSM;
using Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable, ITarget, IShootTarget
{
    public Transform Target => _target;
    public Transform ShootTarget => _shootTarget;
    public bool IsAlive => _health.IsDeath == false;
    public event Action<IDamageable> OnDie;

    // public Vector3 TargetPoint => _targetPoint.position;
    [SerializeField] private Transform _target;
    [SerializeField] private ShowHideHealthUI _health;
    [SerializeField] private Transform _shootTarget;
    [SerializeField] private Quaternion _startRotation;
    [SerializeField] private EnemyData _enemyData;
    private StateMachine<EnemyData> _stateMachine;

    public void TakeDamage(int damage)
    {
        SetTakeDamageState();
        // Toolbox.Get<MeshTextSpawner>().SpawnText(damage.ToString(), Color.red, TargetPoint);
        // _health.ReduceHealth(damage, () =>
        // {
        //     Toolbox.Get<Shop>().AddMoney(Random.Range(50, 100));
        //     OnDie?.Invoke(this);
        // });
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

    private void Update()
    {
        _stateMachine.CurrentState.LogicUpdate();
        _health.Update();
    }

    private void FixedUpdate() => _stateMachine.CurrentState.PhysicsUpdate();

    private void OnEnable()
    {
        _enemyData.EnemyCollider.enabled = true;
        _health.ResetHealth();
        transform.rotation = _startRotation;
        _stateMachine?.ChangeState<EnemyMove>();
    }
}