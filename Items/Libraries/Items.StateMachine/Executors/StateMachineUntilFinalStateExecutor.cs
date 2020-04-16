using System;
using System.Collections;
using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.States;

namespace Items.StateMachine.Executors
{
    internal sealed class StateMachineUntilFinalStateExecutor<TState> :
        IStateMachineExecutor<TState>
        where TState : class
    {

        private readonly IStatefulTask<TState> _initialTask;

        public TState State { get; }

        public IStatefulTask<TState> Current { get; private set; }

        object IEnumerator.Current => Current;


        public StateMachineUntilFinalStateExecutor(
            TState initialState,
            IStatefulTask<TState> initialTask)
        {
            State = initialState.ThrowIfNull(nameof(initialTask));
            _initialTask = initialTask.ThrowIfNull(nameof(initialTask));

            Current = initialTask;
        }

        #region IDisposable Implementation

        public void Dispose()
        {
            // Nothing to dispose.
        }

        #endregion

        #region IEnumerator Implementation

        public bool MoveNext()
        {
            bool isFinal = Current.IsFinal;

            // Perform the task before getting IsFinal flag because it can be changed.
            Current = Current.DoAction(State);
            return !isFinal;
        }

        public void Reset()
        {
            // Cannot reset state because it requieres deep copy to save it in ctor
            // and copy back in Reset method.
            Current = _initialTask;
        }

        #endregion

        #region IEnumerator<IStatefulTask<TState>> Implementation

        public IEnumerator<IStatefulTask<TState>> GetEnumerator()
        {
            return new StateMachineUntilFinalStateExecutor<TState>(State, _initialTask);
        }

        #endregion

        #region IEnumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

    }
}
