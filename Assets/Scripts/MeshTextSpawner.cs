using DefaultNamespace;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MeshTextSpawner : ManagerBase, IStart
{
    [SerializeField] private TextMeshPro _textMeshPro;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _colorDuration;
    [SerializeField] private float _yValue;
    private ManagerPool _managerPool;
    private Sequence _sequence;

    public void SpawnText(string text, Color color, Vector3 position)
    {
        var textMesh = _managerPool.Spawn<TextMeshPro>(PoolType.Fx, _textMeshPro.gameObject, position);
        textMesh.text = text;
        textMesh.color = color;
        var textTransform = textMesh.transform;
        _sequence = DOTween.Sequence();
        _sequence.Append(textMesh.transform.DOMoveY(textTransform.position.y + _yValue, _moveDuration));
        _sequence.Join(textMesh.DOColor(Color.clear, _colorDuration));
        _sequence.onComplete = () => DespawnText(textMesh.gameObject);
    }

    private void DespawnText(GameObject prefab) => _managerPool.Despawn(PoolType.Fx, prefab);

    public void OnStart() => _managerPool = Toolbox.Get<ManagerPool>();
}