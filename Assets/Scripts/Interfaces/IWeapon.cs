using UnityEngine;

public interface IWeapon
{
    public WeaponType WeaponType { get; }
    public void TryShoot();
}