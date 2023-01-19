using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HealthUI : Health
{
    [SerializeField] private Image _progressBar;
    [SerializeReference] private BaseTweenAnimation _baseTween;
    private float _deltaValue;
    private Tween _tween;

    private void TryDoAnimation() => _baseTween?.GetTween().TryPlayTween(ref _tween);

    private void UpdateUI() => _progressBar.fillAmount = CurrentHealth * _deltaValue;

    public override void ResetHealth()
    {
        base.ResetHealth();
        _deltaValue = (float)1 / _maxHealth;
        _progressBar.fillAmount = 1;
    }

    public override void ReduceHealth(int value, Action onDead)
    {
        UpdateUI();
        TryDoAnimation();
        base.ReduceHealth(value, onDead);
    }
}