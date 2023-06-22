using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class TriggerChecker<T>
{
    public event Action<T> OnObjectEnter;
    public event Action<T> OnObjectExit;
    public event Action OnAllEnded;
    public bool HaveElements => _elements.Count > 0;
    public IEnumerable<T> Elements => _elements;
    public T First => _elements.First();
    public Vector3 LastItemPoint => _lastTransform.position;
    private Transform _lastTransform;

    private readonly HashSet<T> _elements = new HashSet<T>(50);

    public void OnTriggerEnter(Collider other) => TryAddItem(other.gameObject);

    public void OnTriggerStay(Collider other) => TryAddItem(other.gameObject);

    public void OnTriggerExit(Collider other) => TryRemoveItem(other.gameObject);

    protected virtual bool IsThisObject(GameObject gameObject) => true;

    public void TryRemoveItem(GameObject gameObject)
    {
        if (gameObject.TryGetComponent<T>(out var component) == false ||
            _elements.Contains(component) == false) return;
        _elements.Remove(component);
        if (HaveElements == false) OnAllEnded?.Invoke();
        OnObjectExit?.Invoke(component);
    }

    public void TryRemoveItem(T component)
    {
        if (_elements.Contains(component) == false) return;
        _elements.Remove(component);
        if (HaveElements == false) OnAllEnded?.Invoke();
        OnObjectExit?.Invoke(component);
    }

    private void TryAddItem(GameObject gameObject)
    {
        if (IsThisObject(gameObject) == false) return;
        if (gameObject.TryGetComponent<T>(out var component) == false || _elements.Contains(component)) return;
        _elements.Add(component);
        _lastTransform = gameObject.transform;
        OnObjectEnter?.Invoke(component);
    }
}