using Acolyte.Assertions;
using Items.Common.Logging;

namespace Items.StateMachine.V4.Tasks.Default.WithRollback
{
    internal sealed class StatefulTaskWithRollbackWrapper<TContext, TStateId> :
        NonFinalStatefulTaskWithRollbackBase<TContext, TStateId>
    {
        private readonly StatefulTaskDoAction<TContext, TStateId> _doAction;
        private readonly StatefulTaskRollbackAction<TContext> _rollbackAction;

        public StatefulTaskWithRollbackWrapper(
            StatefulTaskDoAction<TContext, TStateId> doAction,
            StatefulTaskRollbackAction<TContext>? rollbackAction)
        {
            _doAction = doAction.ThrowIfNull(nameof(doAction));
            _rollbackAction = rollbackAction ??
                (context => StatefulTaskWithRollbackWrapper.DefaultTryRollbackSafe(context));
        }

        #region NonFinalStatefulTaskBase<TContext, TStateId> Overridden Methods

        protected override TStateId DoActionInternal(TContext context)
        {
            return _doAction(context);
        }

        protected override bool TryRollbackInternal(TContext context)
        {
            return _rollbackAction(context);
        }

        #endregion
    }

    internal static class StatefulTaskWithRollbackWrapper
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor(typeof(StatefulTaskWithRollbackWrapper));

        public static StatefulTaskWithRollbackWrapper<TContext, TStateId> Create<TContext, TStateId>(
            StatefulTaskDoAction<TContext, TStateId> doAction,
            StatefulTaskRollbackAction<TContext>? rollbackAction)
        {
            return new StatefulTaskWithRollbackWrapper<TContext, TStateId>(
                doAction, rollbackAction
            );
        }

        internal static bool DefaultTryRollbackSafe<TContext>(TContext _)
        {
            Logger.Message("Rollback. Nothing to rollback.");
            return true;
        }
    }
}
