using FSM;

public class CharacterDeath : State<CharacterData>
{
    public CharacterDeath(CharacterData data, StateMachine<CharacterData> stateMachine) : base(data, stateMachine)
    {
    }

    public override void Enter() => Data.AnimationComponent.PlayAnimation(UnitAnimations.Death);
    public override bool IsStatePlay() => Data.Damageable.IsAlive == false;
}