using System.Collections.Generic;
using Items.Common.Logging;
using Items.StateMachine.V2.Executors;
using Items.StateMachine.V2.States;
using Items.StateMachine.V2.Common;

namespace Items.StateMachine.V2
{
    public static class StateMachineV2Samples
    {
        private static readonly ILogger Logger =
            LoggerFactory.CreateLoggerFor(typeof(StateMachineV2Samples));

        public static void RunSimpleStateMachineSample()
        {
            var initialState = new State(42, 1337);
            var initialAction = new TaskC();

            var transitions = new Dictionary<StateId, IStatefulTask<State, StateId>>
            {
                { StateId.StateA, new TaskA() },
                { StateId.StateB, new TaskB() },
                { StateId.StateC, new TaskC() },
                { StateId.FinalState, FinalStatefulTask<State>.Create(StateId.FinalState) }
            };

            State finalState = StateMachineHelper.PerformStraightforward(
                initialState, initialAction, transitions
            );

            Logger.SkipLine();
            var initialAction2 = new TaskA();

            State finalState2 = StateMachineHelper.PerformStraightforward(
                initialState, initialAction2, transitions
            );
        }

        public static void RunStateMachineUntilFinishEnumeratorSample()
        {
            var initialState = new State(42, 1337);
            var initialAction = new TaskC();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            State finalState = initialAction
                .PerformUntilFinalState(initialState)
                .FillExecutor()
                .Execute();

            Logger.Message($"Final state: {finalState}");

            Logger.SkipLine();
            var initialAction2 = new TaskA();

            Logger.Message($"Initial state: {initialState}");
            Logger.Message("Starting performing.");
            State finalState2 = initialAction2
                .PerformUntilFinalState(initialState)
                .CatchExceptions()
                .FillExecutor()
                .Execute();

            Logger.Message($"Final state: {finalState2}");
        }

        private static IStateMachineExecutor<State, StateId> FillExecutor(
            this IStateMachineExecutor<State, StateId> executor)
        {
            return executor
                .On(StateId.StateA).GoTo(new TaskA())
                .On(StateId.StateB).GoTo(new TaskB())
                .On(StateId.StateC).GoTo(new TaskC())
                .On(StateId.FinalState).GoTo(FinalStatefulTask<State>.Create(StateId.FinalState));
        }
    }
}
