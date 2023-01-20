using UnityEngine;

public class TriggerChecker : MonoBehaviour
{
    public BaseTriggerChecker BaseTriggerChecker => _baseTriggerChecker;
    [SerializeReference] private BaseTriggerChecker _baseTriggerChecker;

    public void SetBaseTriggerChecker(BaseTriggerChecker baseTriggerChecker) =>
        _baseTriggerChecker = baseTriggerChecker;

    private void OnTriggerEnter(Collider other) => _baseTriggerChecker?.OnTriggerEnter(other);

    private void OnTriggerExit(Collider other) => _baseTriggerChecker?.OnTriggerExit(other);
}