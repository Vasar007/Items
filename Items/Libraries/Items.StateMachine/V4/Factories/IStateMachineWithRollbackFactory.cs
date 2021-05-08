using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4.Factories
{
    public interface IStateMachineWithRollbackFactory<TContext, TStateId> :
        IStateMachineFactory<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>>
    {
    }
}
