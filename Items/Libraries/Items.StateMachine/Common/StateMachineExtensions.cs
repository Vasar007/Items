using Items.StateMachine.Executors;
using Items.StateMachine.States;

namespace Items.StateMachine.Common
{
    public static class StateMachineExtensions
    {
        public static IStateMachineExecutor<TState> PerformUntilFinalState<TState>(
            this IStatefulTask<TState> initialTask, TState initialState)
            where TState : class
        {
            return new StateMachineUntilFinalStateExecutor<TState>(initialState, initialTask);
        }

        public static TState Execute<TState>(
            this IStateMachineExecutor<TState> statefulTasks)
            where TState : class
        {
            foreach (IStatefulTask<TState> _ in statefulTasks)
            {
                // All actions perform in MoveNext.
            }

            return statefulTasks.State;
        }
    }
}
