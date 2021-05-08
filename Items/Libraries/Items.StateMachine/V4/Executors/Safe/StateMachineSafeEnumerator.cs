using System;
using Acolyte.Assertions;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Executors.Safe
{
    internal sealed class StateMachineSafeEnumerator<TContext, TStateId, TStatefulTask> :
        StateMachineBaseEnumerator<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        private readonly IStateMachineEnumerator<TContext, TStateId, TStatefulTask> _realEnumerator;
        private readonly bool _continueExecutionOnFailed;

        private readonly Action<Exception> _handler;

        public override TContext Context => _realEnumerator.Context;

        public override TStatefulTask Current => _realEnumerator.Current;

        public override TStatefulTask this[TStateId stateId] => _realEnumerator[stateId];

        public StateMachineSafeEnumerator(
            IStateMachineEnumerator<TContext, TStateId, TStatefulTask> realEnumerator,
            bool continueExecutionOnFailed,
            Action<Exception> handler)
        {
            _realEnumerator = realEnumerator.ThrowIfNull(nameof(realEnumerator));
            _continueExecutionOnFailed = continueExecutionOnFailed;
            _handler = handler.ThrowIfNull(nameof(handler));
        }

        #region IEnumerator Implementation

        public override bool MoveNext()
        {
            var statefulTask = _realEnumerator.Current;
            if (statefulTask is null) throw GetStatefulTaskIsNullException();

            bool isFinal = statefulTask.IsFinal;

            try
            {
                return _realEnumerator.MoveNext();
            }
            catch (Exception ex)
            {
                _handler(ex);

                // If task is final, interrupt state machine execution.
                if (isFinal) return false;

                // Otherwise, continue execution if flag is set.
                return _continueExecutionOnFailed;
            }
        }

        #endregion
    }
}
