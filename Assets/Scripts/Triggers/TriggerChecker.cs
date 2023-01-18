using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    public BaseTriggerChecker BaseTriggerChecker => _baseTriggerChecker;
    [SerializeReference] private BaseTriggerChecker _baseTriggerChecker;

    public void SetBaseTriggerChecker(BaseTriggerChecker baseTriggerChecker) =>
        _baseTriggerChecker = baseTriggerChecker;

    private void OnTriggerEnter2D(Collider2D other) => _baseTriggerChecker?.OnTriggerEnter2D(other);

    private void OnTriggerExit2D(Collider2D other) => _baseTriggerChecker?.OnTriggerExit2D(other);
}