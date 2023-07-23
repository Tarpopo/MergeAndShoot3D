using System;
using FSM;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    private readonly StateMachine _gameStateMachine = new StateMachine();

    private void Awake()
    {
        _gameStateMachine.AddState(new GameLoopState(_gameStateMachine));
        _gameStateMachine.AddState(new GameMenuState(_gameStateMachine));
        _gameStateMachine.AddState(new GamePauseState(_gameStateMachine));
        _gameStateMachine.AddState(new GameWinState(_gameStateMachine));
        _gameStateMachine.Initialize<GameLoopState>();
    }

    private void Start()
    {
        Toolbox.Get<LevelLoader>().LoadStartLevel();
        Toolbox.Get<ApplicationSetter>().SetFrameRate();
    }

    private void Update() => _gameStateMachine.CurrentState.LogicUpdate();

    private void FixedUpdate() => _gameStateMachine.CurrentState.PhysicsUpdate();
// #if UNITY_EDITOR
//     [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
//     private static void OnBeforeSceneLoad() => SceneManager.LoadScene("MainLogic", LoadSceneMode.Additive);
// #endif
}