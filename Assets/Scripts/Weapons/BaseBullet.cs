using System.Linq;
using AmazingAssets.CurvedWorld;
using AmazingAssets.CurvedWorld.Example;
using Interfaces;
using UnityEngine;

public abstract class BaseBullet : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] private float _bulletLlifeTime;
    protected string[] _ignoreTags;

    protected Vector3 _moveDirection;

    private RigidbodyMove _rigidbodyMove;
    private Timer _timer;
    private CurvedMover _curvedMover;

    public virtual void StartMove(Vector3 startMovePosition, Vector3 moveDirection, string[] ignoreTags)
    {
        transform.position = startMovePosition;
        _moveDirection = moveDirection;
        _ignoreTags = ignoreTags;
    }

    protected virtual void Awake()
    {
        _rigidbodyMove = new RigidbodyMove(_rigidbody);

        _timer = new Timer();
        var controller = FindObjectOfType<CurvedWorldController>();
        _curvedMover = new CurvedMover(transform, controller.transform, controller);
        // _transformDynamicPosition.Init(controller.transform, controller);
    }

    protected virtual void Update() => _timer.UpdateTimer();

    protected virtual void FixedUpdate()
    {
        _curvedMover.Move(_moveDirection,_moveSpeed);
        // _rigidbodyMove.Move(_moveDirection, _moveSpeed);
    }

    protected virtual void OnEnable() => _timer.StartTimer(_bulletLlifeTime, DisableBullet);
    // protected virtual void OnDisable() => _rigidbodyMove.StopMove();

    protected virtual void OnTriggerEnter(Collider collider)
    {
        if (_ignoreTags.Any(ignoreTag => collider.tag.Equals(ignoreTag))) return;
        if (collider.TryGetComponent<IDamageable>(out var damageable)) damageable.TakeDamage(1);
        DisableBullet();
    }

    protected abstract void DisableBullet();
}