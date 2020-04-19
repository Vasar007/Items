namespace Items.StateMachine.V1.States
{
    public sealed class TaskB : IStatefulTask<State>
    {
        public bool IsFinal { get; } = true;


        public TaskB()
        {
        }

        #region IStatefulTask<State> Imlementation

        IStatefulTask<State> IStatefulTask<State>.DoAction(State state)
        {
            (state.A, state.B) = (state.B, state.A);

            return new FinalStatefulTask();
        }

        #endregion
    }
}
