namespace Items.StateMachine.V1.States
{
    public sealed class FinalStatefulTask<TState> : IStatefulTask<TState>
        where TState : class
    {
        public bool IsFinal { get; } = true;


        public FinalStatefulTask()
        {
        }

        #region IStatefullTask<TState> Implementation

        IStatefulTask<TState> IStatefulTask<TState>.DoAction(TState state)
        {
            return new FinalStatefulTask<TState>();
        }

        #endregion
    }
}
