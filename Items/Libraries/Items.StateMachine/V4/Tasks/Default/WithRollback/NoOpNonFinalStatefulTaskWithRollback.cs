namespace Items.StateMachine.V4.Tasks.Default.WithRollback
{
    public sealed class NoOpNonFinalStatefulTaskWithRollback<TContext, TStateId> :
        NonFinalStatefulTaskWithRollbackBase<TContext, TStateId>
    {
        private readonly TStateId _stateIdToMove;

        public NoOpNonFinalStatefulTaskWithRollback(
            TStateId stateIdToMove)
        {
            _stateIdToMove = stateIdToMove;
        }

        #region NonFinalStatefulTaskWithRollbackBase<TContext, TStateId> Overridden Methods

        protected override TStateId DoActionInternal(TContext context)
        {
            return _stateIdToMove;
        }

        protected override bool TryRollbackInternalSafe(TContext context)
        {
            // Nothing to rollback.
            return true;
        }

        #endregion
    }

    public static class NoOpNonFinalStatefulTaskWithRollback<TContext>
    {
        public static NoOpNonFinalStatefulTaskWithRollback<TContext, TStateId> Create<TStateId>(
            TStateId stateIdToMove)
        {
            return new NoOpNonFinalStatefulTaskWithRollback<TContext, TStateId>(stateIdToMove);
        }
    }
}
