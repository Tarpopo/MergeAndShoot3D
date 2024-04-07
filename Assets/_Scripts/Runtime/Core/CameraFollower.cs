using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private Transform _followPoint;
    private Vector3 _followDelta;

    private void Awake()
    {
        transform.SetParent(null);
        _followDelta = _followPoint.position - _playerTransform.position;
    }

    private void Update()
    {
        _followPoint.position = _playerTransform.position + _followDelta;
    }
}