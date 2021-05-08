using System;
using System.Collections;
using System.Collections.Generic;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Executors
{
    public interface IStateMachineEnumerator<TContext, TStateId, out TStatefulTask> :
        IEnumerator<TStatefulTask>, IDisposable, IEnumerator
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        TContext Context { get; }

        TStatefulTask this[TStateId stateId] { get; }
    }
}
