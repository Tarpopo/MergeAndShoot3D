using Scriptables.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/" + nameof(OnJoystickMove))]
public class OnJoystickMove : BaseEventSO<Vector2>
{
}