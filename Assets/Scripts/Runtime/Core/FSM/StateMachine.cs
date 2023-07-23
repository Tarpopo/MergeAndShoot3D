using System.Collections.Generic;
using System.Linq;

namespace FSM
{
    public class StateMachine
    {
        public State CurrentState { get; private set; }
        private readonly List<State> _states;

        public StateMachine() => _states = new List<State>();

        public void AddState(State state) => _states.Add(state);

        public void Initialize<T>() where T : State
        {
            CurrentState = _states.FirstOrDefault(elem => elem is T);
            CurrentState.Enter();
        }

        public void ChangeState<T>() where T : State
        {
            if (CurrentState.IsStatePlay() || CurrentState is T) return;
            var newState = _states.FirstOrDefault(elem => elem is T);
            CurrentState.Exit();
            CurrentState = newState;
            newState.Enter();
        }

        // public void ChangeStateImmediately<V>() where V : State
        // {
        //     var newState = _states.FirstOrDefault(elem => elem is V);
        //     CurrentState.Exit();
        //     CurrentState = newState;
        //     newState.Enter();
        // }
    }
}