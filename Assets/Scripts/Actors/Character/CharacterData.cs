using System;
using UnityEngine;

[Serializable]
public class CharacterData : BaseActorData
{
    public Weapon Weapon => _weaponSetter.CurrentWeapon;
    [SerializeField] private WeaponSetter _weaponSetter;
}