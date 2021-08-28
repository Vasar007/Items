using Items.RollbackEngine.Simple;

namespace Items.StateMachine.V4.Tasks.Straightforward.WithRollback
{
    public abstract class NonFinalStraightforwardStatefulTaskWithRollbackBase<TContext> :
        IStraightforwardStatefulTaskWithRollback<TContext>
    {
        bool IStatefulTask<TContext, int>.IsFinal { get; } = false;

        protected NonFinalStraightforwardStatefulTaskWithRollbackBase()
        {
        }

        int IStatefulTask<TContext, int>.DoAction(TContext context)
        {
            DoActionInternal(context);
            // Logic to determine the next state is up to state machine enumerator.
            return default;
        }

        protected abstract void DoActionInternal(TContext context);

        bool IRollbackAction<TContext>.TryRollback(TContext context)
        {
            return TryRollbackInternal(context);
        }

        protected abstract bool TryRollbackInternal(TContext context);
    }
}
