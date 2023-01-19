using System;
using UnityEngine;

public class Timer
{
    public float CurrentTime { get; private set; }
    private Action _onTimerEnded;
    private Action _onUpdateTimer;
    public bool IsTick => CurrentTime > 0;

    public void StartTimer(float time, Action onEndTimer, Action onUpdate = null)
    {
        CurrentTime = time;
        _onTimerEnded = onEndTimer;
        _onUpdateTimer = onUpdate;
    }

    public void UpdateTimer()
    {
        if (CurrentTime <= 0) return;
        CurrentTime -= Time.deltaTime;
        _onUpdateTimer?.Invoke();
        if (CurrentTime <= 0) _onTimerEnded?.Invoke();
    }
}