using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Builders
{
    public interface IStateMachineBuilderWithStateId<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        TStateId StateId { get; }

        IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> AddStatefulTask(TStatefulTask statefulTask);
    }
}
