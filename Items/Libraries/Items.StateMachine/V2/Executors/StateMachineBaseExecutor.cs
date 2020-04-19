using System;
using System.Collections;
using System.Collections.Generic;
using Items.StateMachine.V2.States;

namespace Items.StateMachine.V2.Executors
{
    public abstract class StateMachineBaseExecutor<TState, TStateId> :
        IStateMachineExecutor<TState, TStateId>
        where TState : class
    {
        public abstract TState State { get; }

        public abstract IStatefulTask<TState, TStateId> Current { get; }

        object IEnumerator.Current => Current;

        public abstract IStatefulTask<TState, TStateId> this[TStateId stateId] { get; set; }

        protected StateMachineBaseExecutor()
        {
        }

        #region IDisposable Implementation

        public void Dispose()
        {
            // Contract: all inheritors should not define finalizer.
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Nothing to dispose.
        }

        #endregion

        #region IEnumerator Implementation

        public abstract bool MoveNext();

        public void Reset()
        {
            throw new NotImplementedException("Reset method is not implemented.");
        }

        #endregion

        #region IEnumerator<IStatefulTask<TState, TStateId>> Implementation

        public abstract IEnumerator<IStatefulTask<TState, TStateId>> GetEnumerator();

        #endregion

        #region IEnumerator Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
