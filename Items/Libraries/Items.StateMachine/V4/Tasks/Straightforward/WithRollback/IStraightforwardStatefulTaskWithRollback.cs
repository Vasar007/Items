using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4.Tasks.Straightforward.WithRollback
{
    public interface IStraightforwardStatefulTaskWithRollback<TContext> :
        IStraightforwardStatefulTask<TContext>, IStatefulTaskWithRollback<TContext, int>
    {
    }
}
