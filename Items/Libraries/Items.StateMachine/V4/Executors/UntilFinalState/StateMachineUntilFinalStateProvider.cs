using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Executors.UntilFinalState
{
    internal sealed class StateMachineUntilFinalStateProvider<TContext, TStateId, TStatefulTask> :
        StateMachineBaseProvider<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        private readonly TContext _context;
        private readonly TStatefulTask _initialTask;
        private readonly IReadOnlyDictionary<TStateId, TStatefulTask> _transitionsTable;
        private readonly CustomStateMachineAction<TStateId> _customAction;

        public StateMachineUntilFinalStateProvider(
            TContext context,
            TStatefulTask initialTask,
            IReadOnlyDictionary<TStateId, TStatefulTask> transitionsTable,
            CustomStateMachineAction<TStateId>? customAction)
        {
            _context = context;
            _initialTask = initialTask.ThrowIfNull(nameof(initialTask));
            _transitionsTable = transitionsTable.ThrowIfNull(nameof(transitionsTable));
            _customAction = customAction ?? (doAction => doAction());
        }

        #region IStateMachineProvider<TContext, TStateId, TStatefulTask> Implementation

        public override IStateMachineEnumerator<TContext, TStateId, TStatefulTask> GetStateMachineEnumerator()
        {
            return new StateMachineUntilFinalStateEnumerator<TContext, TStateId, TStatefulTask>(
                context: _context,
                initialTask: _initialTask,
                transitionsTable: _transitionsTable,
                customAction: _customAction
            );
        }

        #endregion
    }

    internal static class StateMachineUntilFinalStateProvider
    {
        public static StateMachineUntilFinalStateProvider<TContext, TStateId, TStatefulTask> Create<TContext, TStateId, TStatefulTask>(
            TContext context,
            TStatefulTask initialTask,
            IReadOnlyDictionary<TStateId, TStatefulTask> transitionsTable,
            CustomStateMachineAction<TStateId>? customAction)
            where TStatefulTask : class, IStatefulTask<TContext, TStateId>
        {
            return new StateMachineUntilFinalStateProvider<TContext, TStateId, TStatefulTask>(
                context: context,
                initialTask: initialTask,
                transitionsTable: transitionsTable,
                customAction: customAction
            );
        }
    }
}
