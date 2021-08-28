using Items.RollbackEngine.Simple;

namespace Items.StateMachine.V4.Tasks.Default.WithRollback
{
    public abstract class NonFinalStatefulTaskWithRollbackBase<TContext, TStateId> :
        IStatefulTaskWithRollback<TContext, TStateId>
    {
        bool IStatefulTask<TContext, TStateId>.IsFinal { get; } = false;

        protected NonFinalStatefulTaskWithRollbackBase()
        {
        }

        TStateId IStatefulTask<TContext, TStateId>.DoAction(TContext context)
        {
            return DoActionInternal(context);
        }

        protected abstract TStateId DoActionInternal(TContext context);

        bool IRollbackAction<TContext>.TryRollback(TContext context)
        {
            return TryRollbackInternal(context);
        }

        protected abstract bool TryRollbackInternal(TContext context);
    }
}
