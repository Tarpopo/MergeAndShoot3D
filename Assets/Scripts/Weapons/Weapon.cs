using System;
using DefaultNamespace;
using NaughtyAttributes;
using UnityEngine;

public class Weapon : MonoBehaviour//, ICanon
{
    [SerializeField] private OnJoystickUp _onJoystickUp;
    [SerializeField] private OnJoystickMove _onJoystickMove;
    [SerializeField] private Transform _rotatable;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDelay;
    [SerializeField] private BaseBulletPool _bulletPool;
    [SerializeField, Tag] private string[] _ignoreTags;

    private readonly Timer _timer = new Timer();

    public void TryShoot()
    {
        if (_timer.IsTick) return;
        _timer.StartTimer(_shootDelay, null);
        var bullet = _bulletPool.Get();
        bullet.StartMove(_shootPoint.position, _shootPoint.right, _ignoreTags);
    }

    private void RotateWeapon(Vector2 direction) => _rotatable.right = direction.normalized;

    private void OnEnable()
    {
        _onJoystickMove.Subscribe(RotateWeapon);
        _onJoystickUp.Subscribe(TryShoot);
    }

    private void OnDisable()
    {
        _onJoystickMove.Unsubscribe(RotateWeapon);
        _onJoystickUp.Unsubscribe(TryShoot);
    }

    private void Start() => _bulletPool.Load();
    private void Update() => _timer.UpdateTimer();
    // public WeaponType WeaponType => _weaponType;
    // [SerializeField] private ParticleSystem _onEnableParticles;
    // [SerializeField] private AnimationComponent _animationComponent;
    // [SerializeField] private float _bulletMoveSpeed;
    // [SerializeField] private GameObject _bullet;
    // [SerializeField] private Rotator _wheels;
    // [SerializeField] private WeaponType _weaponType;
    // [SerializeField] private Transform _shootPoint;
    // public void Enable() => gameObject.SetActive(true);
    //
    // public void Disable() => gameObject.SetActive(false);
    //
    // public void TryShoot() => _animationComponent.PlayAnimation(WeaponAnimations.Shoot);
    //
    // public void RotateWheels(int direction) => _wheels.Rotate(direction);
    //
    // public void Shoot()
    // {
    //     Toolbox.Get<ParticleSpawner>().SpawnParticle(ParticleType.CanonShoot, _shootPoint.position);
    //     var position = _shootPoint.position;
    //     Toolbox.Get<CameraShaker>().Shake();
    //     var bullet = Toolbox.Get<ManagerPool>().Spawn<Bullet>(PoolType.Entities, _bullet, position);
    //     if (Toolbox.Get<EnemySpawner>().TryGetClosetEnemy(transform, out var enemy) == false) return;
    //     bullet.StartMove(enemy.TargetPoint - position, _bulletMoveSpeed);
    // }
    //
    // public void DoIdleAnimation() => _animationComponent.PlayAnimation(WeaponAnimations.Idle);
    //
    // private void OnEnable() => _onEnableParticles.Play();
}

public enum WeaponAnimations
{
    Idle,
    Shoot
}