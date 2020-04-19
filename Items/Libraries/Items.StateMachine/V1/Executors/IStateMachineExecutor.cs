using System;
using System.Collections;
using System.Collections.Generic;
using Items.StateMachine.V1.States;

namespace Items.StateMachine.V1.Executors
{
    public interface IStateMachineExecutor<TState> : IEnumerable<IStatefulTask<TState>>,
        IEnumerator<IStatefulTask<TState>>, IDisposable, IEnumerable, IEnumerator
        where TState : class
    {
        TState State { get; }
    }
}
