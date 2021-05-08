using Items.StateMachine.V4.Tasks.Default;

namespace Items.StateMachine.V4.Samples
{
    public sealed class TaskC : NonFinalStatefulTaskBase<Context, StateId>
    {
        public TaskC()
        {
        }

        #region NonFinalStatefulTaskBase<Context, StateId> Overridden Methods

        protected override StateId DoActionInternal(Context context)
        {
            if (context.B > 10)
            {
                context.B *= 10;
                context.A = 24;
            }

            return StateId.StateB;
        }

        #endregion
    }
}
