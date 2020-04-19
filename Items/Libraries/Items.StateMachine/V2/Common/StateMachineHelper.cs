using System;
using System.Collections.Generic;
using Items.Common.Logging;
using Items.StateMachine.V2.States;

namespace Items.StateMachine.V2.Common
{
    public static class StateMachineHelper
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor(typeof(StateMachineHelper));

        public static TState PerformStraightforward<TState, TStateId>(TState initialState,
            IStatefulTask<TState, TStateId> initialTask,
            IReadOnlyDictionary<TStateId, IStatefulTask<TState, TStateId>> transitions)
            where TState : class
        {
            Logger.Message($"Initial state: {initialState}");

            TState currentState = initialState;
            IStatefulTask<TState, TStateId> currentTask = initialTask;

            try
            {
                Logger.Message("Starting performing.");

                while (!currentTask.IsFinal)
                {
                    Logger.Message($"Executing task: {currentTask}.");
                    TStateId stateId = currentTask.DoAction(currentState);
                    currentTask = transitions[stateId];
                    Logger.Message($"Current state: {currentState}.");
                }

                // Perform the final task.
                Logger.Message($"Executing task: {currentTask}.");
                currentTask.DoAction(currentState);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, "Exception occured during perform.");
            }

            Logger.Message($"Final state: {currentState}");
            return currentState;
        }
    }
}
