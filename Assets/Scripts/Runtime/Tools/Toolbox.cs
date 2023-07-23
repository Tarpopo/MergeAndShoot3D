using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Toolbox : Singleton<Toolbox>
{
    [SerializeField] private Tool[] _tools;
    private readonly Dictionary<Type, Tool> _data = new Dictionary<Type, Tool>();

    private void Add(Tool obj)
    {
        _data.Add(obj.GetType(), obj);
        if (obj is IAwake awake) awake.OnAwake();
    }

    public static T Get<T>() where T : Tool
    {
        Instance._data.TryGetValue(typeof(T), out var resolve);
        return (T)resolve;
    }

    private void ClearScene(Scene scene)
    {
        foreach (var data in _data) data.Value.ClearScene();
    }

    private void SceneChanged(Scene scene, LoadSceneMode loadSceneMode)
    {
        foreach (var changed in _data.Select(obj => obj.Value as ISceneChanged)) changed?.OnChangeScene();
    }

    private void Awake()
    {
        foreach (var tool in _tools) Add(tool);
        SceneManager.sceneLoaded += SceneChanged;
        SceneManager.sceneUnloaded += ClearScene;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= SceneChanged;
        SceneManager.sceneUnloaded -= ClearScene;
    }
}