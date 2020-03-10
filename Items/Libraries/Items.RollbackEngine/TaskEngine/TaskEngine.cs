using System;
using System.Collections.Generic;
using Items.Common.Utils;

namespace Items.RollbackEngine.TaskEngine
{
    internal sealed class TaskEngine
    {
        private static readonly PrefixLogger Logger = PrefixLogger.Create(nameof(TaskEngine));

        private List<IStatefulTask<State>> Actions { get; }


        public TaskEngine()
        {
            Actions = new List<IStatefulTask<State>>();
        }

        public void Demo()
        {
            Logger.Message("Start task engine demo.");

            Actions.Add(new ActionA());
            Actions.Add(new ActionB());
            Actions.Add(new ActionA());
            Actions.Add(new ActionB());

            var initialState = new State(42, 1337);
            Logger.Message($"Initial state: {initialState}");

            State finalState = Perform(initialState);
            Logger.Message($"Final state: {finalState}");

            Actions.Add(new BadAction());

            State finalState2 = Perform(initialState);
            Logger.Message($"Final state 2: {finalState2}");
        }

        private State Perform(State initialState)
        {
            State currentState = initialState;
            var doneActions = new Stack<IStatefulTask<State>>();

            try
            {
                foreach (IStatefulTask<State> action in Actions)
                {
                    doneActions.Push(action);

                    currentState = action.DoAction(currentState);
                }
            }
            catch (Exception ex)
            {
                Logger.Exception(ex, "Exception occured during perform.");
                Logger.Message($"Perform rollback for {doneActions.Count.ToString()} actions.");

                foreach (IStatefulTask<State> action in doneActions)
                {
                    currentState = action.RollbackSafe(currentState);
                }
            }

            return currentState;
        }
    }
}
