using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShootBoosterSlider : MonoBehaviour
{
    public event Action OnFastSet;
    public event Action OnFastEnd;

    [SerializeField] private Image _progressBar;
    [SerializeField] private float _fillTime;
    [SerializeReference] private BaseTweenAnimation _baseTween;
    private readonly Timer _timer = new Timer();
    private Tween _tween;
    private Action _onUpdate;

    private void Start()
    {
        _tween = _baseTween.GetTween();
        _tween.Pause();
        _tween.SetAutoKill(false);
        _progressBar.fillAmount = 1;
        _baseTween.SetStartValues();
        var userInput = Toolbox.Get<UserInput>();
        userInput.OnTouchDown += DoEnableAnimation;
        userInput.OnTouchDown += ReduceFill;
        userInput.OnTouchUp += Fill;
    }

    private void FixedUpdate() => _timer.UpdateTimer();

    private void DoEnableAnimation() => _tween.PlayForward();

    private void DoDisableAnimation() => _tween.PlayBackwards();

    private void Fill()
    {
        OnFastEnd?.Invoke();
        var time = Mathf.Lerp(_fillTime, 0, _progressBar.fillAmount);
        _timer.StartTimer(time, DoDisableAnimation, () => UpdateFillUI(GetFillValue(_fillTime)));
    }

    private void ReduceFill()
    {
        OnFastSet?.Invoke();
        var time = Mathf.Lerp(0, _fillTime, _progressBar.fillAmount);
        _timer.StartTimer(time, Fill, () => UpdateFillUI(GetReduceFillValue(_fillTime)));
    }

    private void UpdateFillUI(float value) => _progressBar.fillAmount = value;

    private float GetFillValue(float maxValue) => Mathf.InverseLerp(maxValue, 0, _timer.CurrentTime);

    private float GetReduceFillValue(float maxValue) => Mathf.InverseLerp(0, maxValue, _timer.CurrentTime);
}