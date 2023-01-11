using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class TriggerCollector<T> : BaseTriggerChecker
{
    public event Action<T> OnObjectEnter;
    public event Action<T> OnObjectExit;
    public IEnumerable<T> Elements => _elements.Values;
    private readonly Dictionary<int, T> _elements = new Dictionary<int, T>(5);

    protected override bool IsThisObject(GameObject gameObject) => false;

    protected override void OnEnter(GameObject gameObject)
    {
        var component = GetComponent(gameObject);
        _elements.Add(gameObject.GetInstanceID(), component);
        OnObjectEnter?.Invoke(component);
    }

    protected override void OnExit(GameObject gameObject)
    {
        var instanceID = gameObject.GetInstanceID();
        OnObjectExit?.Invoke(_elements[instanceID]);
        _elements.Remove(instanceID);
    }

    protected abstract T GetComponent(GameObject gameObject);
}