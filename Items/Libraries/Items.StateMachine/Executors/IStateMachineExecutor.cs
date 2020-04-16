using System;
using System.Collections;
using System.Collections.Generic;
using Items.StateMachine.States;

namespace Items.StateMachine.Executors
{
    public interface IStateMachineExecutor<TState> : IEnumerable<IStatefulTask<TState>>,
        IEnumerator<IStatefulTask<TState>>, IDisposable, IEnumerable, IEnumerator
        where TState : class
    {
        TState State { get; }
    }
}
