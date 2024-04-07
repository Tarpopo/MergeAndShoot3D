using UnityEngine;

public static class GameObjectExtensions
{
    // public static void StopAllCoroutinesProcessor(this GameObject gameObject)
    // {
    //     CoroutinesProcessor.Instance.RemoveCoroutine(gameObject.GetHashCode());
    // }

    public static void Active(this GameObject gameObject) => gameObject.SetActive(true);
    public static void Deactivate(this GameObject gameObject) => gameObject.SetActive(false);
}