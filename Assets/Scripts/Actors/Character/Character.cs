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

    public void SetIdleAnimation() => _characterData.AnimationComponent.PlayAnimation(UnitAnimations.Idle);
    private void SetDeathState(IDamageable character) => _stateMachine.ChangeState<CharacterDeath>();

    private void OnEnable() => _health.ResetHealth();

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
        _characterData.EnemyTriggerCollector.OnGetObject += SetAttackState;
        _characterData.EnemyTriggerCollector.OnLostObject += TrySetIdleState;
        _characterData.ShootBoosterSlider.OnFastEnd += _characterData.AttackDurationSetter.SetRegularDuration;
        _characterData.ShootBoosterSlider.OnFastSet += _characterData.AttackDurationSetter.SetFastDuration;
        _characterData.AttackDurationSetter.SetRegularDuration();
    }

    private void TryMoveToNextPoint()
    {
        transform.DORotateQuaternion(_startRotation, _characterData.RotateDuration);
        _stateMachine.ChangeState<CharacterMove>();
        Toolbox.Get<PointMover>().MoveToPoint(transform, _characterData.MoveDuration, SetIdleState);
    }

    private void TrySetIdleState()
    {
        if (_characterData.EnemyTriggerCollector.HaveElements) return;
        _stateMachine.ChangeState<CharacterIdle>();
    }

    private void SetAttackState() => _stateMachine.ChangeState<CharacterAttack>();

    private void SetIdleState() => _stateMachine.ChangeState<CharacterIdle>();

    private void Update() => _stateMachine.CurrentState.LogicUpdate();

    private void FixedUpdate() => _stateMachine.CurrentState.PhysicsUpdate();

    private void OnTriggerEnter(Collider other) => _characterData.EnemyTriggerCollector.OnTriggerEnter(other);

    private void OnTriggerExit(Collider other) => _characterData.EnemyTriggerCollector.OnTriggerExit(other);
}