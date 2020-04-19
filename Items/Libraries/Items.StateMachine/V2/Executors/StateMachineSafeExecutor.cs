using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Items.Common.Logging;
using Items.StateMachine.V2.States;

namespace Items.StateMachine.V2.Executors
{
    internal sealed class StateMachineSafeExecutor<TState, TStateId> :
        StateMachineBaseExecutor<TState, TStateId>
        where TState : class
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor<StateMachineSafeExecutor<TState, TStateId>>();

        private readonly IStateMachineExecutor<TState, TStateId> _realExecutor;

        private readonly Action<Exception> _handler;

        public override TState State => _realExecutor.State;

        public override IStatefulTask<TState, TStateId> Current => _realExecutor.Current;

        public override IStatefulTask<TState, TStateId> this[TStateId stateId]
        {
            get => _realExecutor[stateId];
            set => _realExecutor[stateId] = value;
        }


        public StateMachineSafeExecutor(
            IStateMachineExecutor<TState, TStateId> realExecutor,
            Action<Exception>? handler)
        {
            _realExecutor = realExecutor.ThrowIfNull(nameof(realExecutor));

            _handler = handler ?? (ex => Logger.Exception(ex));
        }

        #region IEnumerator Implementation

        public override bool MoveNext()
        {
            try
            {
                return _realExecutor.MoveNext();
            }
            catch (Exception ex)
            {
                _handler(ex);
                return false;
            }
        }

        #endregion

        #region IEnumerator<IStatefulTask<TState, TStateId>> Implementation

        public override IEnumerator<IStatefulTask<TState, TStateId>> GetEnumerator()
        {
            return new StateMachineSafeExecutor<TState, TStateId>(_realExecutor, _handler);
        }

        #endregion
    }
}
