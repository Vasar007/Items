using System;
using Acolyte.Assertions;
using Items.StateMachine.V1.Executors;
using Items.StateMachine.V1.States;

namespace Items.StateMachine.V1.Common
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
            this IStateMachineExecutor<TState> executor)
            where TState : class
        {
            executor.ThrowIfNull(nameof(executor));

            using (var enumerator = executor.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    // All actions perform in MoveNext.
                }
            }

            return executor.State;
        }
    }
}
