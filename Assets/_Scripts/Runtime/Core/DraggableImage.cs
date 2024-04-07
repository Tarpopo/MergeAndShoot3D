using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class DraggableImage : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    public bool IsPointerEnter { get; private set; }
    [SerializeField] private ScaleAnimation _scaleAnimation;
    private RectTransform _rectTransform;
    private RectTransform _parent;
    private Vector2 _startAnchoredPosition;
    private Tween _tween;

    protected virtual void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _startAnchoredPosition = _rectTransform.anchoredPosition;
        _parent = (RectTransform)_rectTransform.parent;
        _tween = _scaleAnimation.GetTween();
        _tween.Pause();
        _tween.SetAutoKill(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = RectTransformUtility.ScreenPointToLocalPointInRectangle(_parent,
            eventData.pointerCurrentRaycast.screenPosition,
            Camera.main, out var point)
            ? point
            : _startAnchoredPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _rectTransform.SetSiblingIndex(0);
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition = _startAnchoredPosition;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsPointerEnter = true;
        _tween.PlayForward();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsPointerEnter = false;
        _tween.PlayBackwards();
    }
}