using DefaultNamespace;
using UnityEngine;

public class Weapon : MonoBehaviour, ICanon
{
    public WeaponType WeaponType => _weaponType;
    [SerializeField] private float _bulletMoveSpeed;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Rotator _wheels;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Transform _shootPoint;

    public void Enable() => gameObject.SetActive(true);

    public void Disable() => gameObject.SetActive(false);

    public void TryShoot(Vector3 shootDirection)
    {
        Toolbox.Get<ParticleSpawner>().SpawnParticle(ParticleType.CanonShoot, _shootPoint.position);
        var position = _shootPoint.position;
        Toolbox.Get<CameraShaker>().Shake();
        var bullet = Toolbox.Get<ManagerPool>().Spawn<Bullet>(PoolType.Entities, _bullet, position);
        bullet.StartMove(shootDirection - position, _bulletMoveSpeed);
    }

    public void RotateWheels(int direction) => _wheels.Rotate(direction);
}