using FSM;
using UnityEngine;

public class EnemyMove : State<EnemyData>
{
    public EnemyMove(EnemyData data, StateMachine<EnemyData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter() => Data.AnimationComponent.PlayAnimation(UnitAnimations.Run);

    public override void LogicUpdate()
    {
        if (Vector3.Distance(Data.Transform.position, Data.PlayerPosition) <= Data.AttackDistance)
        {
            Machine.ChangeState<EnemyAttack>();
            return;
        }

        var position = Data.Transform.position;
        var direction = (Data.PlayerPosition - position).normalized;
        position += direction * (Data.MoveSpeed * Time.deltaTime);
        Data.Transform.position = position;
    }
}