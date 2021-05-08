using System.Collections.Generic;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Builders
{
    public interface IStateMachineBuilderWithoutStateId<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        TStatefulTask InitialTask { get; }
        IReadOnlyDictionary<TStateId, TStatefulTask> TransitionsTable { get; }

        IStateMachineBuilderWithStateId<TContext, TStateId, TStatefulTask> RememberStateId(TStateId stateId);
    }
}
