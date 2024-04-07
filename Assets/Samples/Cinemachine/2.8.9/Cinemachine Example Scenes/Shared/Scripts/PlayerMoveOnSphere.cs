using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class PlayerMoveOnSphere : MonoBehaviour
{
    public Transform _rotatable;
    public SphereCollider Sphere;
    public float speed = 5;
    public bool rotatePlayer = true;
    public float rotationDamping = 0.5f;
    public AxisConstraint _AxisConstraint;
    private UserInput _userInput;

    [Inject]
    private void Construct(UserInput userInput)
    {
        _userInput = userInput;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate(transform);
        // Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 input = new Vector3(_userInput.JoyStick.JoystickDirection.x, 0,
            _userInput.JoyStick.JoystickDirection.y);
        Vector3 moveDirection = new Vector3(_userInput.JoyStick.JoystickDirection.x, 0,
            _userInput.JoyStick.JoystickDirection.y).normalized;

        if (input.magnitude > 0)
        {
            input = Camera.main.transform.rotation * input;
            if (input.magnitude > 0.001f)
            {
                transform.position += input * (speed * Time.deltaTime);
                if (rotatePlayer)
                {
                    float t = Cinemachine.Utility.Damper.Damp(1, rotationDamping, Time.deltaTime);
                    Quaternion newRotation = Quaternion.LookRotation(input, transform.up);
                    // _rotatable.localRotation = newRotation;
                    transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, t);
                    // _rotatable.rotation.SetLookRotation(input.normalized, _rotatable.up);
                }
            }
        }

        // Stick to sphere surface
        if (Sphere != null)
        {
            var up = transform.position - Sphere.transform.position;
            up = up.normalized;
            var fwd = transform.forward.ProjectOntoPlane(up);
            transform.position = Sphere.transform.position + up * (Sphere.radius + transform.localScale.y / 2);
            transform.rotation = Quaternion.LookRotation(fwd, up);
        }
    }

    private void Rotate(Transform rotatable)
    {
        var gravityUp = (rotatable.position - Sphere.transform.position).normalized;
        var targetRotation = Quaternion.FromToRotation(rotatable.up, gravityUp) * rotatable.rotation;
        rotatable.rotation = Quaternion.Slerp(rotatable.rotation, targetRotation, 50 * Time.deltaTime);
    }
}