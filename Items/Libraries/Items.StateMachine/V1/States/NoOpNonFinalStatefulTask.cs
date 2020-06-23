namespace Items.StateMachine.V1.States
{
    public sealed class NoOpNonFinalStatefulTask<TState> : NonFinalStatefulTaskBase<TState>
        where TState : class
    {
        private readonly IStatefulTask<TState> _stateToMove;

        public NoOpNonFinalStatefulTask(IStatefulTask<TState> stateIdToMove)
        {
            _stateToMove = stateIdToMove;
        }

        #region NonFinalStatefulTaskBase<TState> Overridden Methods

        protected override IStatefulTask<TState> DoActionInternal(TState state)
        {
            return _stateToMove;
        }

        #endregion
    }
}
