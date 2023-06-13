using Scriptables.Events;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Events/" + nameof(OnPlayerBecameInvisible))]
public class OnPlayerBecameInvisible : BaseEventSO<Transform>
{
}