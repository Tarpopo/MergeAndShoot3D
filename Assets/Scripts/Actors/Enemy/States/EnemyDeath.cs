using FSM;

public class EnemyDeath : State<EnemyData>
{
    public EnemyDeath(EnemyData data, StateMachine<EnemyData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter() => Data.AnimationComponent.PlayAnimation(UnitAnimations.Death);
    public override bool IsStatePlay() => Data.Damageable.IsAlive == false;
}