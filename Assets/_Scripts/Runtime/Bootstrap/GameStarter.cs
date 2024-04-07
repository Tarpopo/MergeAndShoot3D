using FSM;
using UnityEngine;
using Zenject;

public class GameStarter : ITickable, IFixedTickable, IInitializable
{
    private readonly StateMachine _gameStateMachine = new StateMachine();
    private readonly LevelLoader _levelLoader;

    public GameStarter(LevelLoader levelLoader)
    {
        _levelLoader = levelLoader;
    }

    public void Tick() => _gameStateMachine.CurrentState.LogicUpdate();

    public void FixedTick() => _gameStateMachine.CurrentState.PhysicsUpdate();

    public void Initialize()
    {
        _gameStateMachine.AddState(new GameLoopState(_gameStateMachine));
        _gameStateMachine.AddState(new GameMenuState(_gameStateMachine));
        _gameStateMachine.AddState(new GamePauseState(_gameStateMachine));
        _gameStateMachine.AddState(new GameWinState(_gameStateMachine));
        _gameStateMachine.Initialize<GameLoopState>();
        Application.targetFrameRate = 60;
    }
}