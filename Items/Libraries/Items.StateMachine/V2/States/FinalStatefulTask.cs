namespace Items.StateMachine.V2.States
{
    public sealed class FinalStatefulTask<TState, TStateId> : IStatefulTask<TState, TStateId>
        where TState : class
    {
        private readonly TStateId _finalStateId;

        public bool IsFinal { get; } = true;


        public FinalStatefulTask(
            TStateId finalStateId)
        {
            _finalStateId = finalStateId;
        }

        #region IStatefullTask<State, TStateId> Implementation

        TStateId IStatefulTask<TState, TStateId>.DoAction(TState state)
        {
            return _finalStateId;
        }

        #endregion
    }

    public static class FinalStatefulTask<TState>
        where TState : class
    {
        public static FinalStatefulTask<TState, TStateId> Create<TStateId>(
            TStateId finalStateId)
        {
            return new FinalStatefulTask<TState, TStateId>(finalStateId);
        }
    }
}
