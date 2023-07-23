using UnityEngine;
using UnityEngine.AI;

public class NavMeshMove : IMove
{
    public bool IsClose => Close(_navMeshAgent.stoppingDistance);
    public Vector3 EndPathPosition => _navMeshAgent.pathEndPosition;
    private readonly NavMeshAgent _navMeshAgent;

    public NavMeshMove(NavMeshAgent navMeshAgent) => _navMeshAgent = navMeshAgent;

    public bool Close(float stopDistance) =>
        _navMeshAgent.pathPending == false && _navMeshAgent.remainingDistance <= stopDistance;

    public void Move(Vector3 direction, float moveSpeed)
    {
        //if (_navMeshAgent.isActiveAndEnabled) 
        _navMeshAgent.Warp(direction);
    }

    public void SetMoveDestination(Vector3 point, float moveSpeed)
    {
        _navMeshAgent.speed = moveSpeed;
        _navMeshAgent.SetDestination(point);
    }

    public void StopMove()
    {
        if (_navMeshAgent.isOnNavMesh) _navMeshAgent.ResetPath();
    }

    public void DisableNavmesh() => _navMeshAgent.enabled = false;

    public void EnableNavMesh() => _navMeshAgent.enabled = true;
}