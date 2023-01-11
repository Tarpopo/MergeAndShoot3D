using UnityEngine;
using UnityEngine.Events;

public class UserInput : ManagerBase
{
    public Vector2 Direction { get; private set; }
    [SerializeField] private float _minActiveDistance = 0.5f;
    [SerializeField] private float _minSectorValue = 0.5f;
    private Vector2 _startPosition;
    private bool _isSwipe;

    [SerializeField] private UnityEvent _onTouchDown;

    public event UnityAction OnTouchDown
    {
        add => _onTouchDown.AddListener(value);
        remove => _onTouchDown.RemoveListener(value);
    }

    [SerializeField] private UnityEvent _onTouchUp;

    public event UnityAction OnTouchUp
    {
        add => _onTouchUp.AddListener(value);
        remove => _onTouchUp.RemoveListener(value);
    }

    [SerializeField] private UnityEvent _onVerticalSwipe;

    public event UnityAction OnSwipeVertical
    {
        add => _onVerticalSwipe.AddListener(value);
        remove => _onVerticalSwipe.RemoveListener(value);
    }

    [SerializeField] private UnityEvent _onSwipeUp;

    public event UnityAction OnSwipeUp
    {
        add => _onSwipeUp.AddListener(value);
        remove => _onSwipeUp.RemoveListener(value);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPosition = Input.mousePosition;
            _onTouchDown?.Invoke();
        }

        if (Input.GetMouseButton(0))
        {
            TrySwipe();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _onTouchUp?.Invoke();
            _isSwipe = false;
        }
    }

    private void TrySwipe()
    {
        var mousePosition = (Vector2)Input.mousePosition;
        Direction = (mousePosition - _startPosition).normalized;
        if (IsActiveDistance(mousePosition) == false || _isSwipe) return;
        var direction = Direction;
        direction.x = Mathf.Abs(direction.x);
        direction.y = Mathf.Abs(direction.y);
        if (Vector2.Dot(Vector2.up, direction) >= _minSectorValue)
        {
            _onVerticalSwipe?.Invoke();
            if (Direction.y > 0) _onSwipeUp?.Invoke();
        }

        _isSwipe = true;
    }

    public Vector2 GetSingDirection() => new Vector2(Mathf.Sign(Direction.x), Mathf.Sign(Direction.y));

    private bool IsActiveDistance(Vector2 position) => Vector2.Distance(_startPosition, position) >= _minActiveDistance;
}