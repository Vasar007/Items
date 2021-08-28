using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Items.Common.Logging;

namespace Items.RollbackEngine.Simple
{
    public sealed class RollbackScope : IDisposable
    {
        internal static readonly ILogger Logger =
           LoggerFactory.CreateLoggerFor<RollbackScope>();

        internal const string FailedToAddActionToDisposed =
            "Failed to add action to disposed scope.";

        private readonly Stack<IRollbackAction> _rollbackActions;
        private readonly bool _continueRollbackOnFailed;


        public RollbackScope(
            bool continueRollbackOnFailed)
        {
            _continueRollbackOnFailed = continueRollbackOnFailed;

            _rollbackActions = new Stack<IRollbackAction>();
        }

        public RollbackScope(
            bool continueRollbackOnFailed,
            int capacity)
        {
            _continueRollbackOnFailed = continueRollbackOnFailed;

            _rollbackActions = new Stack<IRollbackAction>(capacity);
        }

        public RollbackScope(
            bool continueRollbackOnFailed,
            IEnumerable<IRollbackAction> rollbackActions)
        {
            rollbackActions.ThrowIfNull(nameof(rollbackActions));
            _continueRollbackOnFailed = continueRollbackOnFailed;

            _rollbackActions = new Stack<IRollbackAction>(rollbackActions);
        }

        public void Add(IRollbackAction rollbackAction)
        {
            rollbackAction.ThrowIfNull(nameof(rollbackAction));

            EnsureScopeIsNotDisposed(_disposed);

            _rollbackActions.Push(rollbackAction);
        }

        public void Add(IEnumerable<IRollbackAction> rollbackActions)
        {
            rollbackActions.ThrowIfNull(nameof(rollbackActions));

            EnsureScopeIsNotDisposed(_disposed);

            foreach (IRollbackAction rollbackAction in rollbackActions)
            {
                _rollbackActions.Push(rollbackAction);
            }
        }

        public bool TryRollbackSafe()
        {
            return TryRollbackInternalSafe(disposing: false);
        }

        public void Clear()
        {
            Logger.Debug($"Clearing {_rollbackActions.Count.ToString()} rollback actions.");

            _rollbackActions.Clear();
            _rollbackActions.TrimExcess();
        }

        public void CommitAndClear()
        {
            Logger.Debug(
                $"Committing {_rollbackActions.Count.ToString()} rollback actions. " +
                "Rollback will not be perform."
            );
            _disposed = true;

            Clear();
        }

        #region IDisposable Implementation

        private bool _disposed;

        public void Dispose()
        {
            if (_disposed) return;

            if (TryRollbackInternalSafe(disposing: true))
            {
                Logger.Debug("Rollback all actions successfully.");
            }
            else
            {
                Logger.Warning("Failed to perform rollback for actions.");
            }

            Clear();

            _disposed = true;
        }

        #endregion

        internal static void EnsureScopeIsNotDisposed(bool disposed)
        {
            if (disposed)
            {
                throw new ObjectDisposedException(
                    nameof(RollbackScope), FailedToAddActionToDisposed
                );
            }
        }

        private bool ShouldContinueRollback(bool isSuccessfulRollback)
        {
            return _rollbackActions.Count > 0 &&
                   (isSuccessfulRollback || _continueRollbackOnFailed);
        }

        private bool TryRollbackInternalSafe(bool disposing)
        {
            if (_disposed)
            {
                Logger.Warning(
                    "Tried to rollback actions for disposed scope. Rollback will not be perform."
                );
                return false;
            }

            if (_rollbackActions.Count == 0)
            {
                Logger.Debug("No rollback actions to perform.");
                return true;
            }

            string message =
                $"Performing {_rollbackActions.Count.ToString()} rollback actions. " +
                $"Continue rollback on failed: '{_continueRollbackOnFailed.ToString()}'.";
            if (disposing)
            {
                Logger.Warning(message);
            }
            else
            {
                Logger.Debug(message);
            }

            bool isSuccessfulRollback = true;
            while (ShouldContinueRollback(isSuccessfulRollback))
            {
                IRollbackAction rollbackAction = _rollbackActions.Pop();
                isSuccessfulRollback = InternalRollbackSafe(rollbackAction);
            }

            Logger.Debug(
                $"Scope remains {_rollbackActions.Count.ToString()} non-completed rollback " +
                "action. Some actions may failed."
            );
            _rollbackActions.TrimExcess();
            return isSuccessfulRollback;
        }

        private bool InternalRollbackSafe(IRollbackAction rollbackAction)
        {
            try
            {
                Logger.Debug(
                    "Trying to rollback action. If exception will occur, this will be considered " +
                    "as a failed rollback."
                );
                return rollbackAction.TryRollback();
            }
            catch (Exception ex)
            {
                string lastPartOfMessage = _continueRollbackOnFailed
                    ? "Continue rollback on failed option is enabled, the rollback will continue."
                    : "Continue rollback on failed option is disabled, the rest of rollback " +
                      "actions will be skipped.";

                Logger.Exception(
                    ex, $"Failed to perform safe rollback for action. {lastPartOfMessage}"
                );
                return false;
            }
        }
    }
}
