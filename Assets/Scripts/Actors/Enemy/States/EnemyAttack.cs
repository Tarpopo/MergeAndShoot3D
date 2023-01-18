using FSM;

public class EnemyAttack : State<EnemyData>
{
    public EnemyAttack(EnemyData data, StateMachine<EnemyData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter()
    {
        Data.AnimationComponent.PlayAnimation(UnitAnimations.FirstAttack);
    }
}