namespace Items.StateMachine.V2.States
{
    public sealed class TaskC : NonFinalStatefulTaskBase<State, StateId>
    {
        public TaskC()
        {
        }

        #region NonFinalStatefulTaskBase<State, StateId> Overridden Methods

        protected override StateId DoActionInternal(State state)
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
