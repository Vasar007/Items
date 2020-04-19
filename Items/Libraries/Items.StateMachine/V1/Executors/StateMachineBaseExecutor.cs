using System;
using System.Collections;
using System.Collections.Generic;
using Items.StateMachine.V1.States;

namespace Items.StateMachine.V1.Executors
{
    public abstract class StateMachineBaseExecutor<TState> :
        IStateMachineExecutor<TState>
        where TState : class
    {
        public abstract TState State { get; }

        public abstract IStatefulTask<TState> Current { get; }

        object IEnumerator.Current => Current;


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

        #region IEnumerator<IStatefulTask<TState>> Implementation

        public abstract IEnumerator<IStatefulTask<TState>> GetEnumerator();

        #endregion

        #region IEnumerator Implementation

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
