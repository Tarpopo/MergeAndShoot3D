using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponType WeaponType => _weaponType;
    [SerializeField] private Rotator _wheels;
    [SerializeField] private WeaponType _weaponType;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private ParticleSystem _shootParticle;

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void RotateWheels() => _wheels.Rotate();

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void Shoot()
    {
        _shootParticle.Play();
    }
}