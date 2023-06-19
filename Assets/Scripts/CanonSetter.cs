using System.Linq;
using UnityEngine;

public class CanonSetter : MonoBehaviour
{
    [SerializeField] private Weapon[] _weapons;
    public ICanon CurrentCanon { get; private set; }

    private void Start()
    {
        Toolbox.Get<CellMerger>().OnMerge += TryActiveWeapon;
        // CurrentCanon = _weapons[0];
        CurrentCanon.Enable();
    }

    private void TryActiveWeapon(WeaponType weaponType)
    {
        // if (CurrentCanon.WeaponType.Equals(weaponType)) return;
        // var weapon = _weapons.FirstOrDefault(item => item.WeaponType.Equals(weaponType));
        // if (weapon == null) return;
        // CurrentCanon.Disable();
        // CurrentCanon = weapon;
        // CurrentCanon.Enable();
    }
}