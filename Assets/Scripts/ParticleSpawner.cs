using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class ParticleSpawner : ManagerBase, IStart
{
    [SerializeField] private Particle[] _particles;
    private ManagerPool _managerPool;

    public void OnStart()
    {
        _managerPool = Toolbox.Get<ManagerPool>();
        _managerPool.AddPool(PoolType.Fx);
    }

    public Particle SpawnParticle(ParticleType particleType, Vector3 position, Transform parent = null)
    {
        var particle = _managerPool.Spawn<Particle>(PoolType.Fx,
            _particles.First(item => item.ParticleType.Equals(particleType)).gameObject, position);
        if (parent != null) particle.transform.SetParent(parent);
        return particle;
    }

    public void DespawnParticle(Particle particle) => _managerPool.Despawn(PoolType.Fx, particle.gameObject);
}


public enum ParticleType
{
    CanonShoot,
    CannonballFlyTrail,
    CannonBallExplosion
}