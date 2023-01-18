using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class HealthUI : Health
{
    [SerializeField] private Image _progressBar;
    private float _deltaValue;

    private void UpdateUI()
    {
        _progressBar.fillAmount = CurrentHealth * _deltaValue;
    }

    public override void ResetHealth()
    {
        base.ResetHealth();
        _deltaValue = (float)1 / _health;
        _progressBar.fillAmount = 1;
    }

    public override void ReduceHealth(int value, Action onDead)
    {
        UpdateUI();
        base.ReduceHealth(value, onDead);
    }
}