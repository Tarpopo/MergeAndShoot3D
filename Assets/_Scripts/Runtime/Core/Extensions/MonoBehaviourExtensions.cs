using System;
using System.Collections;
using UnityEngine;

public static class MonoBehaviourExtensions
{
    public static void Timer(this MonoBehaviour monoBehaviour, float time, Action onUpdate, Action onEnd) =>
        monoBehaviour.StartCoroutine(TimerRoutine(time, onUpdate, onEnd));

    private static IEnumerator TimerRoutine(float time, Action onUpdate, Action onEnd)
    {
        var currentTime = time;
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            onUpdate?.Invoke();
            yield return null;
        }

        onEnd?.Invoke();
    }
}