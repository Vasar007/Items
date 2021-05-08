using System;
using Acolyte.Assertions;
using Items.Common.Logging;
using Items.RollbackEngine.Simple;
using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4.Executors.WithRollback
{
    internal sealed class StateMachineWithRollbackEnumerator<TContext, TStateId, TStatefulTaskWithRollback> :
        StateMachineBaseEnumerator<TContext, TStateId, TStatefulTaskWithRollback>
        where TStatefulTaskWithRollback : class, IStatefulTaskWithRollback<TContext, TStateId>
    {
        private static ILogger Logger => StateMachineWithRollbackEnumerator.Logger;

        private readonly IStateMachineEnumerator<TContext, TStateId, TStatefulTaskWithRollback> _realEnumerator;

        private readonly RollbackScope<TContext> _rollbackScope;

        public override TContext Context => _realEnumerator.Context;

        public override TStatefulTaskWithRollback Current => _realEnumerator.Current;

        public override TStatefulTaskWithRollback this[TStateId stateId] => _realEnumerator[stateId];

        public StateMachineWithRollbackEnumerator(
            IStateMachineEnumerator<TContext, TStateId, TStatefulTaskWithRollback> realEnumerator,
            bool continueRollbackOnFailed)
        {
            _realEnumerator = realEnumerator.ThrowIfNull(nameof(realEnumerator));

            _rollbackScope = new RollbackScope<TContext>(
                continueRollbackOnFailed, realEnumerator.Context
            );
        }

        #region IDisposable Implementation

        private bool _disposed;

        protected override void Dispose(bool disposing)
        {
            if (_disposed) return;

            base.Dispose(disposing);
            if (disposing)
            {
                _rollbackScope.Dispose();
            }

            _disposed = true;
        }

        #endregion

        #region IEnumerator Implementation

        public override bool MoveNext()
        {
            var statefulTaskWithRollback = _realEnumerator.Current;
            if (statefulTaskWithRollback is null) throw GetStatefulTaskIsNullException();

            try
            {
                // Execute current task.
                bool canMoveNext = _realEnumerator.MoveNext();
                // "Current" property can change -> add to scope the previous one.
                _rollbackScope.Add(statefulTaskWithRollback);

                // On final task clear rollback scope.
                if (!canMoveNext)
                {
                    _rollbackScope.CommitAndClear();
                }

                return canMoveNext;
            }
            catch (Exception ex)
            {
                Logger.Exception(
                    ex, "Failed to execute state machine. Trying to rollback completed tasks."
                );
                _rollbackScope.TryRollbackSafe();

                // Rethrow original exception to signalize callers that state machine execution is failed.
                throw;
            }
        }

        #endregion
    }

    internal static class StateMachineWithRollbackEnumerator
    {
        internal static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor(typeof(StateMachineWithRollbackEnumerator));
    }
}
