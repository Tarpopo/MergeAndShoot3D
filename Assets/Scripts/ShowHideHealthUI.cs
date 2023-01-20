using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class ShowHideHealthUI : HealthUI
{
    [SerializeField] private float _hideDelay;
    [SerializeReference] private BaseTweenAnimation _showTween;
    private readonly Timer _timer = new Timer();
    private Tween _tween;

    public void Update() => _timer.UpdateTimer();

    public override void ResetHealth()
    {
        base.ResetHealth();
        if (_tween != null) return;
        _showTween.SetStartValues();
        _showTween.GetTween().TryPlayTween(ref _tween);
        _tween.SetAutoKill(false);
        _tween.Pause();
    }

    public override void ReduceHealth(int value, Action onDead)
    {
        base.ReduceHealth(value, onDead);
        if (IsDeath) return;
        Show();
    }

    private void Show()
    {
        _tween.PlayForward();
        _timer.StartTimer(_hideDelay, Hide);
    }

    private void Hide() => _tween.PlayBackwards();
}