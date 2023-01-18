using DG.Tweening;
using FSM;

public class CharacterAttack : State<CharacterData>
{
    private Tween _tween;
    private Enemy _enemy;

    public CharacterAttack(CharacterData data, StateMachine<CharacterData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter()
    {
        if (Data.EnemySpawner.HaveEnemies == false ||
            Data.EnemySpawner.TryGetClosetEnemy(Data.Transform, out var enemy) == false)
        {
            Machine.ChangeState<CharacterIdle>();
            return;
        }

        if (_enemy == enemy)
        {
            Data.AnimationComponent.PlayAnimation(UnitAnimations.FirstAttack);
            return;
        }

        _enemy = enemy;
        Data.Transform.DOLookAt(enemy.transform.position, Data.RotateDuration).TryPlayTween(ref _tween);
        _tween.onPlay += () => Data.AnimationComponent.PlayAnimation(UnitAnimations.Run);
        _tween.onComplete += () => Data.AnimationComponent.PlayAnimation(UnitAnimations.FirstAttack);
    }

    public override void Exit()
    {
        _tween?.Kill();
    }
}