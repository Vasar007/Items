using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4.Samples
{
    public sealed class TaskBWithRollback : NonFinalStatefulTaskWithRollbackBase<Context, StateId>
    {
        public TaskBWithRollback()
        {
        }

        #region NonFinalStatefulTaskWithRollbackBase<Context, StateId> Overridden Methods

        protected override StateId DoActionInternal(Context context)
        {
            (context.A, context.B) = (context.B, context.A);

            return StateId.Final;
        }

        protected override bool TryRollbackInternalSafe(Context context)
        {
            (context.B, context.A) = (context.A, context.B);

            return true;
        }

        #endregion
    }
}
