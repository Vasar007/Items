using System;
using System.Collections.Generic;
using Items.Common.Logging;
using Items.StateMachine.V4.Tasks;

namespace Items.StateMachine.V4
{
    public static class StateMachineHelper
    {
        private static readonly ILogger Logger =
             LoggerFactory.CreateLoggerFor(typeof(StateMachineHelper));

        public static TContext PerformStraightforward<TContext, TStateId>(
            TContext context,
            IStatefulTask<TContext, TStateId> initialTask,
            IReadOnlyDictionary<TStateId, IStatefulTask<TContext, TStateId>> transitions)
        {
            // We can log type names of state and tasks but it will be helpful for debugging.
            Logger.Debug($"Initial state: {context}");

            IStatefulTask<TContext, TStateId> currentTask = initialTask;

            try
            {
                Logger.Debug("Starting performing.");

                while (!currentTask.IsFinal)
                {
                    Logger.Debug($"Executing task: {currentTask}.");
                    TStateId stateId = currentTask.DoAction(context);
                    currentTask = transitions[stateId];
                    Logger.Debug($"Current state: {context}.");
                }

                // Perform the final task.
                Logger.Debug($"Executing task: {currentTask}.");
                currentTask.DoAction(context);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, "Exception occurred during state machine performing.");
            }

            Logger.Debug($"Final state: {context}");
            return context;
        }
    }
}
