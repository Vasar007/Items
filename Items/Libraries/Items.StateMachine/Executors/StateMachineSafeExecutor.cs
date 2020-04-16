using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Items.Common.Logging;
using Items.StateMachine.States;

namespace Items.StateMachine.Executors
{
    internal sealed class StateMachineSafeExecutor<TState> :
        StateMachineBaseExecutor<TState>
        where TState : class
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor<StateMachineSafeExecutor<TState>>();

        private readonly IStateMachineExecutor<TState> _realExecutor;

        private readonly Action<Exception> _handler;

        public override TState State => _realExecutor.State;

        public override IStatefulTask<TState> Current => _realExecutor.Current;


        public StateMachineSafeExecutor(
            IStateMachineExecutor<TState> realExecutor,
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

        #region IEnumerator<IStatefulTask<TState>> Implementation

        public override IEnumerator<IStatefulTask<TState>> GetEnumerator()
        {
            return new StateMachineSafeExecutor<TState>(_realExecutor, _handler);
        }

        #endregion
    }
}
