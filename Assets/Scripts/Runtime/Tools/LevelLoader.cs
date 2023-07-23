using System;
using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelLoader : Tool
{
    public event UnityAction OnNextLevel;
    public event UnityAction<string> OnLevelUnloadedStart;
    public event UnityAction<string> OnLevelUnloaded;
    public event UnityAction<string> OnLevelLoadedStart;
    public event UnityAction<string> OnLevelLoaded;
    [SerializeField, Scene] private string _mainScene;
    [SerializeField, Scene] private string _startLevel;
    private string _currentLevel;

    public void LoadNextLevel()
    {
        OnNextLevel?.Invoke();
        StartCoroutine(UnloadSceneAsync(_currentLevel));
        StartCoroutine(LoadSceneAsync(_currentLevel));
    }

    public void LoadStartLevel()
    {
        StartCoroutine(LoadSceneAsync(_startLevel));
    }

    public void RestartLevel()
    {
        StartCoroutine(UnloadSceneAsync(_currentLevel));
        StartCoroutine(LoadSceneAsync(_currentLevel));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        OnLevelLoadedStart?.Invoke(sceneName);
        var asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        while (asyncLoad.isDone == false)
        {
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
        OnLevelLoaded?.Invoke(sceneName);
    }

    private IEnumerator UnloadSceneAsync(string sceneName, Action onStart = null, Action onEnd = null)
    {
        OnLevelUnloadedStart?.Invoke(sceneName);
        onStart?.Invoke();
        var asyncLoad = SceneManager.UnloadSceneAsync(sceneName);
        while (asyncLoad.isDone == false)
        {
            yield return null;
        }

        onEnd?.Invoke();
        OnLevelUnloaded?.Invoke(sceneName);
    }

    public static bool IsSceneLoad(string sceneName)
    {
        var countLoaded = SceneManager.sceneCount;

        for (int i = 0; i < countLoaded; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.name.Equals(sceneName)) return true;
        }

        return false;
    }

    private string GetActiveScene()
    {
        var countLoaded = SceneManager.sceneCount;

        for (int i = 0; i < countLoaded; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            if (scene.isLoaded == false) continue;
            if (scene.name.Equals(_mainScene)) continue;
            return scene.name;
        }

        return string.Empty;
    }
}