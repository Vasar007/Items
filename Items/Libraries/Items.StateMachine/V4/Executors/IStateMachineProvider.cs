using System.Collections;
using System.Collections.Generic;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Executors
{
    public interface IStateMachineProvider<TContext, TStateId, out TStatefulTask> :
        IEnumerable<TStatefulTask>, IEnumerable
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        IStateMachineEnumerator<TContext, TStateId, TStatefulTask> GetStateMachineEnumerator();
    }
}
