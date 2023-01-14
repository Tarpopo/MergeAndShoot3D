using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : DraggableImage
{
    public event Action<Cell> OnEndDragAction;
    public bool Free => WeaponType == WeaponType.Empty;
    public WeaponType WeaponType { get; private set; } = WeaponType.Empty;
    public Sprite CurrentSprite => _image.sprite;

    [SerializeField] private PunchScale _punchScale;
    private Image _image;
    private Sprite _startSprite;
    private Tween _tween;

    public void OccupiedCell(Sprite weaponIcon, WeaponType weaponType)
    {
        WeaponType = weaponType;
        SetSprite(weaponIcon);
    }

    public void FreeCell()
    {
        WeaponType = WeaponType.Empty;
        SetStartSprite();
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragAction?.Invoke(this);
        base.OnEndDrag(eventData);
    }

    protected override void Awake()
    {
        base.Awake();
        _image = GetComponent<Image>();
        _startSprite = _image.sprite;
    }

    private void SetStartSprite() => SetSprite(_startSprite);

    private void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
        _punchScale.SetRectTransform(_image.rectTransform);
        _punchScale.GetTween().TryPlayTween(ref _tween);
    }
}