namespace Items.StateMachine.V4.Tasks.Default
{
    public abstract class NonFinalStatefulTaskBase<TContext, TStateId> :
        IStatefulTask<TContext, TStateId>
    {
        bool IStatefulTask<TContext, TStateId>.IsFinal { get; } = false;

        protected NonFinalStatefulTaskBase()
        {
        }

        TStateId IStatefulTask<TContext, TStateId>.DoAction(TContext context)
        {
            return DoActionInternal(context);
        }

        protected abstract TStateId DoActionInternal(TContext context);
    }
}
