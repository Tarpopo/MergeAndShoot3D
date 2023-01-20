using FSM;

public class CharacterIdle : State<CharacterData>
{
    public CharacterIdle(CharacterData data, StateMachine<CharacterData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter() => Data.AnimationComponent.PlayAnimation(UnitAnimations.Idle);

    // private void TryAttack()
    // {
    //     Data.Timer.StartTimer(Data.IdleTime, TryAttack);
    //     if (Data.EnemySpawner.TryGetClosetEnemy(Data.Transform, out var enemy) == false) return;
    //     if (Vector3.Distance(Data.Transform.position, enemy.transform.position) > Data.AttackDistance) return;
    //     Machine.ChangeState<CharacterAttack>();
    // }
}