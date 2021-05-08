using Items.StateMachine.V4.Tasks.Default;

namespace Items.StateMachine.V4.Samples
{
    public sealed class TaskB : NonFinalStatefulTaskBase<Context, StateId>
    {
        public TaskB()
        {
        }

        #region NonFinalStatefulTaskBase<Context, StateId> Overridden Methods

        protected override StateId DoActionInternal(Context context)
        {
            (context.A, context.B) = (context.B, context.A);

            return StateId.Final;
        }

        #endregion
    }
}
