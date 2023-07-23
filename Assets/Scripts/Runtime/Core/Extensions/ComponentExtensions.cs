using System;
using UnityEngine;

namespace Extensions
{
    public static class ComponentExtensions
    {
        public static void Activate(this Component component) => component.gameObject.SetActive(true);
        public static void Deactivate(this Component component) => component.gameObject.SetActive(false);

        public static void IfNotNull<T>(this T component, Action<T> action) where T : Component
        {
            if (component != null)
                action?.Invoke(component);
        }

        public static void IfNull<T>(this T component, Action<T> action) where T : Component
        {
            if (component == null)
                action?.Invoke(component);
        }

        public static void EnableParent(this Component component)
        {
            var parent = component.transform.parent;

            if (parent == null)
                component.Activate();
            else
                parent.Activate();
        }

        public static void DisableParent(this Component component)
        {
            var parent = component.transform.parent;

            if (parent == null) component.Deactivate();
            else parent.Deactivate();
        }

        public static Transform TryGetParent(this Component component)
        {
            var transform = component.transform;
            var parent = transform.parent;
            return parent == null ? transform : parent;
        }

        public static Transform TryGetChild(this Component component)
        {
            var transform = component.transform;
            var children = transform.GetChild(0);
            return children == null ? transform : children;
        }

        public static T GetNearby<T>(this Component component) where T : Component
        {
            T instance = null;

            if (component.transform.parent != null)
                instance = component.GetComponentInParent<T>();

            if (instance == null)
                instance = component.GetComponentInChildren<T>();

            if (instance == null)
                throw new NullReferenceException(typeof(T).Name);

            return instance;
        }
    }
}