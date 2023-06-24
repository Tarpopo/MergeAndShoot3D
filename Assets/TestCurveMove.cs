using AmazingAssets.CurvedWorld;
using UnityEngine;

public class TestCurveMove : MonoBehaviour
{
    public Transform _target;
    public float _moveSpeed;
    public CurvedWorldController curvedWorldController;

    public Transform parent;

    public Vector3 offset;
    public bool recalculateRotation;
    private Vector3 _moveDirection;


    void Start()
    {
        if (parent == null)
            parent = transform.parent;
        // _moveDirection = (_target.position - transform.position).normalized;
        _moveDirection = curvedWorldController.TransformPosition(_target.position - transform.position).normalized;
    }

    private void LateUpdate()
    {
        if (parent == null || curvedWorldController == null)
        {
            //Do nothing
        }
        else
        {
            //Transform position
            offset += _moveDirection * (_moveSpeed * Time.deltaTime);
            transform.position = curvedWorldController.TransformPosition(parent.position + offset);


            //Transform normal (calcualte rotation)
            if (recalculateRotation)
                transform.rotation =
                    curvedWorldController.TransformRotation(parent.position + offset, parent.forward, parent.right);
        }
    }
}