using System;
using System.Collections;
using System.Collections.Generic;
using Items.StateMachine.V2.States;

namespace Items.StateMachine.V2.Executors
{
    public interface IStateMachineExecutor<TState, TStateId> :
        IEnumerable<IStatefulTask<TState, TStateId>>, IEnumerator<IStatefulTask<TState, TStateId>>,
        IDisposable, IEnumerable, IEnumerator
        where TState : class
    {
        TState State { get; }

        IStatefulTask<TState, TStateId> this[TStateId stateId] { get; set; }
    }
}
