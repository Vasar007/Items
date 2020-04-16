using System.Collections.Generic;
using Acolyte.Assertions;
using Items.StateMachine.States;

namespace Items.StateMachine.Executors
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
            bool isFinal = _current.IsFinal;

            // Perform the task before getting IsFinal flag because it can be changed.
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
