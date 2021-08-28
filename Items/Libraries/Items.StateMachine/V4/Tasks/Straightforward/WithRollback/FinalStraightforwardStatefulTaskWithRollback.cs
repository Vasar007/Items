using Items.RollbackEngine.Simple;
using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4.Tasks.Straightforward.WithRollback
{
    public sealed class FinalStraightforwardStatefulTaskWithRollback<TContext> :
        IStraightforwardStatefulTaskWithRollback<TContext>
    {
        bool IStatefulTask<TContext, int>.IsFinal { get; } = true;

        public FinalStraightforwardStatefulTaskWithRollback()
        {
        }

        public static FinalStraightforwardStatefulTaskWithRollback<TContext> Create()
        {
            return new FinalStraightforwardStatefulTaskWithRollback<TContext>();
        }

        int IStatefulTask<TContext, int>.DoAction(TContext context)
        {
            // Logic to determine the next state is up to state machine enumerator.
            // Enumerator should check "IsFinal" flag and break enumeration of state machine.
            return default;
        }

        bool IRollbackAction<TContext>.TryRollback(TContext context)
        {
            return StatefulTaskWithRollbackWrapper.DefaultTryRollbackSafe(context);
        }
    }
}
