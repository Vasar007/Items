namespace Items.StateMachine.V4.Tasks.Default
{
    public sealed class FinalStatefulTask<TContext, TStateId> : IStatefulTask<TContext, TStateId>
    {
        private readonly TStateId _finalStateId;

        bool IStatefulTask<TContext, TStateId>.IsFinal { get; } = true;

        public FinalStatefulTask(
            TStateId finalStateId)
        {
            _finalStateId = finalStateId;
        }

        TStateId IStatefulTask<TContext, TStateId>.DoAction(TContext context)
        {
            // Return the same state ID to make infinite loop.
            // Enumerator should check "IsFinal" flag and break enumeration of state machine.
            return _finalStateId;
        }
    }

    public static class FinalStatefulTask<TContext>
    {
        public static FinalStatefulTask<TContext, TStateId> Create<TStateId>(
            TStateId finalStateId)
        {
            return new FinalStatefulTask<TContext, TStateId>(finalStateId);
        }
    }
}
