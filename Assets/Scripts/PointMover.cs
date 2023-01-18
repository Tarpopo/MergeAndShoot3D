using System;
using DG.Tweening;
using UnityEngine;

public class PointMover : ManagerBase
{
    public event Action<int> OnEndMoveToPoint;
    [SerializeField] private Ease _ease;
    [SerializeField] private Transform[] _points;
    private Transform _currentPoint => _points[_currentIndex];
    private Tween _tween;
    private int _currentIndex;

    public void MoveToPoint(Transform movable, float moveDuration, Action onEnd = null, bool setNextPoint = true)
    {
        var tween = movable.DOMoveX(_currentPoint.position.x, moveDuration);
        tween.SetUpdate(UpdateType.Fixed);
        tween.SetEase(_ease);
        tween.TryPlayTween(ref _tween);
        Toolbox.Get<CameraChanger>().SetCharacterCamera();
        tween.onComplete += () =>
        {
            Toolbox.Get<CameraChanger>().ChangeCamera(_currentIndex);
            OnEndMoveToPoint?.Invoke(_currentIndex);
            if (setNextPoint) TrySetNextPoint();
            onEnd?.Invoke();
        };
    }

    private void TrySetNextPoint()
    {
        if (_currentIndex >= _points.Length - 1) return;
        _currentIndex++;
    }
}