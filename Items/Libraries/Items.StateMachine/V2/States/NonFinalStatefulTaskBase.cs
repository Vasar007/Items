namespace Items.StateMachine.V2.States
{
    public abstract class NonFinalStatefulTaskBase<TState, TStateId> :
        IStatefulTask<TState, TStateId>
        where TState : class
    {
        bool IStatefulTask<TState, TStateId>.IsFinal { get; } = false;

        protected NonFinalStatefulTaskBase()
        {
        }

        TStateId IStatefulTask<TState, TStateId>.DoAction(TState state)
        {
            return DoActionInternal(state);
        }

        protected abstract TStateId DoActionInternal(TState state);
    }
}
