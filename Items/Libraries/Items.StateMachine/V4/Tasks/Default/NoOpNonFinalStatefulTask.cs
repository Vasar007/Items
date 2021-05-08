namespace Items.StateMachine.V4.Tasks.Default
{
    public sealed class NoOpNonFinalStatefulTask<TContext, TStateId> :
        NonFinalStatefulTaskBase<TContext, TStateId>
    {
        private readonly TStateId _stateIdToMove;

        public NoOpNonFinalStatefulTask(
            TStateId stateIdToMove)
        {
            _stateIdToMove = stateIdToMove;
        }

        #region NonFinalStatefulTaskBase<TContext, TStateId> Overridden Methods

        protected override TStateId DoActionInternal(TContext context)
        {
            return _stateIdToMove;
        }

        #endregion
    }

    public static class NoOpNonFinalStatefulTask<TContext>
    {
        public static NoOpNonFinalStatefulTask<TContext, TStateId> Create<TStateId>(
            TStateId stateIdToMove)
        {
            return new NoOpNonFinalStatefulTask<TContext, TStateId>(stateIdToMove);
        }
    }
}
