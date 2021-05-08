namespace Items.StateMachine.V2.States
{
    public sealed class TaskB : NonFinalStatefulTaskBase<State, StateId>
    {
        public TaskB()
        {
        }

        #region NonFinalStatefulTaskBase<State, StateId> Overridden Methods

        protected override StateId DoActionInternal(State state)
        {
            (state.A, state.B) = (state.B, state.A);

            return StateId.Final;
        }

        #endregion
    }
}
