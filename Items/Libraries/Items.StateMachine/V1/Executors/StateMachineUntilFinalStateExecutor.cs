using System;
using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.V1.States;

namespace Items.StateMachine.V1.Executors
{
    internal sealed class StateMachineUntilFinalStateExecutor<TState> :
        StateMachineBaseExecutor<TState>
        where TState : class
    {
        private readonly IStatefulTask<TState> _initialTask;

        private IStatefulTask<TState> _current;

        public override TState State { get; }

        public override IStatefulTask<TState> Current => _current;


        public StateMachineUntilFinalStateExecutor(
            TState initialState,
            IStatefulTask<TState> initialTask)
        {
            State = initialState.ThrowIfNull(nameof(initialTask));
            _initialTask = initialTask.ThrowIfNull(nameof(initialTask));

            _current = initialTask;
        }

        #region IEnumerator Implementation

        public override bool MoveNext()
        {
            if (_current is null)
            {
                throw new InvalidOperationException("Invalid stateful task to process.");
            }

            bool isFinal = _current.IsFinal;

            // Perform the task after getting 'IsFinal' flag because we can face final task and do not call it 'DoAction' method.
            _current = _current.DoAction(State);
            return !isFinal;
        }

        #endregion

        #region IEnumerator<IStatefulTask<TState>> Implementation

        public override IEnumerator<IStatefulTask<TState>> GetEnumerator()
        {
            return new StateMachineUntilFinalStateExecutor<TState>(State, _initialTask);
        }

        #endregion
    }
}
