using System.Collections.Generic;
using Items.StateMachine.V3.States;
using Items.StateMachine.V3.Tasks;

namespace Items.StateMachine.V3.Executors
{
    public static class SimpleExecutor
    {
        public static StateBase Run(StateBase initialState,
            IReadOnlyCollection<ITaskBase<StateBase,StateBase>> workflow )
        {
            StateBase currentState = initialState;
            foreach (ITaskBase<StateBase,StateBase> task in workflow)
            {
                currentState = task.Do(currentState);
            }

            return currentState;
        }
    }
}
