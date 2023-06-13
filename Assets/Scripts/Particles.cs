using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField] private AnimationComponent _animationComponent;
    [SerializeField] private ParticlesPool particlesPool;
    [SerializeField] private float _lifeTime;
    private Timer _timer;

    public void SetParticle(Vector2 position)
    {
        transform.position = position;
        // _animationComponent.PlayAnimation(particlesAnimation);
        _timer.StartTimer(_lifeTime, () => particlesPool.Return(this));
    }

    private void Awake() => _timer = new Timer();

    private void Update() => _timer.UpdateTimer();
}