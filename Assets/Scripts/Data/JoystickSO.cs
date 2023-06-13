using UnityEngine;

[CreateAssetMenu(menuName = "Data/" + nameof(JoystickSO))]
public class JoystickSO : ScriptableObject
{
    public float MinActiveRadius => _minActiveRadius;
    public float RadiusInsideCircle => _radiusInsideCircle;
    public OnJoystickUp OnJoystickUp;
    public OnJoystickMove OnJoystickMove;
    [SerializeField] private float _radiusInsideCircle = 50;
    [SerializeField] private float _minActiveRadius = 10;
}