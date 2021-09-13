using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4.Executors.UntilFinalState
{
    internal sealed class StateMachineUntilFinalStateEnumerator<TContext, TStateId, TStatefulTask> :
        StateMachineBaseEnumerator<TContext, TStateId, TStatefulTask>
        where TStatefulTask : class, IStatefulTask<TContext, TStateId>
    {
        private readonly CustomStateMachineAction<TContext, TStateId, TStatefulTask> _customAction;

        public override TContext Context { get; }

        private TStatefulTask _current;
        public override TStatefulTask Current => _current;

        private readonly IReadOnlyDictionary<TStateId, TStatefulTask> _transitionsTable;
        public override TStatefulTask this[TStateId stateId] => _transitionsTable[stateId];

        public StateMachineUntilFinalStateEnumerator(
            TContext context,
            TStatefulTask initialTask,
            IReadOnlyDictionary<TStateId, TStatefulTask> transitionsTable,
            CustomStateMachineAction<TContext, TStateId, TStatefulTask> customAction)
        {
            Context = context;
            _current = initialTask.ThrowIfNull(nameof(initialTask));
            _transitionsTable = transitionsTable.ThrowIfNull(nameof(transitionsTable));
            _customAction = customAction.ThrowIfNull(nameof(customAction));
        }

        #region IEnumerator Implementation

        public override bool MoveNext()
        {
            if (_current is null) throw GetStatefulTaskIsNullException();

            bool isFinal = _current.IsFinal;

            // Perform the task after getting "IsFinal" flag because we can face final task and do not call it "DoAction" method.
            // "DoAction" method can be wrapped into custom action.
            TStateId stateId = _customAction(_current, Context);
            _current = this[stateId];
            return !isFinal;
        }

        #endregion
    }
}
