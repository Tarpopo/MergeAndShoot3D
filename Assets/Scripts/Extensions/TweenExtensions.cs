using DG.Tweening;

public static class TweenExtensions
{
    public static void TryPlayTween(this Tween tween, ref Tween existTween)
    {
        tween.Pause();
        if (existTween is { active: true }) return;
        existTween = tween;
        existTween.PlayForward();
    }
}