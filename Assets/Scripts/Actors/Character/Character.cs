using FSM;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private CharacterData _characterData;
    private StateMachine<CharacterData> _stateMachine;

    private void Start()
    {
        _stateMachine = new StateMachine<CharacterData>();
        _stateMachine.AddState(new CharacterIdle(_characterData, _stateMachine));
        _stateMachine.AddState(new CharacterMove(_characterData, _stateMachine));
        _stateMachine.Initialize<CharacterIdle>();
        var pointMover = Toolbox.Get<PointMover>();
        _stateMachine.ChangeState<CharacterMove>();
        pointMover.MoveToPoint(transform, _characterData.MoveDuration, SetIdleState);
    }

    private void SetIdleState() => _stateMachine.ChangeState<CharacterIdle>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _stateMachine.ChangeState<CharacterMove>();
            Toolbox.Get<PointMover>().MoveToPoint(transform, _characterData.MoveDuration, SetIdleState);
        }

        _stateMachine.CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        _stateMachine.CurrentState.PhysicsUpdate();
    }
}