using Sirenix.OdinInspector;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public ParticleType ParticleType => _particleType;
    [SerializeField] private ParticleType _particleType;
    [SerializeField] private float _disableTime;
    [SerializeField] private bool _hasLifeTime;

    [SerializeField, ShowIf(nameof(_hasLifeTime))]
    private float _lifeTime;

    private readonly Timer _timer = new Timer();
    private ParticleSystem _particleSystem;
    private Transform _startParent;

    public void DisableParticle()
    {
        _particleSystem.Stop();
        transform.SetParent(_startParent);
        _timer.StartTimer(_disableTime, () => Toolbox.Get<ParticleSpawner>().DespawnParticle(this));
    }

    private void Awake()
    {
        _startParent = transform.parent;
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void Update() => _timer.UpdateTimer();

    private void OnEnable()
    {
        if (_hasLifeTime) _timer.StartTimer(_lifeTime, DisableParticle);
        _particleSystem.Play();
    }
}