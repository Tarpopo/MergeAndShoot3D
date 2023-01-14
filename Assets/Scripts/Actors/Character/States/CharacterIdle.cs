using FSM;
using UnityEngine;

public class CharacterIdle : State<CharacterData>
{
    public CharacterIdle(CharacterData data, StateMachine<CharacterData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter()
    {
        Data.AnimationComponent.PlayAnimation(UnitAnimations.Idle);
    }
}