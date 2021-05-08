using Acolyte.Assertions;

namespace Items.StateMachine.V4.Tasks.Straightforward
{
    internal sealed class StraightforwardStatefulTaskWrapper<TContext> :
        NonFinalStraightforwardStatefulTaskBase<TContext>
    {
        private readonly StraightforwardStatefulTaskDoAction<TContext> _doAction;

        public StraightforwardStatefulTaskWrapper(
            StraightforwardStatefulTaskDoAction<TContext> doAction)
        {
            _doAction = doAction.ThrowIfNull(nameof(doAction));
        }

        #region NonFinalStraightforwardStatefulTaskBase<TContext> Overridden Methods

        protected override void DoActionInternal(TContext context)
        {
            _doAction(context);
        }

        #endregion
    }

    internal static class StraightforwardStatefulTaskWrapper
    {
        public static StraightforwardStatefulTaskWrapper<TContext> Create<TContext>(
            StraightforwardStatefulTaskDoAction<TContext> doAction)
        {
            return new StraightforwardStatefulTaskWrapper<TContext>(doAction);
        }
    }
}
