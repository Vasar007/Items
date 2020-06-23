namespace Items.StateMachine.V2.States
{
    public sealed class NoOpNonFinalStatefulTask<TState, TStateId>
        : NonFinalStatefulTaskBase<TState, TStateId>
        where TState : class
    {
        private readonly TStateId _stateIdToMove;

        public NoOpNonFinalStatefulTask(TStateId stateIdToMove)
        {
            _stateIdToMove = stateIdToMove;
        }

        #region NonFinalStatefulTaskBase<TState, TStateId> Overridden Methods

        protected override TStateId DoActionInternal(TState state)
        {
            return _stateIdToMove;
        }

        #endregion
    }

    public static class NoOpNonFinalStatefulTask<TState>
        where TState : class
    {
        public static NoOpNonFinalStatefulTask<TState, TStateId> Create<TStateId>(
            TStateId stateIdToMove)
        {
            return new NoOpNonFinalStatefulTask<TState, TStateId>(stateIdToMove);
        }
    }
}
