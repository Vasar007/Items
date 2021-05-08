using System;
using Acolyte.Assertions;
using Items.Common.Logging;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Executors.Safe
{
    internal sealed class StateMachineSafeProvider<TContext, TStateId, TStatefulTask> :
        StateMachineBaseProvider<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        private static ILogger Logger => StateMachineSafeProvider.Logger;

        private readonly IStateMachineProvider<TContext, TStateId, TStatefulTask> _realProvider;
        private readonly bool _continueExecutionOnFailed;
        private readonly Action<Exception> _handler;

        public StateMachineSafeProvider(
            IStateMachineProvider<TContext, TStateId, TStatefulTask> realProvider,
            bool continueExecutionOnFailed,
            Action<Exception>? handler)
        {
            _realProvider = realProvider.ThrowIfNull(nameof(realProvider));
            _continueExecutionOnFailed = continueExecutionOnFailed;
            _handler = handler ?? (ex => Logger.Exception(ex));
        }

        #region IStateMachineProvider<TContext, TStateId, TStatefulTask> Implementation

        public override IStateMachineEnumerator<TContext, TStateId, TStatefulTask> GetStateMachineEnumerator()
        {
            return new StateMachineSafeEnumerator<TContext, TStateId, TStatefulTask>(
                realEnumerator: _realProvider.GetStateMachineEnumerator(),
                continueExecutionOnFailed: _continueExecutionOnFailed,
                handler: _handler
            );
        }

        #endregion
    }

    internal static class StateMachineSafeProvider
    {
        internal static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor(typeof(StateMachineSafeProvider));
    }
}
