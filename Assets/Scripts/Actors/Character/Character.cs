using System;
using DG.Tweening;
using FSM;
using Interfaces;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public bool IsAlive => _health.IsDeath == false;
    public Health Health => _health;
    public event Action<IDamageable> OnDie;
    [SerializeReference] private Health _health;
    [SerializeField] private CharacterData _characterData;
    private StateMachine<CharacterData> _stateMachine;
    private Quaternion _startRotation;
    public void TakeDamage(int damage) => _health.ReduceHealth(damage, () => OnDie?.Invoke(this));

    public void Shoot()
    {
        if (_characterData.EnemySpawner.TryGetClosetEnemy(transform, out var enemy) == false) return;
        _characterData.Canon.TryShoot();
    }

    private void SetDeathState(IDamageable character) => _stateMachine.ChangeState<CharacterDeath>();

    private void Start()
    {
        _startRotation = transform.rotation;
        OnDie += SetDeathState;
        _stateMachine = new StateMachine<CharacterData>();
        _stateMachine.AddState(new CharacterIdle(_characterData, _stateMachine));
        _stateMachine.AddState(new CharacterMove(_characterData, _stateMachine));
        _stateMachine.AddState(new CharacterAttack(_characterData, _stateMachine));
        _stateMachine.AddState(new CharacterDeath(_characterData, _stateMachine));
        _stateMachine.Initialize<CharacterIdle>();
        TryMoveToNextPoint();
        Toolbox.Get<EnemySpawner>().OnAllEnemiesDie += TryMoveToNextPoint;
    }

    private void OnEnable() => _health.ResetHealth();

    private void TryMoveToNextPoint()
    {
        transform.DORotateQuaternion(_startRotation, _characterData.RotateDuration);
        _stateMachine.ChangeState<CharacterMove>();
        Toolbox.Get<PointMover>().MoveToPoint(transform, _characterData.MoveDuration, SetIdleState);
    }

    private void SetIdleState() => _stateMachine.ChangeState<CharacterIdle>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _stateMachine.ChangeState<CharacterMove>();
            Toolbox.Get<PointMover>().MoveToPoint(transform, _characterData.MoveDuration, SetIdleState);
        }

        _stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }
}