using System;
using Acolyte.Assertions;
using Items.StateMachine.Executors;
using Items.StateMachine.States;

namespace Items.StateMachine.Common
{
    public static class StateMachineExecutorExtensions
    {
        public static IStateMachineExecutor<TState> PerformUntilFinalState<TState>(
            this IStatefulTask<TState> initialTask, TState initialState)
            where TState : class
        {
            return new StateMachineUntilFinalStateExecutor<TState>(initialState, initialTask);
        }

        public static IStateMachineExecutor<TState> CatchExceptions<TState>(
            this IStateMachineExecutor<TState> executor, Action<Exception>? handler)
            where TState : class
        {
            return new StateMachineSafeExecutor<TState>(executor, handler);
        }

        public static IStateMachineExecutor<TState> CatchExceptions<TState>(
            this IStateMachineExecutor<TState> executor)
            where TState : class
        {
            return executor.CatchExceptions(handler: null);
        }

        public static TState Execute<TState>(
            this IStateMachineExecutor<TState> statefulTasks)
            where TState : class
        {
            statefulTasks.ThrowIfNull(nameof(statefulTasks));

            using (var enumerator = statefulTasks.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    // All actions perform in MoveNext.
                }
            }

            return statefulTasks.State;
        }
    }
}
