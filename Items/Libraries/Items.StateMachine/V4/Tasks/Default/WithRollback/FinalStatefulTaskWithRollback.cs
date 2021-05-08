using Items.RollbackEngine.Simple;

namespace Items.StateMachine.V4.Tasks.Default.WithRollback
{
    public sealed class FinalStatefulTaskWithRollback<TContext, TStateId> :
        IStatefulTaskWithRollback<TContext, TStateId>
    {
        private readonly TStateId _finalStateId;

        bool IStatefulTask<TContext, TStateId>.IsFinal { get; } = true;

        public FinalStatefulTaskWithRollback(
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

        bool IRollbackAction<TContext>.TryRollbackSafe(TContext context)
        {
            return StatefulTaskWithRollbackWrapper.DefaultTryRollbackSafe(context);
        }
    }

    public static class FinalStatefulTaskWithRollback<TContext>
    {
        public static FinalStatefulTaskWithRollback<TContext, TStateId> Create<TStateId>(
            TStateId finalStateId)
        {
            return new FinalStatefulTaskWithRollback<TContext, TStateId>(finalStateId);
        }
    }
}
