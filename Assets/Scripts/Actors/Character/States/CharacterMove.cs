using FSM;

public class CharacterMove : State<CharacterData>
{
    public CharacterMove(CharacterData data, StateMachine<CharacterData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter()
    {
        Data.AnimationComponent.PlayAnimation(UnitAnimations.Run);
    }

    public override void PhysicsUpdate()
    {
        Data.Canon.RotateWheels(1);
    }

    public override void Exit()
    {
    }
}