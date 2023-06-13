using System.Collections.Generic;
using UnityEngine;

public static class Helpers
{
    private static Camera _camera;
    private static readonly Dictionary<float, WaitForSeconds> _waitDictionary = new Dictionary<float, WaitForSeconds>();

    public static Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
    }

    public static WaitForSeconds GetWait(float time)
    {
        if (_waitDictionary.TryGetValue(time, out var wait)) return wait;
        _waitDictionary[time] = new WaitForSeconds(time);
        return _waitDictionary[time];
    }
}