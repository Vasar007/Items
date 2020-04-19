using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.V2.States;

namespace Items.StateMachine.V2.Executors
{
    internal sealed class StateMachineUntilFinalStateExecutor<TState, TStateId> :
        StateMachineBaseExecutor<TState, TStateId>
        where TState : class
    {
        private readonly IStatefulTask<TState, TStateId> _initialTask;

        private IStatefulTask<TState, TStateId> _current;

        public override TState State { get; }

        public override IStatefulTask<TState, TStateId> Current => _current;

        private readonly Dictionary<TStateId, IStatefulTask<TState, TStateId>> _transitions;
        public override IStatefulTask<TState, TStateId> this[TStateId stateId]
        {
            get => _transitions[stateId];
            set => _transitions[stateId] = value;
        }


        public StateMachineUntilFinalStateExecutor(
            TState initialState,
            IStatefulTask<TState, TStateId> initialTask,
            Dictionary<TStateId, IStatefulTask<TState, TStateId>> transitions)
        {
            State = initialState.ThrowIfNull(nameof(initialTask));
            _initialTask = initialTask.ThrowIfNull(nameof(initialTask));
            _transitions = transitions.ThrowIfNull(nameof(transitions));

            _current = initialTask;
        }

        #region IEnumerator Implementation

        public override bool MoveNext()
        {
            bool isFinal = _current.IsFinal;

            // Perform the task after getting IsFinal flag because it can be changed.
            TStateId stateId = _current.DoAction(State);
            _current = this[stateId];
            return !isFinal;
        }

        #endregion

        #region IEnumerator<IStatefulTask<TState, TStateId>> Implementation

        public override IEnumerator<IStatefulTask<TState, TStateId>> GetEnumerator()
        {
            return new StateMachineUntilFinalStateExecutor<TState, TStateId>(
                initialState: State,
                initialTask: _initialTask,
                transitions: _transitions
            );
        }

        #endregion
    }

    internal static class StateMachineUntilFinalStateExecutor
    {
        public static StateMachineUntilFinalStateExecutor<TState, TStateId> CreateNew<TState, TStateId>(
           TState initialState,
           IStatefulTask<TState, TStateId> initialTask)
            where TState : class
        {
            return new StateMachineUntilFinalStateExecutor<TState, TStateId>(
                initialState: initialState,
                initialTask: initialTask,
                transitions: new Dictionary<TStateId, IStatefulTask<TState, TStateId>>()
            );
        }
    }
}
