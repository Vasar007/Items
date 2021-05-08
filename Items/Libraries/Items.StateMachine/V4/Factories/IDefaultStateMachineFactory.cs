using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Factories
{
    public interface IDefaultStateMachineFactory<TContext, TStateId> :
        IStateMachineFactory<TContext, TStateId, IStatefulTask<TContext, TStateId>>
    {
    }
}
