using DefaultNamespace;
using Interfaces;
using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    [SerializeField] private int _damage;
    [SerializeField] private float _lifeTime;
    private Timer _timer;
    private Vector3 _moveDirection;
    private float _moveSpeed;
    private Particle _trail;

    public void StartMove(Vector3 direction, float moveSpeed)
    {
        _moveDirection = direction.normalized;
        _moveSpeed = moveSpeed;
        _timer.StartTimer(_lifeTime, DestroyBullet);
        _trail = Toolbox.Get<ParticleSpawner>()
            .SpawnParticle(ParticleType.CannonballFlyTrail, transform.position, transform);
    }

    private void Awake() => _timer = new Timer();

    private void Update()
    {
        if (_timer.IsTick == false) return;
        _timer.UpdateTimer();
        transform.position += _moveDirection * (_moveSpeed * Time.deltaTime);
    }

    private void DestroyBullet()
    {
        Toolbox.Get<ParticleSpawner>().SpawnParticle(ParticleType.CannonBallExplosion, transform.position);
        _trail.DisableParticle();
        Toolbox.Get<CameraShaker>().Shake();
        Toolbox.Get<ManagerPool>().Despawn(PoolType.Entities, gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamageable>(out var damagable) == false) return;
        damagable.TakeDamage(Random.Range(_damage / 2, _damage));
        DestroyBullet();
    }
}