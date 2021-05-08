using Items.RollbackEngine.Simple;

namespace Items.StateMachine.V4.Tasks.Default.WithRollback
{
    public interface IStatefulTaskWithRollback<TContext, TStateId> :
        IStatefulTask<TContext, TStateId>, IRollbackAction<TContext>
    {
    }
}
