using System;
using UnityEngine;

[Serializable]
public class AttackDurationSetter
{
    public float CurrentDuration { get; private set; }
    [SerializeField] private float _regularAttackDuration;
    [SerializeField] private float _fastAttackDuration;

    public void SetFastDuration() => CurrentDuration = _fastAttackDuration;

    public void SetRegularDuration() => CurrentDuration = _regularAttackDuration;
}