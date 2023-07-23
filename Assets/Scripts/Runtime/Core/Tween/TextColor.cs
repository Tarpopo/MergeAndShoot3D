using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

[Serializable]
public class TextColor : BaseTweenAnimation
{
    [SerializeField] private TMP_Text _image;
    [SerializeField] private Color _endValue;
    [SerializeField] private Color _startValue;

    public override Tween GetTween() => _image.DOColor(_endValue, _duration);

    public override void SetStartValues() => _image.color = _startValue;

    public override void SetEndValues() => _image.color = _endValue;
}