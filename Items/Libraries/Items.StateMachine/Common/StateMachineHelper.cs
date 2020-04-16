using System;
using Items.Common.Utils;
using Items.StateMachine.States;

namespace Items.StateMachine.Common
{
    public static class StateMachineHelper
    {
        private static readonly PrefixLogger Logger =
            PrefixLogger.Create(nameof(StateMachineHelper));

        public static TState PerformCasual<TState>(TState initialState,
            IStatefulTask<TState> initialTask)
            where TState : class
        {
            Logger.Message($"Initial state: {initialState}");

            TState currentState = initialState;
            IStatefulTask<TState> currentTask = initialTask;

            try
            {
                Logger.Message("Starting performing.");

                while (!currentTask.IsFinal)
                {
                    Logger.Message($"Executing task: {currentTask}.");
                    currentTask = currentTask.DoAction(currentState);
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
