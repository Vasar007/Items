namespace Items.StateMachine.V1.States
{
    public sealed class TaskC : NonFinalStatefulTaskBase<State>
    {
        public TaskC()
        {
        }

        #region NonFinalStatefulTaskBase<State> Overridden Methods

        protected override IStatefulTask<State> DoActionInternal(State state)
        {
            if (state.B > 10)
            {
                state.B *= 10;
                state.A = 24;
            }

            return new TaskB();
        }

        #endregion
    }
}
