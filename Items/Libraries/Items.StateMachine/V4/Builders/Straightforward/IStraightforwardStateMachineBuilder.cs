using System.Collections.Generic;
using Items.StateMachine.V4.Tasks.Straightforward;

namespace Items.StateMachine.V4.Builders.Straightforward
{
    public interface IStraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask>
        where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
    {
        TStraightforwardStatefulTask InitialTask { get; }
        IReadOnlyList<TStraightforwardStatefulTask> TransitionsList { get; }

        IStraightforwardStateMachineBuilder<TContext, TStraightforwardStatefulTask> AddStatefulTask(
            TStraightforwardStatefulTask statefulTask);
    }
}
