using System.Linq;
using UnityEngine;

public class WeaponSetter : MonoBehaviour
{
    [SerializeField] private Weapon[] _weapons;
    public Weapon CurrentWeapon { get; private set; }

    private void Start()
    {
        Toolbox.Get<CellMerger>().OnMerge += TryActiveWeapon;
        CurrentWeapon = _weapons[0];
        CurrentWeapon.Enable();
    }

    private void TryActiveWeapon(WeaponType weaponType)
    {
        if (CurrentWeapon.WeaponType.Equals(weaponType)) return;
        var weapon = _weapons.FirstOrDefault(item => item.WeaponType.Equals(weaponType));
        if (weapon == null) return;
        CurrentWeapon.Disable();
        CurrentWeapon = weapon;
        CurrentWeapon.Enable();
    }
}