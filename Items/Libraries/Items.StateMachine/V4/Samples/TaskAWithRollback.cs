using System;
using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4.Samples
{
    public sealed class TaskAWithRollback : NonFinalStatefulTaskWithRollbackBase<Context, StateId>
    {
        public TaskAWithRollback()
        {
        }

        #region NonFinalStatefulTaskBase<Context, StateId> Overridden Methods

        protected override StateId DoActionInternal(Context context)
        {
            if (context.A > 10)
            {
                context.A *= 10;
                context.B = 42;
            }

            throw new Exception("Something goes wrong.");
        }

        protected override bool TryRollbackInternal(Context context)
        {
            context.A /= 10;

            return true;
        }

        #endregion
    }
}
