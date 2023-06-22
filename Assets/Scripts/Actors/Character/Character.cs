using System;
using Interfaces;
using RootMotion.FinalIK;
using Sirenix.OdinInspector;
using UnityEngine;

public class Character : MonoBehaviour, IDamageable
{
    public bool IsAlive => true;

    //_health.IsDeath == false;
    // public Health Health => _health;
    public event Action<IDamageable> OnDie;

    // [SerializeReference] private Health _health;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private TagTriggerChecker<ITarget> _tagTriggerChecker;
    [SerializeField] private CharacterData _characterData;
    [SerializeField] private RollData _rollData;
    [SerializeField] private OnJoystickMove _onJoystickMove;
    [SerializeField] private OnJoystickUp _onJoystickUp;
    [SerializeField] private OnRoll _onRoll;
    [SerializeField] private AimIK _aimIK;
    private TargetRotator _targetRotator;
    private PlayerMove _playerMove;
    private RollMove _rollMove;

    public void TakeDamage(int damage)
    {
        // _health.ReduceHealth(damage, () => OnDie?.Invoke(this));
    }

    [Button]
    public void SetFrameRate(int frameRate) => Application.targetFrameRate = frameRate;

    public void Shoot()
    {
        // if (_characterData.EnemySpawner.TryGetClosetEnemy(transform, out var enemy) == false) return;
        // _characterData.Canon.TryShoot();
        _characterData.AnimationComponent.PlayAnimation(UnitAnimations.FirstAttack);
        _weapon.TryShoot();
    }

    private void Awake()
    {
        var rigidbody = GetComponent<Rigidbody>();
        _rollMove = new RollMove(_rollData, rigidbody, _characterData.AnimationComponent, this);
        _targetRotator = new TargetRotator(transform, _aimIK, _tagTriggerChecker, _rollMove);
        _playerMove = new PlayerMove(transform, rigidbody, _characterData.AnimationComponent, _targetRotator);
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        _onJoystickMove.Subscribe(Move);
        _onJoystickUp.Subscribe(StopMove);
        _onJoystickUp.Subscribe(Shoot);
        // _rollMove.onRollStart += _targetRotator.Disable;
        _rollMove.onRollStart += _playerMove.Disable;
        // _rollMove.onRollEnd += _targetRotator.Enable;
        _rollMove.onRollEnd += _playerMove.Enable;
        _rollMove.onRollEnd += StopMove;
        _onRoll.Subscribe(_rollMove.Move);
    }

    private void OnDisable()
    {
        _onJoystickMove.Unsubscribe(Move);
        _onJoystickUp.Unsubscribe(StopMove);
        _onJoystickUp.Unsubscribe(Shoot);
        // _rollMove.onRollStart -= _targetRotator.Disable;
        _rollMove.onRollStart -= _playerMove.Disable;
        // _rollMove.onRollEnd -= _targetRotator.Enable;
        _rollMove.onRollEnd -= _playerMove.Enable;
        _rollMove.onRollEnd -= StopMove;
        _onRoll.Unsubscribe(_rollMove.Move);
    }

    private void Move(Vector2 moveDirection) => _playerMove.Move(moveDirection.ConvertToXZ(), _characterData.MoveSpeed,
        _characterData.RotateSpeed);

    private void StopMove()
    {
        if (_rollMove.IsRoll) return;
        _characterData.AnimationComponent.PlayAnimation(UnitAnimations.Idle);
        _playerMove.StopMove();
    }

    private void FixedUpdate()
    {
        _rollMove.Move();
        _targetRotator.Update(_characterData.RotateSpeed);
    }

    private void OnTriggerEnter(Collider other) => _tagTriggerChecker.OnTriggerEnter(other);

    private void OnTriggerStay(Collider other) => _tagTriggerChecker.OnTriggerStay(other);

    private void OnTriggerExit(Collider other) => _tagTriggerChecker.OnTriggerExit(other);
}