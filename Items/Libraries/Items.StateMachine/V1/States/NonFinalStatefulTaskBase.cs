namespace Items.StateMachine.V1.States
{
    public abstract class NonFinalStatefulTaskBase<TState> : IStatefulTask<TState>
        where TState : class
    {
        bool IStatefulTask<TState>.IsFinal { get; } = false;

        protected NonFinalStatefulTaskBase()
        {
        }

        #region IStatefullTask<State> Implementation

        IStatefulTask<TState> IStatefulTask<TState>.DoAction(TState state)
        {
            return DoActionInternal(state);
        }

        protected abstract IStatefulTask<TState> DoActionInternal(TState state);

        #endregion
    }
}
