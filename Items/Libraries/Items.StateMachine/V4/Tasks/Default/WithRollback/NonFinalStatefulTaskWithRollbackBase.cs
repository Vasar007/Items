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

        bool IRollbackAction<TContext>.TryRollbackSafe(TContext context)
        {
            return TryRollbackInternalSafe(context);
        }

        protected abstract bool TryRollbackInternalSafe(TContext context);
    }
}
