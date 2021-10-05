using System;
using System.Collections.Generic;
using System.Linq;
using Items.Common.Logging;
using Items.RollbackEngine.Simple;
using Items.StateMachine.V4.Tasks;
using Items.StateMachine.V4.Tasks.Default.WithRollback;
using Items.StateMachine.V4.Tasks.Straightforward;
using Items.StateMachine.V4.Tasks.Straightforward.WithRollback;

namespace Items.StateMachine.V4
{
    public static class StateMachineHelper
    {
        private static readonly ILogger Logger =
             LoggerFactory.CreateLoggerFor(typeof(StateMachineHelper));

        public static TContext Perform<TContext, TStateId>(
            TContext context,
            bool catchExceptions,
            IStatefulTask<TContext, TStateId> initialTask,
            IReadOnlyDictionary<TStateId, IStatefulTask<TContext, TStateId>> transitions)
        {
            // We can log type names of state and tasks but it will be helpful for debugging.
            Logger.Debug($"Initial state: {context}");

            try
            {
                Logger.Debug("Starting performing.");

                ExecuteInternal(context, initialTask, transitions, addRollback: null);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, "Exception occurred during state machine performing.");

                if (!catchExceptions)
                    throw;
            }

            Logger.Debug($"Final state: {context}");
            return context;
        }

        public static TContext PerformWithRollback<TContext, TStateId>(
            TContext context,
            bool catchExceptions,
            IStatefulTaskWithRollback<TContext, TStateId> initialTask,
            IReadOnlyDictionary<TStateId, IStatefulTaskWithRollback<TContext, TStateId>> transitions)
        {
            // We can log type names of state and tasks but it will be helpful for debugging.
            Logger.Debug($"Initial state: {context}");

            var internalTransitions = transitions.ToDictionary(pair => pair.Key, pair => (IStatefulTask<TContext, TStateId>) pair.Value);

            try
            {
                Logger.Debug("Starting performing with rollback.");

                using var rollbackScope = new RollbackScope<TContext>(continueRollbackOnFailed: true, context);
                ExecuteInternal(context, initialTask, internalTransitions, action => rollbackScope.Add((IRollbackAction<TContext>) action));

                rollbackScope.CommitAndClear();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, "Exception occurred during state machine performing.");

                if (!catchExceptions)
                    throw;
            }

            Logger.Debug($"Final state: {context}");
            return context;
        }

        public static TContext PerformStraightforward<TContext>(
            TContext context,
            bool catchExceptions,
            IReadOnlyList<IStraightforwardStatefulTask<TContext>> tasks)
        {
            // We can log type names of state and tasks but it will be helpful for debugging.
            Logger.Debug($"Initial state: {context}");

            try
            {
                Logger.Debug("Starting straightforward performing.");

                ExecuteStraightforwardInternal(context, tasks, addRollback: null);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, "Exception occurred during state machine performing.");

                if (!catchExceptions)
                    throw;
            }

            Logger.Debug($"Final state: {context}");
            return context;
        }

        public static TContext PerformStraightforward<TContext>(
            TContext context,
            bool catchExceptions,
            params IStraightforwardStatefulTask<TContext>[] tasks)
        {
            return PerformStraightforward(context, catchExceptions, (IReadOnlyList<IStraightforwardStatefulTask<TContext>>) tasks);
        }

        public static TContext PerformStraightforwardWithRollback<TContext>(
            TContext context,
            bool catchExceptions,
            IReadOnlyList<IStraightforwardStatefulTaskWithRollback<TContext>> tasks)
        {
            // We can log type names of state and tasks but it will be helpful for debugging.
            Logger.Debug($"Initial state: {context}");

            try
            {
                Logger.Debug("Starting straightforward performing with rollback.");

                using var rollbackScope = new RollbackScope<TContext>(continueRollbackOnFailed: true, context);
                ExecuteStraightforwardInternal(context, tasks, action => rollbackScope.Add((IRollbackAction<TContext>) action));

                rollbackScope.CommitAndClear();
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, "Exception occurred during state machine performing.");

                if (!catchExceptions)
                    throw;
            }

            Logger.Debug($"Final state: {context}");
            return context;
        }

        public static TContext PerformStraightforwardWithRollback<TContext>(
            TContext context,
            bool catchExceptions,
            params IStraightforwardStatefulTaskWithRollback<TContext>[] tasks)
        {
            return PerformStraightforwardWithRollback(context, catchExceptions, (IReadOnlyList<IStraightforwardStatefulTaskWithRollback<TContext>>) tasks);
        }

        private static void ExecuteInternal<TContext, TStateId>(
            TContext context,
            IStatefulTask<TContext, TStateId> initialTask,
            IReadOnlyDictionary<TStateId, IStatefulTask<TContext, TStateId>> transitions,
            Action<IStatefulTask<TContext, TStateId>>? addRollback)
        {
            IStatefulTask<TContext, TStateId> currentTask = initialTask;

            while (!currentTask.IsFinal)
            {
                Logger.Debug($"Executing task: {currentTask}.");
                TStateId stateId = currentTask.DoAction(context);
                addRollback?.Invoke(currentTask);

                currentTask = transitions[stateId];
                Logger.Debug($"Current state: {context}.");
            }

            // Perform the final task.
            Logger.Debug($"Executing task: {currentTask}.");
            currentTask.DoAction(context);
        }

        private static void ExecuteStraightforwardInternal<TContext>(
            TContext context,
            IReadOnlyList<IStraightforwardStatefulTask<TContext>> tasks,
            Action<IStraightforwardStatefulTask<TContext>>? addRollback)
        {
            foreach (IStraightforwardStatefulTask<TContext> task in tasks)
            {
                Logger.Debug($"Executing task: {task}.");

                _ = task.DoAction(context);
                addRollback?.Invoke(task);

                Logger.Debug($"Current state: {context}.");

                // On final task break execution.
                if (task.IsFinal)
                {
                    break;
                }
            }
        }
    }
}
