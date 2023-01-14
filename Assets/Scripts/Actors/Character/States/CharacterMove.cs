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
        Data.Weapon.RotateWheels();
    }

    public override void Exit()
    {
    }
}