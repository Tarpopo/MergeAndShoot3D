using FSM;

public class EnemyIdle : State<EnemyData>
{
    public EnemyIdle(EnemyData data, StateMachine<EnemyData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter() => Data.AnimationComponent.PlayAnimation(UnitAnimations.Idle);

    public override void LogicUpdate() => Data.Timer.UpdateTimer();
}