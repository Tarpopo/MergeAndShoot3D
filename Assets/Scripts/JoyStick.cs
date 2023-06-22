using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Vector2 JoystickDirection { get; private set; }

    public bool IsActive => _isActive && JoystickDirection.magnitude >= _joystickSo.MinActiveRadius;

    [SerializeField] private OnRoll _onRollEvent;
    [SerializeField] private JoystickSO _joystickSo;
    [SerializeField] private RectTransform _canvas;
    [SerializeField] private RectTransform _outsideCircle;
    [SerializeField] private RectTransform _insideCircle;
    private bool _isActive;
    private Vector2 _direction = Vector2.zero;

    // [SerializeField] private RectTransform _arrowTransform;

    // [SerializeField] private GameObject _cicle;
    // [SerializeField] private GameObject _arrow;
    private Vector2 _startPosition;

    private void Start() => DisableJoystick();

    private void FixedUpdate()
    {
        if (IsActive) _joystickSo.OnJoystickMove.Invoke(_direction);
    }

    private void EnableJoystick()
    {
        _isActive = true;
        _outsideCircle.gameObject.SetActive(true);
    }

    private void DisableJoystick()
    {
        _isActive = false;
        _outsideCircle.gameObject.SetActive(false);
        JoystickDirection = Vector2.zero;
        _insideCircle.localPosition = Vector3.zero;
        // _joystickSo.OnJoystickMove.Invoke(JoystickDirection);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EnableJoystick();
        _outsideCircle.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        _startPosition = eventData.position;
        // SetActiveInside(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if ((eventData.position - _startPosition).magnitude >= _joystickSo.RadiusInsideCircle)
            _onRollEvent.Invoke(JoystickDirection);
        DisableJoystick();
        _joystickSo.OnJoystickUp.Invoke();
    }

    public void OnDrag(PointerEventData eventData)
    {
        JoystickDirection = eventData.position - _startPosition;
        var magnitude = JoystickDirection.magnitude;
        var position = JoystickDirection.normalized * Mathf.Clamp(magnitude, 0, _joystickSo.RadiusInsideCircle);
        _insideCircle.anchoredPosition = position;
        _direction = new Vector2(Lerp01(position.x, _joystickSo.RadiusInsideCircle),
            Lerp01(position.y, _joystickSo.RadiusInsideCircle));
        // print(Mathf.InverseLerp(-_joystickSo.RadiusInsideCircle, _joystickSo.RadiusInsideCircle, position.x));
        // _arrowTransform.anchoredPosition = position;
        // SetActiveInside(magnitude < _joystickSo.RadiusInsideCircle);
        // SetArrowRotate(JoystickDirection.normalized);
    }

    private float Lerp01(float value, float maxValue) =>
        Mathf.Sign(value) * Mathf.Lerp(0, 1, Mathf.InverseLerp(0, maxValue, Mathf.Abs(value)));
    // private void SetActiveInside(bool touchInside)
    // {
    //     _cicle.SetActive(touchInside);
    //     _arrow.SetActive(!touchInside);
    // }

    // private void SetArrowRotate(Vector2 direction) => _arrowTransform.right = direction;

    private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition) =>
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvas, screenPosition, Helpers.Camera,
            out var localPoint)
            ? localPoint
            : Vector2.zero;
}