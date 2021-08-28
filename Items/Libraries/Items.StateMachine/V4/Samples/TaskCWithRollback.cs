using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4.Samples
{
    public sealed class TaskCWithRollback : NonFinalStatefulTaskWithRollbackBase<Context, StateId>
    {
        public TaskCWithRollback()
        {
        }

        #region NonFinalStatefulTaskWithRollbackBase<Context, StateId> Overridden Methods

        protected override StateId DoActionInternal(Context context)
        {
            if (context.B > 10)
            {
                context.B *= 10;
                context.A = 24;
            }

            return StateId.StateB;
        }

        protected override bool TryRollbackInternal(Context context)
        {
            context.B /= 10;

            return true;
        }

        #endregion
    }
}
