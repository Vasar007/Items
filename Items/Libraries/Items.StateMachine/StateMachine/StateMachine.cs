using System;
using Items.StateMachine.States;
using Items.Common.Utils;

namespace Items.StateMachine.StateMachine
{
    internal sealed class StateMachine<TState>
    {
        private static readonly PrefixLogger Logger = PrefixLogger.Create(nameof(StateMachine<TState>));

        public StateMachine()
        {
        }

        public TState Perform(TState initialState, IStatefulTask<TState> initialAction)
        {
            Logger.Message($"Initial state: {initialState}");

            TState currentState = initialState;
            IStatefulTask<TState> currentAction = initialAction;

            try
            {
                Logger.Message("Starting performing.");

                while (!currentAction.IsFinal)
                {
                    Logger.Message($"Executing action: {currentAction}.");
                    currentAction = currentAction.DoAction(currentState);
                    Logger.Message($"Current state: {currentState}.");
                }

                // Perform the final action.
                Logger.Message($"Executing action: {currentAction}.");
                currentAction.DoAction(currentState);
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
