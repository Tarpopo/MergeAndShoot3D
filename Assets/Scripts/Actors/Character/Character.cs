using System;
using FSM;
using Interfaces;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public bool IsAlive => true; //_health.IsDeath == false;

    // public Health Health => _health;
    public event Action<IDamageable> OnDie;

    // [SerializeReference] private Health _health;
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private RollData _rollData;
    [SerializeField] private OnJoystickMove _onJoystickMove;
    [SerializeField] private OnJoystickUp _onJoystickUp;
    [SerializeField] private TargetRotator _targetRotator;
    [SerializeField] private OnRoll _onRoll;
    private StateMachine<CharacterData> _stateMachine;

    private Quaternion _startRotation;

    // private RigidbodyMove _rigidbodyMove;
    private PlayerMove _playerMove;
    private RollMove _rollMove;

    public void TakeDamage(int damage)
    {
        // _health.ReduceHealth(damage, () => OnDie?.Invoke(this));
    }

    public void Shoot()
    {
        if (_characterData.EnemySpawner.TryGetClosetEnemy(transform, out var enemy) == false) return;
        _characterData.Canon.TryShoot();
    }

    public void SetIdleAnimation()
    {
        if (_rollMove.IsRoll) return;
        _characterData.AnimationComponent.PlayAnimation(UnitAnimations.Idle);
    }

    private void SetDeathState(IDamageable character) => _stateMachine.ChangeState<CharacterDeath>();

    // private void OnEnable() => _health.ResetHealth();

    private void Awake()
    {
        var rigidbody = GetComponent<Rigidbody>();
        // _rigidbodyMove = new RigidbodyMove(rigidbody, _characterData.AnimationComponent);
        _playerMove = new PlayerMove(transform, rigidbody, _characterData.AnimationComponent);
        _rollMove = new RollMove(_rollData, rigidbody, _characterData.AnimationComponent, this);
        // _startRotation = transform.rotation;
        // OnDie += SetDeathState;
        // _stateMachine = new StateMachine<CharacterData>();
        // _stateMachine.AddState(new CharacterIdle(_characterData, _stateMachine));
        // _stateMachine.AddState(new CharacterMove(_characterData, _stateMachine));
        // _stateMachine.AddState(new CharacterAttack(_characterData, _stateMachine));
        // _stateMachine.AddState(new CharacterDeath(_characterData, _stateMachine));
        // _stateMachine.Initialize<CharacterIdle>();
        // TryMoveToNextPoint();
        // Toolbox.Get<EnemySpawner>().OnAllEnemiesDie += TryMoveToNextPoint;
        // _characterData.EnemyTriggerCollector.OnGetObject += SetAttackState;
        // _characterData.EnemyTriggerCollector.OnLostObject += TrySetIdleState;
        // _characterData.ShootBoosterSlider.OnFastEnd += _characterData.AttackDurationSetter.SetRegularDuration;
        // _characterData.ShootBoosterSlider.OnFastSet += _characterData.AttackDurationSetter.SetFastDuration;
        // _characterData.AttackDurationSetter.SetRegularDuration();
    }

    private void OnEnable()
    {
        _onJoystickMove.Subscribe(Move);
        _onJoystickUp.Subscribe(SetIdleAnimation);
        _rollMove.onRollStart += _playerMove.Disable;
        _rollMove.onRollEnd += _playerMove.Enable;
        _rollMove.onRollEnd += SetIdleAnimation;
        _onRoll.Subscribe(_rollMove.Move);
    }

    private void OnDisable()
    {
        _onJoystickMove.Unsubscribe(Move);
        _onJoystickUp.Unsubscribe(SetIdleAnimation);
        _rollMove.onRollStart -= _playerMove.Disable;
        _rollMove.onRollEnd -= _playerMove.Enable;
        _rollMove.onRollEnd -= SetIdleAnimation;
        _onRoll.Unsubscribe(_rollMove.Move);
    }

    private void Move(Vector2 moveDirection)
    {
        // var move = Quaternion.AngleAxis(transform.eulerAngles.y, Vector3.up) * moveDirection.ConvertToXZ();
        // var sign = Mathf.Abs(moveDirection.y) > Mathf.Abs(moveDirection.x) ? -1 : 1;
        //sign * new Vector2(move.x, move.z)
        // _characterData.AnimationComponent.PlayMoveAnimation(new Vector2(
        //     -Mathf.Sign(_targetRotator.TargetDirection.x) * moveDirection.x,
        //     -Mathf.Sign(_targetRotator.TargetDirection.z) * moveDirection.y));
        _playerMove.Move(moveDirection.ConvertToXZ(), _characterData.MoveSpeed, _characterData.RotateSpeed);
    }

    private void FixedUpdate()
    {
        // _targetRotator.UpdateRotation();
    }
    // private void TryMoveToNextPoint()
    // {
    //     transform.DORotateQuaternion(_startRotation, _characterData.RotateDuration);
    //     _stateMachine.ChangeState<CharacterMove>();
    //     Toolbox.Get<PointMover>().MoveToPoint(transform, _characterData.MoveDuration, SetIdleState);
    // }

    // private void TrySetIdleState()
    // {
    //     if (_characterData.EnemyTriggerCollector.HaveElements) return;
    //     _stateMachine.ChangeState<CharacterIdle>();
    // }
    //
    // private void SetAttackState() => _stateMachine.ChangeState<CharacterAttack>();
    //
    // private void SetIdleState() => _stateMachine.ChangeState<CharacterIdle>();
    //
    // private void Update() => _stateMachine.CurrentState.LogicUpdate();
    //
    // private void FixedUpdate() => _stateMachine.CurrentState.PhysicsUpdate();
    //
    // private void OnTriggerEnter(Collider other) => _characterData.EnemyTriggerCollector.OnTriggerEnter(other);
    //
    // private void OnTriggerExit(Collider other) => _characterData.EnemyTriggerCollector.OnTriggerExit(other);
}