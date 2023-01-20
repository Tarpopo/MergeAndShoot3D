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
        Attack();
        Data.ShootBoosterSlider.OnFastSet += Attack;
    }

    public override void LogicUpdate() => Data.Timer.UpdateTimer();

    public override void Exit()
    {
        Data.ShootBoosterSlider.OnFastSet -= Attack;
        _tween?.Kill();
    }

    private void Attack()
    {
        if (Data.EnemySpawner.TryGetClosetEnemy(Data.Transform, out var enemy) == false) return;
        if (_enemy == enemy)
        {
            Data.AnimationComponent.PlayAnimation(UnitAnimations.FirstAttack);
        }
        else
        {
            Data.Transform.DOLookAt(enemy.transform.position, Data.RotateDuration).TryPlayTween(ref _tween);
            _tween.onPlay += () => Data.AnimationComponent.PlayAnimation(UnitAnimations.Run);
            _tween.onComplete += () => Data.AnimationComponent.PlayAnimation(UnitAnimations.FirstAttack);
        }

        _enemy = enemy;
        Data.Timer.StartTimer(Data.AttackDuration, Attack);
    }
}