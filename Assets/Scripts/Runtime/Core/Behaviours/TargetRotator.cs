using Extensions;
using RootMotion.FinalIK;
using UnityEngine;

public class TargetRotator
{
    public bool Active => _targetChecker.HaveElements && _rollMove.IsRoll == false;
    public Vector3 TargetDirection => (_target.Target.position - _playerTransform.position).WithY(0).normalized;
    private AimIK _aimIK;
    private Transform _playerTransform;
    private TagTriggerChecker<ITarget> _targetChecker;
    private RollMove _rollMove;
    private ITarget _target;

    public TargetRotator(Transform playerTransform, AimIK aimIK, TagTriggerChecker<ITarget> targetChecker,
        RollMove rollMove)
    {
        _playerTransform = playerTransform;
        _aimIK = aimIK;
        _targetChecker = targetChecker;
        _rollMove = rollMove;
    }

    private void UpdateTarget()
    {
        _target = _targetChecker.Elements.GetClosestTarget(_playerTransform.position);
        _aimIK.solver.target = _target.Target;
    }

    private void UpdateRotation(float rotationSpeed)
    {
        _playerTransform.forward = Vector3.Lerp(_playerTransform.transform.forward, TargetDirection, rotationSpeed);
        _aimIK.solver.SetIKPositionWeight(Mathf.Lerp(_aimIK.solver.GetIKPositionWeight(), 1, rotationSpeed / 2));
    }

    public void Update(float rotationSpeed)
    {
        if (Active == false)
        {
            _aimIK.solver.SetIKPositionWeight(0);
            return;
        }

        UpdateTarget();
        UpdateRotation(rotationSpeed);
    }
}