using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Extensions
{
    public static class IEnumerableExtensions
    {
        public static T GetRandomElement<T>(this IEnumerable<T> mass)
        {
            var enumerable = mass as T[] ?? mass.ToArray();
            return enumerable[Random.Range(0, enumerable.Length)];
        }

        public static Transform GetClosestElement(this IEnumerable<Transform> list, Vector3 point)
        {
            var minDistance = Mathf.Infinity;
            Transform minPoint = null;
            foreach (var element in list)
            {
                var distance = Vector3.Distance(element.position, point);
                if (distance > minDistance) continue;
                minDistance = distance;
                minPoint = element;
            }

            return minPoint;
        }

        public static T GetClosestTarget<T>(this IEnumerable<T> list, Vector3 point) where T : ITarget
        {
            var minDistance = Mathf.Infinity;
            T minPoint = default;
            foreach (var element in list)
            {
                var distance = Vector3.Distance(element.Target.position, point);
                if (distance > minDistance) continue;
                minDistance = distance;
                minPoint = element;
            }

            return minPoint;
        }

        public static GameObject GetClosestGameObject(this IEnumerable<GameObject> list, Vector3 point)
        {
            var minDistance = Mathf.Infinity;
            GameObject minPoint = null;
            foreach (var element in list)
            {
                var distance = Vector3.Distance(element.transform.position, point);
                if (distance > minDistance) continue;
                minDistance = distance;
                minPoint = element;
            }

            return minPoint;
        }
    }
}