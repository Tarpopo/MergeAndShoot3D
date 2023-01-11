using System;
using UnityEngine;

namespace Extensions
{
    public static class TransformExtensions
    {
        public static Transform Move(this Transform transform, Vector3 targetPosition, float speed,
            Action onMoveEnd = null)
        {
            CoroutinesProcessor.Instance.PlayCoroutine(transform.gameObject.GetHashCode(),
                Coroutines.Move(transform, targetPosition, speed, onMoveEnd));
            return transform;
        }

        public static Transform LocalMove(this Transform transform, Vector3 targetPosition, float speed,
            Action onMoveEnd = null)
        {
            CoroutinesProcessor.Instance.PlayCoroutine(transform.gameObject.GetHashCode(),
                Coroutines.LocalMove(transform, targetPosition, speed, onMoveEnd));
            return transform;
        }

        public static Transform PunchScale(this Transform transform, Vector3 scale, float speed, Action onScaleEnd)
        {
            CoroutinesProcessor.Instance.PlayCoroutine(transform.gameObject.GetHashCode(),
                Coroutines.PunchScale(transform, scale, speed, onScaleEnd));
            return transform;
        }

        public static void Rotate(this Transform transform, Vector3 targetPosition)
        {
        }

        public static void LocalRotate(this Transform transform, Vector3 targetPosition)
        {
        }
    }
}