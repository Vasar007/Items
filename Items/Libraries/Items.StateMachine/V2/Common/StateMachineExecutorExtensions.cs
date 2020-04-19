using System;
using Acolyte.Assertions;
using Items.StateMachine.V2.Executors;
using Items.StateMachine.V2.Executors.Fillers;
using Items.StateMachine.V2.States;

namespace Items.StateMachine.V2.Common
{
    public static class StateMachineExecutorExtensions
    {
        public static IStateMachineFiller<TState, TStateId> On<TState, TStateId>(
             this IStateMachineExecutor<TState, TStateId> executor, TStateId stateId)
             where TState : class
        {
            return DefaultStateMachineFiller.Create(executor, stateId);
        }

        public static IStateMachineExecutor<TState, TStateId> GoTo<TState, TStateId>(
            this IStateMachineFiller<TState, TStateId> filler,
            IStatefulTask<TState, TStateId> statefulTask)
            where TState : class
        {
            return filler.FillExecutor(statefulTask);
        }

        public static IStateMachineExecutor<TState, TStateId> PerformUntilFinalState<TState, TStateId>(
            this IStatefulTask<TState, TStateId> initialTask, TState initialState)
            where TState : class
        {
            return StateMachineUntilFinalStateExecutor.CreateNew(initialState, initialTask);
        }

        public static IStateMachineExecutor<TState, TStateId> CatchExceptions<TState, TStateId>(
            this IStateMachineExecutor<TState, TStateId> executor, Action<Exception>? handler)
            where TState : class
        {
            return new StateMachineSafeExecutor<TState, TStateId>(executor, handler);
        }

        public static IStateMachineExecutor<TState, TStateId> CatchExceptions<TState, TStateId>(
            this IStateMachineExecutor<TState, TStateId> executor)
            where TState : class
        {
            return executor.CatchExceptions(handler: null);
        }

        public static TState Execute<TState, TStateId>(
            this IStateMachineExecutor<TState, TStateId> statefulTasks)
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
