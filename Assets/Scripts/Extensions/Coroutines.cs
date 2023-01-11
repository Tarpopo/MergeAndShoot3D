using System;
using UnityEngine;
using System.Collections;

public static class Coroutines
{
    public static IEnumerator PunchScale(Transform transform, Vector3 scale, float speed, Action onAnimationEnd)
    {
        var steps = Vector3.Distance(scale, transform.localPosition) / (speed * Time.fixedDeltaTime);
        var delta = (scale - transform.localScale) / steps;
        var startScale = transform.localScale;
        for (int i = 0; i < steps; i++)
        {
            transform.localScale += delta;
            yield return Waiters.fixedUpdate;
        }

        delta = (startScale - transform.localScale) / steps;
        for (int i = 0; i < steps; i++)
        {
            transform.localScale += delta;
            yield return Waiters.fixedUpdate;
        }

        transform.localScale = startScale;
        onAnimationEnd?.Invoke();
    }

    // public static IEnumerator ScaleAnimation(this GameObject[] list, Vector3 startScale, Vector3 endScale,
    //     int speed, Action onEnd)
    // {
    //     var delta = (endScale - startScale) / speed;
    //     foreach (var element in list)
    //     {
    //         element.SetActive(true);
    //         element.transform.localScale = startScale;
    //     }
    //
    //     for (int i = 0; i < speed; i++)
    //     {
    //         foreach (var element in list) element.transform.localScale += delta;
    //         yield return null;
    //     }
    //
    //     onEnd?.Invoke();
    // }

    // public static IEnumerator MoveLocalAndBack(Transform transform, Vector3 target, int speed, Action onAnimationEnd)
    // {
    //     var delta = (target - transform.localPosition) / speed;
    //     var startPosition = transform.localPosition;
    //     for (int i = 0; i < speed; i++)
    //     {
    //         transform.localPosition += delta;
    //         yield return null;
    //     }
    //
    //     delta = (startPosition - transform.localPosition) / speed;
    //     for (int i = 0; i < speed; i++)
    //     {
    //         transform.localPosition += delta;
    //         yield return null;
    //     }
    //
    //     onAnimationEnd?.Invoke();
    // }

    // public static IEnumerator EulerRotate(Transform transform, Vector3 rotateTo, int frames,
    //     Action onEndAnimation = null)
    // {
    //     var delta = Quaternion.Euler((rotateTo - transform.eulerAngles) / frames);
    //     for (int i = 0; i < frames; i++)
    //     {
    //         transform.rotation *= delta;
    //         yield return null;
    //     }
    //
    //     onEndAnimation?.Invoke();
    // }

    // public static IEnumerator ChangeCameraField(this Camera camera, float targetValue, int frames,
    //     float actionDelay = 0, Action onZoomEnd = null)
    // {
    //     var step = (targetValue - camera.fieldOfView) / frames;
    //     for (int i = 0; i < frames; i++)
    //     {
    //         camera.fieldOfView += step;
    //         yield return null;
    //     }
    //
    //     camera.fieldOfView = targetValue;
    //     yield return new WaitForSeconds(actionDelay);
    //     onZoomEnd?.Invoke();
    // }

    // public static IEnumerator TryEulerRotate(Transform transform, Vector3 rotateTo, int frames,
    //     Action onEndAnimation = null)
    // {
    //     var delta = (rotateTo - transform.eulerAngles) / frames;
    //     for (int i = 0; i < frames; i++)
    //     {
    //         transform.eulerAngles += delta;
    //         yield return null;
    //     }
    //
    //     onEndAnimation?.Invoke();
    // }

    // public static IEnumerator LocalEulerRotate(Transform transform, Vector3 rotateTo, int frames,
    //     Action onEndAnimation = null)
    // {
    //     var delta = Quaternion.Euler((rotateTo - transform.localEulerAngles) / frames);
    //     for (int i = 0; i < frames; i++)
    //     {
    //         transform.localRotation *= delta;
    //         yield return null;
    //     }
    //
    //     onEndAnimation?.Invoke();
    // }

    public static IEnumerator LocalMove(Transform transform, Vector3 target, float speed, Action onAnimationEnd)
    {
        var steps = Vector3.Distance(target, transform.localPosition) / (speed * Time.deltaTime);
        var delta = (target - transform.localPosition) / steps;
        for (int i = 0; i < steps; i++)
        {
            transform.position += delta;
            yield return null;
        }

        onAnimationEnd?.Invoke();
    }

    // public static IEnumerator MoveFromTarget(Transform transform, Vector3 target, int speed, Action onAnimationEnd)
    // {
    //     var startPosition = transform.position;
    //     transform.position = target;
    //     var delta = (startPosition - transform.position) / speed;
    //     for (int i = 0; i < speed; i++)
    //     {
    //         transform.position += delta;
    //         yield return null;
    //     }
    //
    //     onAnimationEnd?.Invoke();
    // }

    public static IEnumerator Move(Transform transform, Vector3 target, float speed, Action onAnimationEnd)
    {
        var steps = Vector3.Distance(target, transform.position) / (speed * Time.deltaTime);
        var delta = (target - transform.position) / steps;
        for (int i = 0; i < steps; i++)
        {
            transform.position += delta;
            yield return null;
        }

        onAnimationEnd?.Invoke();
    }

    // public static IEnumerator FixedMove(Transform transform, Vector3 target, int speed, Action onAnimationEnd,
    //     float delay = 0)
    // {
    //     yield return new WaitForSeconds(delay);
    //     var delta = (target - transform.position) / speed;
    //     for (int i = 0; i < speed; i++)
    //     {
    //         transform.position += delta;
    //         yield return Waiters.fixedUpdate;
    //     }
    //
    //     onAnimationEnd?.Invoke();
    // }
}