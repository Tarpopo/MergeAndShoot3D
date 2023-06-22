using System;
using NaughtyAttributes;
using UnityEngine;

[Serializable]
public class TagTriggerChecker<T> : TriggerChecker<T>
{
    [SerializeField, Tag] private string _tag;
    protected override bool IsThisObject(GameObject gameObject) => _tag.Equals(gameObject.tag);
}