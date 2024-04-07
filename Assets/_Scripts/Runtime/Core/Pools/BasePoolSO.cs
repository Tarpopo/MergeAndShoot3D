using System.Collections.Generic;
using UnityEngine;
using Utils;

public interface IPool<T> where T : MonoBehaviour
{
    void Return(T item);
    void OnBeforeReturn(T item, out bool last);
    T Get(Vector3 pos = default, Quaternion rot = default, Transform parent = null);
}

public abstract class BasePoolSO<T> : ScriptableObject, ILoadable, IPool<T> where T : MonoBehaviour
{
    [SerializeField] private T _item;

    private readonly Queue<T> _pool = new(10);

    public virtual void OnBeforeReturn(T item, out bool last)
    {
        last = false;
    }

    public T Get(Vector3 pos = default, Quaternion rot = default, Transform parent = null)
    {
        T item;
        if (_pool.Count == 0)
        {
            item = Instantiate(_item);
            if (parent != null) item.transform.SetParent(parent);
        }
        else item = _pool.Dequeue();

        item.transform.SetPositionAndRotation(pos, rot);
        item.gameObject.SetActive(true);
        return item;
    }

    public void Return(T item)
    {
        _pool.Enqueue(item);
        item.gameObject.SetActive(false);
    }

    public void Load() => _pool.Clear();
}