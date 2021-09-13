using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.V4.Tasks.Straightforward;

namespace Items.StateMachine.V4.Executors.Straightforward
{
    internal sealed class StraightforwardStateMachineEnumerator<TContext, TStraightforwardStatefulTask> :
        StateMachineBaseEnumerator<TContext, int, TStraightforwardStatefulTask>
        where TStraightforwardStatefulTask : class, IStraightforwardStatefulTask<TContext>
    {
        private readonly CustomStraightforwardStateMachineAction<TContext, TStraightforwardStatefulTask> _customAction;

        private int _currentStateId;

        public override TContext Context { get; }

        private TStraightforwardStatefulTask _current;
        public override TStraightforwardStatefulTask Current => _current;

        private readonly IReadOnlyList<TStraightforwardStatefulTask> _transitionsList;
        public override TStraightforwardStatefulTask this[int stateId] => _transitionsList[stateId];

        public StraightforwardStateMachineEnumerator(
            TContext context,
            TStraightforwardStatefulTask initialTask,
            IReadOnlyList<TStraightforwardStatefulTask> transitionsList,
            CustomStraightforwardStateMachineAction<TContext, TStraightforwardStatefulTask> customAction)
        {
            Context = context;
            _current = initialTask.ThrowIfNull(nameof(initialTask));
            _transitionsList = transitionsList.ThrowIfNull(nameof(transitionsList));
            _customAction = customAction.ThrowIfNull(nameof(customAction));

            _currentStateId = 0;
        }

        #region IEnumerator Implementation

        public override bool MoveNext()
        {
            if (_current is null)
                throw GetStatefulTaskIsNullException();

            bool isFinal = _current.IsFinal;

            // Perform the task after getting "IsFinal" flag because we can face final task and do not call it "DoAction" method.
            // "DoAction" method can be wrapped into custom action.
            // There is no returning value because our state machine is straightforward.
            _customAction(_current, Context);

            // Try to change state safely.
            // On final task there are no reason to change state ID because our state machine is straightforward.
            int nextStateId = _currentStateId + 1;
            if (!isFinal && nextStateId < _transitionsList.Count)
            {
                _currentStateId = nextStateId;
                _current = this[nextStateId];
            }

            return !isFinal;
        }

        #endregion
    }
}
