using System;
using Items.StateMachine.V4.Tasks.Default;

namespace Items.StateMachine.V4.Samples
{
    public sealed class TaskA : NonFinalStatefulTaskBase<Context, StateId>
    {
        public TaskA()
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

        #endregion
    }
}
