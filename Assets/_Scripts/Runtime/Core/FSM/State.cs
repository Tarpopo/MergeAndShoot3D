namespace FSM
{
    public abstract class State
    {
        protected readonly StateMachine Machine;

        protected State(StateMachine stateMachine) => Machine = stateMachine;

        public virtual bool IsStatePlay() => false;

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void LogicUpdate()
        {
        }

        public virtual void PhysicsUpdate()
        {
        }
    }
}