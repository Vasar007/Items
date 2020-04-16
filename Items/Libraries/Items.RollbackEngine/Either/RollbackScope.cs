using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Items.Common.Logging;

namespace Items.RollbackEngine.Either
{
    public sealed class RollbackScope : IDisposable
    {
        private static readonly ILogger Logger = LoggerFactory.CreateLoggerFor<RollbackScope>();

        private readonly Stack<IRollbackAction> _rollbackActions;


        public RollbackScope()
        {
            _rollbackActions = new Stack<IRollbackAction>();
        }

        public RollbackScope(int capacity)
        {
            _rollbackActions = new Stack<IRollbackAction>(capacity);
        }

        public RollbackScope(IEnumerable<IRollbackAction> rollbackActions)
        {
            _rollbackActions = new Stack<IRollbackAction>(rollbackActions);
        }

        public void Add(IRollbackAction rollbackAction)
        {
            rollbackAction.ThrowIfNull(nameof(rollbackAction));

            if (_disposed)
                throw new ObjectDisposedException(nameof(RollbackScope), "Failed to add action to disposed scope.");

            _rollbackActions.Push(rollbackAction);
        }

        public void Add(IEnumerable<IRollbackAction> rollbackActions)
        {
            rollbackActions.ThrowIfNull(nameof(rollbackActions));

            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(RollbackScope), "Failed to add action to disposed scope.");
            }

            foreach (IRollbackAction rollbackAction in rollbackActions)
            {
                _rollbackActions.Push(rollbackAction);
            }
        }

        public bool TryRollbackSafe()
        {
            if (_disposed)
            {
                Logger.Warning("Tried to rollback actions for disposed scope. Rollback will not be perform.");
                return false;
            }

            Logger.Warning($"Performing {_rollbackActions.Count.ToString()} rollback actions.");

            bool isSuccessfulRollback = true;
            while (_rollbackActions.Count > 0 && isSuccessfulRollback)
            {
                IRollbackAction rollbackAction = _rollbackActions.Pop();
                isSuccessfulRollback = rollbackAction.TryRollbackSafe();
            }

            _rollbackActions.TrimExcess();
            return isSuccessfulRollback;
        }

        public void Clear()
        {
            Logger.Message($"Clearing {_rollbackActions.Count.ToString()} rollback actions.");

            _rollbackActions.Clear();
            _rollbackActions.TrimExcess();
        }

        public void CommitAndClear()
        {
            Logger.Message($"Committing {_rollbackActions.Count.ToString()} rollback actions. Rollback will not be perform.");
            _disposed = true;

            Clear();
        }

        #region Implemenation of IDisposable

        private bool _disposed;

        public void Dispose()
        {
            if (_disposed) return;

            TryRollbackSafe();
            Clear();

            _disposed = true;
        }

        #endregion
    }
}
