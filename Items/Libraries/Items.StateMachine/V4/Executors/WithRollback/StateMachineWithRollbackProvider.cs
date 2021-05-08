using Acolyte.Assertions;
using Items.StateMachine.V4.Tasks.Default.WithRollback;

namespace Items.StateMachine.V4.Executors.WithRollback
{
    internal sealed class StateMachineWithRollbackProvider<TContext, TStateId, TStatefulTaskWithRollback> :
        StateMachineBaseProvider<TContext, TStateId, TStatefulTaskWithRollback>
        where TStatefulTaskWithRollback : class, IStatefulTaskWithRollback<TContext, TStateId>
    {
        private readonly IStateMachineProvider<TContext, TStateId, TStatefulTaskWithRollback> _realProvider;
        private readonly bool _continueRollbackOnFailed;

        public StateMachineWithRollbackProvider(
            IStateMachineProvider<TContext, TStateId, TStatefulTaskWithRollback> realProvider,
            bool continueRollbackOnFailed)
        {
            _realProvider = realProvider.ThrowIfNull(nameof(realProvider));
            _continueRollbackOnFailed = continueRollbackOnFailed;
        }

        #region IStateMachineProvider<TContext, TStateId, IStatefulTaskWithRollback<TContext, TStateId>> Implementation

        public override IStateMachineEnumerator<TContext, TStateId, TStatefulTaskWithRollback> GetStateMachineEnumerator()
        {
            return new StateMachineWithRollbackEnumerator<TContext, TStateId, TStatefulTaskWithRollback>(
                realEnumerator: _realProvider.GetStateMachineEnumerator(),
                continueRollbackOnFailed: _continueRollbackOnFailed
            );
        }

        #endregion
    }
}
