namespace Items.StateMachine.V2.States
{
    public sealed class TaskB : IStatefulTask<State, StateId>
    {
        public bool IsFinal { get; } = true;


        public TaskB()
        {
        }

        #region IStatefulTask<State, StateId> Imlementation

        StateId IStatefulTask<State, StateId>.DoAction(State state)
        {
            (state.A, state.B) = (state.B, state.A);

            return StateId.FinalState;
        }

        #endregion
    }
}
