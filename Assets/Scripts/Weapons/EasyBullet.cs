using UnityEngine;

public class EasyBullet : BaseBullet
{
    [SerializeField] private BaseBulletPool _bulletPool;
    protected override void DisableBullet() => _bulletPool.Return(this);
}