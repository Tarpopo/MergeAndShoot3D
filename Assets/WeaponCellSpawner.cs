using UnityEngine;

public class WeaponCellSpawner : ManagerBase
{
    [SerializeField] private GameObject[] _prefabs;

    // public GameObject SpawnWeapon(WeaponType weaponType, pri)
    // {
    //     Instantiate(_prefabs[(int)weaponType])
    // }
}

public enum WeaponType
{
    EasyCanon,
    MiddleCanon
}