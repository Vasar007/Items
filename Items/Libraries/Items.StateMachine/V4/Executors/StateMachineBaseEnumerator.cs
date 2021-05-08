using System;
using System.Collections;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Executors
{
    internal abstract class StateMachineBaseEnumerator<TContext, TStateId, TStatefulTask> :
        IStateMachineEnumerator<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        public abstract TContext Context { get; }

        public abstract TStatefulTask Current { get; }

        object IEnumerator.Current => Current;

        public abstract TStatefulTask this[TStateId stateId] { get; }

        protected StateMachineBaseEnumerator()
        {
        }

        protected InvalidOperationException GetStatefulTaskIsNullException()
        {
            return new InvalidOperationException("Invalid stateful task to process: current task is null.");
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
    }
}
