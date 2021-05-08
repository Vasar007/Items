using Acolyte.Assertions;
using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4.Tasks.Straightforward.WithRollback
{
    internal sealed class StraightforwardStatefulTaskWithRollbackWrapper<TContext> :
        NonFinalStraightforwardStatefulTaskWithRollbackBase<TContext>
    {
        private readonly StraightforwardStatefulTaskDoAction<TContext> _doAction;
        private readonly StatefulTaskRollbackAction<TContext> _rollbackAction;

        public StraightforwardStatefulTaskWithRollbackWrapper(
            StraightforwardStatefulTaskDoAction<TContext> doAction,
            StatefulTaskRollbackAction<TContext>? rollbackAction)
        {
            _doAction = doAction.ThrowIfNull(nameof(doAction));
            _rollbackAction = rollbackAction ??
                (context => StatefulTaskWithRollbackWrapper.DefaultTryRollbackSafe(context));
        }

        #region NonFinalStraightforwardStatefulTaskBase<TState> Overridden Methods

        protected override void DoActionInternal(TContext context)
        {
            _doAction(context);
        }

        protected override bool TryRollbackInternalSafe(TContext context)
        {
            return _rollbackAction(context);
        }

        #endregion
    }

    internal static class StraightforwardStatefulTaskWithRollbackWrapper
    {
        public static StraightforwardStatefulTaskWithRollbackWrapper<TState> Create<TState>(
            StraightforwardStatefulTaskDoAction<TState> doAction,
            StatefulTaskRollbackAction<TState>? rollbackAction)
        {
            return new StraightforwardStatefulTaskWithRollbackWrapper<TState>(
                doAction, rollbackAction
            );
        }
    }
}
