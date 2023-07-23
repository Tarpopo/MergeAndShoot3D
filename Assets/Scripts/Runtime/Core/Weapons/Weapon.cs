using Extensions;
using MoreMountains.Feedbacks;
using NaughtyAttributes;
using RootMotion.FinalIK;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _magnitude;
    [SerializeField] private Recoil _recoil;
    [SerializeField] private MMF_Player _shootFeedback;
    // [SerializeField] private OnJoystickUp _onJoystickUp;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _shootDelay;
    [SerializeField] private BaseBulletPool _bulletPool;
    [SerializeField, Tag] private string[] _ignoreTags;
    private AnimationComponent _animationComponent;
    private readonly Timer _timer = new Timer();
    private TagTriggerChecker<ITarget> _targetChecker;

    public void Init(AnimationComponent animationComponent, TagTriggerChecker<ITarget> targetChecker)
    {
        _targetChecker = targetChecker;
        _animationComponent = animationComponent;
    }

    public void TryShoot()
    {
        if (_timer.IsTick || _targetChecker.HaveElements == false) return;
        _shootFeedback.PlayFeedbacks();
        _recoil.Fire(_magnitude);
        var target = (IShootTarget)_targetChecker.Elements.GetClosestTarget(_shootPoint.position);
        _animationComponent.PlayAnimation(UnitAnimations.FirstAttack);
        _timer.StartTimer(_shootDelay, () => _animationComponent.PlayAnimation(UnitAnimations.Idle));
        var bullet = _bulletPool.Get();
        bullet.StartMove(_shootPoint.position, target.ShootTarget.position - _shootPoint.position, _ignoreTags);
    }

    private void OnEnable()
    {
        Toolbox.Get<UserInput>().JoyStick.onPonterUp += TryShoot;
    }

    private void OnDisable()
    {
        Toolbox.Get<UserInput>().JoyStick.onPonterUp -= TryShoot;
    }

    private void Start() => _bulletPool.Load();
    
    private void Update() => _timer.UpdateTimer();
}

public enum WeaponAnimations
{
    Idle,
    Shoot
}