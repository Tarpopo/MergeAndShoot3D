using Sirenix.OdinInspector;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _bodies;
    [SerializeField] private Transform[] _rotatables;
    public float gravity = -10;

    private void Attract(Rigidbody rigidbody)
    {
        var bodyTransform = rigidbody.transform;
        var gravityUp = (bodyTransform.position - transform.position).normalized;
        var targetRotation = Quaternion.FromToRotation(bodyTransform.up, gravityUp) * bodyTransform.rotation;
        bodyTransform.rotation = Quaternion.Slerp(bodyTransform.rotation, targetRotation, 50 * Time.deltaTime);
        rigidbody.AddForce(gravityUp * gravity);
    }

    [Button]
    private void RotateAll()
    {
        foreach (var rotatable in _rotatables) Rotate(rotatable);
    }

    private void Rotate(Transform rotatable)
    {
        var gravityUp = (rotatable.position - transform.position).normalized;
        var targetRotation = Quaternion.FromToRotation(rotatable.up, gravityUp) * rotatable.rotation;
        rotatable.rotation = Quaternion.Slerp(rotatable.rotation, targetRotation, 50 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        foreach (var rigidbody in _bodies) Attract(rigidbody);
    }
}