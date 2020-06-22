namespace Items.StateMachine.V1.States
{
    public sealed class TaskB : NonFinalStatefulTaskBase<State>
    {
        public TaskB()
        {
        }

        #region NonFinalStatefulTaskBase<State> Overridden Methods

        protected override IStatefulTask<State> DoActionInternal(State state)
        {
            (state.A, state.B) = (state.B, state.A);

            return new FinalStatefulTask<State>();
        }

        #endregion
    }
}
