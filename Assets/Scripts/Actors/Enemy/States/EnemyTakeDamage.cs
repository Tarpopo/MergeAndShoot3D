using FSM;

public class EnemyTakeDamage : State<EnemyData>
{
    public EnemyTakeDamage(EnemyData data, StateMachine<EnemyData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter() => Data.AnimationComponent.PlayAnimation(UnitAnimations.TakeDamage);
}