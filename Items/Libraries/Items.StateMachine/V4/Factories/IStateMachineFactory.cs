using Items.StateMachine.V4.Builders;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Factories
{
    public interface IStateMachineFactory<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask> FillWithTransitionsTable();
    }
}
