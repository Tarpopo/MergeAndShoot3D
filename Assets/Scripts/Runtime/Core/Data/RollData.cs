using UnityEngine;

[CreateAssetMenu(menuName = "Data/" + nameof(RollData))]
public class RollData : ScriptableObject
{
    public float RollDuration => _rollDuration;
    public float RollDistance => _rollDistance;
    public float AfterRollDelay => _afterRollDelay;
    public float YMultiply => _yMultiply;
    public float RollSpeed => _rollSpeed;
    public AnimationCurve YCurve => _yCurve;

    [SerializeField] private float _afterRollDelay;
    [SerializeField] private float _rollDuration;
    [SerializeField] private float _rollDistance;
    [SerializeField] private float _rollSpeed;
    [SerializeField] private float _yMultiply;
    [SerializeField] private AnimationCurve _yCurve;
}