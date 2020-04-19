namespace Items.StateMachine.V2.States
{
    public sealed class TaskC : IStatefulTask<State, StateId>
    {
        public bool IsFinal { get; } = false;


        public TaskC()
        {
        }

        #region IStatefullTask<State, StateId> Implementation

        StateId IStatefulTask<State, StateId>.DoAction(State state)
        {
            if (state.B > 10)
            {
                state.B *= 10;
                state.A = 24;
            }

            return StateId.StateB;
        }

        #endregion
    }
}
